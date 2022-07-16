using BL;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Netico
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

            services.AddControllers()
            .AddJsonOptions(opts => opts.JsonSerializerOptions.PropertyNamingPolicy = null);


            services.DependencyInjectionBL();

            // Validation Token
            var key = Encoding.ASCII.GetBytes(Configuration["Jwt:Secret"]);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    // đặt hoặc nhận các xác thực nếu ký bảo mật được gọi
                    ValidateIssuerSigningKey = true,
                    // khóa chung được sử dụng để xác thực mã thông báo JWT đến
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    // Yêu cầu xác nhận từ nhà phát hành
                    ValidateIssuer = false,
                    // Nhận hoặc đặt boolean để kiểm soát nếu đối tượng sẽ được xác thực trong quá trình xác thực mã thông báo.
                    ValidateAudience = false,
                    // Nhận mã nhà phát hành
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    // Nhận hoặc đặt một chuỗi đại diện cho đối tượng hợp lệ sẽ được sử dụng để kiểm tra đối tượng của mã thông báo
                    ValidAudience = Configuration["Jwt:Audience"],
                };
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseCors(x => x.SetIsOriginAllowed(origin => true).AllowCredentials().AllowAnyMethod().AllowAnyHeader());

            app.UseRouting();

            app.UseSetRequestHeaderMiddleWare();

            app.UseAuthentication();
            app.UseAuthorization();

           

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
