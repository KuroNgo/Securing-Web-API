using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using QuanLiTuyenXeBusDalat.Data;
using QuanLiTuyenXeBusDalat.Models;
using System.Text;

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
            // Define dependence
            services.AddControllers();
            services.AddDbContext<MyDBContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("MyDB"));
            });
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
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if(env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "QuanLiTuyenXeBusDalat v1"));
                
                app.UseHttpsRedirection();


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
}
