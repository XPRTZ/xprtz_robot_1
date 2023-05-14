using Robot.Infrastructure.BrickPi.Movement;
using Robot.Infrastructure.BrickPi.Sensors;
using robot2.Models;
using robot2.Models.Enums;

namespace robot2.Programs
{
    public class RangeRobotProgram : Models.RobotProgram, IRangeRobotProgram
    {
        public RangeRobotProgram(IMotorFactory motorFactory, ISensorFactory sensorFactory) : base(motorFactory, sensorFactory)
        {
        }

        public override void ConfigureProgram()
        {
            var sensor = _sensorFactory.CreateUSRangeSensor(SensorPorts.S4);

            var motorRight = _motorFactory.CreateMotor(MotorPorts.MA);
            var motorLeft = _motorFactory.CreateMotor(MotorPorts.MB);

            _motors.Add(motorRight);
            _motors.Add(motorLeft);

            var turnCommand = Command.Create("turnCommand", new MotorWithFunc[]
            {
                new MotorWithFunc { Motor = motorLeft, CommandAction = (motor) => motor.Start(Direction.Forward) },
                new MotorWithFunc { Motor = motorRight, CommandAction = (motor) => motor.Start(Direction.Backward) }
            });
            var forwardCommand = Command.Create("forwardCommand", new MotorWithFunc[]
            {
                new MotorWithFunc { Motor = motorLeft, CommandAction = (motor) => motor.Start(Direction.Forward) },
                new MotorWithFunc { Motor = motorRight, CommandAction = (motor) => motor.Start(Direction.Forward) }
            });

            AddCondition(new Condition(
                "CloseRange",
                ConditionType.ContinuousEvaluation,
                sensor,
                (sensor) => ((USRangeSensor)sensor).GetDistance() < 20,
                turnCommand
            ));

            AddCondition(new Condition(
                "FarRange",
                ConditionType.ContinuousEvaluation,
                sensor,
                (sensor) => ((USRangeSensor)sensor).GetDistance() >= 20,
                forwardCommand
            ));
        }
    }
}
