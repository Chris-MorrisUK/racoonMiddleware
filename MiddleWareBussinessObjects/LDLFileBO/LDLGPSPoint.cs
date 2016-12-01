﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MiddleWareBussinessObjects.LDLFileBO
{
    public class LDLGPSPoint
    {
        public LDLGPSPoint(double lat, double longitude,double offset)
        {
            Latitude = lat;
            Longitude = longitude;
            Offset = offset;
        }
        public double Latitude;
        public double Longitude;
        public double Offset;

        public IEnumerable<BOTripple> GetTripples(string locationForUri,BONode locatedItem)
        {
            List<BOTripple> tripples = new List<BOTripple>();

            BONode LocationNode = new BONode(new Uri(locationForUri + "Location"));
            tripples.Add(BOTripple.CreateTrippleFromValues(LocationNode, LDLUris.RDFType, LDLUris.GeodesicLocation));            
            tripples.Add(BOTripple.CreateTrippleFromValues(LocationNode, LDLUris.LatitudeProperty, Latitude));
            tripples.Add(BOTripple.CreateTrippleFromValues(LocationNode, LDLUris.LongditudeProperty, Longitude));
            tripples.Add(new BOTripple(LocationNode, new BONode( LDLUris.LocatedOnProperty), locatedItem));
            if (Offset > 0)
            {
                BONode offsetLocationNode = new BONode(new Uri(locationForUri + "OffsetLocation" + Offset.ToString()));
                tripples.Add(BOTripple.CreateTrippleFromValues(offsetLocationNode, LDLUris.RDFType,LDLUris.OffsetLocationType));
                tripples.Add(BOTripple.CreateTrippleFromValues(offsetLocationNode, LDLUris.UnitProperty, LDLUris.Metre));
                tripples.Add(new BOTripple(LocationNode, new BONode(LDLUris.OffsetLocationProperty), offsetLocationNode));
                tripples.Add(BOTripple.CreateTrippleFromValues(offsetLocationNode, LDLUris.MeasurementValueProperty, Offset));
            }
            return tripples;
        }
    }
}