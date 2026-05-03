using System;
using System.Collections.Generic;
using System.Text;

namespace FourthApp.Application.Customers.GetCustomersOverview
{
    public sealed record GetCustomersOverviewResult(IReadOnlyList<CustomerDto> Customers, int Page, int PageSize, int TotalCount);
}
