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
    public class TransactionVm : IMapWith<Transaction>
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Transaction, TransactionVm>()
                .ForMember(vm => vm.Id, opt => opt.MapFrom(tra => tra.Id))
                .ForMember(vm => vm.Name, opt => opt.MapFrom(tra => tra.Type == "up" ? tra.UserFrom!.Name : tra.UserTo!.Name))
                .ForMember(vm => vm.Description, opt => opt.MapFrom(tra => tra.Description))
                .ForMember(vm => vm.Status, opt => opt.MapFrom(tra => tra.Status))
                .ForMember(vm => vm.Type, opt => opt.MapFrom(tra => tra.Type))
                .ForMember(vm => vm.Amount, opt => opt.MapFrom(tra => tra.Amount))
                .ForMember(vm => vm.Date, opt => opt.MapFrom(tra => tra.Date));
        }
    }


    public class GetAllTransactionsQuery : IRequest<List<TransactionVm>>
    {
        public string Id { get; set; } = string.Empty;
    }

    public class GetAllTransactionsQueryHandler : IRequestHandler<GetAllTransactionsQuery, List<TransactionVm>>
    {
        private readonly IMapper _mapper;
        public IMongoCollection<Transaction> _transactions { get; set; }

        public GetAllTransactionsQueryHandler(IMongoDBSettings settings, IMongoClient mongoClient, IMapper mapper)
        {
            IMongoDatabase database = mongoClient.GetDatabase(settings.DatabaseName);
            _transactions = database.GetCollection<Transaction>(settings.TransactionCollectionName);
            _mapper = mapper;
        }

        public async Task<List<TransactionVm>> Handle(GetAllTransactionsQuery request, CancellationToken cancellationToken)
        {
            var transactions = await _transactions.Find(a => a.UserFromId == request.Id || a.UserToId == request.Id).ToListAsync();

            if (transactions == null)
            {
                throw new NotFoundException(nameof(transactions), request.Id);
            }

            return transactions.Select(tra => _mapper.Map<TransactionVm>(tra)).ToList();
        }
    }
}

