using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTickets.HelperClasses
{
    public class Time
    {
        public int Hours { get; set; }
        public int Minutes { get; set; }
        public int Seconds { get; set; }

        public Time(int hours, int minutes, int seconds)
        {
            Hours = hours;
            Minutes = minutes;
            Seconds = seconds;
        }

        public Time()
        {
            Hours = 0;
            Minutes = 0;
            Seconds = 0;
        }

        public void SetTimeFromTotalSeconds(int totalSeconds)
        {
            Hours = totalSeconds / 3600;  // 3600 seconds in an hour
            Minutes = (totalSeconds % 3600) / 60;  // 60 seconds in a minute
            Seconds = totalSeconds % 60;  // remaining seconds
        }

        public int GetTimeAsTotalSeconds()
        {
            return (Hours * 3600) + (Minutes * 60) + Seconds;
        }
    }
}
