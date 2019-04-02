using System;
using System.Collections.Generic;
using System.Text;
using eFolio.EF;
using System.Threading.Tasks;
namespace eFolio.BL.Interfaces
{
    public interface IAdminService
    {
        void Delete(int id);
        void Update(UserEntity user);
        UserEntity GetUser(int id);
        IEnumerable<UserEntity> GetUsersList();
    }
}