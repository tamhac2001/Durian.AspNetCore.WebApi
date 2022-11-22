using TamHac.AspNetCore.DurianApi.Application.Durian;
using TamHac.AspNetCore.DurianApi.configuration;
using TamHac.AspNetCore.DurianApi.Infrastructure.durian;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(options =>
{
    options.InputFormatters.Insert(0, JsonPatchInputFormatter.GetJsonPatchInputFormatter());
});
builder.Services.AddProblemDetails();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IDurianRepository, DurianRepository>();
builder.Services.AddSingleton<IDurianService, DurianService>();

// settings
builder.Services.Configure<DurianDatabaseConfiguration>(builder.Configuration.GetSection("DurianDatabase"));

var app = builder.Build();


app.UseExceptionHandler();
app.UseStatusCodePages();

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