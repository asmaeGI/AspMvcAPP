using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using BusinessComponents;
using Entities;
using PagedList;

namespace WcfService
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "Service1" à la fois dans le code et le fichier de configuration.
    public class UColaborateur : IUColaborateur
    {
        Colab colab = new Colab();
       
        public void ajouterColab(Colaborateur colaborateur)
        {
            colab.ajouterColab(colaborateur);
        }
        public IPagedList<Colaborateur> afficherColab(string sortOrder, string searchString, string currentFilter, int? page, int pageSize)
        {
            return colab.afficherColab(sortOrder, searchString, currentFilter, page, pageSize);
        }
        public bool supprimerColaborateurConfirmer(int? id)
        {
            return colab.supprimerColaborateurConfirmer(id);
        }
        public Colaborateur supprimerColaborateur(int? id)
        {
            return colab.supprimerColaborateur(id);
        }
        public Colaborateur detaillerColaborateur(int? id)
        {
            return colab.detaillerColaborateur(id);
        }
        public Colaborateur modifierColborateur(int? id)
        {
            return colab.modifierColborateur(id);
        }
        public void modifierColaborateur(Colaborateur colaborateur)
        {
                colab.modifierColaborateur(colaborateur);
        }
        public String FileExtension(string exportType)
        {
            return FileExtension(exportType);
        }
        public byte[] ExporterColaborateur(string exportType, String path)
        {
            return colab.ExporterColaborateur(exportType, path);
        }
    }
}
