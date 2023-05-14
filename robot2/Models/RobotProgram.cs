using Robot.Infrastructure.BrickPi.Movement;
using Robot.Infrastructure.BrickPi.Sensors;

namespace robot2.Models
{
    public abstract class RobotProgram : IRobotProgram
    {
        private List<Command> _startingCommands;
        private List<Condition> _conditions;

        protected IMotorFactory _motorFactory;
        protected ISensorFactory _sensorFactory;

        protected List<Motor> _motors;

        protected RobotProgram(IMotorFactory motorFactory, ISensorFactory sensorFactory)
        {
            _motorFactory = motorFactory;
            _sensorFactory = sensorFactory;
            _startingCommands = new List<Command>();
            _conditions = new List<Condition>();
            _motors = new List<Motor>();

            ConfigureProgram();
        }

        public void AddCommand(Command command)
        {
            _startingCommands.Add(command);
        }

        public void AddCondition(Condition condition)
        {
            _conditions.Add(condition);
        }

        public abstract void ConfigureProgram();

        public List<Command> GetCommands => _startingCommands;

        public List<Condition> GetConditions => _conditions;

        public List<Motor> GetMotors => _motors;
    }
}
