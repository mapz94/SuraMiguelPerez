using Sura.UI;
using SuraMiguelPerez;
using SuraMiguelPerez.UI;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Sura.Classes
{
    class Session
    {

        private static Session _instance;

        private static readonly Session _lock = new Session();

        public administradore user { get; set; }

        private Session() { }

        public MenuForm mainMenu { get; set; }


        // Singleton
        public static Session GetInstance()
        {
            if (_instance == null)
            {
                // Proteger la instancia de Multi-Threading
                lock (_lock)
                {
                    _instance = new Session();
                    _instance.setForms();
                }
            }
            return _instance;
        }

        public List<KeyValuePair<string, Form>> forms;

        private void setForms()
        {
            forms = new List<KeyValuePair<string, Form>>();
            forms.Add(new KeyValuePair<string, Form>("Admin", new GestionAdmin()));
            forms.Add(new KeyValuePair<string, Form>("Client", new GestionClientes()));
            forms.Add(new KeyValuePair<string, Form>("Seguros", new GestionSeguros()));
            forms.Add(new KeyValuePair<string, Form>("KM", new GestionKM()));
        }

        public Form getForm(string key)
        {
            if (forms.Exists(pair => pair.Key == key))
            {
                return forms.Find(pair => pair.Key == key).Value;
            }
            return null;
        }

        public void clearSession()
        {
            user = null;
            setForms();
        }

    }
}
