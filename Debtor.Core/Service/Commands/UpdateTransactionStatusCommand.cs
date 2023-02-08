using System;
using Debtor.Core.Common;
using Debtor.Core.Common.Exceptions;
using Debtor.Core.Models;
using MediatR;
using MongoDB.Driver;

namespace Debtor.Core.Service.Commands;

public class UpdateTransactionStatusCommand : IRequest
{
    public string Id { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
}

public class UpdateTransactionStatusCommandHandler : IRequestHandler<UpdateTransactionStatusCommand>
{
    public IMongoCollection<Transaction> _transactions { get; set; }

    public UpdateTransactionStatusCommandHandler(IMongoDBSettings settings, IMongoClient mongoClient)
    {
        IMongoDatabase database = mongoClient.GetDatabase(settings.DatabaseName);
        _transactions = database.GetCollection<Transaction>(settings.UsersCollectionName);
    }

    public async Task<Unit> Handle(UpdateTransactionStatusCommand request, CancellationToken cancellationToken)
    {
        var transaction = await _transactions.Find(a => a.Id == request.Id).FirstOrDefaultAsync();

        if (transaction == null || transaction.Id != request.Id)
        {
            throw new NotFoundException(nameof(transaction), request.Id);
        }

        transaction.Status = request.Status;

        await _transactions.ReplaceOneAsync(a => a.Id == request.Id, transaction);

        return Unit.Value;
    }
}