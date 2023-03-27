using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using QuanLiTuyenXeBusDalat.Data;
using QuanLiTuyenXeBusDalat.Models;
using QuanLiTuyenXeBusDalat.Services;
using System.Text;
using Microsoft.AspNetCore.Mvc;
// 2 thư viện này phục vụ cho chức năng Rate Limit
using System.Threading.RateLimiting;
using Microsoft.AspNetCore.RateLimiting;
using AutoWrapper;
using Serilog;
using System.Reflection.PortableExecutable;
using QuanLiTuyenXeBusDalat.Extension;
using AspNetCoreRateLimit;

namespace QuanLiTuyenXeBusDalat
{
    public class Startup
    {

        // Khởi tạo hàm constructor
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
           services.AddOptions();
            services.AddMemoryCache();
            services.Configure<IpRateLimitOptions>(Configuration.GetSection("IpRateLimiting"));
            services.Configure<IpRateLimitPolicies>(Configuration.GetSection("IpRateLimitPolicies"));
            services.AddInMemoryRateLimiting();
            // Define dependence
            services.AddControllers();
            #region CORS
            // TODO: CORS
            // Bật cors
            services.AddCors(options => options.AddPolicy("MyCors", build =>
            {
                // Chỉ cho phép một vài trang web kết nối đến API
                build.WithOrigins("https://hienlth.info", "https://localhost:3000");
                // Cho phép mọi trang web kết nối đến API
                build.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
            }));

            #endregion
            #region  Khởi tạo versioning
            // Khởi tạo versioning
            services.AddApiVersioning(x =>
            {
                //DefaultApiVersion được sử dụng để đặt phiên bản mặc định thành API
                x.DefaultApiVersion = new ApiVersion(1, 0);
                //được sử dụng để đặt phiên bản mặc định khi khách hàng chưa chỉ định bất kỳ phiên bản nào.
                //Nếu chưa đặt cờ này thành true và ứng dụng khách truy cập API mà không đề cập đến phiên bản thì sẽ xảy ra ngoại lệ UnsupportedApiVersion
                x.AssumeDefaultVersionWhenUnspecified = true;
                //Để trả về phiên bản API trong tiêu đề phản hồi.
                x.ReportApiVersions = true;
                //Khi user sử dụng API, họ phải gửi phiên bản x-api vào tiêu đề với giá trị phiên bản cụ thể để gọi đúng bộ điều khiển.
                x.ApiVersionReader = new HeaderApiVersionReader("x-api-version");

            });
            #endregion
            #region Rate Limit
            services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();

            //services.AddRateLimiter(_ => _.AddFixedWindowLimiter(policyName: "fixed", options =>
            //{
            //    options.PermitLimit = 2;
            //    options.Window = TimeSpan.FromSeconds(12);
            //    options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
            //    options.QueueLimit = 2;
            //}));
            #endregion
            services.AddDbContext<MyDBContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("MyDB"));
            });
            services.AddScoped<IDonViQuanLiXe, DonViQuanLiXeRepositoryInMemory>();    
            services.AddScoped<ITuyenRepository, TuyenRepositoryInMemory>();

            //services.AddScoped<ICategoryRepository, LoaiRepository>();
            //services.AddScoped<ICategoryRepository, CategoryRepositoryInMemory>();
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            // Dùng để mã hóa JWT
            // JWT dùng secretKey để sài
            // Thuật toán mã hóa chỉ sử dụng trên bit cần phải convert về mảng byte
            var secretKey = Configuration["AppSettings:SecretKey"];
            var secretKeyBytes = Encoding.UTF8.GetBytes(secretKey);

            // AddAuthentication
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters
                {

                    // Tự cấp token
                    ValidateIssuer = false,
                    ValidateAudience = false,

                    // Ký vào token
                    ValidateIssuerSigningKey = true,
                    // Sử dụng thuật toán đối xứng ứng với cái Key, sẽ tự động mã hóa, về mặt mã hóa thì phải làm được trên bit thì phải encode lại
                    IssuerSigningKey = new SymmetricSecurityKey(secretKeyBytes),

                    ClockSkew = TimeSpan.Zero
                };
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "QuanLiTuyenXeBusDaLat", Version = "v1" });
            });
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
        }
        internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
        {
            public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseIpRateLimiting();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "QuanLiTuyenXeBusDalat v1"));
            }

            #region CORS
            app.UseCors("MyCors");
            #endregion
          
            app.UseHttpsRedirection();

           
            #region Rate Limit
            //app.UseRateLimiter();
            
            #endregion
            app.UseRouting();

            // Authentication phải đặt trước Authorization
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
