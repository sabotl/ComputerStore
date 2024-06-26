using ComputerStore.Application.DTOs;
using ComputerStore.Domain.Entities;

namespace ComputerStore.Application.Mappers
{
    public class GoodsMapper
    {
        public DTOs.GoodsDTO MapToDTO(ComputerStore.Domain.Entities.Goods goods)
        {
            return new DTOs.GoodsDTO
            {
                Id = goods.id,
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
                id = goodsDTO.Id,
                Productname = goodsDTO.Productname,
                Description = goodsDTO.Description,
                Price = goodsDTO.Price,
                CategoryID = goodsDTO.CategoryID,
                Quantity = goodsDTO.Quantity,
                ShopId = goodsDTO.ShopID
            };
        }
        public IReadOnlyList<GoodsDTO> ToList(IReadOnlyList<Goods> goods)
        {
            return goods.Select(g => new GoodsDTO
            {
                Id = g.id,
                Productname = g.Productname,
                Description = g.Description,
                Price = g.Price,
                CategoryID = g.CategoryID,
                Quantity = g.Quantity,
                ShopID = g.ShopId
            }).ToList();
        }
    }
}
