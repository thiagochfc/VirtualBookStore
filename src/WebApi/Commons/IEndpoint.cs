namespace VirtualBookstore.WebApi.Commons;

internal interface IEndpoint
{
    static abstract void Map(IEndpointRouteBuilder app);
}
