using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using MySql.EntityFrameworkCore.Extensions;
using QuizAPiService.Entities;
using QuizAPiService.Service.Abstract;
using QuizAPiService.Service;
using QuizAPiService.Middleware;
using Microsoft.Extensions.Logging;
using System.Web.Http.Filters;


public class Startup
{
    public IConfiguration Configuration { get; set; }

   
public Startup(IConfiguration configuration)
    {
        var builder = new ConfigurationBuilder()
            .AddJsonFile("appSettings.json");

        Configuration = builder.Build();

    }
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddRazorPages();
        services.AddSingleton<IDesignTimeServices, MysqlEntityFrameworkDesignTimeServices>();
        services.AddTransient<IQuestionDetails, QuestionDetailRepository>();
        services.AddTransient<ILevelInterfaceService, LevelService>();
        services.AddTransient<IQuizdetailService, QuizdetailService>();
        services.AddTransient<ICategoryQuestionService, CategoryQuestionService>();
        services.AddTransient<IRoleDetailsInterfaceService, RoleDetailsService>();
        services.AddTransient<IUserRoleInterface, UserRoleService>();
        services.AddTransient<IUserDetail, UserDetailRepository>();


        services.AddControllers(options =>
        {
            options.Filters.Add<CustomExceptionFilter>();
            options.Filters.Add(new ModelValidationActionFilterAttribute());
        });


        services.AddTransient<CustomExceptionFilter>();
        services.AddSwaggerGen();

    }
    public void Configure(WebApplication app, IWebHostEnvironment env)
    {
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "QuizApiservice"));

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthorization();
        app.MapRazorPages();
        app.UseMiddleware<ExceptionHandlingMiddleware>();
        app.MapControllerRoute(
           name: "default",
           pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();

    }
}
public class MysqlEntityFrameworkDesignTimeServices : IDesignTimeServices
{
    public void ConfigureDesignTimeServices(IServiceCollection serviceCollection)
    {
        serviceCollection.AddEntityFrameworkMySQL();
        new EntityFrameworkRelationalDesignServicesBuilder(serviceCollection)
            .TryAddCoreServices();
    }
}