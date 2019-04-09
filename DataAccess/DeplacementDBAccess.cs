using Entities;
using System.Data.Entity;
using System.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonComponents;

namespace DataAccess
{
    public class DeplacementDBAccess:IDisposable
    {
        private Sqli_GD_Tarkett_Model db = new Sqli_GD_Tarkett_Model();
        CollaborateurDBAccess col = new CollaborateurDBAccess();
        private bool disposed = false;
        /// <summary>
        /// get all displacements
        /// </summary>
        /// <returns></returns>
        public IList<Deplacement> getDisplacements()
        {
            try
            {
                var deplacements = db.Deplacement.Include(d => d.Colaborateur);
                return deplacements.ToList();
            }
            catch (Exception e)
            {
                Log.Error(e.StackTrace);
                return null;
            }
        }
        /// <summary>
        /// add new displacement to database
        /// </summary>
        /// <param name="deplacement"></param>
        /// <param name="IdU"></param>
        public void addDisplacement(Deplacement deplacement, int?[] IdU)
        {
            try
            {
                db.Deplacement.Add(deplacement);
                db.SaveChanges();
                col.addDisplacementToCollaborators(deplacement, IdU);
            }
            catch (Exception e)
            {
                Log.Error(e.StackTrace);
            }
        }
        /// <summary>
        /// get the displacement to delete using id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Deplacement deleteDisplacement(int? id)
        {
            try
            {
                Deplacement deplacement = db.Deplacement.Include(d => d.Colaborateur).SingleOrDefault(i => i.Id == id);
                return deplacement;
            }
            catch (Exception e)
            {
                Log.Error(e.StackTrace);
                return null;
            }
        }
        /// <summary>
        /// delete the displacement from database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool deleteDisplacementConfirmed(int? id)
        {
            try
            {
                Deplacement deplacement = db.Deplacement.Find(id);
                List<Colaborateur> colaborateurs = (from c in db.Colaborateur where c.IdD == id select c).ToList();
                if (colaborateurs != null)
                {
                    foreach (Colaborateur c in colaborateurs)
                    {
                        c.IdD = null;
                        db.SaveChanges();
                    }
                }
                db.Deplacement.Remove(deplacement);
                db.SaveChanges();
            }
            catch(ArgumentNullException)
            {
                return false;
            }
            catch (Exception e)
            {
                Log.Error(e.StackTrace);
                return false;
            }
            return true;
        }
        /// <summary>
        /// get displacement using id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Deplacement detailsDisplacement(int? id)
        {
            try
            {
                Deplacement deplacement = db.Deplacement.Include(d => d.Colaborateur).SingleOrDefault(i => i.Id == id);
                return deplacement;
            }
            catch (Exception e)
            {
                Log.Error(e.StackTrace);
                return null;
            }
        }
        /// <summary>
        /// get the displacement to edit
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Deplacement editDisplacement(int? id)
        {
            try
            {
                Deplacement deplacement = db.Deplacement.Find(id);
                return deplacement;
            }
            catch (Exception e)
            {
                Log.Error(e.StackTrace);
                return null;
            }
        }
        /// <summary>
        /// edit displacement in database  
        /// </summary>
        /// <param name="deplacement"></param>
        /// <param name="IdU"></param>
        public void editDisplacement(Deplacement deplacement,int? []IdU)
        {
            try
            {
                col.addDisplacementToCollaborators(deplacement, IdU);
                db.Entry(deplacement).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception e)
            {
                Log.Error(e.StackTrace);
            }
        }
       /// <summary>
       /// Collaborators Suggestion to next displacement
       /// </summary>
       /// <returns></returns>
        public IList<Colaborateur> CollaboratorsSuggestion()
        {
            try
            {
                var ColaborateurProposer = from v in db.DemandeVisa where v.Status.Equals("Acceptee") where v.Colaborateur.IdD == null select v.Colaborateur;
                if (db.Deplacement.ToList().Count != 0)
                {
                    Deplacement deplacement = (db.Deplacement.OrderByDescending(d => d.Date).ToList())[0];

                    Colaborateur colaborateur = (from c in db.Colaborateur where c.IdD == deplacement.Id select c).ToList()[0];
                    if (colaborateur.Equipe.Equals("tma"))
                    {
                        var colaborateurs = (from v in db.DemandeVisa where v.Status.Equals("Acceptee") where v.Colaborateur.IdD.Equals(null) where v.Colaborateur.Equipe.Equals("roadmap") select v.Colaborateur);
                        ColaborateurProposer = colaborateurs.OrderBy(c => c.NombreDeplacement).ThenByDescending(c => c.Anciennete);
                    }
                    if (colaborateur.Equipe.Equals("roadmap"))
                    {
                        var colaborateurs = (from v in db.DemandeVisa where v.Status.Equals("Acceptee") where v.Colaborateur.IdD == null where v.Colaborateur.Equipe.Equals("tma") select v.Colaborateur);
                        ColaborateurProposer = colaborateurs.OrderBy(c => c.NombreDeplacement).ThenByDescending(c => c.Anciennete);
                    }
                }
                else
                {
                    var colaborateurs = (from v in db.DemandeVisa where v.Status.Equals("Acceptee") where v.Colaborateur.Equipe.Equals("roadmap") select v.Colaborateur);
                    ColaborateurProposer = colaborateurs.OrderBy(c => c.NombreDeplacement).ThenByDescending(c => c.Anciennete);
                }
                return ColaborateurProposer.ToList();
            }
            catch (Exception e)
            {
                Log.Error(e.StackTrace);
                return null;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            col.Dispose();
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
