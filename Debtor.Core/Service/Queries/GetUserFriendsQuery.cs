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
    public class GetUserFriendsQuery : IRequest<List<User>>
    {
        public string Id { get; set; } = string.Empty;
    }

    public class GetUserFriendsQueryHandler : IRequestHandler<GetUserFriendsQuery, List<User>>
    {
        public IMongoCollection<User> _users { get; set; }

        public GetUserFriendsQueryHandler(IMongoDBSettings settings, IMongoClient mongoClient)
        {
            IMongoDatabase database = mongoClient.GetDatabase(settings.DatabaseName);
            _users = database.GetCollection<User>(settings.UserCollectionName);
        }

        public async Task<List<User>> Handle(GetUserFriendsQuery request, CancellationToken cancellationToken)
            => await _users.Find(a => a.friendsId.Contains(request.Id)).ToListAsync();
    }
}

