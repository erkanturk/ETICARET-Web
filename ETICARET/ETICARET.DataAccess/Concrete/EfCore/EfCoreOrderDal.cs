using ETICARET.DataAccess.Abstract;
using ETICARET.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETICARET.DataAccess.Concrete.EfCore
{
    public class EfCoreOrderDal : EfCoreGenericRepository<Order, DataContext>, IOrderDal
    {
        public List<Order> GetOrders(string userId)
        {
            using (var context = new DataContext())
            {
                var orders = context.Orders.Include(i => i.OrderItems)//Include: İlişkili varlıkları dahil etmek için kullanılır.
                     .ThenInclude(i => i.Product)//ThenInclude: İlişkili varlıkları dahil etmek için kullanılır.
                     .ThenInclude(i => i.Images)
                     .AsQueryable();
                //AsQueryable(): LINQ sorgularını daha esnek hale getirmek için kullanılır.
                //Bu, sorgunun daha sonra filtreleme, sıralama veya diğer işlemlerle genişletilebileceği anlamına gelir.

                if (!string.IsNullOrEmpty(userId))
                {
                    orders = orders.Where(i => i.UserId == userId);
                }
                return orders.ToList();//ToList(): Sorgu sonucunu listeye dönüştürür ve veritabanından verileri getirir.
            }
        }
    }
}
