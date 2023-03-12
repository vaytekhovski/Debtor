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
    public class GetUserQuery : IRequest<User>
    {
        public string Id { get; set; } = string.Empty;
    }

    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, User>
    {
        public IMongoCollection<User> _users { get; set; }

        public GetUserQueryHandler(IMongoDBSettings settings, IMongoClient mongoClient)
        {
            IMongoDatabase database = mongoClient.GetDatabase(settings.DatabaseName);
            _users = database.GetCollection<User>(settings.UserCollectionName);
        }

        public async Task<User> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _users.Find(a => a.Id == request.Id).FirstOrDefaultAsync();

            if (user == null)
            {
                throw new NotFoundException(nameof(user), request.Id);
            }

            return user;
        }
    }
}

