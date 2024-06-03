using ComputerStore.Domain.Entities;
using ComputerStore.Domain.Services;

namespace ComputerStore.Application.UseCase
{
    public class GoodsUseCase
    {
        private readonly IProductService<Goods> _productService;

        public GoodsUseCase(IProductService<Goods> productService)
        {
            _productService = productService;
        }

        public async Task CreateProductAsync(ComputerStore.Application.DTOs.GoodsDTO goodsDTO)
        {
            var newProduct = new Goods
            {
                Productname = goodsDTO.Productname,
                Description = goodsDTO.Description,
                Price = goodsDTO.Price,
                CategoryID = goodsDTO.CategoryID,
                Quantity = goodsDTO.Quantity,
                ShopId = goodsDTO.ShopID
            };

            await _productService.AddAsync(newProduct);
        }
        public async Task<IEnumerable<Goods>?> GetAllGoods()
        {
            return await _productService.GetAllAsync();
        }
    }
}
