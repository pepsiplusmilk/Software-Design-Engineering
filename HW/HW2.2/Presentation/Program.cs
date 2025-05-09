using System.Text.Json.Serialization;
using Application.AnimalServices;
using Application.EnclosureServices;
using Application.Event;
using Application.FeedingService;
using Application.ReportSaving;
using Application.ZooStatistics;
using Domain.Enclosure;
using Infrastructure.Animal;
using Infrastructure.Enclosure;
using Infrastructure.Feeding;
using Infrastructure.FeedingTable;
using Infrastructure.Statistics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().
  AddJsonOptions(options => {
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
  });
builder.Services.AddSwaggerGen();

// Repositories
builder.Services.AddSingleton<IAnimalRepository, AnimalRepository>();
builder.Services.AddSingleton<IEnclosureRepository, EnclosureRepository>();
builder.Services.AddSingleton<IFeedingRepository, FeedingRepository>();
builder.Services.AddSingleton<IReportRepository, ReportRepository>();

// Business logic
builder.Services.AddSingleton<IDomainEventService, DomainEventService>();
builder.Services.AddScoped<IAnimalRegisterService, AnimalRegisterService>();
builder.Services.AddScoped<IEnclosureRegisterService, EnclosureRegisterService>();
builder.Services.AddScoped<IAnimalHealthMonitoringService, AnimalHealthMonitoringService>();
builder.Services.AddScoped<IAnimalFeedingService, AnimalFeedingService>();
builder.Services.AddScoped<IReportSaverService, ReportSaverService>();

builder.Services.AddSingleton<ZooReporterService>();
builder.Services.AddSingleton<IZooReporterService>(srv => srv.GetRequiredService<ZooReporterService>());

//Hosted services
builder.Services.AddHostedService<ZooReporterInitService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) {
  app.UseExceptionHandler("/Home/Error");
  // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
  app.UseHsts();
}

app.UseSwagger();
app.UseSwaggerUI(middleware => 
  {middleware.SwaggerEndpoint("/swagger/v1/swagger.json", "Moscow Zoo api");});

app.UseHttpsRedirection();

app.UseRouting();
//app.UseEndpoints(endpoints => endpoints.MapControllers());

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
  .WithStaticAssets();


app.Run();