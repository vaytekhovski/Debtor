using System;
using Debtor.Core.Common;
using Debtor.Core.Models;
using MediatR;
using MongoDB.Driver;

namespace Debtor.Core.Service.Commands;

public class CreateUserCommand : IRequest<string?>
{
    public string Name { get; set; } = string.Empty;
    public string? Login { get; set; }
    public string? Password { get; set; }
}

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, string?>
{
    public IMongoCollection<User> _users { get; set; }

    public CreateUserCommandHandler(IMongoDBSettings settings, IMongoClient mongoClient)
    {
        IMongoDatabase database = mongoClient.GetDatabase(settings.DatabaseName);
        _users = database.GetCollection<User>(settings.UsersCollectionName);
    }

    public async Task<string?> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User()
        {
            Name = request.Name,
            Login = request.Login,
            Password = request.Password //TODO: hash
        };

        await _users.InsertOneAsync(user);

        return user.Id;
    }
}
