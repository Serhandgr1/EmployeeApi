using BuisenessLayer;
using DataAccessLayer.Models;
using EmailService;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:3000",
                                              "http://www.contoso.com");
                          policy.AllowAnyMethod();
                          policy.AllowAnyHeader();
                          policy.AllowAnyOrigin();
                      });
});
// Add services to the container.

builder.Services.AddControllers();
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IBuiseness, BuisenessCodes>();
builder.Services.AddSingleton<IIsContunieMailService, MailSendBg>();
//builder.Services.AddTransient<IBackGroundService<BgMailModel>, MailSendBg>();
builder.Services.AddSingleton(typeof( IBackGroundService<BgMailModel>), typeof(MailSendBg));
builder.Services.AddHostedService<BackGroundServicesAsycn>();
var app = builder.Build();
app.UseCors(MyAllowSpecificOrigins);
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
