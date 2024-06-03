namespace ComputerStore.Application.Mappers
{
    public class GoodsMapper
    {
        public DTOs.GoodsDTO MapToDTO(ComputerStore.Domain.Entities.Goods goods)
        {
            return new DTOs.GoodsDTO
            {
                Productname = goods.Productname,
                Description = goods.Description,
                Price = goods.Price,
                CategoryID = goods.CategoryID,
                Quantity = goods.Quantity,
                ShopID = goods.ShopId
            };
        }
        public ComputerStore.Domain.Entities.Goods MapToEntity(DTOs.GoodsDTO goodsDTO)
        {
            return new ComputerStore.Domain.Entities.Goods
            {
                Productname = goodsDTO.Productname,
                Description = goodsDTO.Description,
                Price = goodsDTO.Price,
                CategoryID = goodsDTO.CategoryID,
                Quantity = goodsDTO.Quantity,
                ShopId = goodsDTO.ShopID
            };
        }
    }
}
