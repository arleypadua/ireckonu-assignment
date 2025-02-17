using ImportFile.Adapters;
using ImportFile.Core;
using ImportFile.Core.Inventory.Ports;
using ImportFile.SharedKernel.Messaging;
using ImportFile.SharedKernel.Messaging.Mediatr;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ImportFile.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            
            services.AddHttpClient(HttpClientFileDownloader.ClientName);

            services.AddMediatR(typeof(AssemblyMarker).Assembly);

            services.AddSingleton<IDownloadFiles, HttpClientFileDownloader>();
            services.AddSingleton<IWriteJsonIntoStreams, JsonIntoStreamWriter>();
            services.AddScoped<ISendMessages, MediatrMessageSender>();
            services.AddScoped<IInventoryItemUnitOfWork, InventoryItemMongoDbUnitOfWork>();

            services.ConfigureMongoDb(Configuration.GetConnectionString("MongoDb"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
