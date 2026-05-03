using System;
using System.Collections.Generic;
using System.Text;

namespace FourthApp.Application.Customers.GetCustomerDetail
{
    public sealed class OrderDto(int orderId, decimal totalAmount, int totalItems)
    {
        public int OrderId { get; } = orderId;
        public decimal TotalAmount { get; } = Math.Round(totalAmount, 2);
        public int TotalItems { get; } = totalItems;
    }
}
