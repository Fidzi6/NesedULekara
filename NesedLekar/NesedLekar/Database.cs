using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
        private IMobileServiceTable<DoktoriIntervals> tableIntervals;

        public Database()
        {
            tableLogin = App.MobileService.GetTable<login>();
            tableDoctors = App.MobileService.GetTable<doctors>();
            tableComments = App.MobileService.GetTable<comments>();
            tablePatients = App.MobileService.GetTable<patients>();
            tableIntervals = App.MobileService.GetTable<DoktoriIntervals>();
        }
        
        public void InsertRow(login login)
        {
            tableLogin.InsertAsync(login);
        }

        public void InsertRow(doctors doctor)
        {
            tableDoctors.InsertAsync(doctor);
        }

        public void InsertRow(patients patient)
        {
            tablePatients.InsertAsync(patient);
        }

        public void InsertRow(comments comment)
        {
            tableComments.InsertAsync(comment);
        }

        public void InsertRow(DoktoriIntervals doctorInterval)
        {
            tableIntervals.InsertAsync(doctorInterval);
        }

        public async Task<List<login>> SelectAsync(string login, string pass)
        {
            return await tableLogin.Where(x => x.nick==login && x.pass==pass).ToListAsync();
        }

        public async Task<List<login>> SelectAsync(string login)
        {
            return await tableLogin.Where(x => x.nick == login).ToListAsync();
        }

        public async Task<List<patients>> SelectAsynch(string patient)
        {
            return await tablePatients.Where(x => x.patient == patient).ToListAsync();
        }

        public async Task<List<doctors>> SelectAsynchD(string city)
        {
            return await tableDoctors.Where(x => x.adress.Split(',')[0] == city).ToListAsync();
        }

        public async Task<List<doctors>> SelectAsynchDoc(string doctorEmail)
        {
            return await tableDoctors.Where(x => x.email == doctorEmail).ToListAsync();
        }

        public async Task<List<comments>> SelectAsynchC(string doctor)
        {
            return await tableComments.Where(x => x.doctor_name == doctor).ToListAsync();
        }

        public async Task<List<DoktoriIntervals>> SelectAsynch(patients order)
        {
            return await tableIntervals.Where(x => x.doktor == order.doctor && x.date==order.date).ToListAsync();
        }

        public async Task<IEnumerable<doctors>> AllDoctors()
        {
            return await tableDoctors.ReadAsync();

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

