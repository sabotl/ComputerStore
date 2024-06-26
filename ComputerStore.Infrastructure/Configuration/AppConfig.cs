using ComputerStore.Application.Services;
using ComputerStore.Domain.Services;
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
            services.AddScoped<IUserService, UserService>(provider =>
            {
                return new UserService(
                    provider.GetRequiredService<ComputerStore.Domain.Repositories.IUserRepostory>(),
                    provider.GetRequiredService<IAccessTokenService>(),
                    provider.GetRequiredService<IRefreshTokenService>()
                );
            });



            // Регистрация TokenService
            services.AddScoped<IAccessTokenService>(provider =>
            {
                var configuration = provider.GetRequiredService<IConfiguration>();

                string secretKey = configuration["JwtSettings:Secret"];
                string audience = configuration["JwtSettings:Audience"];
                string issuer = configuration["JwtSettings:Issuer"];
                string accessTokenExpirationConfig = configuration["JwtSettings:ExpirationInMinutes"];

                if (string.IsNullOrEmpty(secretKey) || string.IsNullOrEmpty(accessTokenExpirationConfig))
                {
                    throw new Exception("Missing configuration parameters for AccessTokenService.");
                }

                if (!int.TryParse(accessTokenExpirationConfig, out int accessTokenExpirationMinutes))
                {
                    throw new Exception("Invalid configuration parameters for AccessTokenService.");
                }

                TimeSpan accessTokenExpiration = TimeSpan.FromMinutes(accessTokenExpirationMinutes);

                return new AccessTokenService(secretKey, audience, issuer, accessTokenExpiration);
            });

            services.AddScoped<IRefreshTokenService>(provider =>
            {
                var configuration = provider.GetRequiredService<IConfiguration>();

                string secretKey = configuration["JwtSettings:Secret"];
                string audience = configuration["JwtSettings:Audience"];
                string issuer = configuration["JwtSettings:Issuer"];
                string refreshTokenExpirationConfig = configuration["JwtSettings:RefreshTokenExpirationDays"];

                if (string.IsNullOrEmpty(secretKey) || string.IsNullOrEmpty(refreshTokenExpirationConfig))
                {
                    throw new Exception("Missing configuration parameters for RefreshTokenService.");
                }

                if (!int.TryParse(refreshTokenExpirationConfig, out int refreshTokenExpirationDays))
                {
                    throw new Exception("Invalid configuration parameters for RefreshTokenService.");
                }

                TimeSpan refreshTokenExpiration = TimeSpan.FromDays(refreshTokenExpirationDays);

                return new RefreshTokenService(
                    provider.GetRequiredService<ComputerStore.Domain.Repositories.IUserRepostory>(),
                    secretKey,
                    audience,
                    issuer,
                    refreshTokenExpiration
                );
            });

            services.AddScoped<ComputerStore.Application.UseCase.GoodsUseCase>();
            services.AddScoped<ComputerStore.Application.UseCase.UserUseCase>();

            services.AddScoped<ComputerStore.Application.Mappers.GoodsMapper>();
            services.AddScoped<Application.Mappers.UserMapper>();

            services.AddScoped(typeof(ComputerStore.Domain.Repositories.IBaseRepository<>), typeof(ComputerStore.Infrastructure.Repository.BaseRepository<>));
        }
    }
}
