using ExamenSegundoP.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

var connStr = builder.Configuration.GetConnectionString("DefaultConnection")
              ?? Environment.GetEnvironmentVariable("DATABASE_URL");


if (string.IsNullOrEmpty(connStr))
{
    throw new Exception("No se encontró la cadena de conexión. Configura DefaultConnection o DATABASE_URL.");
}

if (connStr.StartsWith("postgres://", StringComparison.OrdinalIgnoreCase) ||
    connStr.StartsWith("postgresql://", StringComparison.OrdinalIgnoreCase))
{
    connStr = connStr.Replace("postgresql://", "postgres://", StringComparison.OrdinalIgnoreCase);

    var uri = new Uri(connStr);
    var userInfo = uri.UserInfo.Split(':');
    var npgBuilder = new Npgsql.NpgsqlConnectionStringBuilder
  
    {
        Host = uri.Host,
        Port = uri.Port > 0 ? uri.Port : 5432,
        Username = userInfo[0],
        Password = userInfo.Length > 1 ? userInfo[1] : "",
        Database = uri.AbsolutePath.TrimStart('/'),
        SslMode = Npgsql.SslMode.Require,
        TrustServerCertificate = true
    };
    
    connStr = npgBuilder.ToString();
}


builder.Services.AddDbContext<CongresoDbContext>(options =>
    options.UseNpgsql(connStr));


builder.Services.AddControllers()
    .AddJsonOptions(opts => opts.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<CongresoDbContext>();
    db.Database.EnsureCreated();
}


app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
