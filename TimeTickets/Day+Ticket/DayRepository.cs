using System.Collections.ObjectModel;
using System.Xml;
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

            foreach (var day in Days)
            {
                XmlElement dayNode = doc.CreateElement("Day");

                XmlAttribute dateAttribute = doc.CreateAttribute("Date");
                dateAttribute.Value = day.Date.ToString("yyyy-MM-dd");
                dayNode.Attributes.Append(dateAttribute);

                rootNode.AppendChild(dayNode);

                foreach (var ticket in day.GetAllTickets())
                {
                    XmlElement ticketNode = doc.CreateElement("Ticket");
                    dayNode.AppendChild(ticketNode);

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
