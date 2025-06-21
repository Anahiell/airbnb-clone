using System.Text.Json;
using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Airbnb.Application.UseCases;
using Airbnb.Connection.ConnectionRealization;
using Airbnb.Connection.ConnectionService.HttpConnection.Services;
using Airbnb.Domain;
using Airbnb.Domain.BoundedContexts.AddressManagement.Aggregates;
using Airbnb.Domain.BoundedContexts.AddressManagement.Interfaces;
using Airbnb.Domain.BoundedContexts.ProductManagement.Interfaces;
using Airbnb.Domain.BoundedContexts.PropertyTypeManagement.Aggregates;
using Airbnb.MongoRepository.Interfaces;
using Airbnb.MongoRepository.Repositories;
using Airbnb.ProductManagement.Application.BoundedContext.ProductManagement.UseCases.User;
using Airbnb.ProductManagement.Application.BoundedContext.QueryObjects;
using Airbnb.SharedKernel.ConnectionService.HttpConnection;
using Airbnb.SharedKernel.Repositories;
using MediatR;
using MongoDB.Driver;

namespace Airbnb.ProductManagement.Application.BoundedContext.Queries;

public class GetProductPaginatedQueryHandler : IQueryHandler<GetProductPaginatedQuery,
    Result<IEnumerable<Data>>>
{
    private readonly BaseMongoRepository<ProductEntityInfo> _repository;
    private readonly IProductDataAggregator _aggregator;
    private readonly IRepository<AddressLegal> _addressRepository;
    private readonly IRepository<ApartmentType> apartmentRepository;
    private readonly IUseCaseDispatcher _dispatcher;

    public GetProductPaginatedQueryHandler(BaseMongoRepository<ProductEntityInfo> repository,
        IProductDataAggregator httpConnectionService, IRepository<AddressLegal> addressRepository, IRepository<ApartmentType> apartmentRepository, IUseCaseDispatcher dispatcher)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _aggregator = httpConnectionService ?? throw new ArgumentNullException(nameof(httpConnectionService));
        _addressRepository = addressRepository;
        this.apartmentRepository = apartmentRepository;
        _dispatcher = dispatcher;
    }

    public async Task<Result<IEnumerable<Data>>> Handle(
        GetProductPaginatedQuery request, CancellationToken cancellationToken)
    {
        var filter = ProductFilterBuilder.Build(request);
        var sort = request.SortOrder switch
        {
            SortState.PriceAsc => Builders<ProductEntityInfo>.Sort.Ascending(p => p.Price),
            SortState.PriceDesc => Builders<ProductEntityInfo>.Sort.Descending(p => p.Price),
            _ => Builders<ProductEntityInfo>.Sort.Descending(p => p.CreatedDate)
        };

        var result = await _repository.GetFilteredPaginatedAsync(filter, sort, request.Page, request.PageSize);
        var products = result.Items.ToList();
        
        foreach (var product in products)
        {
            var address = await _addressRepository.GetByIdAsync(product.AddressLegalId, cancellationToken);
            if (address != null)
            {
                product.AddressFull = $"{address.Country?.Value}, {address.City?.Value}, " +
                                      $"{address.District?.Value}, д. {address.House?.Value}" +
                                      $"{(address.Block != null ? $", к. {address.Block.Value}" : "")}" +
                                      $"{(address.Flat != null ? $", кв. {address.Flat.Value}" : "")}";

                var apartmentType = await apartmentRepository.GetByIdAsync(product.ApartmentTypeId, cancellationToken);
                var typeName = apartmentType?.Value;
                product.ApartmentType = typeName;
            }
        }
        if (request.MinRating.HasValue)
        {
            products = products
                .Where(p => p.Rating >= request.MinRating.Value)
                .ToList();
        }
        if (request.DateStart.HasValue && request.DateEnd.HasValue)
        {
            products = products
                .Where(p => p.Orders == null || !p.Orders.Any(o =>
                    o.DateStart < request.DateEnd && o.DateEnd > request.DateStart))
                .ToList();
        }
        foreach (var product in products)
        {
            if (product.Review != null)
                foreach (var review in product.Review)
                {
                    var user = await _dispatcher.DispatchAsync(
                        new GetReviewUserByIdUseCase(review.UserId),
                        cancellationToken);

                    review.User = user;
                }
        }
        var dataList = products.Select(p => p.ToData()).ToList();
        return Result<IEnumerable<Data>>.Success(dataList);
        // return Result<IEnumerable<ProductEntityInfo>>.Success(products);
    }
}