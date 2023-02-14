using System;
using Debtor.Core.Common;
using Debtor.Core.Common.Exceptions;
using Debtor.Core.Models;
using MediatR;
using MongoDB.Driver;

namespace Debtor.Core.Service.Commands;

public class UpdateUserNameCommand : IRequest
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
}

public class UpdateUserNameCommandHandler : IRequestHandler<UpdateUserNameCommand>
{
    public IMongoCollection<User> _users { get; set; }

    public UpdateUserNameCommandHandler(IMongoDBSettings settings, IMongoClient mongoClient)
    {
        IMongoDatabase database = mongoClient.GetDatabase(settings.DatabaseName);
        _users = database.GetCollection<User>(settings.UserCollectionName);
    }

    public async Task<Unit> Handle(UpdateUserNameCommand request, CancellationToken cancellationToken)
    {
        var author = await _users.Find(a => a.Id == request.Id).FirstOrDefaultAsync();

        if (author == null || author.Id != request.Id)
        {
            throw new NotFoundException(nameof(author), request.Id);
        }

        author.Name = request.Name;
        await _users.ReplaceOneAsync(a => a.Id == request.Id, author);

        return Unit.Value;
    }
}