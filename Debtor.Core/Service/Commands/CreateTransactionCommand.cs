using System;
using Debtor.Core.Common;
using Debtor.Core.Models;
using MediatR;
using MongoDB.Driver;

namespace Debtor.Core.Service.Commands;

public class CreateTransactionCommand : IRequest<string?>
{
    public string? UserFromId { get; set; }
    public string? UserToId { get; set; }
    public string? JointId { get; set; }
    public decimal Amount { get; set; } = 0;
    public string Status { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}

public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, string?>
{
    public IMongoCollection<Transaction> _transactions { get; set; }
    public IMongoCollection<User> _users { get; set; }
    public IMongoCollection<Joint> _joints { get; set; }

    public CreateTransactionCommandHandler(IMongoDBSettings settings, IMongoClient mongoClient)
    {
        IMongoDatabase database = mongoClient.GetDatabase(settings.DatabaseName);
        _transactions = database.GetCollection<Transaction>(settings.UserCollectionName);
        _users = database.GetCollection<User>(settings.UserCollectionName);
        _joints = database.GetCollection<Joint>(settings.JointCollectionName);
    }

    public async Task<string?> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
    {
        var transaction = new Transaction()
        {
            UserFromId = request.UserFromId,
            UserToId = request.UserToId,
            JointId = request.JointId,
            Amount = request.Amount,
            Type = request.Type,
            Date = new DateTime(),
            Description = request.Description,
            UserFrom = await _users.Find(u => u.Id == request.UserFromId).FirstOrDefaultAsync(),
            UserTo = await _users.Find(u => u.Id == request.UserToId).FirstOrDefaultAsync(),
            Joint = await _joints.Find(j => j.Id == request.JointId).FirstOrDefaultAsync()
        };

        await _transactions.InsertOneAsync(transaction);

        return transaction.Id;
    }
}