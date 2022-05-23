using Navyblue.Consul.Extensions.Discovery;
using Navyblue.Consul.Extensions.Discovery.ServiceRegistry;
using Navyblue.Consul.Extensions.WebApiClient;
using Navyblue.Consul.Sample2;
using Navyblue.Extensions.Configuration.Consul.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOptions();

builder.Configuration.AddConsul(builder.Services);

builder.Services.AddScoped<IConsulServiceRegistryClient, ConsulServiceRegistryClient>();
builder.Services.AddScoped<IConsulServiceRegistry, ConsulServiceRegistry>();
builder.Services.AddConsulRegisterService();

builder.Services.AddHttpApiAsServiceInvoke<ITestApiService>();

WebApplication app = builder.Build();

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
app.UseSwaggerUI();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapSwagger();
});

app.UseConsulRegisterService();

app.Run();