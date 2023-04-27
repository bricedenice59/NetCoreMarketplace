using System.Net;
using Api.Tests.Integration.Fixtures;
using Api.Tests.Integration.Utils;
using Core.Models;
using FluentAssertions;

namespace Api.Tests.Integration.MarketplaceControllers;

[Collection("integrationTest collection")]
public class ProductTypesControllerTests
{
    IntegrationTestFixture _fixture;

    public ProductTypesControllerTests(IntegrationTestFixture fixture)
    {
        this._fixture = fixture;
    }
    
    [Fact]
    public async Task GET_checkProductType_EndpointOK()
    {
        var response = await _fixture.Client.GetAsync("api/Products/types");
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
    
    [Fact]
    public async Task GET_checkProductTypeCount()
    {
         var productTypes = await _fixture.Client.GetAndDeserialize<ProductType[]>("api/Products/types");
         productTypes.Should().HaveCount(2);
    }
}