using ComputerStore.Domain.Entities;
using ComputerStore.Domain.Services;

namespace ComputerStore.Application.UseCase
{
    public class GoodsUseCase
    {
        private readonly IProductService<Goods> _productService;
        private readonly Mappers.GoodsMapper _goodsMapper;
        public GoodsUseCase(IProductService<Goods> productService, Mappers.GoodsMapper goodsMapper)
        {
            _productService = productService;
            _goodsMapper = goodsMapper;
        }

        public async Task CreateProductAsync(ComputerStore.Application.DTOs.GoodsDTO goodsDTO)
        {
            await _productService.AddAsync(_goodsMapper.MapToEntity(goodsDTO));
        }
        public async Task<IEnumerable<Goods>?> GetAllGoods()
        {
            return await _productService.GetAllAsync();
        }
        public async Task<Goods?> GetByID(int id)
        {
            return await _productService.GetByIdAsync(id);
        }
        public async Task<int> GetCountProduct()
        {
            return await _productService.CountAsync();
        }
        public async Task UpdateProductAsync(int id, DTOs.GoodsDTO goodsDTO)
        {
            await _productService.UpdateAsync(id, _goodsMapper.MapToEntity(goodsDTO));
        }
    }
}
