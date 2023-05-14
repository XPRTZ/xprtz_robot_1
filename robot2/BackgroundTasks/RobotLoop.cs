using robot2.DataStructures;
using robot2.Models;
using robot2.Programs;

namespace robot2.BackgroundTasks;

public class RobotLoop : BackgroundService
{
    private readonly IRobotProgram _program;
    private readonly CommandQueue _commandQueue;

    public RobotLoop(CommandQueue commandQueue, IRobotProgram program)
    {
        _program = program;
        _commandQueue = commandQueue;
    }

    public override Task StartAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("RobotLoop started");
        return base.StartAsync(cancellationToken);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Console.WriteLine("RobotLoop executing");
        
        _commandQueue.LoadProgram(_program);

        while (!stoppingToken.IsCancellationRequested)
        {
            if (_commandQueue.IsLoading)
            {
                continue;
            }
            
            // execute next command
            _commandQueue.RunNextCommand();

            await Task.Delay(10, stoppingToken);

            // process conditions
            _commandQueue.ProcessConditions();

            await Task.Delay(10, stoppingToken);
        }
    }

    public override Task StopAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("RobotLoop stopped");
        _commandQueue.StopProgram();
        return base.StopAsync(cancellationToken);
    }
}
