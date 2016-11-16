using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MiddleWareBussinessObjects.LDLFileBO
{
    public class TrackCiruitJoinFactory : LDLFactoryBase, IParsableFactory
    {

        public string Identifier
        {
            get { return LDLTrackCircuitJoin.Identity; }
        }

        public LDLBOBase CreateItem(string[] definition, string id)
        {
            LDLTrackCircuitJoin toPopulate = new LDLTrackCircuitJoin(id);
            int nLines = definition.GetUpperBound(0);
            
            int tcID=0;
            for (int i = 0; i <= nLines; i++)
            {
                if (definition[i].Contains(LDLTrackCircuit.Identitifier))
                    toPopulate.JoinedCircuitStrs[tcID++] = ParseItem(definition[i]);
                else if (definition[i].Contains(LDLMLocation.Identifier))
                    toPopulate.Location =  LDLMLocation.CreateFromCompleteLine(definition[i]);
            }
            return toPopulate;
        }
    }
}
