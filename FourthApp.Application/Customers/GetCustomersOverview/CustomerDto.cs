using System;
using System.Collections.Generic;
using System.Text;

namespace FourthApp.Application.Customers.GetCustomersOverview
{
    public sealed record CustomerDto(string CustomerId, string? CompanyName, int OrdersCount);
}
