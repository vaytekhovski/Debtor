using System;
using Debtor.Core.Common;
using Debtor.Core.Models;
using MediatR;
using MongoDB.Driver;

namespace Debtor.Core.Service.Commands;

public class CreateTransactionCommand : IRequest<Transaction?>
{
    public string? UserFromId { get; set; }
    public string? UserToId { get; set; }
    public string? JointId { get; set; }
    public decimal Amount { get; set; } = 0;
    public string Status { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}

public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, Transaction?>
{
    public IMongoCollection<Transaction> _transactions { get; set; }
    public IMongoCollection<User> _users { get; set; }
    public IMongoCollection<Joint> _joints { get; set; }

    private const string JUST_CREATED_STATUS = "created";

    public CreateTransactionCommandHandler(IMongoDBSettings settings, IMongoClient mongoClient)
    {
        IMongoDatabase database = mongoClient.GetDatabase(settings.DatabaseName);
        _transactions = database.GetCollection<Transaction>(settings.TransactionCollectionName);
        _users = database.GetCollection<User>(settings.UserCollectionName);
        _joints = database.GetCollection<Joint>(settings.JointCollectionName);
    }

    public async Task<Transaction?> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
    {
        var transaction = new Transaction()
        {
            UserFromId = request.UserFromId,
            UserToId = request.UserToId,
            JointId = null,
            Amount = request.Amount,
            Type = request.Type,
            Date = DateTime.Now,
            Description = request.Description,
            Status = JUST_CREATED_STATUS,
            UserFrom = await _users.Find(u => u.Id == request.UserFromId).FirstOrDefaultAsync(),
            UserTo = await _users.Find(u => u.Id == request.UserToId).FirstOrDefaultAsync(),
            Joint = await _joints.Find(j => j.Id == request.JointId).FirstOrDefaultAsync()
        };

        await _transactions.InsertOneAsync(transaction);

        return transaction;
    }
}