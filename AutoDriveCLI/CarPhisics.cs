using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AutoDrive
{
    public class CarPhisics
    {
        public double Heading; //Radiant
        public double Speed;   //m/s
        public double PositionX;
        public double PositionY;

        double Acceleration;
        double Friction;

        public double AcceleratorCommand;
        public double BrakeCommand;
        public double SteeringCommand;

        double Mass=1000; // kg
        double AccelerationMax=2.8; // m/s^2 -> From 0 to 100 km/h in 10 s
        double BrakeMax = 14; // 5 times greater than accelation
        double MaxSpeed=56; // m/s -> 200 kmh
        double Cx;

        public CarPhisics()
        {
            Heading = 0;
            Speed = 0;
            PositionX = 0;
            PositionY = 0;
            AcceleratorCommand = 0;
            BrakeCommand = 0;
            SteeringCommand = 0;
            Acceleration = 0;
            Cx = AccelerationMax / (MaxSpeed * MaxSpeed);
            Friction = Cx * Speed * Speed;
        }

        public void UpdateStatus(double dt)
        {
            Friction = Cx * Speed * Speed;
            Acceleration = AccelerationMax * AcceleratorCommand - BrakeMax * BrakeCommand - Friction;

            Speed += Acceleration * dt;

            PositionX += Math.Cos(Heading) * Speed * dt;
            PositionY += Math.Sin(Heading) * Speed * dt;
        }

        public string toCSVString(char separator)
        {
            return (Speed.ToString() + separator + PositionX.ToString()+ separator+Acceleration.ToString()+separator+Friction.ToString());
                
        }

        public string CSVHeader(char separator)
        {
            return ("Speed" + separator + "PositionX" + separator + "Acceleration" + separator + "Friction");
        }

    }

}
