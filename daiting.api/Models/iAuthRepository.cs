using System.Threading.Tasks;

namespace daiting.api.Models
{
    public interface iAuthRepository
    {
         Task<User> RegesterNewUser(User user , string password);
         Task<User> Login(string userName , string password);

         Task<bool> UserExistis(string userName);
    }
}