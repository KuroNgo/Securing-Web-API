using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc;
using System.Threading.RateLimiting;
using Microsoft.AspNetCore.RateLimiting;
using AspNetCoreRateLimit;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using QuanLiTuyenXeBusDalat.Models;
using System.Text;
using QuanLiTuyenXeBusDalat.Data;
using QuanLiTuyenXeBusDalat.DataSeeder;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Cors;
using System.Net;

var builder = WebApplication.CreateBuilder(args);
{

    // Thêm các dịch vụ được yêu cầu bởi MVC Framwork
    // Đăng ký các dịch vụ với DI Container
    builder.Services.AddDbContext<MyDBContext>(options =>
        options.UseSqlServer(
            builder.Configuration.GetConnectionString("MyDB")));
    builder.Services.AddScoped<IDataSeeder, DataSeeder>();
    builder.Services.AddControllersWithViews();

    IConfiguration Configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables()
    .Build();

    #region cài đặt CORS
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("MyCors", build =>
        {
            // Chỉ cho phép một vài trang web kết nối đến API
            //build.WithOrigins("https://hienlth.info",
            //"https://localhost:3000", "https://www.google.com");
            // Cho phép mọi trang web kết nối đến API, sử dụng phương thức cho phép chấp
            // nhận mọi phương thức và mọi header khi sử dụng api
            build.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
        });

        options.AddPolicy("MyCors2_AllowGETForGoogle", build =>
        {
            build.WithOrigins("*")
                 .WithHeaders("Authorization", "Content-Type")
                 .WithMethods("GET");

        });
        options.AddPolicy("MyCors3_AllowGP", build =>
        {
            build.WithOrigins("https://learn.microsoft.com")
            .WithHeaders("X-Custom-Header")
            .WithMethods("GET", "POST");
        });
    });
    ;
    #endregion
    builder.Services.AddApiVersioning(x =>
    {
        //DefaultApiVersion được sử dụng để đặt phiên bản mặc định thành API
        x.DefaultApiVersion = new ApiVersion(1, 0);
        //được sử dụng để đặt phiên bản mặc định khi khách hàng chưa chỉ định bất kỳ phiên bản nào.
        //Nếu chưa đặt cờ này thành true và ứng dụng khách truy cập API mà không đề cập đến phiên bản
        //thì sẽ xảy ra ngoại lệ UnsupportedApiVersion
        x.AssumeDefaultVersionWhenUnspecified = true;
        //Để trả về phiên bản API trong tiêu đề phản hồi.
        x.ReportApiVersions = true;
        //Khi user sử dụng API, họ phải gửi phiên bản x-api vào tiêu đề với giá trị
        //phiên bản cụ thể để gọi đúng bộ điều khiển.
        x.ApiVersionReader = new HeaderApiVersionReader("x-api-version");

    });
    #region Rate Limit
    builder.Services.AddRateLimiter(options =>
    {
        // Thêm hàm xử lý tùy chỉnh khi yêu cầu bị từ chối do vượt quá giới hạn tần suất.
        options.OnRejected = async (context, token) =>
        {
            context.HttpContext.Response.StatusCode = 429;
            if (context.Lease.TryGetMetadata(MetadataName.RetryAfter, out var retryAfter))
            {
                await context.HttpContext.Response.WriteAsync(
                    $"Too many requests. Please try again after {retryAfter.TotalMinutes} minute(s). " +
                    $"Read more about our rate limits at https://example.org/docs/ratelimiting.", cancellationToken: token);
            }
            else
            {
                await context.HttpContext.Response.WriteAsync(
                    "Too many requests. Please try again later. " +
                    "Read more about our rate limits at https://example.org/docs/ratelimiting.", cancellationToken: token);
            }
        };

        // Thêm chính sách giới hạn tần suất cho yêu cầu API.
        options.AddFixedWindowLimiter("Api", options =>
        {
            options.AutoReplenishment = true;
            options.PermitLimit = 10;
            options.Window = TimeSpan.FromMinutes(1);
        });

        // Thêm chính sách giới hạn tần suất cho yêu cầu Web.
        options.AddFixedWindowLimiter("Web", options =>
        {
            options.AutoReplenishment = true;
            options.PermitLimit = 10;
            options.Window = TimeSpan.FromMinutes(1);
        });
    });

    #endregion
    #region JWT Token
    builder.Services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

    // Dùng để mã hóa JWT
    // JWT dùng secretKey để sài
    // Thuật toán mã hóa chỉ sử dụng trên bit cần phải convert về mảng byte
    var secretKey = Configuration["AppSettings:SecretKey"];
    var secretKeyBytes = Encoding.UTF8.GetBytes(secretKey);

    // AddAuthentication
    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
    {
        opt.TokenValidationParameters = new TokenValidationParameters
        {

            // Tự cấp token
            ValidateIssuer = false,
            ValidateAudience = false,

            // Ký vào token
            ValidateIssuerSigningKey = true,
            // Sử dụng thuật toán đối xứng ứng với cái Key, sẽ tự động mã hóa, về mặt mã hóa thì phải
            // làm được trên bit thì phải encode lại
            IssuerSigningKey = new SymmetricSecurityKey(secretKeyBytes),

            ClockSkew = TimeSpan.Zero
        };
    });
    #endregion
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "QuanLiTuyenXeBusDaLat", Version = "v1" });
    });

}


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
// Add rate limiting logging middleware
// Add rate limiting logging middleware
app.UseRouting();
app.UseRateLimiter();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

#region CORS
app.UseCors("MyCors");
app.UseCors("MyCors2_AllowGETForGoogle");
app.UseCors("MyCors3_AllowGP");

#endregion

app.UseRouting();

app.UseHttpsRedirection();
// Authentication phải đặt trước Authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<IDataSeeder>();
    seeder.Initialize();
}
app.Run();
