using Demo_3.Commands;
using Demo_3.Models;
using static Demo_3.Interface.ICommandHandler;

namespace Demo_3.Handlers
{
    public class UserCommandHandler : ICommandHandler<CreateUserCommand>, ICommandHandler<UpdateUserCommand>, ICommandHandler<DeleteUserCommand>
    {
        private readonly UserDbContext _dbContext;

        public UserCommandHandler(UserDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task HandleAsync(CreateUserCommand command)
        {
            var user = new User { Name = command.Name, Email = command.Email };
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task HandleAsync(UpdateUserCommand command)
        {
            var user = _dbContext.Users.Find(command.Id);
            if (user != null)
            {
                user.Name = command.Name;
                user.Email = command.Email;
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task HandleAsync(DeleteUserCommand command)
        {
            var user = _dbContext.Users.Find(command.Id);
            if (user != null)
            {
                _dbContext.Users.Remove(user);
                await _dbContext.SaveChangesAsync();
            }
        }


    }


}