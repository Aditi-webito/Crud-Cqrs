using Cqrs.Commands;
using Demo_3.Commands;
using Demo_3.Handlers;
using Demo_3.Models;
using Demo_3.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Demo_3.Interface.ICommandHandler;

namespace Demo_3.Controllers
{
    //implement usingcqrs pattern 
    [Route("api/[controller]")]
    [ApiController]

    public class UsersController : ControllerBase
    {
        private readonly ICommandHandler<CreateUserCommand> _createUserCommandHandler;
        private readonly ICommandHandler<UpdateUserCommand> _updateUserCommandHandler;
        private readonly ICommandHandler<DeleteUserCommand> _deleteUserCommandHandler;
        private readonly IQueryHandler<GetAllUsersQuery, IEnumerable<User>> _getAllUsersQueryHandler;
        private readonly IQueryHandler<GetUserByIdQuery, User> _getUserByIdQueryHandler;

        public UsersController(ICommandHandler<CreateUserCommand> createUserCommandHandler, ICommandHandler<UpdateUserCommand> updateUserCommandHandler, ICommandHandler<DeleteUserCommand> deleteUserCommandHandler, IQueryHandler<GetAllUsersQuery, IEnumerable<User>> getAllUsersQueryHandler, IQueryHandler<GetUserByIdQuery, User> getUserByIdQueryHandler)
        {
            _createUserCommandHandler = createUserCommandHandler;
            _updateUserCommandHandler = updateUserCommandHandler;
            _deleteUserCommandHandler = deleteUserCommandHandler;
            _getAllUsersQueryHandler = getAllUsersQueryHandler;
            _getUserByIdQueryHandler = getUserByIdQueryHandler;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserCommand command)
        {
            await _createUserCommandHandler.HandleAsync(command);
            return CreatedAtAction(nameof(Get), new { id = command.Id }, command);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var users = await _getAllUsersQueryHandler.HandleAsync(new GetAllUsersQuery());
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var user = await _getUserByIdQueryHandler.HandleAsync(new GetUserByIdQuery { Id = id });
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateUserCommand command)
        {
            command.Id = id;
            await _updateUserCommandHandler.HandleAsync(command);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _deleteUserCommandHandler.HandleAsync(new DeleteUserCommand {   Id = id });
            return NoContent();
        }
    }

}