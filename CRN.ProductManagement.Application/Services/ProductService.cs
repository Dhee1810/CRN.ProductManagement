using CRN.ProductManagement.Application.DTOs;
using CRN.ProductManagement.Application.Interfaces;
using CRN.ProductManagement.Domain.Entities;

namespace CRN.ProductManagement.Application.Services;

public class ProductService
{
    private readonly IProductRepository _repository;

    public ProductService(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<PagedResult<ProductDto>> GetProductsAsync(
        int pageNumber,
        int pageSize,
        string? search,
        CancellationToken cancellationToken)
    {
        if (pageNumber < 1)
            pageNumber = 1;

        if (pageSize < 1)
            pageSize = 10;

        if (pageSize > 100)
            pageSize = 100;

        var result = await _repository.GetPagedAsync(
            pageNumber,
            pageSize,
            search,
            cancellationToken);

        return new PagedResult<ProductDto>
        {
            Items = result.Items.Select(Map),
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalRecords = result.TotalRecords
        };
    }

    public async Task<ProductDto?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken)
    {
        var product = await _repository.GetByIdAsync(
            id,
            cancellationToken);

        return product == null ? null : Map(product);
    }

    public async Task<ProductDto> CreateAsync(
        CreateProductRequest request,
        string username,
        CancellationToken cancellationToken)
    {
        var product = new Product
        {
            ProductName = request.ProductName,
            CreatedBy = username,
            CreatedOn = DateTime.UtcNow
        };

        await _repository.AddAsync(product, cancellationToken);

        return Map(product);
    }

    public async Task<ProductDto?> UpdateAsync(
        int id,
        UpdateProductRequest request,
        string username,
        CancellationToken cancellationToken)
    {
        var product = await _repository.GetByIdAsync(
            id,
            cancellationToken);

        if (product == null)
            return null;

        product.ProductName = request.ProductName;
        product.ModifiedBy = username;
        product.ModifiedOn = DateTime.UtcNow;

        await _repository.UpdateAsync(product);

        return Map(product);
    }

    public async Task<bool> DeleteAsync(
        int id,
        CancellationToken cancellationToken)
    {
        var product = await _repository.GetByIdAsync(
            id,
            cancellationToken);

        if (product == null)
            return false;

        await _repository.DeleteAsync(product);

        return true;
    }

    private static ProductDto Map(Product product)
    {
        return new ProductDto
        {
            Id = product.Id,
            ProductName = product.ProductName,
            CreatedBy = product.CreatedBy,
            CreatedOn = product.CreatedOn,
            ModifiedBy = product.ModifiedBy,
            ModifiedOn = product.ModifiedOn
        };
    }
}