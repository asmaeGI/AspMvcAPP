using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using BusinessComponents;
using PagedList;
using Entities;
using System.Web.Mvc;

namespace WcfService
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "Service1" à la fois dans le code et le fichier de configuration.
    public class UColaborateur : IUColaborateur,IDisposable
    {
        UColaborateurBusniss colab = new UColaborateurBusniss();
        UDemandeVisaBusniss visa = new UDemandeVisaBusniss();
        DeplacementBusniss deplace = new DeplacementBusniss();
        private bool disposed = false;

        public void ajouterColab(Colaborateur colaborateur)
        {
            colab.addCollaborator(colaborateur);
        }
        public Colaborateur getColaborateur(int? id)
        {
           return colab.getCollaborator(id);
        }
        public IList<Colaborateur> getColaborateurs()
        {
            return colab.getCollaborators();
        }

        public IList<Colaborateur> afficherColab()
        {
            return colab.showCollaborators();
        }
        public bool supprimerColaborateurConfirmer(int? id)
        {
            return colab.deleteCollaboratorConfirmed(id);
        }
        public Colaborateur supprimerColaborateur(int? id)
        {
            return colab.deleteCollaborator(id);
        }
        public Colaborateur detaillerColaborateur(int? id)
        {
            return colab.detailsCollaborator(id);
        }
        public Colaborateur modifierColborateurById(int? id)
        {

            return colab.detailsCollaborator(id);
        }
        public void modifierColaborateur(Colaborateur colaborateur)
        {
            colab.editCollaborator(colaborateur);
        }
        public string FileExtension(string exportType)
        {
            return colab.FileExtension(exportType);
        }
        public byte[] ExporterColaborateur(string exportType, String path)
        {
            return colab.ExportCollaborator(exportType, path);
        }
        public bool sendMail(string email, string objet, string message)
        {
            return colab.sendMail(email, objet, message);
        }

        /*** Demande Visa***/
        public void ajouterDemandeVisa(DemandeVisa demandeVisa)
        {
            visa.addVisaRequest(demandeVisa);
        }
        public IList<DemandeVisa> afficherVisa()
        {
            return visa.showVisaRequest();
        }
        public bool supprimerDemandeVisaConfirmer(int? id)
        {
            return visa.deleteVisaRequestConfirmed(id);
        }
        public DemandeVisa supprimerDemandeVisa(int? id)
        {
            return visa.deleteVisaRequest(id);
        }
        public DemandeVisa detaillerDemandeVisa(int? id)
        {
            return visa.detailsVisaRequest(id);
        }
        public DemandeVisa modifierDemandeVisaById(int? id)
        {

            return visa.detailsVisaRequest(id);
        }
        public void modifierDemandeVisa(DemandeVisa demandeVisa)
        {
            visa.editVisaRequest(demandeVisa);
        }
        /*****Deplacement Colaborateurs********/ 
        public void ajouterDeplacement(Deplacement deplacement,int?[] IdU)
        {
            deplace.addDisplacement(deplacement,IdU);
        }

        public IList<Deplacement> afficherDeplacement()
        {
            return deplace.showDisplacement();
        }
        public bool supprimerDeplacementConfirmer(int? id)
        {
            return deplace.deleteDisplacementConfirmed(id);
        }
        public Deplacement supprimerDeplacement(int? id)
        {
            return deplace.deleteDisplacement(id);
        }
        public Deplacement detaillerDeplacement(int? id)
        {
            return deplace.detailsDisplacement(id);
        }
        public Deplacement modifierDeplacementById(int? id)
        {
            return deplace.editDisplacementById(id);
        }
        public void modifierDeplacement(Deplacement deplacement,int?[] IdU)
        {
            deplace.editDisplacement(deplacement,IdU);
        }
        public IList<Colaborateur> ProposeColaborateur()
        {
           return deplace.CollaboratorsSuggest();
        }
        public IList<Deplacement> getDeplacements()
        {
            return deplace.getDeplacements();
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
                    deplace.Dispose();
                    visa.Dispose();
                    colab.Dispose();
                    deplace=null;
                    visa=null;
                    colab = null;
                }
                disposed = true;
            }
        }
    }
}
