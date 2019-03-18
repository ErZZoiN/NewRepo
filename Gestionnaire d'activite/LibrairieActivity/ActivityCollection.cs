using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Xml.Serialization;
using System.IO;
using ServiceStack.Text;

namespace LibrairieActivity
{
    public class ActivityCollection
    {
        private ObservableCollection<Activity> _liste;

        public ActivityCollection()
        {
            ListeActivite = new ObservableCollection<Activity>();
            ListeActivite.CollectionChanged += ListeActivite_CollectionChanged;
            ListeIntitule = new ObservableCollection<string>();
        }

        private void ListeActivite_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            ListeIntitule.Clear();
            foreach (Activity a in ListeActivite)
            {
                if(a!=null)
                    ListeIntitule.Add(a.Intitule);
            }
        }

        public ObservableCollection<Activity> ListeActivite
        {
            get => _liste;
            set => _liste = value;
        }
        public ObservableCollection<String> ListeIntitule
        {
            get;set;
        }

        public void SaveInXMLFormat(string path)
        {

            System.Xml.Serialization.XmlSerializer xmlformat = new System.Xml.Serialization.XmlSerializer(typeof(List<Activity>));
            using (Stream fStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                xmlformat.Serialize(fStream, ListeActivite.ToList());
            }
        }

        public void LoadFromXMLFormat(string filename)
        {
            System.Xml.Serialization.XmlSerializer xmlFormat = new System.Xml.Serialization.XmlSerializer(typeof(List<Activity>));
            ListeActivite.Clear();
            using (Stream fStream = File.OpenRead(filename))
            {
                ((List<Activity>)xmlFormat.Deserialize(fStream)).ForEach(item => Add(item));
            }
        }

        public void SaveInCSVFormat(string path)
        {
            using (Stream fStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                fStream.WriteCsv(ListeActivite);
            }
        }

        public void LoadFromCSVFormat(string filename)
        {
            string tmp;
            ListeActivite.Clear();
            using (Stream fStream = File.OpenRead(filename))
            {
                StreamReader sr = new StreamReader(fStream);
                //sr.ReadLine();
                while(!sr.EndOfStream)
                {
                    tmp = sr.ReadLine();
                    ListeActivite.Add(CsvSerializer.DeserializeFromString<Activity>(tmp));
                }
            }
        }

        public void Sort()
        {
            List<Activity> sortableList = new List<Activity>(ListeActivite);

            sortableList.Sort();

            for (int i = 0; i < sortableList.Count; i++)
            {
                ListeActivite.Move(ListeActivite.IndexOf(sortableList[i]), i);
            }
        }

        public void Add(Activity a)
        {
            ListeActivite.Add(a);
            Sort();
        }
    }
}
