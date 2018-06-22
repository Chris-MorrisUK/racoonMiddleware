<?xml version="1.0"?>
<ArrayOfStoredProcedure xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <StoredProcedure>
    <KeyHash>864686231</KeyHash>
    <Name>getindividual</Name>
    <TypeOfQuerry>StardogConnection.StardogQuery, StardogConnection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null</TypeOfQuerry>
    <StoredProcText>select *
WHERE
{
?ind a @class .
?ind rdfs:label ?label
}</StoredProcText>
  </StoredProcedure>
</ArrayOfStoredProcedure>