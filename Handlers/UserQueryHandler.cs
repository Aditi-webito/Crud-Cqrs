using Demo_3.Models;
using Demo_3.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Demo_3.Handlers
{
    public class UserQueryHandler : IQueryHandler<GetAllUsersQuery, IEnumerable<User>>, IQueryHandler<GetUserByIdQuery, User>
    {
        private readonly UserDbContext _dbContext;

        public UserQueryHandler(UserDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<User>> HandleAsync(GetAllUsersQuery query)
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<User> HandleAsync(GetUserByIdQuery query)
        {
            var user = await _dbContext.Users.FindAsync(query.Id);
            return user;
        }
    }

    public interface IQueryHandler<TQuery, TResult>
    {
        Task<TResult> HandleAsync(TQuery query);
    }
}