using AutoMapper;
using Ordering.Application.Features.Commands.CheckoutOrder;
using Ordering.Application.Features.Commands.UpdateOrder;
using Ordering.Application.Models.ViewModels;
using Ordering.Domain.Aggregates.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Mappers
{
    public class ProfileMapper : Profile 
    {
        public ProfileMapper()
        {
            CreateMap<Order, OrderViewModel>().ReverseMap();
            CreateMap<Order, CheckoutOrderCommand>().ReverseMap();
            CreateMap<Order, UpdateOrderCommand>().ReverseMap();
        }
    }
}
