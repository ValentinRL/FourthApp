using System.ComponentModel.DataAnnotations;

namespace FourthApp.API.Contracts
{
    public sealed class GetCustomersOverviewRequest
    {
        public string? Search { get; init; }

        [Range(1, int.MaxValue, ErrorMessage = "Page must be greater than 0.")]
        public int Page { get; init; } = 1;

        [Range(1, 100, ErrorMessage = "PageSize must be between 1 and 100.")]
        public int PageSize { get; init; } = 20;
    }
}
