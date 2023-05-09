/***********************************************************************************************************\
 *  Demo Application : SignalRWebApi   
 *  Description:     : DotNet Core 6 WebApi with Included SignalR
 * 
 * Created by   Marius Celliers
 * Date         2023-05-09
 * 
 * Nuget Package Add : Microsoft.AspNet.SignalR (ver2.4.3)
 * 
 \***********************************************************************************************************/

using SignalRWebApi.Hubs;
using Microsoft.AspNetCore.ResponseCompression;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddResponseCompression(opts =>
   {
       opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
            new[] { "Application/octet-stream" });
   });
builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapHub<NotificationsHub>("/Notifications");
app.Run();
