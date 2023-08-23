using Authentication.Net.Domain.Interfaces;
using Authentication.Net.Infra.Data.Context;
using Authentication.Net.Infra.Data.Repositories;
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

			return services;
		}
	}
}
