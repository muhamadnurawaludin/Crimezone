using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crimezone.Model
{

 
   public class User
    {
        private static User instance;
        private static readonly IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
        private string namapengguna;
        private string password;
        private string fotopengguna;

        public User()
        {
            namapengguna = this.LoadSettings("namaPengguna");
            password = this.LoadSettings("idFacebook");
            fotopengguna = this.LoadSettings("fotoPengguna");
        }

        #region IsolatedStorage
        private void SaveSettings(string key, string value)
        {
            if (!settings.Contains(key))
                settings.Add(key, value);
            else
                settings[key] = value;
            settings.Save();
        }

        private string LoadSettings(string key)
        {
            string result = "";

            if (IsolatedStorageSettings.ApplicationSettings.Contains(key))
                result = IsolatedStorageSettings.ApplicationSettings[key] as string;

            return result;
        }
        #endregion

        public static User Instance
        {
            get
            {
                if (instance == null)
                    instance = new User();
                return instance;
            }
            set { instance = value; }
        }

        public string Namapengguna
        {
            get { return namapengguna; }
            set { 
                namapengguna = value;
                this.SaveSettings("namaPengguna", namapengguna);
            }

        }

        public string Password
        {
            get { return password; }
            set { password = value;
                this.SaveSettings("idFacebook", password);
            }
        }

        

        public string Fotopengguna
        {
            get { return fotopengguna; }
            set { fotopengguna = value;
            this.SaveSettings("fotoPengguna", fotopengguna);
            }
        }

    }
}
