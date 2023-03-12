using System;
using AutoMapper;
using Debtor.Core.Common;
using Debtor.Core.Common.Exceptions;
using Debtor.Core.Common.Mapping;
using Debtor.Core.Models;
using MediatR;
using MongoDB.Driver;

namespace Debtor.Core.Service.Queries
{

    public class GetAllTransactionsQuery : IRequest<List<Transaction>>
    {
        public string userId { get; set; } = string.Empty;
    }

    public class GetAllTransactionsQueryHandler : IRequestHandler<GetAllTransactionsQuery, List<Transaction>>
    {
        private readonly IMapper _mapper;
        public IMongoCollection<Transaction> _transactions { get; set; }

        public GetAllTransactionsQueryHandler(IMongoDBSettings settings, IMongoClient mongoClient, IMapper mapper)
        {
            IMongoDatabase database = mongoClient.GetDatabase(settings.DatabaseName);
            _transactions = database.GetCollection<Transaction>(settings.TransactionCollectionName);
            _mapper = mapper;
        }

        public async Task<List<Transaction>> Handle(GetAllTransactionsQuery request, CancellationToken cancellationToken)
        {
            var transactions = await _transactions.Find(a => a.UserFromId == request.userId || a.UserToId == request.userId).ToListAsync();

            if (transactions == null)
            {
                throw new NotFoundException(nameof(transactions), request.userId);
            }

            return transactions.Select(tra => _mapper.Map<Transaction>(tra)).OrderByDescending(x=>x.Date).ToList();
        }
    }
}

