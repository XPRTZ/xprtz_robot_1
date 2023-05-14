using BrickPi3 = Iot.Device.BrickPi3.Movement;
using Iot.Device.BrickPi3.Models;
using Robot.Infrastructure.BrickPi.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Iot.Device.Mcp23xxx;

namespace Robot.Infrastructure.BrickPi.Movement
{
    public class Motor : IMotor
    {
        private readonly BrickPi3.Motor _motor;
        private Direction _direction;

        protected internal Motor(BrickPi3.Motor motor)
        {
            _motor = motor;
        }

        public void Start()
        {
            Start(Direction.Forward);
        }

        public void Start(Direction direction) 
        {
            _direction = direction;

            if (direction == Direction.Forward)
            {
                _motor.SetSpeed(25);
            }
            else
            {
                _motor.SetSpeed(-25);
            }
        }

        public void Reverse()
        {
            if (_direction == Direction.Forward)
            {
                _direction = Direction.Backward;
                _motor.SetSpeed(-25);
            }
            else
            {
                _direction = Direction.Forward;
                _motor.SetSpeed(25);
            }
        }

        public void Stop()
        {
            _motor.Stop();
        }
    }
}
