using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Xml.Serialization;
using System.IO;

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
            int flag = 0;
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    //Sort();
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                    /*foreach(String s in ListeIntitule)
                    {
                        flag = 0;
                        foreach(Activity a in ListeActivite)
                        {
                            if (a.Intitule == s)
                                flag = 1;
                        }
                        if (flag == 0)
                        {
                            ListeIntitule.Remove(s);
                            break;
                        }
                    }*/
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Replace:
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Move:
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Reset:
                    break;
                default:
                    break;
            }
            ListeIntitule.Clear();
            foreach (Activity a in ListeActivite)
            {
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
            //get
            //{
            //    ObservableCollection<String> s = new ObservableCollection<string>();
            //    foreach(Activity a in _liste)
            //    {
            //        s.Add(a.Intitule);
            //    }
            //    return s;
            //}
        }

        public void SaveListOfActivity(string path)
        {

            XmlSerializer xmlformat = new XmlSerializer(typeof(List<Activity>));
            using (Stream fStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                xmlformat.Serialize(fStream, ListeActivite.ToList());
            }
        }

        public void LoadFromXMLFormat(string filename)
        {
            XmlSerializer xmlFormat = new XmlSerializer(typeof(List<Activity>));
            ListeActivite.Clear();
            using (Stream fStream = File.OpenRead(filename))
            {
                ((List<Activity>)xmlFormat.Deserialize(fStream)).ForEach(item => ListeActivite.Add(item));
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
