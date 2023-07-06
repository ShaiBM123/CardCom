using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CardComSite1.Data;
using CardComSite1;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<CardComSite1Context>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("CardComSite1Context") ?? 
        throw new InvalidOperationException("Connection string 'CardComSite1Context' not found.")), ServiceLifetime.Scoped);


// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseSwagger();
    //app.UseSwaggerUI();
    //app.UseDeveloperExceptionPage();
}

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseHttpsRedirection();

app.MapPersonEndpoints();

app.Run();
