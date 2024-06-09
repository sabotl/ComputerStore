using ComputerStore.Application.Services;
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

            services.AddScoped<ComputerStore.Domain.Repositories.IProductRepository, ComputerStore.Infrastructure.Repository.GoodsRepository>();
            services.AddScoped<ComputerStore.Domain.Repositories.IUserRepostory, ComputerStore.Infrastructure.Repository.UserRepostiry>();

            services.AddScoped<ComputerStore.Domain.Services.IProductService, ComputerStore.Application.Services.GoodsService>();
            services.AddScoped<ComputerStore.Domain.Services.IUserService, ComputerStore.Application.Services.UserService>();


            // Регистрация TokenService
            services.AddScoped<ComputerStore.Application.Services.TokenService>(provider =>
            {
                var configuration = provider.GetRequiredService<IConfiguration>();

                string secretKey = configuration["JwtSettings:Secret"];
                string accessTokenExpirationConfig = configuration["JwtSettings:ExpirationInMinutes"];
                string refreshTokenExpirationConfig = configuration["JwtSettings:RefreshTokenExpirationDays"];

                if (string.IsNullOrEmpty(secretKey) || string.IsNullOrEmpty(accessTokenExpirationConfig) || string.IsNullOrEmpty(refreshTokenExpirationConfig))
                {
                    throw new Exception("Missing configuration parameters for TokenService.");
                }

                if (!TimeSpan.TryParse(accessTokenExpirationConfig, out TimeSpan accessTokenExpiration) ||
                    !TimeSpan.TryParse(refreshTokenExpirationConfig, out TimeSpan refreshTokenExpiration))
                {
                    throw new Exception("Invalid configuration parameters for TokenService.");
                }

                return new TokenService(provider.GetRequiredService<ComputerStore.Domain.Repositories.IUserRepostory>(), secretKey, accessTokenExpiration, refreshTokenExpiration);
            });


            services.AddScoped<ComputerStore.Application.UseCase.GoodsUseCase>();
            services.AddScoped<ComputerStore.Application.UseCase.UserUseCase>();

            services.AddScoped<ComputerStore.Application.Mappers.GoodsMapper>();

            services.AddScoped(typeof(ComputerStore.Domain.Repositories.IBaseRepository<>), typeof(ComputerStore.Infrastructure.Repository.BaseRepository<>));
        }
    }
}
