using FluentResults;
using FourthApp.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FourthApp.Application.Customers.GetCustomersOverview
{
    public sealed class GetCustomersOverviewQueryHandler(INorthwindDbContext dbContext) : IRequestHandler<GetCustomersOverviewQuery, Result<GetCustomersOverviewResult>>
    {
        public async Task<Result<GetCustomersOverviewResult>> Handle(GetCustomersOverviewQuery request, CancellationToken cancellationToken)
        {
            var query = dbContext.Customers
                .AsNoTracking();

            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                var search = request.Search.ToLower();

                query = query.Where(c =>
                    c.CompanyName != null && c.CompanyName.ToLower().Contains(search));
            }

            var totalCount = await query.CountAsync(cancellationToken);

            var customers = await query
                .OrderBy(c => c.CompanyName)
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(c => new CustomerDto(
                    c.CustomerId,
                    c.CompanyName,
                    c.Orders.Count()
                ))
                .ToListAsync(cancellationToken);

            var result = new GetCustomersOverviewResult(
                customers,
                request.Page,
                request.PageSize,
                totalCount
            );

            return Result.Ok(result);
        }
    }
}
