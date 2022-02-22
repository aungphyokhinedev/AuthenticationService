using MassTransit;
using DataService;
using TokenService;
using AuthenticationService;
using AuthenticationService.DataAccess;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IDataAccess,ServiceDataAccess>();
builder.Services.AddScoped<IAuthenticate,ServiceAuthenticate>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMassTransit(x =>
            {
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(builder.Configuration["RabbitMq:host"], "/", h =>
                    {
                        h.Username(builder.Configuration["RabbitMq:user"]);
                        h.Password(builder.Configuration["RabbitMq:password"]);
                    });
                    cfg.ConfigureEndpoints(context);
                });

                //data service
                x.AddRequestClient<DataServiceContract>();        
                //token service
                x.AddRequestClient<TokenServiceContract>();


            }).AddMassTransitHostedService();


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();


app.MapControllers();

app.Run();
