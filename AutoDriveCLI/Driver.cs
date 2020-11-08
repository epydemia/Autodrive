using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDrive
{
    public class Driver
    {
        public double AcceleratorCMD;
        public double BrakeCMD;
        public double DistanceToObstacle;
        public double Speed;
        double _FinalPosition;
        double _GainAccel;
        double _GainBrake;
        double GeneticVariance = 0.01;


        public Driver(double GA, double GB)
        {
            AcceleratorCMD = 0;
            BrakeCMD = 0;
            DistanceToObstacle = 0;
            Speed = 0;
            _GainAccel = GA;
            _GainBrake = GB;

        }
        public Driver()
        {
            AcceleratorCMD = 0;
            BrakeCMD = 0;
            DistanceToObstacle = 0;
            Speed = 0;
            _GainAccel = 0 ;
            _GainBrake = 0;
            _FinalPosition = 0;

        }

        public Driver(Driver Driver1, Driver Driver2)
        {
            //Random rnd = new Random();

            int RecombinationType = MathUtils.GenerateRandomNumber(2);
            switch (RecombinationType)
            {
                case 0:
                    this._GainAccel = (Driver1.GainAccel + Driver2.GainAccel) / 2;
                    break;
                case 1:
                    this._GainAccel = Driver1.GainAccel;
                    break;
                case 2:
                    this._GainAccel = Driver2.GainAccel;
                    break;
            }



            RecombinationType = MathUtils.GenerateRandomNumber(2);
            switch (RecombinationType)
            {
                case 0:
                    this._GainBrake = (Driver1.GainBrake + Driver2.GainBrake) / 2;
                    break;
                case 1:
                    this._GainBrake = Driver1.GainBrake;
                    break;
                case 2:
                    this._GainBrake = Driver2.GainBrake;
                    break;
            }

            double deltaAcc = MathUtils.RandomDouble(-GeneticVariance, GeneticVariance) * this._GainAccel;
            double deltaBrk = MathUtils.RandomDouble(-GeneticVariance, GeneticVariance) * this._GainBrake;

            this._GainAccel = this._GainAccel + deltaAcc;
            this._GainBrake = this._GainBrake + deltaBrk;

        }

        public double FinalPosition
        {
            get
            {
                return _FinalPosition;
            }
            set
            {
                _FinalPosition = value;
            }
        }

        public double GainAccel
        {
            get
            {
                return _GainAccel;
            }
            set
            {
                _GainAccel = value;
            }
            
        }

        public double GainBrake
        {
            set
            {
                _GainBrake = value;
            }
            get
            {
                return _GainBrake;
            }
           
        }


        public void Update()
        {
            double Command;
            Command = MathUtils.Bound(DistanceToObstacle * _GainAccel + 1/DistanceToObstacle*_GainBrake,-1,1);
            if (Command > 0)
            {
                AcceleratorCMD = Command;
                BrakeCMD = 0;
            }
            else
            {
                AcceleratorCMD = 0;
                BrakeCMD = Command * -1;
            }

           
        }

    }
}
