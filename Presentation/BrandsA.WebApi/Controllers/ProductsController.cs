using Azure.Core;
using BrandsA.Application.Features.Commands.Product.AddProduct;
using BrandsA.Application.Features.Commands.Product.DeleteProduct;
using BrandsA.Application.Features.Commands.Product.UpdateProduct;
using BrandsA.Application.Features.Queries.Product.GetAllProduct;
using BrandsA.Application.Features.Queries.Product.GetProduct;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BrandsA.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        readonly IMediator _mediator;
        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet] // Ürün Listeleme
        public async Task<IActionResult> Get([FromQuery] GetAllProductQueryRequest request)
        {
            if (ModelState.IsValid)
            {
                GetAllProductQueryResponse response = await _mediator.Send(request);
                if (response.Products.Count<1)
                {
                    return BadRequest("Ürün Bulunamadı!");
                }
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }
           
        }
        [HttpGet("{id}")] // Köşeli parantezler düzeltildi ve route template eklendi
        public async Task<IActionResult> Get([FromRoute] GetProductQueryRequest request) // FromQuery -> FromRoute
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            GetProductQueryResponse response = await _mediator.Send(request);

            if (response.Product == null)
            {
                return BadRequest("Ürün Bulunamadı!");
            }

            return Ok(response);
        }
        [HttpPost] //Ürün Ekleme
        public async Task<IActionResult> Post(AddProductCommandRequest request)
        {
            if (ModelState.IsValid)
            {
                AddProductCommandResponse response = await _mediator.Send(request);
              
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut] //Ürün Güncelleme
        public async Task<IActionResult> Put(UpdateProductCommandRequest request)
        {
            if (ModelState.IsValid)
            {
                UpdateProductCommandResponse response = await _mediator.Send(request);

                return Ok(response);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete] //Ürün Silme
        public async Task<IActionResult> Delete(DeleteProductCommandRequest request)
        {
            if (ModelState.IsValid)
            {
                DeleteProductCommandResponse response = await _mediator.Send(request);

                return Ok(response);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
