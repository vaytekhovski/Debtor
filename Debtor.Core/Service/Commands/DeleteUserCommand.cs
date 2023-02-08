using System;
using Debtor.Core.Common;
using Debtor.Core.Common.Exceptions;
using Debtor.Core.Models;
using MediatR;
using MongoDB.Driver;

namespace Debtor.Core.Service.Commands;

public class DeleteUserCommand : IRequest
{
    public string Id { get; set; } = string.Empty;
}

public class DeleteAuthorCommandHandler : IRequestHandler<DeleteUserCommand>
{
    public IMongoCollection<User> _users { get; set; }

    public DeleteAuthorCommandHandler(IMongoDBSettings settings, IMongoClient mongoClient)
    {
        IMongoDatabase database = mongoClient.GetDatabase(settings.DatabaseName);
        _users = database.GetCollection<User>(settings.UsersCollectionName);
    }

    public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var author = await _users.Find(a => a.Id == request.Id).FirstOrDefaultAsync();

        if (author == null || author.Id != request.Id)
        {
            throw new NotFoundException(nameof(author), request.Id);
        }

        await _users.DeleteOneAsync(a => a.Id == request.Id);
        return Unit.Value;
    }
}
