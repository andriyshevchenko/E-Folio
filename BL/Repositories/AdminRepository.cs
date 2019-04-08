using eFolio.EF;
using System;
using System.Collections.Generic;
using System.Linq;

namespace eFolio.BL.Repositories
{
    class AdminRepository : IRepository<UserEntity>
    {
        private AuthDBContext authDB;
        public AdminRepository(AuthDBContext db)
        {
            authDB = db;
        }
        public IEnumerable<UserEntity> GetUsersList()
        {
            return authDB.Users.ToList();
        }
        public UserEntity GetUser(int id)
        {
            UserEntity user = authDB.Users.Find(id);
            return user;
        }

        public void Add(UserEntity user)
        {
            authDB.Users.Add(user);
            authDB.SaveChanges();
        }
        public void Delete(int id)
        {
            UserEntity user = authDB.Users.Find(id);
            if (user != null)
                authDB.Users.Remove(user);
            authDB.SaveChanges();
        }
        public void Update(UserEntity user)
        {
            authDB.Users.Update(user);
            authDB.SaveChanges();
        }
        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    authDB.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}