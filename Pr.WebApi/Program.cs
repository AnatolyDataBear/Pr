
using Microsoft.EntityFrameworkCore;
using Pr.Bll.Interfaces;
using Pr.Bll.Services;
using Pr.Dal;
using Pr.Dal.Interfaces;
using Pr.Dal.Repositories;
using Pr.Models.Db;
using Pr.Models.Dto;
using Pr.Models.Mapping;
using Pr.WebApi.Exceptions;
using Serilog;
using System.Reflection;

namespace Pr.WebApi
{
	public class Program
	{
		public static void Main(string[] args)
		{
			Log.Logger = new LoggerConfiguration()
				.WriteTo.Console()
				.CreateLogger();
			try
			{
				Log.Information("Starting Pr.WebApi");

				var builder = WebApplication.CreateBuilder(args);
				
				builder.Host.UseSerilog();

				builder.Services.AddSingleton(builder.Configuration);

				string connection = builder.Configuration.GetConnectionString("DefaultConnection")!;
				builder.Services.AddDbContext<PrDbContext>(options =>
				{
					options.UseNpgsql(connection);
				});

				// Add services to the container.

				builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

				#region Repositories

				builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
				builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();

				#endregion

				#region Services

				builder.Services.AddScoped<IBaseService<Company, Guid, CompanyDto>, CompanyService>();
				builder.Services.AddScoped<IBaseService<Employee, Guid, EmployeeDto>, EmployeeService>();

				#endregion


				builder.Services.AddControllers();
				// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
				builder.Services.AddEndpointsApiExplorer();
				
				builder.Services.AddSwaggerGen(options =>
				{
					var basePath = AppContext.BaseDirectory;
					var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
					var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
					options.IncludeXmlComments(xmlPath);

					xmlFile = "Pr.Models.xml";
					xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
					options.IncludeXmlComments(xmlPath);
				});

				builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
				builder.Services.AddProblemDetails();

				var app = builder.Build();

				// Configure the HTTP request pipeline.
				if (app.Environment.IsDevelopment())
				{
					//app.UseSwagger();
					//app.UseSwaggerUI();
				}

				app.UseExceptionHandler();

				app.UseSwagger();
				app.UseSwaggerUI();

				app.UseHttpsRedirection();

				app.UseAuthorization();


				app.MapControllers();

				app.Run();
			}
			catch (Exception ex)
			{
				Log.Fatal(ex, "Pr.WebApi run app error!!!");
			}
			finally
			{
				Log.CloseAndFlush();
			}
		}
	}
}
