using Robot.Infrastructure.BrickPi.Movement;

namespace robot2.Models
{
    public class MotorWithFunc
    {
        public IMotor Motor = null!;
        public Action<IMotor> CommandAction = null!;
    }
}
