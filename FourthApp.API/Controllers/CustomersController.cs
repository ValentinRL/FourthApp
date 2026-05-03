using FourthApp.API.Contracts;
using FourthApp.Application.Customers.GetCustomerDetail;
using FourthApp.Application.Customers.GetCustomersOverview;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Northwind.Api.Customers;

[ApiController]
[Route("api/customers")]
public sealed class CustomersController(ISender sender) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(GetCustomersOverviewResult), StatusCodes.Status200OK)]
    public async Task<ActionResult<GetCustomersOverviewResult>> GetCustomerOverview([FromQuery] GetCustomersOverviewRequest request, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetCustomersOverviewQuery(request.Search, request.Page, request.PageSize), cancellationToken);

        if (result.IsFailed)
            return BadRequest(result.Errors);

        return Ok(result.Value); 
    }

    [HttpGet("{customerId}")]
    [ProducesResponseType(typeof(GetCustomerDetailResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetCustomerDetailResult>> GetCustomer(string customerId, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetCustomerDetailQuery(customerId), cancellationToken);

        if (result.IsFailed)
            return NotFound();

        return Ok(result.Value);
    }
}