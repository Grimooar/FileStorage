using FileStorage.API.Extentions;
using FileStorage.Core.Extentions;
using FileStorage.Domain.Models;
using FileStorage.Infrastructure.Context;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var authOptions = builder.Configuration.GetSection("AuthOptions").Get<AuthOptions>();
var connectionString = builder.Configuration.GetConnectionString("SqlConnection");
builder.Services.AddDbContext<FileDbContext>(options =>
    options.UseSqlite("Data Source=FileDb.db"));
/*builder.Services.AddDbContext<IdentityDbContext>(options =>
    options.UseSqlite("Data Source=IdentityDb.db"));*/

builder.Services.AddSingleton(authOptions);
builder.Services.AddControllers();
builder.Services.AddServices();
builder.Services.AddFilesRepositories();

/*builder.Services.AddIdentity<User, IdentityRole<int>>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<IdentityDbContext>();*/
builder.Services.AddScoped<UserManager<User>>();
builder.Services.AddMapper();
    builder.Services.Configure<FormOptions>(options =>
    {
        options.ValueLengthLimit = int.MaxValue; 
        options.MultipartBodyLengthLimit = int.MaxValue; 
    });

builder.Services.AddSwagger(authOptions);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
FileDbInitialize.Initialize(app.Services.GetRequiredService<IServiceProvider>().CreateScope().ServiceProvider);
/*IdentityDbInitialize.Initialize(app.Services.GetRequiredService<IServiceProvider>().CreateScope().ServiceProvider);*/
// Configure the HTTP request pipeline.
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