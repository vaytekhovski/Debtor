using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Debtor.API.Models;
using Debtor.Core.Models;
using Debtor.Core.Service.Commands;
using Debtor.Core.Service.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Debtor.API.Controllers;

[ApiController]
[Route("[controller]")]
public class DashboardController : ControllerBase
{
    public DashboardController(IMapper mapper, IMediator mediator) => (_mapper, _mediator) = (mapper, mediator);

    private readonly IMapper _mapper;
    private readonly IMediator _mediator;


    [HttpGet("{userId}")]
    public async Task<ActionResult<DashboardVm>> Get(string userId)
    {
        var transactions = await _mediator.Send(new GetAllTransactionsQuery
        {
            userId = userId
        });

        var vm = new DashboardVm
        {
            IncomingAmount = transactions.Where(x => x.Type == "up").Select(x => x.Amount).Sum().ToString(),
            OutcomingAmount = transactions.Where(x => x.Type != "up").Select(x => x.Amount).Sum().ToString()
        };

        return Ok(vm);
    }
}