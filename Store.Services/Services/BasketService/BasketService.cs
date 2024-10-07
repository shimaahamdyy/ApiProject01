using AutoMapper;
using Store.Repository.Basket;
using Store.Repository.Basket.Models;
using Store.Services.Services.BasketService.Dtos;

namespace Store.Services.Services.BasketService
{
    public class BasketService : IBasketService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public BasketService(IBasketRepository basketRepository , IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }
        public async Task<bool> DeleteBasketAsync(string BasketId)
            => await _basketRepository.DeleteBasketAsync(BasketId);

        public async Task<CustomerBasketDto> GetBasketAsync(string BasketId)
        {
            var basket = await _basketRepository.GetBasketAsync(BasketId);

            if (basket == null)
                return new CustomerBasketDto();

            var mappedBasket = _mapper.Map<CustomerBasketDto>(basket);

            return mappedBasket;
        }

        public async Task<CustomerBasketDto> UpdateBasketAsync(CustomerBasketDto input)
        {
            if (input.Id is null)
                input.Id = GenerateRandomBasketid();

            var customerBasket = _mapper.Map<CustomerBasket>(input);

            var updatedBasket = await _basketRepository.UpdateBasketAsync(customerBasket);

            var mappedUpdatedBasket = _mapper.Map<CustomerBasketDto>(updatedBasket);

            return mappedUpdatedBasket;

        }

        private string GenerateRandomBasketid()
        {
            Random random = new Random();

            int randomDigits = random.Next(1000, 10000);

            return $"BS - {randomDigits}";

        }
    }
}
