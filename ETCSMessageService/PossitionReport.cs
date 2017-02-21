using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ETCSMessageService
{
    public class CompletePossitionReport
    {

        public PositionReport Basic;
        public GPSFix GPSDetails;
        public bool? FrontCabNotBack=null;
        public uint NID_Engine;
        public StirStatusEnum Stir_Status;
        public StirMessage Source;
        public ushort TSAP;
    }
}
