using System.Net;
using Api.Tests.Integration.Fixtures;
using Api.Tests.Integration.Utils;
using Core.Models;
using FluentAssertions;

namespace Api.Tests.Integration.MarketplaceControllers;

public class ProductControllerTests : IntegrationTest
{
    [Fact]
    public async Task GET_checkProductType_EndpointOK()
    {
       
        var response = await _client.GetAsync("api/Products/types");
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
    
    [Fact]
    public async Task GET_checkProductTypeCount()
    {
        var response = await _client.GetAsync("api/Products/types");

        // var productTypes = JsonConvert.DeserializeObject<ProductType[]>(
        //     await response.Content.ReadAsStringAsync()
        // );
        
         var productTypes = await _client.GetAndDeserialize<ProductType[]>("api/Products/types");
         productTypes.Should().HaveCount(3);
    }
}