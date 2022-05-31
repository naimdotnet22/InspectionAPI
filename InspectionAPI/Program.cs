using InspectionAPI.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string conn = builder.Configuration.GetConnectionString("conn");
builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(conn));

//Enable CORS
builder.Services.AddCors(p => p.AddPolicy("corpolicy", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("corpolicy");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
