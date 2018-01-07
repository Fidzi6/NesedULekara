using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NesedLekar
{
    public class Database
    {
        private IMobileServiceTable<login> tableLogin;
        private IMobileServiceTable<doctors> tableDoctors;
        private IMobileServiceTable<comments> tableComments;
        private IMobileServiceTable<patients> tablePatients;

        public Database()
        {
            tableLogin = App.MobileService.GetTable<login>();
            tableDoctors = App.MobileService.GetTable<doctors>();
            tableComments = App.MobileService.GetTable<comments>();
            tablePatients = App.MobileService.GetTable<patients>();
        }


        public void InsertRow(Table table, List<object> data)
        {
            try
            {
                switch (table)
                {
                    case Table.Login:
                        {
                            login l = new login();

                            tableLogin.InsertAsync(l);

                            break;
                        }
                    default: break;
                }
            }
            catch (Exception ex) { }
        }


    }

    public enum Table
    {
        Login,
        Doctors,
        Comments,
        Patients
    }
}

