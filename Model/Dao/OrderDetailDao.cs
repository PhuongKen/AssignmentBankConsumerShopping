using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class OrderDetailDao
    {
        OnlinePhotoDataModel db = null;
        public OrderDetailDao()
        {
            db = new OnlinePhotoDataModel();
        }
        public bool Insert(OrderDetail detail)
        {
            try
            {
                db.OrderDetails.Add(detail);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public long Find(long id)
        {
            var orderDetail = db.OrderDetails.Where(x => x.OrderID == id).ToList();
            long total = 0;
            foreach (var item in orderDetail)
            {
                long price = Convert.ToInt64(item.Price);
                total += price;
            }
            return total;
        }
    }
}
