using Navyblue.Consul.Extensions.Discovery;
using Navyblue.Consul.Extensions.Discovery.ServiceRegistry;
using Navyblue.Consul.Sample;
using Navyblue.Extensions.Configuration.Consul.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOptions();

builder.Configuration.AddConsul(builder.Services);

builder.Services.Configure<ConsulConfigTest>(builder.Configuration.GetSection("UserInfo"));
builder.Services.AddScoped<IConsulServiceRegistryClient, ConsulServiceRegistryClient>();
builder.Services.AddScoped<IConsulServiceRegistry, ConsulServiceRegistry>();
builder.Services.AddConsulRegisterService();

var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

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