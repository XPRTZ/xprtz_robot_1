using Robot.Infrastructure.BrickPi.Movement;
using Robot.Infrastructure.BrickPi.Sensors;
using robot2.Models;
using robot2.Models.Enums;

namespace robot2.Programs
{
    public class RangeRobotProgram : Models.RobotProgram
    {
        public RangeRobotProgram(IMotorFactory motorFactory, ISensorFactory sensorFactory) : base(motorFactory, sensorFactory)
        {
        }

        public override void ConfigureProgram()
        {
            var sensor = _sensorFactory.CreateUSRangeSensor(SensorPorts.S4);

            var motor = _motorFactory.CreateMotor(MotorPorts.MA);
            _motors.Add(motor);

            var startCommand = Command.Create("motor1start", motor, (motor) => motor.Start(Direction.Forward));
            var stopCommand = Command.Create("motor1stop", motor, (motor) => motor.Stop());
            
            AddCondition(new Condition(
                "CloseRange", 
                ConditionType.ContinuousEvaluation,
                sensor,
                (sensor) => ((USRangeSensor)sensor).GetDistance() < 20,
                startCommand
            ));

            AddCondition(new Condition(
                "FarRange",
                ConditionType.ContinuousEvaluation,
                sensor,
                (sensor) => ((USRangeSensor)sensor).GetDistance() >= 20,
                stopCommand
            ));
        }
    }
}
