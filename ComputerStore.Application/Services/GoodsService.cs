using ComputerStore.Application.Mappers;
using ComputerStore.Domain.Entities;
using ComputerStore.Domain.Services;
using System.Linq.Expressions;

namespace ComputerStore.Application.Services
{
    public class GoodsService : BaseService<Goods>, IProductService
    {
        private readonly ComputerStore.Domain.Repositories.IProductRepository _goodsRepository;
        private readonly GoodsMapper _goodsMapper;

        public GoodsService(ComputerStore.Domain.Repositories.IProductRepository goodsRepository, GoodsMapper goodsMapper): base(goodsRepository)
        {
            _goodsMapper = goodsMapper;
            _goodsRepository = goodsRepository;
        }
        public async Task<int> CountAsync()
        {
            return await _goodsRepository.CountAsync();
        }
        public async Task<IReadOnlyList<Goods>?> FindAsync(Expression<Func<Goods, bool>> predicate)
        {
            return await _goodsRepository.FindAsync(predicate);
        }

    }
}
