using Robot.Infrastructure.BrickPi.Movement;

namespace robot2.Models;

public interface IRobotProgram
{
    void ConfigureProgram();
    void AddCommand(Command command);
    void AddCondition(Condition condition);
    List<Command> GetCommands { get; }
    List<Condition> GetConditions { get; }
    List<Motor> GetMotors { get; }
}
