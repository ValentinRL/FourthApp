using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FourthApp.Application.Customers.GetCustomersOverview
{
    public sealed record GetCustomersOverviewQuery(string? Search = null, int Page = 1, int PageSize = 20) : IRequest<Result<GetCustomersOverviewResult>>;
}
