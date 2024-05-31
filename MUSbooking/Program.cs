using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MUSbooking.Context;
using MUSbooking.Interface;
using MUSbooking.Middleware;
using MUSbooking.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IEquipmentServices, EquipmentServices>();
builder.Services.AddScoped<IOrderServices, OrderServices>();
builder.Services.AddDbContext<PrimaryDataBaseContext>(options =>
    options.UseSqlite(("Data Source=orders.db")));

builder.Services.AddControllers()
    .AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "MusBookingApi", Version = "v1" });
});

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "OrderEquipmentAPI v1"));
app.UseRouting();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
app.UseHttpsRedirection();
app.Run();

