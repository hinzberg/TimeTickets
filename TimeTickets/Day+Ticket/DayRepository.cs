using System.Collections.ObjectModel;
using System.Xml;
using TimeTickets.HelperClasses;
using TimeTickets.TimeTicket;

namespace TimeTickets.Day_Ticket
{
    public class DayRepository : IRepository<Day>
    {
        public ObservableCollection<Day> Days { get; set; }

        public DayRepository()
        {
            Days = new ObservableCollection<Day>();
        }

        public void Add(Day item)
        {
            Days.Add(item);
        }

        public bool Remove(Day item)
        {
            return Days.Remove(item);
        }

        public bool Load(string filename)
        {
            if (!System.IO.File.Exists(filename))
                return false;

            XmlDocument doc = new XmlDocument();
            doc.Load(filename);

            XmlNodeList daysNodes = doc.SelectNodes("TimeTickets/Days/Day");
            foreach (XmlNode dayNode in daysNodes)
            {
                Day day = new Day();
                var dateAttrib = dayNode.GetNodeAttribute("Date");
                day.Date = dateAttrib.ToDate();

                XmlNodeList ticketsNodes = dayNode.SelectNodes("Tickets/Ticket");
                foreach (XmlNode ticketNode in ticketsNodes)
                {
                    string id = ticketNode.SelectSingleNode("Id").InnerText;
                    string description = ticketNode.SelectSingleNode("Description").InnerText;
                    string seconds = ticketNode.SelectSingleNode("TotalElapsedSeconds").InnerText;
                    int elapsedSeconds = int.Parse(seconds);

                    Ticket ticket = Ticket.Create(id, description, elapsedSeconds);
                    day.AddTicket(ticket);
                }
                Add(day);
            }

            return true;
        }

        public bool Save(string filename)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode rootNode = doc.CreateElement("TimeTickets");
            
            XmlAttribute versionAttribute = doc.CreateAttribute("Version");
            versionAttribute.Value = "1.0";
            rootNode.Attributes.Append(versionAttribute);
            doc.AppendChild(rootNode);

            XmlElement daysNode = doc.CreateElement("Days");
            rootNode.AppendChild(daysNode);

            foreach (var day in Days)
            {
                XmlElement dayNode = doc.CreateElement("Day");

                XmlAttribute dateAttribute = doc.CreateAttribute("Date");
                dateAttribute.Value = day.Date.ToString("yyyy-MM-dd");
                dayNode.Attributes.Append(dateAttribute);

                daysNode.AppendChild(dayNode);

                XmlElement ticketsNode = doc.CreateElement("Tickets");
                dayNode.AppendChild(ticketsNode);

                foreach (var ticket in day.GetAllTickets())
                {
                    XmlElement ticketNode = doc.CreateElement("Ticket");
                    ticketsNode.AppendChild(ticketNode);

                    XmlElement idNode = doc.CreateElement("Id");
                    idNode.InnerText = ticket.Id.ToString();
                    ticketNode.AppendChild(idNode);

                    XmlElement descriptionNode = doc.CreateElement("Description");
                    descriptionNode.InnerText = ticket.Description;
                    ticketNode.AppendChild(descriptionNode);

                    XmlElement secondsNode = doc.CreateElement("TotalElapsedSeconds");
                    secondsNode.InnerText = ticket.TotalElapsedSeconds.ToString();
                    ticketNode.AppendChild(secondsNode);
                }
            }

            doc.Save(filename);
            return true;
        }
    }
}
