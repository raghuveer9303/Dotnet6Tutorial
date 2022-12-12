using System.Security.Cryptography;
using System.Linq;
using TutorialFirst.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<Middleware>();
var app = builder.Build();

/*app.Run(async (HttpContext context) => {
    context.Response.Headers["Content-Type"]= "application/html";
    if (context.Request.Method == "GET") {
        context.Response.StatusCode = 200;
        context.Response.Headers["accept"] = "application/json";
    }
    if (context.Request.Query.ContainsKey("id")) { 
        var id = context.Request.Query["id"];
        await context.Response.WriteAsync($"<h1>{id}</h1>");
    }

});
*/

app.Use(async (HttpContext context, RequestDelegate next) =>{
    await context.Response.WriteAsync("<h1>Hello</h1>");
    await next(context);
});

app.UseMyMiddleware();

app.UseHelloMiddleware();

app.Use(async (HttpContext context, RequestDelegate next) => {
    await context.Response.WriteAsync("<h1>Hello Hello</h1>");
    await next(context);
});

app.Run(async (HttpContext context) => {
    await context.Response.WriteAsync("<h1>World</h1>");
});



//app.MapGet("/", () => "Hello World!");

app.Run();