using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ETCSMessageService
{
    public enum StirStatusEnum :byte
    {
        SwitchingOver=0,
        Connected=1,
        Registering=2,
        Supervised=3,
        SwitchingBack=4,
        Disconnected=5,
        Unknown=6,
        Unknown = 7,
        Unknown=8
    }
}
