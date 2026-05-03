using FourthApp.Application.Customers.GetCustomersOverview;
using FourthApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FourthApp.Application.Tests
{
    public sealed class GetCustomersOverviewQueryHandlerTests
    {
        [Fact]
        public async Task Handle_ReturnsCustomersWithOrderCount()
        {
            await using var dbContext = TestDbContextFactory.Create();

            dbContext.Customers.Add(new Customer
            {
                CustomerId = "ALFKI",
                CompanyName = "Alfreds Futterkiste",
                ContactName = "Maria Anders",
                Orders =
                [
                    new Order { OrderId = 1 },
                new Order { OrderId = 2 }
                ]
            });

            await dbContext.SaveChangesAsync(CancellationToken.None);

            var handler = new GetCustomersOverviewQueryHandler(dbContext);

            var result = await handler.Handle(
                new GetCustomersOverviewQuery(Page: 1, PageSize: 20),
                CancellationToken.None);

            Assert.True(result.IsSuccess);
            Assert.Single(result.Value.Customers);

            var customer = result.Value.Customers[0];

            Assert.Equal("ALFKI", customer.CustomerId);
            Assert.Equal(2, customer.OrdersCount);
            Assert.Equal(1, result.Value.Page);
            Assert.Equal(20, result.Value.PageSize);
            Assert.Equal(1, result.Value.TotalCount);
        }

        [Fact]
        public async Task Handle_AppliesSearchFilter()
        {
            await using var dbContext = TestDbContextFactory.Create();

            dbContext.Customers.AddRange(
                new Customer
                {
                    CustomerId = "ALFKI",
                    CompanyName = "Alfreds Futterkiste",
                    ContactName = "Maria Anders"
                },
                new Customer
                {
                    CustomerId = "BONAP",
                    CompanyName = "Bon app",
                    ContactName = "Laurence Lebihan"
                });

            await dbContext.SaveChangesAsync(CancellationToken.None);

            var handler = new GetCustomersOverviewQueryHandler(dbContext);

            var result = await handler.Handle(
                new GetCustomersOverviewQuery(Search: "Alfreds", Page: 1, PageSize: 20),
                CancellationToken.None);

            Assert.True(result.IsSuccess);
            Assert.Single(result.Value.Customers);
            Assert.Equal("ALFKI", result.Value.Customers[0].CustomerId);
        }
    }
}
