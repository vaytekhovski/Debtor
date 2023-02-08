using System;
using Debtor.Core.Common;
using Debtor.Core.Common.Exceptions;
using Debtor.Core.Models;
using MediatR;
using MongoDB.Driver;

namespace Debtor.Core.Service.Commands;

public class UpdateJointCommand : IRequest
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<User> Participants = new List<User>();
    public decimal Amount { get; set; } = 0;
}

public class UpdateJointCommandHandler : IRequestHandler<UpdateJointCommand>
{
    public IMongoCollection<Joint> _joint { get; set; }

    public UpdateJointCommandHandler(IMongoDBSettings settings, IMongoClient mongoClient)
    {
        IMongoDatabase database = mongoClient.GetDatabase(settings.DatabaseName);
        _joint = database.GetCollection<Joint>(settings.UsersCollectionName);
    }

    public async Task<Unit> Handle(UpdateJointCommand request, CancellationToken cancellationToken)
    {
        var transaction = await _joint.Find(a => a.Id == request.Id).FirstOrDefaultAsync();

        if (transaction == null || transaction.Id != request.Id)
        {
            throw new NotFoundException(nameof(transaction), request.Id);
        }

        transaction.Name = request.Name;
        transaction.Description = request.Description;
        transaction.Participants = request.Participants;
        transaction.Amount = request.Amount;

        await _joint.ReplaceOneAsync(a => a.Id == request.Id, transaction);

        return Unit.Value;
    }
}
