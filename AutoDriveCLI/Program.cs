using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AutoDrive
{
    class Program
    {
        static void Main(string[] args)
        {
            int childCount = 100;
            double GainAccel = 0.015;
            double GainBrake = -200;
            double ObstaclePosition = 1000;
            int[] BestDriverNumber = new int[2];

            Random rnd = new Random();
            Driver Root = new Driver(GainAccel, GainBrake);

            Driver[] Driver = new Driver[childCount];

            Driver[] BestDriver = new Driver[2];
            BestDriver[0] = new Driver(GainAccel, GainBrake);
            BestDriver[1] = new Driver(GainAccel, GainBrake);

            for (int gen = 0; gen < 100; gen++)
            {
                if (gen < 30)
                    ObstaclePosition = 1000;
                else if (gen < 60)
                    ObstaclePosition = 750;
                else
                    ObstaclePosition = 10000;


                Console.WriteLine("Generation {0} - Distance: {1}", gen, ObstaclePosition);
                for (int i = 0; i < childCount; i++)
                {
                    Driver[i] = new Driver(BestDriver[0], BestDriver[1]);
                }

                BestDriver[0] = new Driver();
                BestDriver[1] = new Driver();


                for (int i = 0; i < childCount; i++)
                {

                    Console.Write("{2} GA={0} GB={1} ", Driver[i].GainAccel, Driver[i].GainBrake, i);
                    Driver[i].FinalPosition = Test2(Driver[i], ObstaclePosition);
                    Console.WriteLine(Driver[i].FinalPosition);
                    if (ObstaclePosition - Driver[i].FinalPosition > 0)
                    {
                        if (Driver[i].FinalPosition > BestDriver[0].FinalPosition)
                        {
                            BestDriver[0].GainAccel = Driver[i].GainAccel;
                            BestDriver[0].GainBrake = Driver[i].GainBrake;
                            BestDriver[0].FinalPosition = Driver[i].FinalPosition;
                            BestDriverNumber[0] = i;
                        }
                        else if (Driver[i].FinalPosition > BestDriver[1].FinalPosition)
                        {
                            BestDriver[1].GainAccel = Driver[i].GainAccel;
                            BestDriver[1].GainBrake = Driver[i].GainBrake;
                            BestDriver[1].FinalPosition = Driver[i].FinalPosition;
                            BestDriverNumber[1] = i;
                        }
                    }
                }

                Console.WriteLine("Best Driver 1: {0} {1} {2} {3}", BestDriverNumber[0], BestDriver[0].GainAccel, BestDriver[0].GainBrake, BestDriver[0].FinalPosition);
                Console.WriteLine("Best Driver 2: {0} {1} {2} {3}", BestDriverNumber[1], BestDriver[1].GainAccel, BestDriver[1].GainBrake, BestDriver[1].FinalPosition);
                Console.ReadLine();
            }
        }

        static double  Test2(Driver Driver, double ObstaclePosition)
        {
            CarPhisics Car1 = new CarPhisics();
            
            
            double t = 0;
            double dt = 0.01;

            do
            {
                t += dt;
                Driver.DistanceToObstacle = ObstaclePosition - Car1.PositionX;
                Driver.Update();
                Car1.AcceleratorCommand = Driver.AcceleratorCMD;
                Car1.BrakeCommand = Driver.BrakeCMD;
                Car1.UpdateStatus(dt);
            } while (Car1.Speed > 0);
            //Console.Write("Time: {0}\tPosition: {1}",t,Car1.PositionX);
            return Car1.PositionX;

        }

        static void Test1()
        {
            CarPhisics Car1 = new CarPhisics();
            Driver Driver = new Driver();

            StreamWriter sw = new StreamWriter("output.csv");
            sw.WriteLine("Time" + '\t' + Car1.CSVHeader('\t'));
            double t = 0;
            double dt = 0.1;
            double AccelCMD = 0;
            double brakeCMD = 0;
            double MaxSpeed = 0;
            sw.WriteLine(t.ToString() + '\t' + Car1.toCSVString('\t'));
            while (t < 10)
            {
                if (t < 7)
                {
                    AccelCMD = 1;
                    brakeCMD = 0;
                }
                else
                {
                    AccelCMD = 0;
                    brakeCMD = 1;
                }
                Car1.AcceleratorCommand = MathUtils.Bound(AccelCMD, 0, 1);
                Car1.UpdateStatus(dt);

                MaxSpeed = Car1.Speed > MaxSpeed ? Car1.Speed : MaxSpeed;

                t += dt;
                sw.WriteLine(t.ToString() + '\t' + Car1.toCSVString('\t'));

            }
            Console.WriteLine((MaxSpeed * 3.6).ToString() + "kmh");
            //Console.ReadLine();
            sw.Close();
        }
    }
}
