using FluentResults;
using FourthApp.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FourthApp.Application.Customers.GetCustomerDetail
{
    public sealed class GetCustomerDetailQueryHandler(INorthwindDbContext dbContext) : IRequestHandler<GetCustomerDetailQuery, Result<GetCustomerDetailResult>>
    {
        public async Task<Result<GetCustomerDetailResult>> Handle(GetCustomerDetailQuery request, CancellationToken cancellationToken)
        {
            var customer = await dbContext.Customers
                      .AsNoTracking()
                      .Where(c => c.CustomerId == request.CustomerId)
                      .Select(c => new GetCustomerDetailResult(
                          c.CustomerId,
                          c.CompanyName,
                          c.ContactName,
                          c.ContactTitle,
                          c.Address,
                          c.City,
                          c.PostalCode,
                          c.Country,
                          c.Phone,
                          c.Orders
                              .OrderByDescending(o => o.OrderDate)
                              .Select(o => new OrderDto(
                                  o.OrderId,
                                  o.OrderDetails.Sum(od => od.UnitPrice * od.Quantity * (1.00m - (decimal)od.Discount)),
                                  o.OrderDetails.Sum(od => od.Quantity)
                              ))
                              .ToList()
                      ))
                      .FirstOrDefaultAsync(cancellationToken);

            if (customer is null)
                return Result.Fail<GetCustomerDetailResult>("Customer not found.");

            return Result.Ok(customer);
        }
    }
}
