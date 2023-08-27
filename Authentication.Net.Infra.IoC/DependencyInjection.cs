using Authentication.Net.Application.Interfaces;
using Authentication.Net.Application.Mappings;
using Authentication.Net.Application.Provider.Bcrypt;
using Authentication.Net.Application.Providers.JWT;
using Authentication.Net.Application.UseCases;
using Authentication.Net.Domain.Interfaces;
using Authentication.Net.Infra.Data.Context;
using Authentication.Net.Infra.Data.Repositories;
using Authentication.Net.Infra.Email;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Authentication.Net.Infra.IoC
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<ApplicationDbContext>(options => options.UseMySQL(configuration.GetConnectionString("MYSQL"),
				b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

			services.AddScoped<IUserRepository, UserRepository>();
			services.AddScoped<IUserServices, UserServices>();
			services.AddScoped<IBcryptProvider, BcryptProvider>();
			services.AddScoped<IMailService, MailService>();
			services.AddScoped<IJwtProvider, JwtProvider>();
			services.AddAutoMapper(typeof(DomainToDTOMapping));

			services.AddMediatR(cfg =>
			cfg.RegisterServicesFromAssembly(typeof(UserServices).Assembly));
			return services;
		}
	}
}
