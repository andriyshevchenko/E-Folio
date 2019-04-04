using System.Collections.Generic;
using eFolio.EF;
namespace eFolio.BL
{
    public interface IAdminService
    {
        void Delete(int id);
        void Update(UserEntity user);
        UserEntity GetUser(int id);
        IEnumerable<UserEntity> GetUsersList();
    }
}