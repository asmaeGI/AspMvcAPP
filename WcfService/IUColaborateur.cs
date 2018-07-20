using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Entities;
using PagedList;

namespace WcfService
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom d'interface "IService1" à la fois dans le code et le fichier de configuration.
    [ServiceContract]
    public interface IUColaborateur
    {
        [OperationContract]
        void ajouterColab(Colaborateur colaborateur);
        [OperationContract]
        PagedList.IPagedList<Colaborateur> afficherColab(string sortOrder, string searchString, string currentFilter, int? page, int pageSize);
        [OperationContract]
        bool supprimerColaborateurConfirmer(int? id);
        [OperationContract]
        Colaborateur supprimerColaborateur(int? id);
        [OperationContract]
        Colaborateur detaillerColaborateur(int? id);
        [OperationContract]
        Colaborateur modifierColborateur(int? id);
        [OperationContract]
        void modifierColaborateur(Colaborateur colaborateur);
        [OperationContract]
        String FileExtension(string exportType);
        [OperationContract]
        byte[] ExporterColaborateur(string exportType, String path);
    }

    // Utilisez un contrat de données comme indiqué dans l'exemple ci-après pour ajouter les types composites aux opérations de service.
    // Vous pouvez ajouter des fichiers XSD au projet. Une fois le projet généré, vous pouvez utiliser directement les types de données qui y sont définis, avec l'espace de noms "WcfService.ContractType".
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}
