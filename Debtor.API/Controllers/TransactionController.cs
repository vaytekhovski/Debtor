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
public class TransactionController : ControllerBase
{
    public TransactionController(IMapper mapper, IMediator mediator) => (_mapper, _mediator) = (mapper, mediator);

    private readonly IMapper _mapper;
    private readonly IMediator _mediator;


    /// <summary>
    /// Get the list of all transactions
    /// </summary>
    /// <returns></returns>
    [HttpGet("{userId}")]
    public async Task<ActionResult<List<Transaction>>> All(string userId)
    {
        var vm = await _mediator.Send(new GetAllTransactionsQuery
        {
            userId = userId
        });
        return Ok(vm);
    }
    /// <summary>
    /// Create Transaction entity
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<Transaction>> Create([FromBody] CreateTransactionCommand createTransactionCommand)
    {
        var createdTransaction = await _mediator.Send(createTransactionCommand);
        return Ok(createdTransaction);
    }

    //[HttpGet("{id}")]
    //public async Task<ActionResult<AuthorDetailVm>> Get(string id)
    //{
    //    var vm = await _mediator.Send(new GetAuthorDetailsQuery
    //    {
    //        Id = id
    //    });
    //    return Ok(vm);
    //}
}


