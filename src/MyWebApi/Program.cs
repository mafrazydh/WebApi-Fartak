using Application;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using MyWebApi.Infrastructure;
using MyWebApi.Infrastructure.Middlewares;
using Scalar.AspNetCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers(options =>
{

    options.InputFormatters.Add(new TextSingleValueFormatter());

})
    .AddJsonOptions(options => {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;

        
    })
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    })
    
    .ConfigureApiBehaviorOptions(options => 
    {
        options.SuppressModelStateInvalidFilter = true;        
    });
builder.Services.AddApiVersioning(ver => {
    ver.DefaultApiVersion =new ApiVersion(1,0);
    ver.AssumeDefaultVersionWhenUnspecified = true;
    ver.ReportApiVersions = true;
    ver.ApiVersionReader = ApiVersionReader.Combine(
        new QueryStringApiVersionReader("api-version"),
        new HeaderApiVersionReader("x-version"),
        new MediaTypeApiVersionReader("ver")
        );
});


builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();


var app = builder.Build();




if (!app.Environment.IsDevelopment())
{
    app.UseMiddleware<ExceptionMiddleware>();
}
else
{
    app.UseMiddleware<ExceptionMiddleware>();
}
app.MapScalarApiReference();


app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
});

app.UseHttpsRedirection();
app.UseStaticFiles();


app.MapControllers();


app.UseAuthorization();

app.Run();

