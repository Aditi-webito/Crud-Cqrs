using Demo_3.Commands;
using Demo_3.Handlers;
using Demo_3.Models;
using Demo_3.Queries;
using Microsoft.EntityFrameworkCore;
using static Demo_3.Interface.ICommandHandler;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<UserDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddTransient<ICommandHandler<CreateUserCommand>, UserCommandHandler>();
builder.Services.AddTransient<ICommandHandler<UpdateUserCommand>, UserCommandHandler>();
builder.Services.AddTransient<ICommandHandler<DeleteUserCommand>, UserCommandHandler>();
builder.Services.AddTransient<IQueryHandler<GetAllUsersQuery, IEnumerable<User>>, UserQueryHandler>();
builder.Services.AddTransient<IQueryHandler<GetUserByIdQuery, User>, UserQueryHandler>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
