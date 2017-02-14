using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MiddleWareBussinessObjects.LDLFileBO
{
    public class LDLGPSPoint
    {
        private const string PointType = "^^geo:wktLiteral";
        public LDLGPSPoint(float lat, float longitude,double offset)
        {
            Point.Latitude = lat;
            Point.Longitude = longitude;
            Offset = offset;
        }
        public LDLGeoPoint Point;
        public double Offset;

        public IEnumerable<BOTripple> GetTripples(string locationFor,BONode locatedItem)
        {
            List<BOTripple> tripples = new List<BOTripple>();
            string locationStr = LDLUris.GeodesicLocationStr  +locationFor;
            BONode LocationNode = new BONode(new Uri(locationStr));
            tripples.Add(BOTripple.CreateTrippleFromValues(LocationNode, LDLUris.RDFType, LDLUris.FeatureTypeUri));
            tripples.Add(BOTripple.CreateTrippleFromValues(LocationNode, LDLUris.LatitudeProperty, Point.Latitude));
            tripples.Add(BOTripple.CreateTrippleFromValues(LocationNode, LDLUris.LongditudeProperty, Point.Longitude));
            tripples.Add(new BOTripple(LocationNode, new BONode( LDLUris.LocatedOnProperty), locatedItem));
            string geometryStr = locationStr + "/geo";
            BONode geometryNode = new BONode(new Uri(geometryStr));            
            tripples.Add(BOTripple.CreateTrippleFromValues(geometryNode, LDLUris.RDFType, LDLUris.GeometryUri));
            tripples.Add(BOTripple.CreateTrippleFromValues(geometryNode, LDLUris.WKTPointUri, Point));
            tripples.Add(new BOTripple(LocationNode, new BONode(LDLUris.HasGeometeryPropertyUri), geometryNode));
            if (Offset > 0)
            {
                string OffsetLocationStr = LDLUris.OffsetLocationStr  + locationFor;
                BONode offsetLocationNode = new BONode(new Uri(OffsetLocationStr));
                tripples.Add(BOTripple.CreateTrippleFromValues(offsetLocationNode, LDLUris.RDFType,LDLUris.OffsetLocationType));
                tripples.Add(BOTripple.CreateTrippleFromValues(offsetLocationNode, LDLUris.UnitProperty, LDLUris.Metre));
                tripples.Add(new BOTripple(LocationNode, new BONode(LDLUris.OffsetLocationProperty), offsetLocationNode));
                tripples.Add(BOTripple.CreateTrippleFromValues(offsetLocationNode, LDLUris.MeasurementValueProperty, Offset));
            }
            return tripples;
        }
    }
}
