using MiniERP.Application.Features.Reports.Sales;

namespace MiniERP.API.Endpoints.Reports;

public static class GetSalesReportEndpoint
{
    public static IEndpointRouteBuilder MapGetSalesReportEndpoint(
        this IEndpointRouteBuilder app)
    {
        app.MapGet(
            "/reports/sales",
    async (
     DateTime? fromDate,
     DateTime? toDate,
     Guid? customerId,
     GetSalesReportHandler handler,
     CancellationToken cancellationToken,
     int pageNumber = 1,
     int pageSize = 20,
     string? sortBy=null,
     bool descending = true) =>
            {
                var query = new GetSalesReportQuery
                {
                    FromDate = fromDate,
                    ToDate = toDate,
                    CustomerId = customerId,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    SortBy = sortBy,
                    Descending = descending,
                };

                var result =
                    await handler.Handle(
                        query,
                        cancellationToken);

                return Results.Ok(result);
            })
            .WithTags("Reports");

        return app;
    }
}