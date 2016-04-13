using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StardogConnection
{
    /* I am not currently in use */
    public class StardogServerDetails
    {
        public StardogServerDetails()
        { }

        public StardogServerDetails(
         string _URI,
         string _KnowledgeBase,
         string _UserName,
         string _Password)
        {
            URI = _URI;
            KnowledgeBase = _KnowledgeBase;
            UserName = _UserName;
            Password = _Password;
        }

        private string uri;
        public string URI
        {
            get
            {
                if (string.IsNullOrEmpty(uri))
                    return string.Empty;
                if (uri.Contains("//"))
                    return uri;
                else
                    return @"http://" + uri;
            }
            set
            {
                uri = value;
            }
        }
        public string KnowledgeBase;
        public string UserName;
        public string Password;//Yes. In plain text



    }

}
