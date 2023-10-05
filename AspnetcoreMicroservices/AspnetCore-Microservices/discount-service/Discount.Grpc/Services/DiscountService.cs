using AutoMapper;
using Discount.Grpc.Entities;
using Discount.Grpc.Protos;
using Discount.Grpc.Repositories.Interfaces;
using Grpc.Core;

namespace Discount.Grpc.Services
{
    public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
    {
        private readonly IDiscountRepository _repository;
        private readonly ILogger<DiscountService> _logger;
        private readonly IMapper _mapper;

        public DiscountService(IDiscountRepository repository, ILogger<DiscountService> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var result = await _repository.GetDiscount(request.ProductName);
            if (result is null)
            {
                return null;
            }
            var couponModel = _mapper.Map<CouponModel>(result);
            _logger.LogInformation($" Discount retrived as {result.ProductName} - {result.Description} - {result.Amount}");
            return couponModel;
        }

        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var coupon = _mapper.Map<Coupon>(request.Coupon);
            var result = await _repository.CreateDiscount(coupon);
            var couponModel = _mapper.Map<CouponModel>(result);
            _logger.LogInformation($"Create discount for {coupon.ProductName}");
            return couponModel;
        }

        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var coupon = _mapper.Map<Coupon>(request.Coupon);
            var result = await _repository.UpdateDiscount(coupon);
            var couponModel = _mapper.Map<CouponModel>(result);
            _logger.LogInformation($"Update discount for {coupon.ProductName}");
            return couponModel;
        }

        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var result = await _repository.DeleteDiscount(request.ProductName);
            var response = new DeleteDiscountResponse
            {
                Success = result
            }; _logger.LogInformation($"{result}");
            return response;
        }
    }
}
