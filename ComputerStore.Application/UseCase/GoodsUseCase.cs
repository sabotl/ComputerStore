using ComputerStore.Application.DTOs;
using ComputerStore.Domain.Entities;
using ComputerStore.Domain.Services;
using System.Linq.Expressions;

namespace ComputerStore.Application.UseCase
{
    public class GoodsUseCase
    {
        private readonly IProductService _productService;
        private readonly Mappers.GoodsMapper _goodsMapper;
        public GoodsUseCase(IProductService productService, Mappers.GoodsMapper goodsMapper)
        {
            _productService = productService;
            _goodsMapper = goodsMapper;
        }
        public async Task<IReadOnlyList<GoodsDTO>?> GetByFilter(Expression<Func<GoodsDTO, bool>> predicate)
        {
            var goodsPredicate = Expressions.ExpressionConverter .Convert<GoodsDTO, Goods>(predicate);
            var products = await _productService.FindAsync(goodsPredicate);
            return _goodsMapper.ToList(products);
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
        public async Task DeleteProductAsync(int id)
        {
            await _productService.DeleteAsync(id);
        }
    }
}
