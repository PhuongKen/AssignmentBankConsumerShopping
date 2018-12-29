using Model.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
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

        public int Insert(User entity)
        {
            try
            {
                if (db.Users.Any(x => x.UserName == entity.UserName))
                {
                    return -1;
                }

                string salt = Guid.NewGuid().ToString().Substring(0, 7);
                string pass = entity.Password;

                string MD5Hash = Encryptor.MD5Hash(pass + salt);

                entity.Password = MD5Hash;
                entity.ConfirmPassword = MD5Hash;
                entity.Salt = salt;
                entity.Status = true;
                entity.CreateDate = DateTime.Now;

                db.Users.Add(entity);
                db.SaveChanges();
                return 1;
            }
            catch
            {
                return 0;
            }
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

        public List<User> ListUser()
        {
            return db.Users.ToList();
        }

        public User GetUserById(int? id)
        {
            return db.Users.SingleOrDefault(x=>x.ID == id);
        }

        public bool update(User user)
        {
            try
            {
                string pass = user.Password;
                string salt = Guid.NewGuid().ToString().Substring(0, 7);
                string MD5Hash = Encryptor.MD5Hash(pass + salt);
                user.Password = MD5Hash;
                user.ConfirmPassword = MD5Hash;
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool delete(User user)
        {
            try
            {
                db.Users.Remove(user);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
