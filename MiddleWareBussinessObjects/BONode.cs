using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MiddleWareBussinessObjects
{
    public class BONode
    {
       
        public BONode(object value,bool addLanguage = true) 
        {
            Value = value;
            AddLanguageIfString = addLanguage;
        }
        public bool AddLanguageIfString;
        public object Value;

    }
}
