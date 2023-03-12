using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Debtor.Core.Models;
using Debtor.Core.Service.Commands;
using Debtor.Core.Service.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Debtor.API.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    public UserController(IMapper mapper, IMediator mediator) => (_mapper, _mediator) = (mapper, mediator);

    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    /// Create User entity
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<User>> Create([FromBody] CreateUserCommand createUserModel)
    {
        var command = _mapper.Map<CreateUserCommand>(createUserModel);
        var createdUser = await _mediator.Send(command);
        return Ok(createdUser);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<User>> Get(string id)
    {
        var vm = await _mediator.Send(new GetUserQuery
        {
            Id = id
        });
        return Ok(vm);
    }

    [HttpGet("{id}/friends")]
    public async Task<ActionResult<List<User>>> GetFriends(string id)
    {
        var vm = await _mediator.Send(new GetUserFriendsQuery
        {
            Id = id
        });
        return Ok(vm);
    }

}

