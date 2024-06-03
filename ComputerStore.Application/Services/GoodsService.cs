using ComputerStore.Application.Mappers;
using ComputerStore.Domain.Entities;
using ComputerStore.Domain.Services;
using System.Linq.Expressions;

namespace ComputerStore.Application.Services
{
    public class GoodsService : BaseService<Goods>, IProductService<Domain.Entities.Goods>
    {
        private readonly ComputerStore.Domain.Repositories.IProductRepository<Goods> _goodsRepository;
        private readonly GoodsMapper _goodsMapper;

        public GoodsService(ComputerStore.Domain.Repositories.IProductRepository<Goods> goodsRepository, GoodsMapper goodsMapper): base(goodsRepository)
        {
            _goodsMapper = goodsMapper;
            _goodsRepository = goodsRepository;
        }
        public async Task<int> CountAsync()
        {
            return await _goodsRepository.CountAsync();
        }
        public async Task<IEnumerable<Goods>?> FindAsync(Expression<Func<Goods, bool>> predicate)
        {
            return await _goodsRepository.FindAsync(predicate);
        }
        protected override void MapEntity(Goods existingEntity, Goods newEntity)
        {
            existingEntity.Productname = newEntity.Productname;
            existingEntity.Price = newEntity.Price;
            existingEntity.Description = newEntity.Description;
            existingEntity.Quantity = newEntity.Quantity;
            existingEntity.ShopId = newEntity.ShopId;
            existingEntity.CategoryID = newEntity.CategoryID;
        }
    }
}
