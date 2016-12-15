using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ETCSMessageService
{
    public enum StirMessage: byte
    {
         NID_TrainPosUpdate = 136,
         NID_NewSTiR_AOC  = 171,
         NID_TrainStatus = 172,
         NID_RevokeSTiR = 173
    }
}
