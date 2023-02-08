using System;
using Debtor.Core.Common;
using Debtor.Core.Models;
using MediatR;
using MongoDB.Driver;

namespace Debtor.Core.Service.Commands.CreateUser;

public class CreateUserCommand : IRequest<string?>
{
    public string Name { get; set; } = string.Empty;
}

public class CreateAuthorCommandHandler : IRequestHandler<CreateUserCommand, string?>
{
    public IMongoCollection<User> _users { get; set; }

    public CreateAuthorCommandHandler(IMongoDBSettings settings, IMongoClient mongoClient)
    {
        IMongoDatabase database = mongoClient.GetDatabase(settings.DatabaseName);
        _users = database.GetCollection<User>(settings.UsersCollectionName);
    }

    public async Task<string?> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User()
        {
            Name = request.Name,
        };

        await _users.InsertOneAsync(user);

        return user.Id;
    }
}
