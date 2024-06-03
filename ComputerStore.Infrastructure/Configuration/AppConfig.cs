using ComputerStore.Application.DTOs;
using ComputerStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace ComputerStore.Infrastructure.Configuration
{
    public class AppConfig
    {
        private readonly IConfiguration _configuration;

        public AppConfig(IConfiguration contfiguration)
        {
            _configuration = contfiguration;
        }

        public string GetConnectionString()
        {
            return _configuration.GetConnectionString("DefaultConnection");
        }
        public string GetExternalApiUrl()
        {
            return _configuration["ExternalApi:Url"];
        }
        public string GetApiToken()
        {
            return _configuration["ApiToken"];
        }
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<ComputerStore.Infrastructure.Data.AppDbContext>(options =>
               options.UseNpgsql(GetConnectionString()));

            services.AddScoped<ComputerStore.Domain.Repositories.IProductRepository<Goods>, ComputerStore.Infrastructure.Repository.GoodsRepository>();

            services.AddScoped<ComputerStore.Domain.Services.IProductService<Goods>, ComputerStore.Application.Services.GoodsService>();
            services.AddScoped<ComputerStore.Application.Services.Interfaces.IGoodsAppService<GoodsDTO>, ComputerStore.Application.Services.GoodsAppService>();

            services.AddScoped<ComputerStore.Application.UseCase.GoodsUseCase>();

            services.AddScoped<ComputerStore.Application.Mappers.GoodsMapper>();

            services.AddScoped(typeof(ComputerStore.Domain.Repositories.IBaseRepository<>), typeof(ComputerStore.Infrastructure.Repository.BaseRepository<>));

        }
    }
}
