using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrairieActivity
{
    public class Activity
    {
        public enum periodicite { quotidien, hebdomadaire, mensuel, annuel };

        #region VARIABLE
        private String _intitule;
        private String _description;
        private String _lieu;

        private DateTime _debut;
        private DateTime _fin;

        private int _nboccurence;
        private periodicite _periodicite;
        #endregion

        #region PROPRIETE

        public String Intitule
        {
            get { return _intitule; }
            set { _intitule = value; }
        }

        public string Description { get => _description; set => _description = value; }
        public string Lieu { get => _lieu; set => _lieu = value; }
        public DateTime Debut { get => _debut; set => _debut = value; }
        public DateTime Fin { get => _fin; set => _fin = value; }
        public int Nboccurence { get => _nboccurence; set => _nboccurence = value; }
        public periodicite Periodicite1 { get => _periodicite; set => _periodicite = value; }



        #endregion

        public static String PeriodiciteToString(periodicite p)
        {
            switch(p)
            {
                case periodicite.annuel:
                    return "Annuel";
                case periodicite.hebdomadaire:
                    return "Hebdomadaire";
                case periodicite.mensuel:
                    return "Mensuel";
                case periodicite.quotidien:
                    return "Quotidien";

                default:
                    return "";
            }
        }

        public static periodicite StringToPeriodicite(String s)
        {
            switch(s)
            {
                case "Annuel":
                    return periodicite.annuel;
                case "Hebdomadaire":
                    return periodicite.hebdomadaire;
                case "Mensuel":
                    return periodicite.mensuel;
                case "Quotidien":
                    return periodicite.quotidien;

                default:
                    return periodicite.quotidien;
            }
        }
    }

}
