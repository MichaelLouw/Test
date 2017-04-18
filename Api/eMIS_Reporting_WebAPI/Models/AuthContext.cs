using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace eMIS_WebAPI.Models
{
    public class AuthContext : IdentityDbContext<IdentityUser>
    {
        public AuthContext(): base("MyDbContext")
        {
            
        }

        public class User
        {
            [Key]
            public System.Guid User_ID { get; set; }
            public string Name { get; set; }
            public string Surname { get; set; }
            public string Password { get; set; }
            public Nullable<int> Security_Level { get; set; }
            public string Contact_Number { get; set; }
            public string Email { get; set; }
            public string Department { get; set; }
            public string Municipality { get; set; }
            public Nullable<int> StatusID { get; set; }
        }

        public class AuthRepository : IDisposable
        {
            private AuthContext _ctx;

            private UserManager<IdentityUser> _userManager;

            public AuthRepository()
            {
                _ctx = new AuthContext();
                _userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(_ctx));
            }

            public async Task<IdentityResult> RegisterUser(User userModel)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = userModel.Email
                };

                var result = await _userManager.CreateAsync(user, userModel.Password);

                return result;
            }

            public async Task<IdentityUser> FindUser(string userName, string password)
            {
                IdentityUser user = await _userManager.FindAsync(userName, password);

                return user;
            }

            public void Dispose()
            {
                _ctx.Dispose();
                _userManager.Dispose();
            }
        }
    }
}