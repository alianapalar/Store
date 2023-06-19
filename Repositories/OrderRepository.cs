using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;

namespace Repositories
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(RepositoryContext context) : base(context)
        {
        }

        public IQueryable<Order> Orders => _context.Orders
                      .Include(x=>x.Lines)
                      .ThenInclude(y=>y.Product)
                      .OrderBy(o=>o.Shipped)
                      .ThenByDescending(o=>o.OrderID);

        public int NumberOfInProcess =>  _context.Orders.Count(o => o.Shipped.Equals(false));

        public void Complete(int id)
        {
            var order = FindByCondition(o => o.OrderID.Equals(id),true);
            if(order is null)
                throw new Exception("Order could not found!");
            order.Shipped = true;
        }

        public Order? GetOneOrder(int id)
        {
            return FindByCondition(x=>x.OrderID.Equals(id),false);
        }

        public void SaveOrder(Order order)
        {
            _context.AttachRange(order.Lines.Select(x=>x.Product));
            if(order.OrderID==0)
            {
                _context.Orders.Add(order);
            }
            _context.SaveChanges();
        }
    }
}