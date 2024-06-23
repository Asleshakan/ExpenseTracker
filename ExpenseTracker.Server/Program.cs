using System.Text.Json.Serialization;
using DinkToPdf;
using DinkToPdf.Contracts;
using ExpenseTracker.Server.AppDbContext;
using ExpenseTracker.Server.Entities;
using ExpenseTracker.Server.Services;
// using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// var context = new CustomAssemblyLoadContext();
// context.LoadUnmanagedLibrary(Path.Combine(Directory.GetCurrentDirectory(), "libwkhtmltox.dll"));

builder.Services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));

builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddRazorPages();  // Combine razor pages and api

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorClient",
        builder =>
        {
            builder.WithOrigins("https://gentle-dune-0ac2e510f.5.azurestaticapps.net")
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});


// 1. Add Authentication Services
// builder.Services.AddAuthentication(options =>
// {
//     options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//     options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
// }).AddJwtBearer(options =>
// {
//     options.Authority = "https://dev-y66xxk5t72zap751.au.auth0.com/";
//     options.Audience = "https://localhost:5002/";
// });


// For entity Framework
builder.Services.AddDbContext<ExpenseTrackerDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// For DI registration
builder.Services.AddTransient<IExpenseTrackerService, ExpenseTrackerService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();

    // app.UseSwagger();
    // app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles(); // to laod wasm static files

app.UseBlazorFrameworkFiles(); //  a special middleware component to serve the client 

app.UseCors("AllowBlazorClient");
app.UseAuthorization();


app.MapRazorPages(); // Combine razor pages and api

app.MapControllers();// handle /api

app.MapFallbackToFile("index.html");  // handle  everything else

app.Run();
