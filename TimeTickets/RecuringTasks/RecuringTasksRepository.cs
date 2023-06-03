using System;
using System.Collections.ObjectModel;
using System.Xml;

namespace TimeTickets.RecuringTasks
{
    public class RecuringTasksRepository : IRepository<RecuringTask>
    {
        public ObservableCollection<RecuringTask> RecuringTasks { get; set; }

        public RecuringTasksRepository()
        {
            RecuringTasks = new ObservableCollection<RecuringTask>();
        }

        public void Add(RecuringTask item)
        {
            RecuringTasks.Add(item);
        }

        public bool Remove(RecuringTask item)
        {
            return RecuringTasks.Remove(item);
        }
        public bool Load(string filename)
        {
            if (!System.IO.File.Exists(filename))
                return false;

            XmlDocument doc = new XmlDocument();
            doc.Load(filename);

            XmlNodeList taskNodes = doc.SelectNodes("RecuringTasks/RecuringTask");
            foreach (XmlNode taskNode in taskNodes)
            {
                string id = taskNode.SelectSingleNode("Id").InnerText;
                string description = taskNode.SelectSingleNode("Description").InnerText;

                RecuringTask task = new RecuringTask();
                task.Id = new Guid(id);
                task.Description = description;
                RecuringTasks.Add(task);
            }
            return true;
        }

        public bool Save(string filename)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode rootNode = doc.CreateElement("RecuringTasks");

            XmlAttribute versionAttribute = doc.CreateAttribute("Version");
            versionAttribute.Value = "1.0";
            rootNode.Attributes.Append(versionAttribute);
            doc.AppendChild(rootNode);

            foreach (var task in RecuringTasks)
            {
                XmlElement taskNode = doc.CreateElement("RecuringTask");

                XmlElement idNode = doc.CreateElement("Id");
                idNode.InnerText = task.Id.ToString();
                taskNode.AppendChild(idNode);

                XmlElement descrNode = doc.CreateElement("Description");
                descrNode.InnerText = task.Description;
                taskNode.AppendChild(descrNode);

                rootNode.AppendChild(taskNode);
            }

            doc.Save(filename);
            return true;
        }
    }
}
