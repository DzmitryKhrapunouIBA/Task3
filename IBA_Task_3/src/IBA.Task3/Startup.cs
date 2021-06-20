using IBA.Task3.DAL.Servises;
using IBA.Task3.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace IBA.Task3
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
            string connection = Configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<DAL.Context.AppContext>(options => options.UseSqlServer(connection));

            services.AddControllers();
            services.AddMvc();

            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ITestService, TestService>();
            services.AddTransient<IQuestionService, QuestionService>();
            services.AddTransient<IAnswerService, AnswerService>();
            services.AddTransient<ITestAssignmentService, TestAssignmentService>();
            services.AddTransient<IUserAnswerService, UserAnswerService>();
            services.AddTransient<ITestResultService, TestResultService>();
            services.AddTransient<IJwtGenerator, JwtGenerator>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                   .AddJwtBearer(options =>
                   {
                       options.RequireHttpsMetadata = Configuration.GetValue<bool>("Authentication:RequireHttpsMetadata");
                       options.TokenValidationParameters = new TokenValidationParameters
                       {
                           ValidateIssuer = Configuration.GetValue<bool>("Authentication:TokenValidationParameters:ValidateIssuer"),
                           ValidIssuer = Configuration.GetValue<string>("Authentication:TokenValidationParameters:ValidIssuer"),
                           ValidateAudience = Configuration.GetValue<bool>("Authentication:TokenValidationParameters:ValidateAudience"),
                           ValidAudience = Configuration.GetValue<string>("Authentication:TokenValidationParameters:ValidAudience"),
                           ValidateLifetime = Configuration.GetValue<bool>("Authentication:TokenValidationParameters:ValidateLifetime"),
                           IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(
                                   Configuration.GetValue<string>("Authentication:TokenValidationParameters:SecretKey"))),
                           ValidateIssuerSigningKey = Configuration.GetValue<bool>("Authentication:TokenValidationParameters:ValidateIssuerSigningKey"),
                       };
                       options.SaveToken = Configuration.GetValue<bool>("Authentication:SaveToken");
                   });
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseDeveloperExceptionPage();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseHttpsRedirection();
            app.UseRouting();
           
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
