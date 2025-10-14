using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Net;

namespace ESG.Compliance.Api.Tests
{
    // PASSO 1: Criamos uma "fábrica" de aplicação customizada para nossos testes
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            // Esta linha diz ao teste onde encontrar os arquivos da API, como o appsettings.json
            builder.UseSolutionRelativeContentRoot("ESG.Compliance.Api");
        }
    }

    // PASSO 2: Modificamos a classe de teste para usar nossa nova "fábrica"
    public class LicencasControllerTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public LicencasControllerTests(CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Get_Licencas_DeveRetornarStatusCode200()
        {
            // Arrange
            var request = "/api/licencas";

            // Act
            var response = await _client.GetAsync(request);

            // Assert
            response.EnsureSuccessStatusCode(); // Isso também verifica se o status é 2xx
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}