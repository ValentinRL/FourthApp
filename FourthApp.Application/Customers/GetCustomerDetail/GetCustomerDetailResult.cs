using System;
using System.Collections.Generic;
using System.Text;

namespace FourthApp.Application.Customers.GetCustomerDetail
{
    public sealed record GetCustomerDetailResult(
        string CustomerId,
        string CompanyName,
        string? ContactName,
        string? ContactTitle,
        string? Address,
        string? City,
        string? PostalCode,
        string? Country,
        string? Phone,
        IReadOnlyList<OrderDto> Orders
    );
}
