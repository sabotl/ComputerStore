using ComputerStore.Application.DTOs;
using ComputerStore.Domain.Entities;

namespace ComputerStore.Application.Services
{
    public class GoodsAppService : BaseService<Goods>, Interfaces.IGoodsAppService<DTOs.GoodsDTO>
    {
        private readonly ComputerStore.Domain.Repositories.IProductRepository<Goods> _goodsRepository;
        private readonly Mappers.GoodsMapper _mapper;
        public GoodsAppService(ComputerStore.Domain.Repositories.IProductRepository<Goods> goodsRepository, Mappers.GoodsMapper goodsMapper) : base(goodsRepository)
        {
            _mapper = goodsMapper;
            _goodsRepository = goodsRepository;
        }
        public async Task CreateProduct(GoodsDTO dTO)
        {
            await _goodsRepository.AddAsync(_mapper.MapToEntity(dTO));
        }
        public async Task UpdateProduct (int id, GoodsDTO UpdatedGoodsDTO)
        {
            await _goodsRepository.UpdateAsync(id, _mapper.MapToEntity(UpdatedGoodsDTO));
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
