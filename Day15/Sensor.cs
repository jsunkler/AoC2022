using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day15
{
    record struct Sensor
    {
        public (int x, int y) SensorPosition { get; private set; }

        public (int x, int y) BeaconPosition { get; private set; }

        public int CoveredDistance { get; private set; }

        public bool CoversPoint((int x, int y) point)
        {
            if (point == SensorPosition || point == BeaconPosition) return true;
            int d = Math.Abs(SensorPosition.x - point.x) + Math.Abs(SensorPosition.y - point.y);
            return d <= CoveredDistance;
        }

        public Rectangle ToRectangle()
        {
            return new Rectangle(SensorPosition.x - CoveredDistance, SensorPosition.y - CoveredDistance, 2 * CoveredDistance + 1, 2 * CoveredDistance + 1);
        }

        public Sensor(
            (int, int) sensor,
            (int, int) beacon
            )
        {
            SensorPosition = sensor;
            BeaconPosition = beacon;
            CoveredDistance = Math.Abs(SensorPosition.x - BeaconPosition.x) + Math.Abs(SensorPosition.y - BeaconPosition.y);
        }

    }
}
