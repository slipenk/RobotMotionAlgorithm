using System;
using System.Collections.Generic;
using Robot.Common;


namespace YuriiSlipenkyi.RobotChallenge
{
    
    public class YuriiSlipenkyiAlgorithm : IRobotAlgorithm
    {

        public string Author => "Yurii Slipenkyi";
        static public List<Position>  listStationPosition = new List<Position>();
        static  ListPurposeStation listPurposeStation;
        public int Count_of_robots = 10;
        public static bool temp = true;

        public RobotCommand DoStep(IList<Robot.Common.Robot> robots, int robotToMoveIndex, Map map)
        {
            if (temp)
            {
                Help_func.FindAllStationOnMap(map);
                listPurposeStation = new ListPurposeStation(listStationPosition);
                temp = false;
            }
            
            listPurposeStation.FreePurpose();
            
            Robot.Common.Robot MoveRobot = robots[robotToMoveIndex];
            listPurposeStation.DeletePurpose(Help_func.FindAllMyStationOnMap(MoveRobot, robots));

            Position nFreeStation = FreeStation.FindNearestFreeStation(MoveRobot, map, robots);
            Position nAlienStation = Help_func.AlienStation(MoveRobot, robots);
            Position nAccessibleStation = AccessibleStation(MoveRobot, robots);
            Position movingRobotPosition = MoveRobot.Position;


            if (Count_of_robots < 100)
            {
                if (DistanceHelper.FindDistance(nAccessibleStation, movingRobotPosition) <= 200 && nAccessibleStation != null)
                {
                    if (nFreeStation != null)
                    {
                        if (Math.Sqrt(DistanceHelper.FindDistance(nFreeStation, movingRobotPosition)) <= 7 && (MoveRobot.Energy >= 300))
                        {
                            ++Count_of_robots;
                            listPurposeStation.SetPurpose(new PurposeStation(nAccessibleStation, true));
                            return new CreateNewRobotCommand() { NewRobotEnergy = 100 };
                        }

                        else if (Math.Sqrt(DistanceHelper.FindDistance(nFreeStation, movingRobotPosition)) < 12 && MoveRobot.Energy > 350)
                        {
                            ++Count_of_robots;
                            return new CreateNewRobotCommand() { NewRobotEnergy = 150 };
                        }
                    }

                    if (Math.Sqrt(DistanceHelper.FindDistance(nAlienStation, movingRobotPosition)) <= 7 && MoveRobot.Energy >= 400)
                    {
                        ++Count_of_robots;
                        listPurposeStation.SetPurpose(new PurposeStation(nAccessibleStation, true));
                        return new CreateNewRobotCommand() { NewRobotEnergy = 200 };
                    }
                    else if (Math.Sqrt(DistanceHelper.FindDistance(nAlienStation, movingRobotPosition)) < 12 && MoveRobot.Energy > 450)
                    {
                        ++Count_of_robots;
                        return new CreateNewRobotCommand() { NewRobotEnergy = 250 };
                    }

                }

                else if (DistanceHelper.FindDistance(nAccessibleStation, movingRobotPosition) <= 625 && nAccessibleStation != null)
                {
                    if (nFreeStation != null)
                    {
                        if (Math.Sqrt(DistanceHelper.FindDistance(nFreeStation, movingRobotPosition)) < 30 && MoveRobot.Energy > 700)
                        {
                            ++Count_of_robots;
                            return new CreateNewRobotCommand() { NewRobotEnergy = 350 };
                        }
                    }
                    else if (Math.Sqrt(DistanceHelper.FindDistance(nAlienStation, movingRobotPosition)) < 20 && MoveRobot.Energy > 700)
                    {
                        ++Count_of_robots;
                        return new CreateNewRobotCommand() { NewRobotEnergy = 450 };
                    }
                }

            }
            for (int i = 0; i < listStationPosition.Count; i++)
            {
                if (movingRobotPosition == listStationPosition[i] )
                {
                    if (!listPurposeStation.IsMyStation(listStationPosition[i]))
                    {
                        listPurposeStation.listMyStation.Add(new PurposeStation(listStationPosition[i], true));
                        return new CollectEnergyCommand();
                    }
                    else
                    {
                        return new CollectEnergyCommand();
                    }
                }
            }



            if (nFreeStation != null)
            {
                if (Math.Sqrt(DistanceHelper.FindDistance(nFreeStation, movingRobotPosition)) <= 13 && MoveRobot.Energy - (DistanceHelper.FindDistance(nFreeStation, movingRobotPosition)) > 0)
                {
                    listPurposeStation.SetPurpose(new PurposeStation(nFreeStation, true));
                    return new MoveCommand() { NewPosition = nFreeStation };
                }
            }

            if (Math.Sqrt(DistanceHelper.FindDistance(nAccessibleStation, movingRobotPosition)) <= 13 && MoveRobot.Energy - (DistanceHelper.FindDistance(nAccessibleStation, movingRobotPosition) + 50) > 0)
            {
                listPurposeStation.SetPurpose(new PurposeStation(nAccessibleStation, true));
                return new MoveCommand() { NewPosition = nAccessibleStation };
            }
            if (Math.Sqrt(DistanceHelper.FindDistance(nAccessibleStation, movingRobotPosition)) < 25 && MoveRobot.Energy >= 290)
            {
                return new MoveCommand() { NewPosition = Help_func.NextStep(movingRobotPosition, nAccessibleStation, (MoveRobot.Energy / DistanceHelper.FindDistance(nAccessibleStation, movingRobotPosition)) > 1 ? MoveRobot.Energy / (int)DistanceHelper.FindDistance(nAccessibleStation, movingRobotPosition) + 4 : 1) };
            }
            if (Math.Sqrt(DistanceHelper.FindDistance(nAccessibleStation, movingRobotPosition)) < 35 && MoveRobot.Energy >= 200)
            {
                return new MoveCommand() { NewPosition = Help_func.NextStep(movingRobotPosition, nAccessibleStation, (MoveRobot.Energy / DistanceHelper.FindDistance(nAccessibleStation, movingRobotPosition)) > 1 ? MoveRobot.Energy / (int)DistanceHelper.FindDistance(nAccessibleStation, movingRobotPosition) + 1 : 1) };
            }
            else
                return new MoveCommand() { NewPosition = Help_func.NextStep(movingRobotPosition, nAccessibleStation, 1) };



        }


        public static Position AccessibleStation(Robot.Common.Robot movingRobot, IList<Robot.Common.Robot> robots)
        {
            Position stationPosition = null;
            for (int i = 0; i < listPurposeStation.listPurpose.Count; i++)
            {
                if (FreeStation.IsCellFreeFromMyRobot(listPurposeStation.listPurpose[i].station, movingRobot, robots) && listPurposeStation.listPurpose[i].Busy == false)
                {
                    if (stationPosition != null)
                    {
                        if (DistanceHelper.FindDistance(movingRobot.Position, listPurposeStation.listPurpose[i].station) < DistanceHelper.FindDistance(movingRobot.Position, stationPosition))
                            stationPosition = listPurposeStation.listPurpose[i].station;
                    }
                    else
                        stationPosition = listPurposeStation.listPurpose[i].station;
                }
            }
            return stationPosition;
        }





    }
}