using Microsoft.AspNetCore.Mvc;

namespace ComputerStore.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GoodsController : ControllerBase
    {
        private readonly Domain.Services.IProductService<Domain.Entities.Goods> _productService;
        private readonly Application.Services.Interfaces.IGoodsAppService<ComputerStore.Application.DTOs.GoodsDTO> _goodsAppService;

        public GoodsController(ComputerStore.Domain.Services.IProductService<Domain.Entities.Goods> productService, Application.Services.Interfaces.IGoodsAppService<GoodsDTO> goodsAppService)
        {
            _goodsAppService = goodsAppService;
            _productService = productService;
        }

        [HttpGet("getall")]
        public async Task<IActionResult> Index()
        {
            return new JsonResult(await _productService.GetAllAsync());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductByID(int id)
        {
            return new JsonResult(await _productService.GetByIdAsync(id));
        }
        [HttpGet("count")]
        public async Task<IActionResult> GetCountProduct()
        {
            return Ok(await _productService.CountAsync());
        }
        [HttpPost("create")]
        public async Task<IActionResult> CreateProduct(ComputerStore.Application.DTOs.GoodsDTO goods)
        {
            await _goodsAppService.CreateProduct(goods);
            return Ok();
        }
        [HttpPut("update")]
        public async Task<IActionResult> UpdateProduct(int idProduct, Application.DTOs.GoodsDTO updatedProductDTO)
        {
            await _productService.UpdateAsync(idProduct, updatedProductDTO);
        }
    }
}
