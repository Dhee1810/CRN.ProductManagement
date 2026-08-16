using CRN.ProductManagement.Domain.Entities;

namespace CRN.ProductManagement.Application.Interfaces;

public interface IProductRepository
{
    Task<Product?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken = default);

    Task<(IEnumerable<Product> Items, int TotalRecords)> GetPagedAsync(
        int pageNumber,
        int pageSize,
        string? search,
        CancellationToken cancellationToken = default);

    Task AddAsync(
        Product product,
        CancellationToken cancellationToken = default);

    Task UpdateAsync(Product product);

    Task DeleteAsync(Product product);

    Task<bool> ExistsAsync(
        int id,
        CancellationToken cancellationToken = default);
}