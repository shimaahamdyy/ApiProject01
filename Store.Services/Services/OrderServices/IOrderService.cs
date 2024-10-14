using Store.Data.Entities;
using Store.Repository.Specifications.ProductSpecs;
using Store.Services.Helper;
using Store.Services.Services.OrderServices.Dtos;
using Store.Services.Services.ProductServices.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Services.Services.OrderServices
{
    public interface IOrderService
    {
        Task<OrderDetailsDto> CreateOrderAsync (OrderDto input);
        Task<IReadOnlyList<OrderDetailsDto>> GetAllOrdersForUsersAsync(string buyerEmail);
        Task<OrderDetailsDto> GetOrderByIdAsync(Guid id);
        Task<IReadOnlyList<DeliveryMethod>> GetAllDeliveryMethodsAsync();
    }
}
