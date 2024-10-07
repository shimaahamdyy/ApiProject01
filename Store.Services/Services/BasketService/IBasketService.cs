using Store.Services.Services.BasketService.Dtos;

namespace Store.Services.Services.BasketService
{
    public interface IBasketService
    {
        Task<CustomerBasketDto> GetBasketAsync(string BasketId);
        Task<CustomerBasketDto> UpdateBasketAsync(CustomerBasketDto Basket);
        Task<bool> DeleteBasketAsync(string BasketId);
    }
}
