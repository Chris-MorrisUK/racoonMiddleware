using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ETCSMessageService
{
    public class PositionReport
    {
        public byte Scale;
        public UInt32 NID_LRBG;
        public UInt16 BaliseGroupID;
        public UInt16 BaliseCountryID;
        public UInt16 DistanceFromBallise;
        public byte DirectionFromBallise;
        public byte DirectionOfTrain;
        public UInt16 DoubtOver;
        public UInt16 DoubtUnder;
        public byte TrainIntegrity;//Q_Length
        public UInt16 SafeTrainLength;
        public byte TrainSpeed;
        public byte TrainDirectionRelativeToBallise;
        public byte Mode;
        public byte Level;


    }
}
