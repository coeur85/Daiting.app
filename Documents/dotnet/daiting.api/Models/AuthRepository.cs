using System;
using System.Linq;
using System.Threading.Tasks;
using daiting.Data;
using Microsoft.EntityFrameworkCore;

namespace daiting.api.Models
{
    public class AuthRepository : iAuthRepository
    {
        public DataContext _context { get; }
        public AuthRepository(DataContext context)
        {
            _context = context;

        }

        public async Task<User> Login(string userName, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x=> x.UserName == userName);
            if(user == null) return null;
            if(!CheckUserPassword(user.PasswordStash, user.PasswordHash, password)) return null;

            // valied info;
            return user;
        }

        private bool CheckUserPassword(byte[] passwordStash, byte[] passwordHash, string password)
        {
            using(var stash = new System.Security.Cryptography.HMACSHA512(passwordStash))
            {
                var hash = stash.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for(int i = 0; i < hash.Length ; i++)
                {
                    if(hash[i] != passwordHash[i])  return false;

                }
            }
            return true;
        }

        public async Task<User> RegesterNewUser(User user, string password)
        {
            byte[] PasswordHash , PasswordStash;
            EncriptPassword(password ,out PasswordHash ,out PasswordStash);
            user.PasswordStash = PasswordStash;
            user.PasswordHash = PasswordHash;

             await _context.AddAsync(user);
             await _context.SaveChangesAsync();
             return user;

        }

        private void EncriptPassword(string password, out byte[] passwordHash, out byte[] passwordStash)
        {
            using(var stash = new System.Security.Cryptography.HMACSHA512())
            {
                passwordStash = stash.Key;
                passwordHash = stash.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

            }
            
        }

        public async Task<bool> UserExistis(string userName)
        {
           if(await _context.Users.AnyAsync(x=> x.UserName == userName))
           {
               return true;
           }
          return false;
        
        }
    }
    }