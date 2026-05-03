using FourthApp.Application.Customers.GetCustomerDetail;
using FourthApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FourthApp.Application.Tests
{
    public sealed class GetCustomerDetailQueryHandlerTests
    {
        [Fact]
        public async Task Handle_ReturnsCustomerWithCalculatedOrderSummary()
        {
            await using var dbContext = TestDbContextFactory.Create();

            dbContext.Customers.Add(new Customer
            {
                CustomerId = "ALFKI",
                CompanyName = "Alfreds Futterkiste",
                ContactName = "Maria Anders",
                Orders =
                [
                    new Order
                {
                    OrderId = 1,
                    OrderDate = new DateTime(2024, 1, 1),
                    OrderDetails =
                    [
                        new OrderDetail
                        {
                            OrderId = 1,
                            ProductId = 1,
                            UnitPrice = 10m,
                            Quantity = 2,
                            Discount = 0
                        },
                        new OrderDetail
                        {
                            OrderId = 1,
                            ProductId = 2,
                            UnitPrice = 20m,
                            Quantity = 3,
                            Discount = 0.1f
                        }
                    ]
                }
                ]
            });

            await dbContext.SaveChangesAsync(CancellationToken.None);

            var handler = new GetCustomerDetailQueryHandler(dbContext);

            var result = await handler.Handle(
                new GetCustomerDetailQuery("ALFKI"),
                CancellationToken.None);

            Assert.True(result.IsSuccess);

            Assert.Equal("ALFKI", result.Value.CustomerId);
            Assert.Equal("Alfreds Futterkiste", result.Value.CompanyName);

            var order = Assert.Single(result.Value.Orders);

            Assert.Equal(74m, order.TotalAmount);
            Assert.Equal(5, order.TotalItems);
        }

        [Fact]
        public async Task Handle_ReturnsFailure_WhenCustomerDoesNotExist()
        {
            await using var dbContext = TestDbContextFactory.Create();

            var handler = new GetCustomerDetailQueryHandler(dbContext);

            var result = await handler.Handle(
                new GetCustomerDetailQuery("XXXXX"),
                CancellationToken.None);

            Assert.True(result.IsFailed);
            Assert.Contains(result.Errors, e => e.Message == "Customer not found.");
        }
    }
}
