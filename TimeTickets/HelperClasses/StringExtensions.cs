using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTickets.HelperClasses
{
    public static class StringExtensions
    {
        public static DateTime ToDate(this string date)
        {
            if (!string.IsNullOrEmpty(date))
            {
                if (DateTime.TryParse(date, out DateTime d))
                    return d;
            }
            return DateTime.Now.Date;
        }
    }
}
