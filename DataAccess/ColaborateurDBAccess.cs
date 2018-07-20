using Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ColaborateurDBAccess
    {
        private Sqli_GD_Tarkett_Model db=new Sqli_GD_Tarkett_Model();
        public void addColaborateur(Colaborateur colaborateur)
        {
            db.Colaborateur.Add(colaborateur);
            db.SaveChanges();
        }
        public DbSet<Colaborateur> showColaborateurs()
        {
            return db.Colaborateur;
        }
        public Colaborateur deleteColaborateurs(int? id)
        {
            Colaborateur colaborateur = db.Colaborateur.Find(id);
            return colaborateur;
        }
        public bool deleteColaborateurConfirmed(int? id)
        {
            Colaborateur colaborateur = db.Colaborateur.Find(id);
            try
            {
                db.Colaborateur.Remove(colaborateur);
                db.SaveChanges();
            }
            catch(ArgumentNullException e)
            {
                return false;
            }
            return true;
        }
        public Colaborateur detailsColaborateur(int? id)
        {
            Colaborateur colaborateur = db.Colaborateur.Find(id);
            return colaborateur;
        }
        public Colaborateur editColaborateur(int? id)
        {
            Colaborateur colaborateur = db.Colaborateur.Find(id);
            return colaborateur;
        }
        public void editColaborateur(Colaborateur colaborateur)
        {
            db.Entry(colaborateur).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}
