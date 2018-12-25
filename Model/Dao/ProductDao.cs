using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class ProductDao
    {
        OnlinePhotoDataModel db = null;

        public ProductDao()
        {
            db = new OnlinePhotoDataModel();
        }

        public List<Product> ListNewProduct(int top)
        {
            return db.Products.OrderByDescending(x => x.CreateDate).Take(top).ToList();
        }

        public List<Product> ListFeatureProduct(int top)
        {
            return db.Products.Where(x=>x.TopHot != null && x.TopHot> DateTime.Now).OrderByDescending(x=>x.CreateDate).Take(top).ToList();
        }

        public Product ViewDetail(long id)
        {
            return db.Products.Find(id);
        }
    }
}
