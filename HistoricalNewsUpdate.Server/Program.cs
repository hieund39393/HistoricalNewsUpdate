using HistoricalNewsUpdate.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add database services
DatabaseStartup.AddDatabaseModule(builder.Services, builder.Configuration);
ServiceStartup.AddServiceModule(builder.Services);
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddRouting(options => { options.LowercaseUrls = true; });
builder.Services.AddHttpContextAccessor();
builder.Services.AddMvc(options =>
{
    options.Filters.Add(typeof(HttpGlobalExceptionFilter));
});

AuthenticationServiceStartup.AddAuthorizationService(builder.Services, builder.Configuration);

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCookiePolicy();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
