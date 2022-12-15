using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day15
{
    internal class Sensor
    {
        public (int x, int y) SensorPosition { get; set; }

        public (int x, int y) BeaconPosition { get; set; }

        public int CoveredDistance
        {
            get
            {
                return Math.Abs(SensorPosition.x - BeaconPosition.x) + Math.Abs(SensorPosition.y - BeaconPosition.y);
            }
        }

        public bool CoversPoint((int x, int y) point)
        {
            int d = Math.Abs(SensorPosition.x - point.x) + Math.Abs(SensorPosition.y - point.y);
            return d <= CoveredDistance;
        }

        public HashSet<(int x, int y)> CoveredPoints
        {
            get
            {
                HashSet<(int x, int y)> temp = new()
                {
                    (SensorPosition.x, SensorPosition.y),
                    (BeaconPosition.x, BeaconPosition.y)
                };

                for (int x = SensorPosition.x - CoveredDistance; x <= SensorPosition.x + CoveredDistance; x++)
                {
                    for (int y = SensorPosition.y - CoveredDistance; y <= SensorPosition.y + CoveredDistance; y++)
                    {
                        if (CoversPoint((x, y))) temp.Add((x, y));
                    }
                }

                return temp;
            }
        }

        public Sensor(
            (int, int) sensor,
            (int, int) beacon
            )
        {
            SensorPosition = sensor;
            BeaconPosition = beacon;
        }

    }
}
