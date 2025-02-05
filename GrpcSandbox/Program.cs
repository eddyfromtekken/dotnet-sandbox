using Grpc.Reflection;
using GrpcSandbox.Services;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseKestrel();
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenLocalhost(5000, listenOptions =>
    {
        listenOptions.Protocols = Microsoft.AspNetCore.Server.Kestrel.Core.HttpProtocols.Http2;
    });
});
builder.Services.AddGrpc();
builder.Services.AddGrpcReflection();

var app = builder.Build();
app.MapGrpcService<GreeterService>();
app.MapGrpcReflectionService().AllowAnonymous();
app.MapGet("/", () => "gRPC Service Running");

app.Run();