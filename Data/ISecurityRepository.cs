using System.Collections.Generic;
using System.Threading.Tasks;
using DevBlog.DatabaseModels;

namespace DevBlog.Data
{
    public interface ISecurityRepository
    {
         Task<List<User>> GetAllUsers();

        Task<bool> SaveAll();
    }
}