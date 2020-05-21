using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElectronicJournal_WEB.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ElectronicJournal_WEB
{
	public class Startup
	{
		public IConfiguration Configuration { get; }

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public void ConfigureServices(IServiceCollection services)
		{
			string connectionString = Configuration.GetConnectionString("DefaultConnection");
			services.AddDbContext<ElectronicalJournalContext>(options => options.UseSqlServer(connectionString));
			//��������� �������, ���������� ��� ������ MVC
			services.AddMvc();
		}

		
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseStaticFiles();

			app.UseRouting();
			//���������� �������� � ��������� �������������� ��������� ����������
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "Default",					//��� ��������
					pattern: "{controller=authorization}/{action=index}/{id?}");   //������ �������
			});
		}
	}
}
