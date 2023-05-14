using Iot.Device.GoPiGo3.Sensors;
using Robot.Infrastructure.BrickPi;
using robot2.BackgroundTasks;
using robot2.DataStructures;
using robot2.Models;
using robot2.Programs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddBrickPi();

builder.Services.AddTransient<ButtonClickRobotProgram>();
builder.Services.AddTransient<RangeRobotProgram>();
builder.Services.AddTransient<RobotProgramResolver>(sp => key =>
{
    return key switch
    {
        nameof(ButtonClickRobotProgram) => sp.GetRequiredService<ButtonClickRobotProgram>(),
        nameof(RangeRobotProgram) => sp.GetRequiredService<RangeRobotProgram>(),
        _ => throw new KeyNotFoundException()
    };
});

builder.Services.AddSingleton<CommandQueue>(_ => CommandQueue.CreateQueue());

builder.Services.AddHostedService<RobotLoop>(sp =>
{
    var commandQueue = sp.GetRequiredService<CommandQueue>();
    
    //var program = sp.GetRequiredService<ButtonClickRobotProgram>();
    var program = sp.GetRequiredService<RangeRobotProgram>();
    
    return new RobotLoop(commandQueue, program);
});


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

app.Run();
