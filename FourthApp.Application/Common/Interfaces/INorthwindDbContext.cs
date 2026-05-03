using FourthApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace FourthApp.Application.Common.Interfaces
{
    public interface INorthwindDbContext
    {
        DbSet<Customer> Customers { get; }
        DbSet<Order> Orders { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
