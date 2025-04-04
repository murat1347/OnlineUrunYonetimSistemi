using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrandsA.Application.Abstractions.Tokens;
using BrandsA.Application.IRepositories.Token;
using BrandsA.Domain.Entities;
using BrandsA.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace BrandsA.Persistence.Repositories.Token
{
    public class TokenWriteRepository : WriteRepository<Domain.Entities.Token>,ITokenWriteRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IJwtGenerator _jwtGenerator;
        public TokenWriteRepository(ApplicationDbContext context, IConfiguration configuration, IJwtGenerator jwtGenerator) : base(context)
        {
            _context = context;
            _jwtGenerator = jwtGenerator;
        }

        public async Task<bool> SaveToken(Domain.Entities.Token token, string username)
        {
            try
            {
                // Kullanıcıyı username'e göre bul
                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.Username == username);

                if (user == null)
                {
                    Log.Error("Kullanıcı bulunamadı: {Username}", username);
                    return false;
                }

                // Yeni token'ı ilişkisel olarak oluştur
                var newToken = new Domain.Entities.Token
                {
                    UserId = user.Id, // Foreign key ataması
                    AccessToken = token.AccessToken,
                    RefreshToken = token.RefreshToken,
                    AccessTokenExpiration = token.AccessTokenExpiration,
                    RefreshTokenExpiration = token.RefreshTokenExpiration
                };

                await _context.Tokens.AddAsync(newToken);
                int affectedRows = await _context.SaveChangesAsync();

                return affectedRows > 0;
            }
            catch (DbUpdateException dbEx)
            {
                Log.Error(dbEx, "Veritabanı hatası [Kullanıcı: {Username}]", username);
                return false;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Token kaydetme hatası [Kullanıcı: {Username}]", username);
                return false;
            }
        }


        public async Task<Domain.Entities.Token> UpdateRefreshToken(string accesstoken, string username)
        {
            try
            {
                // İlgili kullanıcıya ait token bilgilerini alıyoruz
                var entity = await _context.Tokens
                    .Include(t => t.User)
                    .FirstOrDefaultAsync(t => t.User.Username == username);

                if (entity == null)
                    throw new Exception("Kullanıcı bulunamadı.");

                // Refresh token oluşturuluyor
                var result = _jwtGenerator.CreateRefreshToken(30);
                if (result == null)
                    throw new Exception("Token oluşturulamadı.");

                // Mevcut entity'nin token bilgilerini güncelliyoruz
                entity.AccessToken = result.AccessToken;
                entity.RefreshToken = result.RefreshToken;
                entity.RefreshTokenExpiration = result.RefreshTokenExpiration;

                // Entity'nin durumunu Modified olarak işaretliyoruz
                _context.Entry(entity).State = EntityState.Modified;

                // Değişiklikleri kaydediyoruz
                await SaveChanges();

                return entity;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                throw; // Hata kaydedildikten sonra exception yeniden fırlatılıyor
            }
        }



        public bool DeleteTokenById(string token)
        {
            try
            {
                var record = _context.Tokens.FirstOrDefault(x => x.AccessToken == token);

                if (record == null)
                {
                    return false; // Kayıt bulunamadıysa false döndür
                }

                _context.Tokens.Remove(record);
                _context.SaveChanges();

                return true; // Kayıt başarıyla silindi
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Log.Error(ex.Message, ex);
                throw; // Hata durumunda istisna fırlat
            }
        }
    }
}
