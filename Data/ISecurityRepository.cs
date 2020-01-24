using System.Collections.Generic;
using System.Threading.Tasks;
using DevBlog.DatabaseModels;

namespace DevBlog.Data
{
    public interface ISecurityRepository
    {
        Task<List<User>> GetAllUsers();

        void AddUser(User user);

        Task<bool> SaveAll();
    }
}