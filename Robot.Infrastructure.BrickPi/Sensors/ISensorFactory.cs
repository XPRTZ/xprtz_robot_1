﻿namespace Robot.Infrastructure.BrickPi.Sensors;

public interface ISensorFactory
{
    TouchSensor CreateTouchSensor(SensorPorts port);
}