using Debtor.Core.Common;
using Debtor.Core.Models;
using MediatR;
using MongoDB.Driver;

namespace Debtor.Core.Service.Commands;

public class CreateJointCommand : IRequest<string?>
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<User> Participants = new List<User>();
    public decimal Amount { get; set; } = 0;
}

public class CreateJointCommandHandler : IRequestHandler<CreateJointCommand, string?>
{
    public IMongoCollection<Joint> _joints { get; set; }

    public CreateJointCommandHandler(IMongoDBSettings settings, IMongoClient mongoClient)
    {
        IMongoDatabase database = mongoClient.GetDatabase(settings.DatabaseName);
        _joints = database.GetCollection<Joint>(settings.JointCollectionName);
    }

    public async Task<string?> Handle(CreateJointCommand request, CancellationToken cancellationToken)
    {
        var transaction = new Joint()
        {
            Name = request.Name,
            Description = request.Description,
            Participants = request.Participants,
            Amount = request.Amount
        };

        await _joints.InsertOneAsync(transaction);

        return transaction.Id;
    }
}
