using Robot.Infrastructure.BrickPi.Movement;
using Robot.Infrastructure.BrickPi.Sensors;
using robot2.Models;
using robot2.Models.Enums;

namespace robot2.Programs
{
    public class ButtonClickRobotProgram : Models.RobotProgram, IButtonClickRobotProgram
    {
        public ButtonClickRobotProgram(IMotorFactory motorFactory, ISensorFactory sensorFactory) : base(motorFactory, sensorFactory)
        {
        }

        public override void ConfigureProgram()
        {
            var button = _sensorFactory.CreateTouchSensor(SensorPorts.S1);

            var motor = _motorFactory.CreateMotor(MotorPorts.MA);
            _motors.Add(motor);

            //var startCommand = Command.Create("motor1start", motor, (motor) => motor.Start(Direction.Forward));
            //var stopCommand = Command.Create("motor1stop", motor, (motor) => motor.Stop());
            
            //AddCondition(new Condition(
            //    "ButtonPressed", 
            //    ConditionType.ContinuousEvaluation,
            //    button,
            //    (button) => ((TouchSensor)button).IsPressed(),
            //    startCommand
            //));

            //AddCondition(new Condition(
            //    "ButtonReleased",
            //    ConditionType.ContinuousEvaluation,
            //    button,
            //    (button) => !((TouchSensor)button).IsPressed(),
            //    stopCommand
            //));
        }
    }
}
