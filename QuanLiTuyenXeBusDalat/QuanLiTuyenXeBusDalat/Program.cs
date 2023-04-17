using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc;
using AspNetCoreRateLimit;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using QuanLiTuyenXeBusDalat.Models;
using QuanLiTuyenXeBusDalat.RateLimiting;
using System.Text;
using QuanLiTuyenXeBusDalat.Data;
using QuanLiTuyenXeBusDalat.DataSeeder;
using Microsoft.EntityFrameworkCore;



var builder = WebApplication.CreateBuilder(args);
{

    // Thêm các dịch vụ được yêu cầu bởi MVC Framwork
    builder.Services.AddControllersWithViews();
    IConfiguration Configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables()
    .Build();
    // Đăng ký các dịch vụ với DI Container
    builder.Services.AddDbContext<MyDBContext>(options =>
        options.UseSqlServer(
            builder.Configuration.GetConnectionString("MyDB")));
    builder.Services.AddScoped<IDataSeeder, DataSeeder>();
    builder.Services.AddCors(options => options.AddPolicy("MyCors", build =>
    {
        // Chỉ cho phép một vài trang web kết nối đến API
        build.WithOrigins("https://hienlth.info", "https://localhost:3000", "https://www.google.com");
        // Cho phép mọi trang web kết nối đến API, sử dụng phương thức cho phép chấp nhận mọi phương thức và mọi header khi sử dụng api
        //build.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
    }));
    builder.Services.AddApiVersioning(x =>
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
    #region Rate Limit
    builder.Services.AddOptions();
    builder.Services.AddMemoryCache();

    builder.Services.Configure<IpRateLimitOptions>(Configuration.GetSection("IpRateLimiting"));
    builder.Services.Configure<IpRateLimitPolicies>(Configuration.GetSection("IpRateLimitPolicies"));
    builder.Services.Configure<ClientRateLimitOptions>(Configuration.GetSection("ClientRateLimiting"));
    builder.Services.Configure<ClientRateLimitPolicies>(Configuration.GetSection("ClientRateLimitPolicies"));
    builder.Services.AddInMemoryRateLimiting();
    #endregion
    #region JWT Token
    //builder.Services.AddScoped<IDonViQuanLiXe, DonViQuanLiXeRepositoryInMemory>();
    //builder.Services.AddScoped<ITuyenRepository, TuyenRepositoryInMemory>();

    //services.AddScoped<ICategoryRepository, LoaiRepository>();
    //services.AddScoped<ICategoryRepository, CategoryRepositoryInMemory>();
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
            // Sử dụng thuật toán đối xứng ứng với cái Key, sẽ tự động mã hóa, về mặt mã hóa thì phải làm được trên bit thì phải encode lại
            IssuerSigningKey = new SymmetricSecurityKey(secretKeyBytes),

            ClockSkew = TimeSpan.Zero
        };
    });
    #endregion
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "QuanLiTuyenXeBusDaLat", Version = "v1" });
    });

    #region Rate Limit
    //services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
    builder.Services.AddSingleton<IRateLimitConfiguration, CustomRateLimitConfiguration>();
    #endregion

}

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

#region CORS
app.UseCors("MyCors");
#endregion
#region Rate Limit
//app.UseIpRateLimiting();
app.UseClientRateLimiting();
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
