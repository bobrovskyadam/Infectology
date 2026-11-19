using Infectology.WebAPI.Builders;

var builder = WebApplication.CreateBuilder(args);

#if DEBUG
foreach (var c in builder.Configuration.AsEnumerable())
{
    Console.WriteLine($"CONFIG: Key: {c.Key} Value: {c.Value}");
}
#endif

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.AddAzureKeyVault();
builder.Services.AddDatabaseContexts(builder.Configuration);
builder.Services.AddFrontendCors(builder.Configuration);
builder.Services.AddJwtAuthentication(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseCors(CorsBuilder.FrontendPolicy);
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();