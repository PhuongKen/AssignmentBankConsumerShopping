using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class OrderDao
    {
        OnlinePhotoDataModel db = null;
        public OrderDao()
        {
            db = new OnlinePhotoDataModel();
        }
        public long Insert(Order order)
        {
            db.Orders.Add(order);
            db.SaveChanges();
            return order.ID;
        }

        public long Find()
        {
            var order = db.Orders.Where(x=>x.Status == null).OrderByDescending(x => x.CreatedDate).Take(1).ToList();
            long id = 0;
            foreach (var item in order)
            {
                id = item.ID;
            }
            return id;
        }

        public bool UpdateStatus(long id)
        {
            try
            {
                Order order = (from o in db.Orders where o.ID == id && o.Status == null select o).Single();
                if (order == null)
                {
                    return false;
                }
                else
                {
                    order.Status = 1;
                    db.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
