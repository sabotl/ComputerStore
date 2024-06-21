using Microsoft.AspNetCore.Authorization;
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
            try
            {
                return new JsonResult(await _useCase.GetByID(id));
            }catch(Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        [HttpGet("count")]
        public async Task<IActionResult> GetCountProduct()
        {
            try
            {
                return Ok(await _useCase.GetCountProduct());
            }catch(Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        [Authorize(Roles = "manager")]
        [HttpPost("create")]
        public async Task<IActionResult> CreateProduct(ComputerStore.Application.DTOs.GoodsDTO goods)
        {
            try
            {
                await _useCase.CreateProductAsync(goods);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        [Authorize(Roles = "manager")]
        [HttpPut("update")]
        public async Task<IActionResult> UpdateProduct(int idProduct, Application.DTOs.GoodsDTO updatedProductDTO)
        {
            try
            {
                await _useCase.UpdateProductAsync(idProduct, updatedProductDTO);
                return Ok();
            }catch(Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        [Authorize(Roles = "manager")]
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                await _useCase.DeleteProductAsync(id);
                return Ok();
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("filter")]
        public async Task<IReadOnlyList<Application.DTOs.GoodsDTO>?> GetByFilter()
        {
            return null;
        }
    }
}
