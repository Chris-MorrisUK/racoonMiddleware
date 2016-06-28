using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiddleWareBussinessObjects;
using VDS.RDF;
using VDS.RDF.Storage;
using VDS.RDF.Writing.Formatting;
using VDS.RDF.Query;
using VDS.RDF.Parsing;

namespace StardogConnection
{
    public class StardogQuery : IQuerry
    {
        public StardogQuery()
        { 
        }

        public StardogQuery(string SPARQL):this()
        {
            sparql = SPARQL;
        }
        string sparql = string.Empty;
        string server = string.Empty;
        string datastore = string.Empty;
        #region IQuerry Members

        public void SetTarget(string _server,string _datastore)
        {
            server = _server;
            datastore = _datastore;
        }

        public void SetQuerry(string queryText)
        {
            sparql = queryText;
        }


        /// <summary>
        /// This executes the saved SPARQL against the passed server and with the passed parameters
        /// </summary>
        /// <param name="parameters">Parameters to pass on to the SPARQL query. Can be any of the types that extend MiddlewareParameter</param>
        /// <param name="session">This holds server details, including username and password</param>
        /// <returns></returns>
        public IEnumerable<MiddlewareParameter> Execute(IEnumerable<MiddlewareParameter> parameters, Session session,ParameterTypeEnum returnTypeWanted)
        {
            StardogConnector theConnector = getConnector(session);
            SparqlParameterizedString query = getQuery(parameters, sparql);
            IEnumerable<SparqlResult> queryResult = theConnector.Query(query.ToString()) as SparqlResultSet;//actually fire the query
            if (queryResult.Count<SparqlResult>() == 0)
                return Enumerable.Empty<MiddlewareParameter>();//Don't do unnecessary processing, but returning null causes crashes further up
            List<MiddlewareParameter> Result = new List<MiddlewareParameter>();
			bool linkParams = returnTypeWanted.HasFlag(ParameterTypeEnum.Multivalue);
			//I stay null if not doing multivalue requests
			MiddlewareParameter<List<MiddlewareParameter>> multiValue=null;
            int lineNumber = 1;
            foreach (SparqlResult res in queryResult)//for each line
            {
				if (linkParams)
				{
					multiValue = new MiddlewareParameter<List<MiddlewareParameter>>(MiddlewareParameterDirection.Out);
                    multiValue.ParamName = lineNumber++.ToString();
					multiValue.ParamValue = new List<MiddlewareParameter>();
					returnTypeWanted &= ~ParameterTypeEnum.Multivalue;//The effect of this is allowing
				}
                foreach (KeyValuePair<string, INode> parameterValue in res)//each parameter
                {
                    if (linkParams)
                        handleHandleTags( multiValue, parameterValue);
                    MiddlewareParameter toAdd = null;
                    switch (returnTypeWanted)
                    {
                        case ParameterTypeEnum.String:
                            toAdd = createStringParameter(parameterValue);
                            break;
                        case ParameterTypeEnum.ByteArray:
                            toAdd = createByteParameter(parameterValue);
                            break;
                        case ParameterTypeEnum.Uri:
                            toAdd = createUriParameter(parameterValue);
                            break;
                        default:
                            throw new ArgumentException("Invalid return parameter type specified");                            
                    }
                    if((toAdd != null)&&(!linkParams))
                        Result.Add(toAdd);
					else if (linkParams)
					{
						multiValue.ParamValue.Add(toAdd);
					}
                }
				if(linkParams)
					Result.Add(multiValue);
            }

            return Result;
        }

        //adds an extra parameter to a multivalue paramer if a language tag is present
        private static void handleHandleTags(MiddlewareParameter<List<MiddlewareParameter>> multiValue, KeyValuePair<string, INode> parameterValue)
        {
            if ((parameterValue.Value != null) && (parameterValue.Value.NodeType == NodeType.Literal))
            {
                ILiteralNode valNode = parameterValue.Value as ILiteralNode;
                if (!string.IsNullOrEmpty(valNode.Language))
                    multiValue.ParamValue.Add(new MiddlewareParameter<string>(MiddleWareBussinessObjects.Consts.LanguageTag, valNode.Language, MiddlewareParameterDirection.Out));
            }
        }

        private MiddlewareParameter createUriParameter(KeyValuePair<string, INode> parameterValue)
        {
   
            Uri value=null;
            if (parameterValue.Value != null)
            {
                if (parameterValue.Value.NodeType == NodeType.Literal)
                {
                    if (!Uri.TryCreate(parameterValue.Value.ToString().Trim(), UriKind.RelativeOrAbsolute, out value))
                        throw new InvalidCastException("Tried to cast invalid string as URI");
                }
                else if (parameterValue.Value.NodeType == NodeType.Uri)
                    value = ((IUriNode)parameterValue.Value).Uri;
            }

            MiddlewareParameter<Uri> strParam = new MiddlewareParameter<Uri>(
                parameterValue.Key,
                value,
                MiddlewareParameterDirection.Out
                );
            return strParam;
        }


        private static MiddlewareParameter<string> createStringParameter(KeyValuePair<string, INode> v)
        {
            string valueString;
            if (v.Value != null)
                valueString = v.Value.ToString();
            else
                valueString = string.Empty;

            int atLocation = valueString.LastIndexOf('@');
            if (atLocation > 0)
            {
                if((atLocation == valueString.Length -3)||(atLocation == valueString.Length -5))
                    valueString = valueString.Remove(atLocation);
            }

            MiddlewareParameter<string> strParam = new MiddlewareParameter<string>(
                v.Key,
                valueString,
                MiddlewareParameterDirection.Out
                );
            return strParam;
        }

        private static MiddlewareParameter<byte[]> createByteParameter(KeyValuePair<string, INode> v)
        {
            byte[] valueBytes;
            if (v.Value != null)
            {
                valueBytes =  MiddlewareConstants.EncodingInUse.GetBytes(v.Value.ToString());
                //Clever serialization is a job for later 
                //if (v.Value.NodeType == NodeType.Literal)
                //{
                //    ILiteralNode valueNode = v.Value as ILiteralNode;
                //    string typeStr = string.Empty;
                //    if(valueNode.DataType != null)
                //        if(string.IsNullOrEmpty(valueNode.DataType.AbsoluteUri))
                //            typeStr =valueNode.DataType.AbsoluteUri;
                //    switch (typeStr)
                //    {
                //        case XmlSpecsHelper.XmlSchemaDataTypeByte:                            
                //            valueBytes = new byte[] { (byte)(valueNode.Value) };
                //        default:
                //            break;
                //    }
                //}
            }
            //valueBytes = MiddlewareConstants.EncodingInUse.GetBytes(v.)
            else
                valueBytes = null;

            MiddlewareParameter<byte[]> result = new MiddlewareParameter<byte[]>(
                v.Key,
                valueBytes,
                MiddlewareParameterDirection.Out
                );
            return result;
        }

        private static SparqlParameterizedString getQuery(IEnumerable<MiddlewareParameter> parameters,string sparql)
        {
            SparqlParameterizedString query = new SparqlParameterizedString(sparql);
            query = setParams(parameters, query);
            return query;
        }

        private static SparqlParameterizedString setParams(IEnumerable<MiddlewareParameter> parameters, SparqlParameterizedString query)
        {
            foreach (MiddlewareParameter param in parameters)
            {
                if (!string.IsNullOrEmpty(param.ParamName))
                    query.SetParameter(param.ParamName, param.ParamToInode());
            }
            
            return query;
        }



        private  StardogConnector getConnector(Session session)
        {
            checkDetailsPresent(session);
			string serverToUse = (session.StardogServerDetails.StardogServer == null) ? server : session.StardogServerDetails.StardogServer.ToString();
			//serverToUse = serverToUse.Replace(@"http://","");
			//serverToUse = serverToUse.Replace(@"/", "");
			string dbToUse = string.IsNullOrEmpty(session.StardogServerDetails.StardogDB) ? datastore : session.StardogServerDetails.StardogDB;

            StardogConnector theConnector = new StardogConnector(
				serverToUse,
				dbToUse,
                session.StardogServerDetails.StardogUserName,
                session.StardogServerDetails.StardogPassword
                );
            return theConnector;
        }

        private static void checkDetailsPresent(Session session)
        {
            if (string.IsNullOrEmpty(session.StardogServerDetails.StardogDB))
                throw new ArgumentException("Must Specify Stardog Database", "session");
            if (string.IsNullOrEmpty(session.StardogServerDetails.StardogUserName))
                throw new ArgumentException("Must Specify Stardog UserName", "session");
            if (string.IsNullOrEmpty(session.StardogServerDetails.StardogPassword))
                throw new ArgumentException("Must Specify Stardog Password", "session");
            if (session.StardogServerDetails.StardogServer == null)
                throw new ArgumentException("Must Specify Stardog Server", "session");
        }

        #endregion
    }
}
