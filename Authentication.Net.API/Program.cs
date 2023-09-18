using Authentication.Net.Infra.IoC;
using Microsoft.OpenApi.Models;

namespace Authentication.Net.API
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.Services.AddInfrastructure(builder.Configuration);

			// Add services to the container.

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo
				{
					Title = "AuthenticationAPI",
					Version = "v1"
				});
				c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
				{
					Description = @"JWT Authentication header using Bearer Scheme.
									Enter 'Bearer' [space] and then your token in the text input below.
									Example: 'Bearer 12345abcdef'",
					Name = "Authorization",
					In = ParameterLocation.Header,
					Type = SecuritySchemeType.ApiKey,
					Scheme = "Bearer"
				});

				c.AddSecurityRequirement(new OpenApiSecurityRequirement
	{
		{
			new OpenApiSecurityScheme
			{
				Reference = new OpenApiReference
				{
					Type = ReferenceType.SecurityScheme,
					Id = "Bearer"
				}
			},
			new string[] { }
		}
	});
			});

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();
			app.UseAuthorization();
			app.MapControllers();

			app.Run();
		}
	}
}