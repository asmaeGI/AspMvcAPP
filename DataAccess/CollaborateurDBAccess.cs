using CommonComponents;
using Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DataAccess
{
    public class CollaborateurDBAccess : IDisposable
    {
        private Sqli_GD_Tarkett_Model db=new Sqli_GD_Tarkett_Model();
        private bool disposed = false;
        /// <summary>
        /// get the collaborator full name by id (if id is null get all collaborators full names)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SelectList getCollaboratorsNames(int? id)
        {
            try
            {
                if (id != null)
                {
                    return new SelectList((from s in db.Colaborateur.ToList()
                                           where s.IdCol == id
                                           where s.Role.Equals("Colaborateur")
                                           select new
                                           {
                                               Id = s.IdCol,
                                               FullName = s.Nom + " " + s.Prenom
                                           }), "Id", "FullName");
                }
                else
                {
                    return new SelectList((from s in db.Colaborateur.ToList()
                                           where s.Role.Equals("Colaborateur")
                                           select new
                                           {
                                               Id = s.IdCol,
                                               FullName = s.Nom + " " + s.Prenom
                                           }), "Id", "FullName");
                }
            }
            catch(Exception e)
            {
                Log.Error("get Collaborators names failed "+e.StackTrace);
                return null;
            }
        }
        /// <summary>
        /// add displacement to collaborators
        /// </summary>
        /// <param name="deplacement"></param>
        /// <param name="IdU"></param>
        public void addDisplacementToCollaborators(Deplacement deplacement,int?[] IdU)
        {
            try
            {
                if (IdU != null)
                {
                    for (int i = 0; i < IdU.Length; i++)
                    {
                        Colaborateur colabo = db.Colaborateur.Find(IdU[i]);
                        colabo.IdD = deplacement.Id;
                        colabo.NombreDeplacement++;
                        db.SaveChanges();

                    }
                }
            }
            catch (Exception e)
            {
                Log.Error("addDisplacementToCollaborators failed " + e.StackTrace);
            }
        }
        /// <summary>
        /// get all collaborators
        /// </summary>
        /// <returns></returns>
        public IList<Colaborateur> getCollaborators()
        {
            try
            {
                var colaborateurs = db.Colaborateur.Where(c => c.Role.Equals("Colaborateur")).Include(d => d.Deplacement);
                return colaborateurs.ToList();
            }
            catch (Exception e)
            {
                Log.Error("get Collaborators failed " + e.StackTrace);
                return null;
            }

        }
        /// <summary>
        /// get one collaborator using id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Colaborateur getCollaboratorsById(int? id)
        {
            try
            {
                return db.Colaborateur.Find(id);
            }
            catch (Exception e)
            {
                Log.Error("get Collaborators By Id " + e.StackTrace);
                return null;
            }

        }
        /// <summary>
        /// add collaborateur 
        /// </summary>
        /// <param name="collaborator"></param>
        public void addCollaborator(Colaborateur collaborator)
        {
            try
            {
                db.Colaborateur.Add(collaborator);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                Log.Error("addCollaborator failed " + e.StackTrace);
            }

        }
        /// <summary>
        /// delete collaborator by id (find the collaborator)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Colaborateur deleteCollaborators(int? id)
        {
            try
            {
                Colaborateur collaborator = db.Colaborateur.Find(id);
                return collaborator;
            }
            catch (Exception e)
            {
                Log.Error(e.StackTrace);
                return null;
            }
        }
        /// <summary>
        /// delete collaborator from database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool deleteCollaboratorConfirmed(int? id)
        {
            try { 
            Colaborateur collaborator = db.Colaborateur.Find(id);
            DemandeVisa demandeVisa = (from v in db.DemandeVisa where v.IdC == id select v).FirstOrDefault();
            
                if (demandeVisa != null)
                {
                    demandeVisa.IdC = null;
                    db.DemandeVisa.Remove(demandeVisa);
                }

                if (collaborator.IdD != null)
                {
                    collaborator.IdD = null;
                    db.Entry(collaborator).State = EntityState.Modified;
                }
                db.Colaborateur.Remove(collaborator);
                db.SaveChanges();
            }
            catch(ArgumentNullException)
            {
                Log.Error("ArgumentNullException: id passed is null ,Colaborateur can not be found ...");
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
        /// get collaborator by id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Colaborateur detailsCollaborator(int? id)
        {
            try
            {
                Colaborateur collaborator = db.Colaborateur.Include(d => d.Deplacement).SingleOrDefault(i => i.IdCol == id);
                return collaborator;
            }
            catch(Exception e)
            {
                Log.Error(e.StackTrace);
                return null;
            }
        }
        /// <summary>
        /// Edit Collaborator by id (find Collaborator)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Colaborateur editCollaborator(int? id)
        {
            try
            {
                Colaborateur collaborator = db.Colaborateur.Find(id);
                return collaborator;
            }
            catch(Exception e)
            {
                Log.Error(e.StackTrace);
                return null;
            }
        }
        /// <summary>
        /// Edit Collaborator in database
        /// </summary>
        /// <param name="collaborator"></param>
        public void editCollaborator(Colaborateur collaborator)
        {
            try
            {
                db.Entry(collaborator).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception e)
            {
                Log.Error(e.StackTrace);
            }
        }

        /// <summary>
        /// find collaborator using login and password 
        /// </summary>
        /// <param name="collaborator"></param>
        /// <returns></returns>
        public Colaborateur findCollaborator(Colaborateur collaborator)
        {
            try
            {
                var collabo = db.Colaborateur.Where(C => String.Equals(C.Login, collaborator.Login) && String.Equals(C.Password, collaborator.Password)).FirstOrDefault();
                return collabo;
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
