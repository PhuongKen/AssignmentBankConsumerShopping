using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class AdminDao
    {
        OnlinePhotoDataModel db = null;

        public AdminDao()
        {
            db = new OnlinePhotoDataModel();
        }
        
        public Admin GetById(string userName)
        {
            return db.Admins.SingleOrDefault(x => x.UserName == userName);
        }

        public int Login(string userName, string passWord)
        {

            var result = db.Admins.SingleOrDefault(x => x.UserName == userName);
            if (result == null)
            {
                return -1;
            }
            else
            {
                if (result.Status == false)
                {
                    return 0;
                }
                else
                {
                    string salt = result.Salt.ToString();
                    var MD5Hash = Encryptor.MD5Hash(passWord + salt);
                    if (result.Password == MD5Hash)
                        return 1;
                    else
                        return -1;
                }
            }

        }
    }
}
