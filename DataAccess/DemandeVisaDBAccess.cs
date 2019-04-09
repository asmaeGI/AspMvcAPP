using CommonComponents;
using Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class DemandeVisaDBAccess:IDisposable
    {
        private Sqli_GD_Tarkett_Model db = new Sqli_GD_Tarkett_Model();
        private bool disposed = false;
        /// <summary>
        /// get all visas request
       /// </summary>
       /// <returns></returns>
        public IList<DemandeVisa> getRequestVisa()
        {
            try
            {
                var requestVisa = db.DemandeVisa.Include(d => d.Colaborateur);
                return requestVisa.ToList();
            }
            catch (Exception e)
            {
                Log.Error(e.StackTrace);
                return null;
            }
        }
        /// <summary>
        /// add new request to database
        /// </summary>
        /// <param name="demandeVisa"></param>
        public void addRequestVisa(DemandeVisa demandeVisa)
        {
            try
            {
                db.DemandeVisa.Add(demandeVisa);
                Colaborateur c = db.Colaborateur.Find(demandeVisa.IdC);
                c.DateValiditeVisa = demandeVisa.DateValiditeVisa;
                c.DateFinVisa = demandeVisa.DateFinVisa;
                db.Entry(c).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception e)
            {
                Log.Error(e.StackTrace);
            }
        }
        /// <summary>
        /// delete a visa request by id (find the selected visa request)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DemandeVisa deleteRequestVisa(int? id)
        {
            try
            {
                DemandeVisa demandeVisa = db.DemandeVisa.Include(d => d.Colaborateur).SingleOrDefault(i => i.Id == id);
                return demandeVisa;
            }
            catch (Exception e)
            {
                Log.Error(e.StackTrace);
                return null;
            }
        }
        /// <summary>
        /// delete a visa request
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool deleteRequestVisaConfirmed(int? id)
        {
            try { 
            DemandeVisa demandeVisa = db.DemandeVisa.Find(id);
                db.DemandeVisa.Remove(demandeVisa);
                db.SaveChanges();
            }
            catch(ArgumentNullException)
            {
                return false;
            }
            catch(Exception e)
            {
                Log.Error(e.StackTrace);
                return false;
            }
            return true;
        }
        /// <summary>
        /// get visa request details by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DemandeVisa detailsRequestVisa(int? id)
        {
            try
            {
                DemandeVisa demandeVisa = db.DemandeVisa.Include(d => d.Colaborateur).SingleOrDefault(i => i.Id == id);
                return demandeVisa;
            }
            catch (Exception e)
            {
                Log.Error(e.StackTrace);
                return null;
            }
        }
        /// <summary>
        /// get the visa request to edit by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DemandeVisa editRequestVisa(int? id)
        {
            try
            {
                DemandeVisa demandeVisa = db.DemandeVisa.Find(id);
                return demandeVisa;
            }
            catch (Exception e)
            {
                Log.Error(e.StackTrace);
                return null;
            }
        }
        /// <summary>
        /// edit a visa request
        /// </summary>
        /// <param name="demandeVisa"></param>
        public void editRequestVisa(DemandeVisa demandeVisa)
        {
            try
            {
                Colaborateur c = db.Colaborateur.Find(demandeVisa.IdC);
                c.DateValiditeVisa = demandeVisa.DateValiditeVisa;
                c.DateFinVisa = demandeVisa.DateFinVisa;
                db.Entry(demandeVisa).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception e)
            {
                Log.Error(e.StackTrace);
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                    db = null;
                }
                disposed = true;
            }
        }
    }
}
