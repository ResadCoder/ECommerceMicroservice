using Discount.gRPC.Data;
using Discount.gRPC.Models;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Discount.gRPC.Services;

public class DiscountService(DiscountContext dbContext , ILogger<DiscountService> logger) : DiscountProtoService.DiscountProtoServiceBase
{
    public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        var coupon = await dbContext
                         .Coupons
                         .FirstOrDefaultAsync(x => x.ProductName == request.ProductName)
                     ?? new Coupon { ProductName = "No discount", Amount = 0, Description = "No discount" };
        
        logger.LogInformation($"Discount is retrieved for ProductName: {coupon.ProductName}");
        
        return coupon.Adapt<CouponModel>();
        
    }

    public override async  Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        var coupon =  request.Coupon.Adapt<Coupon>()
            ?? throw new RpcException(new Status(StatusCode.NotFound, "No discount"));
        
        dbContext.Coupons.Add(coupon);
        await dbContext.SaveChangesAsync();
        
        logger.LogInformation($"Discount is created for ProductName: {coupon.ProductName}");
        
        return coupon.Adapt<CouponModel>();
    }

    public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        var coupon = request.Coupon.Adapt<Coupon>()
            ?? throw new RpcException(new Status(StatusCode.NotFound, "No discount"));
        
        dbContext.Coupons.Update(coupon);
        await dbContext.SaveChangesAsync();
        
        logger.LogInformation($"Discount is updated for ProductName: {coupon.ProductName}");
        
        return coupon.Adapt<CouponModel>();
    }
    
}