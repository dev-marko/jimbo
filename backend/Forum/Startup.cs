using Forum.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Forum.Repository.Interfaces;
using Forum.Repository.Implementations;
using Forum.Services.Interfaces;
using Forum.Services.Implementations;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace Forum
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
            services.AddMvc();
            services.AddControllers();
            services.AddDbContext<ForumContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

            // Repos
            services.AddScoped<ISubforumRepository, SubforumRepository>();
            services.AddScoped<ITopicRepository, TopicRepository>();
            services.AddScoped<IPostRepository, PostRepository>();

            // Services
            services.AddTransient<ISubforumService, SubforumService>();
            services.AddTransient<ITopicService, TopicService>();
            services.AddTransient<IPostService, PostService>();


            // Https Clients
            services.AddHttpClient<IUserService, UserService>(c =>
            {
                c.BaseAddress = new Uri("https://localhost:44305/api/user/");
            });

            // JSON config
            services.AddControllers()
                .AddNewtonsoftJson(options => 
                options.SerializerSettings.ReferenceLoopHandling = 
                Newtonsoft.Json.ReferenceLoopHandling.Ignore);


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            if (!env.IsDevelopment())
            {
                app.UseHttpsRedirection();
            }

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
