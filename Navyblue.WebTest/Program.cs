using Microsoft.OpenApi.Models;
using Navyblue.Consul.Extensions.Discovery;
using Navyblue.Extensions.Configuration.Consul.Extensions;
using Navyblue.WebTest;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("V1", new OpenApiInfo
    {
        Title = "Swagger½Ó¿ÚÎÄµµ",
        Version = "v1",
        Description = $"WebApi HTTP API V1",
    });
    c.OrderActionsBy(o => o.RelativePath);
});

builder.Configuration.AddConsul(builder.Services);
builder.Services.Configure<ConsulConfigTest>(builder.Configuration.GetSection("UserInfo"));

var app = builder.Build();

app.UseConsulRegisterService(builder.Services);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapSwagger();
});

app.Run();
