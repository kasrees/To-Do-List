using Microsoft.EntityFrameworkCore;
using To_Do_List_Backend.Infrastructure;
using To_Do_List_Backend.Infrastructure.Repositories;
using To_Do_List_Backend.UnitOfWork;

var builder = WebApplication.CreateBuilder( args );

builder.Services.AddCors( options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.WithOrigins( "http://localhost:4200" )
                                .AllowAnyHeader()
                                .AllowAnyMethod();
        } );
} );


builder.Services.AddDbContext<TodoDbContext>( x =>
{
    x.UseSqlServer( builder.Configuration.GetConnectionString( "DefaultConnection" ) );
} );
builder.Services.AddControllers();
builder.Services.AddSwaggerGen( c =>
{
    c.SwaggerDoc( "v1", new() { Title = "TodoApi", Version = "v1" } );
} );

builder.Services.AddScoped<ITodoRepository, TodoRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


var app = builder.Build();
app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI( options =>
{
    options.SwaggerEndpoint( "/swagger/v1/swagger.json", "v1" );
    options.RoutePrefix = string.Empty;
} );
app.UseCors( builder =>
{
    builder
    .WithOrigins( "localhost:4200" )
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
} );
app.Run();