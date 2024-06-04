using Microsoft.AspNetCore.Mvc;

namespace ComputerStore.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GoodsController : ControllerBase
    {
        private readonly Application.UseCase.GoodsUseCase _useCase;

        public GoodsController(Application.UseCase.GoodsUseCase useCase)
        {
            _useCase = useCase;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return new JsonResult(await _useCase.GetAllGoods());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductByID(int id)
        {
            return new JsonResult(await _useCase.GetByID(id));
        }
        [HttpGet("count")]
        public async Task<IActionResult> GetCountProduct()
        {
            return Ok(await _useCase.GetCountProduct());
        }
        [HttpPost("create")]
        public async Task<IActionResult> CreateProduct(ComputerStore.Application.DTOs.GoodsDTO goods)
        {
            await _useCase.CreateProductAsync(goods);
            return Ok();
        }
        [HttpPut("update")]
        public async Task<IActionResult> UpdateProduct(int idProduct, Application.DTOs.GoodsDTO updatedProductDTO)
        {
            await _useCase.UpdateProductAsync(idProduct, updatedProductDTO);
            return Ok();
        }
    }
}
