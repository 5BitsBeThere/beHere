using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Projektas
{
    //Nustatymu klase
    public class SettingsClass
    {
        public string Datasource { get; set; }
        public string Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Database { get; set; }

        public SettingsClass(){ }

        public SettingsClass(string datasource, string port, string username, string password, string database)
        {
            this.Datasource = datasource;
            this.Port = port;
            this.Username = username;
            this.Password = password;
            this.Database = database;

        }
    }
}
