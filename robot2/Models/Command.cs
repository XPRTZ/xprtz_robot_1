namespace robot2.Models
{
    public class Command
    {
        private string _name;
        private readonly MotorWithFunc[] _motorWithFunc;

        private Command(string name, MotorWithFunc[] motorWithFunc)
        {
            _name = name;
            _motorWithFunc = motorWithFunc;
        }

        public static Command Create(string name, MotorWithFunc[] motorWithFunc)
        {
            return new Command(name, motorWithFunc);
        }

        public string Name => _name;

        public void Execute()
        {
            _motorWithFunc.ToList().ForEach(motor => motor.CommandAction.Invoke(motor.Motor));
        }
    }
}
