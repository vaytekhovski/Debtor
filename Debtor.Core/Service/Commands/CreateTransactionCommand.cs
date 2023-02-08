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
}

public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, string?>
{
    public IMongoCollection<Transaction> _transactions { get; set; }

    public CreateTransactionCommandHandler(IMongoDBSettings settings, IMongoClient mongoClient)
    {
        IMongoDatabase database = mongoClient.GetDatabase(settings.DatabaseName);
        _transactions = database.GetCollection<Transaction>(settings.UsersCollectionName);
    }

    public async Task<string?> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
    {
        var transaction = new Transaction()
        {
            UserFromId = request.UserFromId,
            UserToId = request.UserToId,
            JointId = request.JointId,
            Amount = request.Amount
        };

        await _transactions.InsertOneAsync(transaction);

        return transaction.Id;
    }
}