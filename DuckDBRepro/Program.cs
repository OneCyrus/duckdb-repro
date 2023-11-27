using System.Data.Common;
using DuckDB.NET.Data;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.MapGet("/", (IWebHostEnvironment env) =>
{
    var _connection = new DuckDBConnection("DataSource=:memory:");
    _connection.Open();

    var extensionPath = Path.Combine(
         env.ContentRootPath,
         $"httpfs.duckdb_extension_92_linux"
     );

    using var cmd = _connection.CreateCommand();
    cmd.CommandText =
          $"INSTALL '{extensionPath}';"
        + $"LOAD '{extensionPath}';";

    cmd.ExecuteReader();

    return "Success!";
});

app.Run();
