using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Robot.Common;
using YuriiSlipenkyi.RobotChallenge;

namespace UnitTestYuriiSlipenkyi
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestIsCellFreeFromMyRobot()
        {
            Map map = new Map();
            EnergyStation station1 = new EnergyStation() { Energy = 1567, Position = new Position(1, 2), RecoveryRate = 5 };
            EnergyStation station2 = new EnergyStation() { Energy = 1023, Position = new Position(3, 5), RecoveryRate = 2 };
            EnergyStation station3 = new EnergyStation() { Energy = 1237, Position = new Position(10, 2), RecoveryRate = 3 };

            map.Stations.Add(station1);
            map.Stations.Add(station2);
            map.Stations.Add(station3);

            var robots = new List<Robot.Common.Robot>() { new Robot.Common.Robot { Energy = 100, Position = new Position(1, 1) } };
        
            Assert.IsTrue(FreeStation.IsCellFreeFromMyRobot(new Position(1, 2), robots[0], robots));
        }

        [TestMethod]
        public void TestNewRobot()
        {
            Map map = new Map();
            EnergyStation station1 = new EnergyStation() { Energy = 1567, Position = new Position(1, 2), RecoveryRate = 5 };
            EnergyStation station2 = new EnergyStation() { Energy = 1023, Position = new Position(3, 5), RecoveryRate = 2 };
            EnergyStation station3 = new EnergyStation() { Energy = 1237, Position = new Position(10, 2), RecoveryRate = 3 };

            map.Stations.Add(station1);
            map.Stations.Add(station2);
            map.Stations.Add(station3);

            var robots = new List<Robot.Common.Robot>() { new Robot.Common.Robot { Energy = 600, Position = new Position(1, 1) } };
            var alg = new YuriiSlipenkyiAlgorithm();
            var c = alg.DoStep(robots, 0, map);
            
            Assert.IsTrue(c is CreateNewRobotCommand);
        }

        [TestMethod]
        public void TestFindFreeStation()
        {
            Map map = new Map();
            EnergyStation station1 = new EnergyStation() { Energy = 1567, Position = new Position(1, 2), RecoveryRate = 5 };
            EnergyStation station2 = new EnergyStation() { Energy = 1023, Position = new Position(3, 5), RecoveryRate = 2 };
            EnergyStation station3 = new EnergyStation() { Energy = 1237, Position = new Position(10, 2), RecoveryRate = 3 };

            map.Stations.Add(station1);
            map.Stations.Add(station2);
            map.Stations.Add(station3);

            var robots = new List<Robot.Common.Robot>() { new Robot.Common.Robot { Energy = 600, Position = new Position(1, 1) } };
            robots.Add(new Robot.Common.Robot { Energy = 600, Position = new Position(1, 2) });

            

            Assert.AreEqual(FreeStation.FindNearestFreeStation(robots[0], map, robots), station2.Position);
        }

        [TestMethod]
        public void TestIsStationFree()
        {
            Map map = new Map();
            EnergyStation station1 = new EnergyStation() { Energy = 1567, Position = new Position(1, 2), RecoveryRate = 5 };
            EnergyStation station2 = new EnergyStation() { Energy = 1023, Position = new Position(3, 5), RecoveryRate = 2 };
            EnergyStation station3 = new EnergyStation() { Energy = 1237, Position = new Position(10, 2), RecoveryRate = 3 };

            map.Stations.Add(station1);
            map.Stations.Add(station2);
            map.Stations.Add(station3);

            var robots = new List<Robot.Common.Robot>() { new Robot.Common.Robot { Energy = 600, Position = new Position(1, 1) } };
            robots.Add(new Robot.Common.Robot { Energy = 600, Position = new Position(1, 2) });

            


            Assert.AreEqual(FreeStation.IsStationFree(station1, robots[0], robots), false);
        }

        [TestMethod]
        public void TestCollectEnergyCommand()
        {
            Map map = new Map();
            EnergyStation station1 = new EnergyStation() { Energy = 1567, Position = new Position(1, 2), RecoveryRate = 5 };
            EnergyStation station2 = new EnergyStation() { Energy = 1023, Position = new Position(3, 5), RecoveryRate = 2 };
            EnergyStation station3 = new EnergyStation() { Energy = 1237, Position = new Position(10, 2), RecoveryRate = 3 };

            map.Stations.Add(station1);
            map.Stations.Add(station2);
            map.Stations.Add(station3);

            var robots = new List<Robot.Common.Robot>() { new Robot.Common.Robot { Energy = 200, Position = new Position(1, 2) } };
            robots.Add(new Robot.Common.Robot { Energy = 600, Position = new Position(1, 2) });

            var alg = new YuriiSlipenkyiAlgorithm();
         

            var c = alg.DoStep(robots, 0, map);


            Assert.IsTrue(c is CollectEnergyCommand);
        }

        [TestMethod]
        public void TestMove()
        {
            Map map = new Map();
            EnergyStation station1 = new EnergyStation() { Energy = 1567, Position = new Position(1, 2), RecoveryRate = 5 };
            EnergyStation station2 = new EnergyStation() { Energy = 1023, Position = new Position(3, 5), RecoveryRate = 2 };
            EnergyStation station3 = new EnergyStation() { Energy = 1237, Position = new Position(10, 2), RecoveryRate = 3 };

            map.Stations.Add(station1);
            map.Stations.Add(station2);
            map.Stations.Add(station3);

            var robots = new List<Robot.Common.Robot>() { new Robot.Common.Robot { Energy = 200, Position = new Position(3, 4) } };
            robots.Add(new Robot.Common.Robot { Energy = 600, Position = new Position(1, 2) });

            var alg = new YuriiSlipenkyiAlgorithm();


            var c = alg.DoStep(robots, 0, map);


            Assert.IsTrue(c is MoveCommand);
        }

        [TestMethod]
        public void TestAlienStation()
        {
            Map map = new Map();
            EnergyStation station1 = new EnergyStation() { Energy = 1567, Position = new Position(1, 2), RecoveryRate = 5 };
            EnergyStation station2 = new EnergyStation() { Energy = 1023, Position = new Position(3, 5), RecoveryRate = 2 };
            EnergyStation station3 = new EnergyStation() { Energy = 1237, Position = new Position(10, 2), RecoveryRate = 3 };

            map.Stations.Add(station1);
            map.Stations.Add(station2);
            map.Stations.Add(station3);

            var robots = new List<Robot.Common.Robot>() { new Robot.Common.Robot { Energy = 200, Position = new Position(1, 1) } };
            robots.Add(new Robot.Common.Robot { Energy = 600, Position = new Position(1, 2) });

            robots[1].OwnerName = "Salah";

            var alg = new YuriiSlipenkyiAlgorithm();
            YuriiSlipenkyiAlgorithm.listStationPosition.Add(station1.Position);
            YuriiSlipenkyiAlgorithm.listStationPosition.Add(station2.Position);
            YuriiSlipenkyiAlgorithm.listStationPosition.Add(station3.Position);
           


            Assert.AreEqual(Help_func.AlienStation(robots[0], robots), station1.Position);
        }

        [TestMethod]
        public void TestNewPosition()
        {
            Map map = new Map();
            EnergyStation station1 = new EnergyStation() { Energy = 1567, Position = new Position(1, 2), RecoveryRate = 5 };
            EnergyStation station2 = new EnergyStation() { Energy = 1023, Position = new Position(3, 5), RecoveryRate = 2 };
            EnergyStation station3 = new EnergyStation() { Energy = 1237, Position = new Position(10, 2), RecoveryRate = 3 };

            map.Stations.Add(station1);
            map.Stations.Add(station2);
            map.Stations.Add(station3);

            var robots = new List<Robot.Common.Robot>() { new Robot.Common.Robot { Energy = 200, Position = new Position(1, 1) } };
            robots.Add(new Robot.Common.Robot { Energy = 600, Position = new Position(1, 9) });

           

            var alg = new YuriiSlipenkyiAlgorithm();
            var c = alg.DoStep(robots, 0, map) as MoveCommand;


            Assert.AreEqual(map.Stations[0].Position, c.NewPosition);
        }

        [TestMethod]
        public void TestNextPosition()
        {
            Position p1 = new Position(2, 4);
            Position p2 = new Position(2, 5);
            var alg = new YuriiSlipenkyiAlgorithm();
           
            p2 = Help_func.NextStep(p2, p1, 1);


            Assert.AreEqual(p1, p2);
        }


        [TestMethod]
        public void TestDistance()
        {
            Position p1 = new Position(2, 4);
            Position p2 = new Position(8, 4);
            Assert.AreEqual(DistanceHelper.FindDistance(p1, p2), 36);
        }

    }
}
