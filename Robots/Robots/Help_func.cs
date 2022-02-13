using System.Collections.Generic;
using Robot.Common;


namespace YuriiSlipenkyi.RobotChallenge
{
    public class Help_func
    {

        public static void FindAllStationOnMap(Map map)
        {
            for (int i = 0; i < map.Stations.Count; i++)
            {
                YuriiSlipenkyiAlgorithm.listStationPosition.Add(map.Stations[i].Position);
            }
        }


        public static Position NextStep(Position robotPosition, Position nextPosition, int step)
        {
            Position x = new Position(nextPosition.X, nextPosition.Y);
            if (robotPosition.X != nextPosition.X)
            {
                if (robotPosition.X > nextPosition.X && robotPosition.X - nextPosition.X >= step)
                    x.X = robotPosition.X - step;
                if (robotPosition.X < nextPosition.X && nextPosition.X - robotPosition.X >= step)
                    x.X = robotPosition.X + step;
            }

            if (robotPosition.Y != nextPosition.Y)
            {
                if (robotPosition.Y > nextPosition.Y && robotPosition.Y - nextPosition.Y >= step)
                    x.Y = robotPosition.Y - step;
                if (robotPosition.Y < nextPosition.Y && nextPosition.Y - robotPosition.Y >= step)
                    x.Y = robotPosition.Y + step;
            }
            return x;
        }

       

        public static Position AlienStation(Robot.Common.Robot movingRobot, IList<Robot.Common.Robot> robots)
        {
            Position stationPosition = null;
            for (int i = 0; i < YuriiSlipenkyiAlgorithm.listStationPosition.Count; i++)
            {
                if (FreeStation.IsCellFreeFromMyRobot(YuriiSlipenkyiAlgorithm.listStationPosition[i], movingRobot, robots))
                {
                    if (stationPosition != null)
                    {
                        if (DistanceHelper.FindDistance(movingRobot.Position, YuriiSlipenkyiAlgorithm.listStationPosition[i]) < DistanceHelper.FindDistance(movingRobot.Position, stationPosition))
                            stationPosition = YuriiSlipenkyiAlgorithm.listStationPosition[i];
                    }
                    else
                        stationPosition = YuriiSlipenkyiAlgorithm.listStationPosition[i];
                }
            }
            return stationPosition;
        }
        
       

        public static List<Position> FindAllMyStationOnMap(Robot.Common.Robot movingRobot, IList<Robot.Common.Robot> robots)
        {
            List<Position> temp = new List<Position>();

            for (int i = 0; i < YuriiSlipenkyiAlgorithm.listStationPosition.Count; i++)
            {
                for (int j = 0; j < robots.Count; j++)
                {
                    if (YuriiSlipenkyiAlgorithm.listStationPosition[i] == robots[j].Position && robots[j].OwnerName == movingRobot.OwnerName)
                    {
                        temp.Add(YuriiSlipenkyiAlgorithm.listStationPosition[i]);
                    }
                }
            }

            return temp;
        }

    }

}
