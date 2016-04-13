using MiddleWareBussinessObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VDS.RDF;

namespace StardogConnection
{
    public static class ExentionMethods
    {

        public static INode ParamToInode(this MiddlewareParameter param)
        {
            NodeFactory fact = new NodeFactory();
            MiddlewareParameter<string> strParam = param as MiddlewareParameter<string>;
            if (strParam != null)
                return fact.CreateLiteralNode(strParam.ParamValue);
            MiddlewareParameter<byte[]> byteParam = param as MiddlewareParameter<byte[]>;
            if (byteParam != null)
                return fact.CreateLiteralNode(MiddlewareConstants.EncodingInUse.GetString(byteParam.ParamValue));
            MiddlewareParameter<Uri> uriParam = param as MiddlewareParameter<Uri>;
            if (uriParam != null)
                return fact.CreateUriNode(uriParam.ParamValue);

            throw new ArgumentOutOfRangeException("Param is in unsupported format");

        }
    }
}
