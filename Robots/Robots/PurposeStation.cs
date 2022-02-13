
using System.Collections.Generic;
using Robot.Common;

namespace YuriiSlipenkyi.RobotChallenge
{
    class PurposeStation
    {
        public Position station;
        public bool Busy;

        public PurposeStation(Position station, bool Busy)
        {
            this.station = station;
            this.Busy = Busy;
        }
    }
     class ListPurposeStation
    {
        public List<PurposeStation> listPurpose = new List<PurposeStation>();
        public List<PurposeStation> listMyStation = new List<PurposeStation>();
        List<Position> temp = new List<Position>();

        public ListPurposeStation() { }

        public ListPurposeStation(List<Position> listPosition)
        {
            for (int i = 0; i < listPosition.Count; i++)
                listPurpose.Add(new PurposeStation(listPosition[i], false));
        }

        public bool SetPurpose(PurposeStation purpose)
        {
            for (int i = 0; i < listPurpose.Count; i++)
            {
                if (listPurpose[i].station == purpose.station && listPurpose[i].Busy == false)
                {
                    listPurpose[i].Busy = true;
                    return true;
                }
            }
            return false;
        }



        public void ChangePurpose(Position position, bool busy)
        {
            for (int i = 0; i < listPurpose.Count; i++)
            {
                if (listPurpose[i].station == position)
                    listPurpose[i].Busy = busy;
            }
        }

        public void FreePurpose()
        {
            for (int i = 0; i < listPurpose.Count; i++)
            {
                ChangePurpose(listPurpose[i].station, false);
            }

            for (int i = 0; i < listPurpose.Count; i++)
            {
                for (int j = 0; j < listMyStation.Count; j++)
                {
                    if (listPurpose[i].station == listMyStation[j].station)
                    {
                        ChangePurpose(listPurpose[i].station, true);
                        break;
                    }
                }
            }
        }


        public void DeletePurpose(List<Position> list)
        {
            bool tmp = true;
            for (int i = 0; i < listMyStation.Count; i++)
            {
                for (int j = 0; j < list.Count; j++)
                {
                    if (listMyStation[i].station == list[j])
                    {
                        tmp = false;
                        break;
                    }
                }
                if (tmp)
                {
                    temp.Add(listMyStation[i].station);
                    listMyStation.Remove(new PurposeStation(listMyStation[i].station, true));
                }
                tmp = true;
            }

            
            if (temp.Count != 0)
            {
                for (int i = 0; i < temp.Count; i++)
                {
                    for (int j = 0; j < listPurpose.Count; j++)
                    {
                        if (temp[i] == listPurpose[j].station)
                        {
                            ChangePurpose(listPurpose[j].station, false);
                        }
                    }
                }
            }
             temp.Clear();
        }


        

        public  bool IsMyStation(Position position)
        {
            for (int i = 0; i < listMyStation.Count; i++)
            {
                if (listMyStation[i].station == position)
                    return true;
            }
            return false;
        }

    }
}
