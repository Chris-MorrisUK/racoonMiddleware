using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using VDS.RDF;
using VDS.RDF.Storage;
using VDS.RDF.Writing.Formatting;

namespace StardogConnection
{
    public sealed class StardogServer
    {
        static object connectorLock = new object();
        static StardogConnector connector = null;

        public static StardogConnector GetConnector(StardogServerDetails details)
        {
            if (connector == null)
                lock (connectorLock)
                    if (connector == null)
                    {
                        
                            connector = new StardogConnector(details.URI, details.KnowledgeBase, details.UserName, details.Password);
                        
                    }
            return connector;
        }

        private static void addPrefixes(StringBuilder querryBuilder, IGraph hasNamespaces)
        {
            foreach (string prefix in hasNamespaces.NamespaceMap.Prefixes)
                querryBuilder.AppendFormat("prefix {0}: <{1}>\n", prefix, hasNamespaces.NamespaceMap.GetNamespaceUri(prefix));
        }

    }
}