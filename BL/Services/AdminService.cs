using System.Collections.Generic;
using eFolio.BL.Interfaces;
using eFolio.BL.Repositories;
using eFolio.EF;

namespace eFolio.BL.Services
{
    class AdminService : IAdminService
    {
        private AdminRepository adminRepository;
        public AdminService(AuthDBContext authDB)
        {
            adminRepository = new AdminRepository(authDB);
        }
        public void Delete(int id)
        {
            adminRepository.Delete(id);
        }
        public void Update(UserEntity user)
        {
            adminRepository.Update(user);
        }
        public UserEntity GetUser(int id)
        {
            var userEntity = adminRepository.GetUser(id);
            return userEntity; 
        }
        public IEnumerable<UserEntity> GetUsersList()
        {
            return adminRepository.GetUsersList();
        }
    }
}