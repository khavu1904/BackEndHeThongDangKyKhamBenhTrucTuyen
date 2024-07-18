using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebDangKyKhamChuaBenh.Data;
using WebDangKyKhamChuaBenh.Repositories;
using WebDangKyKhamChuaBenh.Helpers;
using AutoMapper;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost3000",
        builder => builder.WithOrigins("http://localhost:3000")
                          .AllowAnyHeader()
                          .AllowAnyMethod());
});

// Configure DbContext
builder.Services.AddDbContext<MyAppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register repositories and AutoMapper
builder.Services.AddScoped<INhomQuyenRepository, NhomQuyenRepository>();
builder.Services.AddScoped<IBenhNhanRepository, BenhNhanRepository>();
builder.Services.AddScoped<IChuyenKhoaRepository, ChuyenKhoaRepository>();
builder.Services.AddScoped<INhanVienRepository, NhanVienRepository>();
builder.Services.AddScoped<IJWTRepository, JWTRepository>();
builder.Services.AddScoped<IKhamBenhRepository, KhamBenhRepository>();
builder.Services.AddScoped<IBacSiRepository, BacSiRepository>();
builder.Services.AddScoped<ILichLamViecRepository, LichLamViecRepository>();
builder.Services.AddScoped<IEmailRepository, EmailRepository>();
builder.Services.AddScoped<IPhongKhamRepository, PhongKhamRepository>();
builder.Services.AddScoped<ILichKhamBacSiRepository, LichKhamBacSiRepository>();
builder.Services.AddAutoMapper(cfg => cfg.AddProfile<ApplicationMapper>());

// Configure JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidAudience = builder.Configuration["JWT:ValidAudience"],
            ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowLocalhost3000");
app.UseAuthentication(); // Ensure authentication middleware is added
app.UseAuthorization();

app.MapControllers();

app.Run();
