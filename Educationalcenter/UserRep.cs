using Educationalcenter.Models;
using System.Security.Cryptography;
using System.Text;

namespace Educationalcenter
{
    public class UserRep
    {
        public static void UserPassword(User user, string password)
        {
            var ps = MD5.Create();
            var hash = ps.ComputeHash(Encoding.UTF8.GetBytes(password));
            user.Password = Convert.ToBase64String(hash);
        }
        public static bool VerifyPassword(User user, string password)
        {
            var ps = MD5.Create();
            var hash = ps.ComputeHash(Encoding.UTF8.GetBytes(password));
            string texthash = Convert.ToBase64String(hash);
            return hash.SequenceEqual(Convert.FromBase64String(user.Password));
        }
        public static bool IsExistUser(EducationalcenterContext context,UserLogin user)
        {
            if (!context.Users.Any(item => item.Login == user.Login))
            {
                return false;
            }

            User finduser = context.Users.First(item => item.Login == user.Login);

            return VerifyPassword(finduser, user.Password);
        }
        

    }
}
