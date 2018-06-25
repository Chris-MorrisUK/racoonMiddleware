<?xml version="1.0"?>
<ArrayOfStoredProcedure xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <StoredProcedure>
    <KeyHash>864686231</KeyHash>
    <Name>getindividual</Name>
    <TypeOfQuerry>StardogConnection.StardogQuery, StardogConnection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null</TypeOfQuerry>
    <StoredProcText>SELECT *
WHERE 
{
  { 
    GRAPH ?ItemGraph 
          {  	
            ?ItemUri a @class .
            ?ItemUri rdfs:label ?Label .
            ?ItemUri dc:description ?ItemComment
          }        
	}
  UNION
  {
    {  	
      ?ItemUri a @class .
      ?ItemUri rdfs:label ?Label .
      ?ItemUri dc:description ?ItemComment
    }
  }
}</StoredProcText>
  </StoredProcedure>
</ArrayOfStoredProcedure>