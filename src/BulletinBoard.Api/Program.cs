using BulletinBoard.Api;
using BulletinBoard.Core;
using BulletinBoard.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddCore()
    .AddInfrastructure(builder.Configuration)
    .AddCustomControllers()
    .AddJwtAuthentication(builder.Configuration)
    .AddSwaggerWithXml();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
