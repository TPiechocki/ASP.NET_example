using Example.WebApi.Context;
using Example.WebApi.Contract;
using Example.WebApi.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Example.WebApi.Services
{
    public interface IUserService
    {
        Task<bool> VerifyLogin(LoginContract login, CancellationToken cancellationToken);
    }

    internal class UserService : IUserService
    {
        private readonly ExampleDbContext _context;

        public UserService(ExampleDbContext context)
        {
            this._context = context;
        }

        public async Task<bool> VerifyLogin(LoginContract login, CancellationToken cancellationToken)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.UserName == login.UserName, cancellationToken);

            if (user == null)
            {
                return false;
            }

            return new PasswordHasher<User>()
                .VerifyHashedPassword(null, user.Password, login.Password) == PasswordVerificationResult.Success;
        }
    }
}