using System;
using System.Collections.Generic;
using Robot.Common;

namespace YuriiSlipenkyi.RobotChallenge
{
   public class FreeStation
    {

        public static Position FindNearestFreeStation(Robot.Common.Robot movingRobot, Map map,
IList<Robot.Common.Robot> robots)
        {
            EnergyStation nearest = null;
            int minDistance = int.MaxValue;
            foreach (var station in map.Stations)
            {
                if (IsStationFree(station, movingRobot, robots))
                {
                    int d = DistanceHelper.FindDistance(station.Position, movingRobot.Position);
                    if (d < minDistance)
                    {
                        minDistance = d;
                        nearest = station;
                    }
                }
            }
            return nearest == null ? null : nearest.Position;
        }


        public static bool IsStationFree(EnergyStation station, Robot.Common.Robot movingRobot,
IList<Robot.Common.Robot> robots)
        {
            return IsCellFree(station.Position, movingRobot, robots);
        }

        public static bool IsCellFree(Position cell, Robot.Common.Robot movingRobot, IList<Robot.Common.Robot> robots)
        {
            foreach (var robot in robots)
            {
                if (robot != movingRobot)
                {
                    if (robot.Position == cell)
                        return false;
                }
            }
            return true;
        }

        public static bool IsCellFreeFromMyRobot(Position cell, Robot.Common.Robot movingRobot, IList<Robot.Common.Robot> robots)
        {
            foreach (var robot in robots)
            {
                if (robot != movingRobot)
                {
                    if (robot.Position == cell && (String.Compare(robot.OwnerName, movingRobot.OwnerName) == 0))
                        return false;
                }
            }
            return true;
        }

    }
}
