using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace TimeTickets.HelperClasses
{
    public static class XmlExtensions
    {
        public static string GetNodeAttribute(this XmlNode node, string attributeName)
        {
            if (node.Attributes != null && node.Attributes[attributeName] != null)
                return node.Attributes[attributeName].Value;
            return string.Empty;
        }
    }
}
