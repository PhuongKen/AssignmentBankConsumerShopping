using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{

    public class UserDao
    {
        OnlinePhotoDataModel db = null;

        public UserDao()
        {
            db = new OnlinePhotoDataModel();
        }

        public long Insert(User entity)
        {
            if (db.Users.Any(x => x.UserName == entity.UserName))
            {
                return Convert.ToInt64("Tài khoản này đã tồn tại.");
            }

            string salt = Guid.NewGuid().ToString().Substring(0, 7);
            string pass = entity.Password;

            string MD5Hash = Encryptor.MD5Hash(salt + pass);

            entity.Password = MD5Hash;

            db.Users.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }

        public User GetById(string userName)
        {
            return db.Users.SingleOrDefault(x => x.UserName == userName);
        }

        public int Login(string userName, string passWord)
        {
           
            var result = db.Users.SingleOrDefault(x => x.UserName == userName);
            if (result == null)
            {
                return -1;
            }
            else
            {
                if(result.Status == false)
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
