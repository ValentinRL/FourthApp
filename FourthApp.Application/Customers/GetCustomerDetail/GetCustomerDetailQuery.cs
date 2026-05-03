using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FourthApp.Application.Customers.GetCustomerDetail
{
    public sealed record GetCustomerDetailQuery(string CustomerId) : IRequest<Result<GetCustomerDetailResult>>;
}
