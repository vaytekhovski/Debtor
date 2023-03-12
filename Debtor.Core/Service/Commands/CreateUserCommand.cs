using System;
using Debtor.Core.Common;
using Debtor.Core.Models;
using MediatR;
using MongoDB.Driver;

namespace Debtor.Core.Service.Commands;

public class CreateUserCommand : IRequest<User?>
{
    public string CreatorId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Login { get; set; }
}

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, User?>
{
    public IMongoCollection<User> _users { get; set; }

    public CreateUserCommandHandler(IMongoDBSettings settings, IMongoClient mongoClient)
    {
        IMongoDatabase database = mongoClient.GetDatabase(settings.DatabaseName);
        _users = database.GetCollection<User>(settings.UserCollectionName);
    }

    public async Task<User?> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        List<string> friends = new List<string>();
        User creator = new User();
        if (!string.IsNullOrEmpty(request.CreatorId))
        {
            creator = await _users.Find(a => a.Id == request.CreatorId).FirstOrDefaultAsync();
            friends.Add(creator.Id);
        }
        var user = new User()
        {
            Name = request.Name,
            Login = request.Login ?? "",
            friendsId = friends
        };

        await _users.InsertOneAsync(user);

        if(creator.Id != null)
        {
            creator.friendsId.Add(user.Id);
        }

        await _users.ReplaceOneAsync(u=> u.Id == request.CreatorId, creator);

        return user;
    }
}
