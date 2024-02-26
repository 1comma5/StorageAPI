using Microsoft.EntityFrameworkCore;
using StorageAPI.Scripts.Entities;
using StorageAPI.Scripts.Models;

namespace StorageAPI.Scripts.Services;
public class OrderProductService
{
    private readonly StorageDbContext _context;
    public OrderProductService(StorageDbContext context)
    {
        _context = context;
    }
    public async Task<OrderProductModel?> Get(int id)
    {
         var orderProduct = await _context.OrderProducts
            .Include(x => x.Order)
            .Include(x => x.Product)
            .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
         if (orderProduct == null) return null;
         return new OrderProductModel(
             orderProduct.Id,
             orderProduct.Order.Id,
             orderProduct.Product.Id,
             orderProduct.Quantity
         );
    }
    public async Task<OrderProductModel?> Add(OrderProductModel orderProductModel)
    {
       var order = await _context.Orders.FindAsync(orderProductModel.OrderId);
       var product = await _context.Products.FindAsync(orderProductModel.ProductId);
         if (order == null || product == null) return null;
         var orderProduct = new OrderProduct
         {
             Order = order,
             Product = product,
             Quantity = orderProductModel.Quantity
         };
         
         await _context.OrderProducts.AddAsync(orderProduct);
         await _context.SaveChangesAsync();
         return new OrderProductModel(
             orderProduct.Id,
             orderProduct.Order.Id,
             orderProduct.Product.Id,
             orderProduct.Quantity
         );
    }
    public async Task<OrderProductModel?> Update(OrderProductModel orderProductModel)
    {
      var orderProduct = await _context.OrderProducts
            .Include(x => x.Order)
            .Include(x => x.Product)
            .FirstOrDefaultAsync(x => x.Id == orderProductModel.Id && !x.IsDeleted);
         if (orderProduct == null) return null;
         var order = await _context.Orders.FindAsync(orderProductModel.OrderId);
         var product = await _context.Products.FindAsync(orderProductModel.ProductId);
         if (order != null) orderProduct.Order = order;
         if (product != null) orderProduct.Product = product;
         
         _context.OrderProducts.Update(orderProduct);
         await _context.SaveChangesAsync();

         return new OrderProductModel(
             orderProduct.Id,
             orderProduct.Order.Id,
             orderProduct.Product.Id,
             orderProduct.Quantity);
    }
    public async Task<bool> Delete(int id)
    {
      var orderProduct = await _context.OrderProducts.FindAsync(id);
        if (orderProduct == null) return false;
        orderProduct.IsDeleted = true;
        _context.OrderProducts.Update(orderProduct);
        await _context.SaveChangesAsync();
        return true;
    }
    public async Task<List<OrderProductModel>> GetAll()
    {
        var orderProducts = await _context.OrderProducts
            .Include(x => x.Order)
            .Include(x => x.Product)
            .Where(x => !x.IsDeleted)
            .ToListAsync();
        return orderProducts.Select(x => new OrderProductModel(
            x.Id,
            x.Order.Id,
            x.Product.Id,
            x.Quantity
        )).ToList();
    }
}