using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VueCliMiddleware;
using SignalRBoard.Hubs;

namespace Alpha.WebUi
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
      services.AddSignalR();
      services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

      // In production, the React files will be served from this directory
      services.AddSpaStaticFiles(configuration =>
      {
        configuration.RootPath = "ClientApp/dist";
      });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseExceptionHandler("/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
        app.UseHttpsRedirection();
      }

      app.UseStaticFiles();
      app.UseSpaStaticFiles();

      app.UseMvc(routes =>
      {
        routes.MapRoute(
                  name: "default",
                  template: "{controller}/{action=Index}/{id?}");
      });

      app.UseWebSockets();
      app.UseSignalR(routes =>
      {
        routes.MapHub<BoardHub>("/boardHub");
      });

      app.UseWhen(
        context => !context.Request.Path.StartsWithSegments("/boardHub"),
        appBuilder =>
        {
          appBuilder.UseSpa(spa =>
          {
            spa.Options.SourcePath = "ClientApp";

            if (env.IsDevelopment())
            {
              // run npm process with client app
              spa.UseVueCli(npmScript: "serve", port: 8080, regex: "Compiled ");
            }
          });
        });
    }
  }
}