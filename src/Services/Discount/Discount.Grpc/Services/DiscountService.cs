using Discount.Grpc.Data;
using Discount.Grpc.Models;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;
using static Discount.Grpc.DiscountProtoService;

namespace Discount.Grpc.Services;

public class DiscountService(DiscountContext dbContext, ILogger<DiscountService> logger)
    : DiscountProtoServiceBase
{
    public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        var coupan = await dbContext.Coupan.FirstOrDefaultAsync(x => x.ProductName == request.ProductName);
        if (coupan == null)
        {
            coupan = new Coupon { ProductName = "No Product", Amount = 0, Description = "No Discount Description" };
        }
        logger.LogInformation("Discount is retrieved for product : {productName}, Amount : {amount}", coupan.ProductName, coupan.Amount);
        return coupan.Adapt<CouponModel>();
    }

    public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        var coupan = request.Coupon.Adapt<Coupon>();
        if (coupan == null)
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request object"));
        await dbContext.Coupan.AddAsync(coupan);
        await dbContext.SaveChangesAsync();
        logger.LogInformation("Discount is created for product : {productName}, Amount : {amount}", coupan.ProductName, coupan.Amount);
        return coupan.Adapt<CouponModel>();
    }

    public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        var coupan = request.Coupon.Adapt<Coupon>();
        if (coupan == null)
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request object"));
        dbContext.Coupan.Update(coupan);
        await dbContext.SaveChangesAsync();
        logger.LogInformation("Discount is updated for product : {productName}, Amount : {amount}", coupan.ProductName, coupan.Amount);
        return coupan.Adapt<CouponModel>();
    }
    public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
    {
        var coupan = await dbContext.Coupan.FirstOrDefaultAsync(x => x.ProductName == request.ProductName);
        if (coupan == null)
            throw new RpcException(new Status(StatusCode.NotFound, $"Discount with ProductNamer={request.ProductName} is not found"));

        dbContext.Coupan.Remove(coupan);
        await dbContext.SaveChangesAsync();
        logger.LogInformation("Discount is deleted for product : {productName}, Amount : {amount}", coupan.ProductName, coupan.Amount);
        return new DeleteDiscountResponse { Success = true };
    }
}
