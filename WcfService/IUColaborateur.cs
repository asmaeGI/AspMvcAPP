using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using PagedList;
using Entities;
using System.Web.Mvc;

namespace WcfService
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom d'interface "IService1" à la fois dans le code et le fichier de configuration.
    [ServiceContract]
    public interface IUColaborateur
    {
        [OperationContract]
        void ajouterColab(Colaborateur colaborateur);
        [OperationContract]
        IList<Colaborateur> getColaborateurs();
        [OperationContract]
        Colaborateur getColaborateur(int? id);
        [OperationContract]
        IList<Colaborateur> afficherColab();
        [OperationContract]
        bool supprimerColaborateurConfirmer(int? id);
        [OperationContract]
        Colaborateur supprimerColaborateur(int? id);
        [OperationContract]
        Colaborateur detaillerColaborateur(int? id);
        [OperationContract]
        Colaborateur modifierColborateurById(int? id);
        [OperationContract]
        void modifierColaborateur(Colaborateur colaborateur);
        [OperationContract]
        string FileExtension(string exportType);
        [OperationContract]
        byte[] ExporterColaborateur(string exportType, String path);
        [OperationContract]
        bool sendMail(string email, string objet, string message);

        /***Demande Visa***/

        [OperationContract]
        void ajouterDemandeVisa(DemandeVisa demandeVisa);
        [OperationContract]
        IList<DemandeVisa> afficherVisa();
        [OperationContract]
        bool supprimerDemandeVisaConfirmer(int? id);
        [OperationContract]
        DemandeVisa supprimerDemandeVisa(int? id);
        [OperationContract]
        DemandeVisa detaillerDemandeVisa(int? id);
        [OperationContract]
        DemandeVisa modifierDemandeVisaById(int? id);
        [OperationContract]
        void modifierDemandeVisa(DemandeVisa demandeVisa);

        /*****Deplacement Colaborateurs*****/
        [OperationContract]
        void ajouterDeplacement(Deplacement deplacement,int?[] IdU);
        [OperationContract]
        IList<Deplacement> afficherDeplacement();
        [OperationContract]
        bool supprimerDeplacementConfirmer(int? id);
        [OperationContract]
        Deplacement supprimerDeplacement(int? id);
        [OperationContract]
        Deplacement detaillerDeplacement(int? id);
        [OperationContract]
        Deplacement modifierDeplacementById(int? id);
        [OperationContract]
        void modifierDeplacement(Deplacement deplacement,int?[] IdU);
        [OperationContract]
        IList<Colaborateur> ProposeColaborateur();
        [OperationContract]
        IList<Deplacement> getDeplacements();

    }

    // Utilisez un contrat de données comme indiqué dans l'exemple ci-après pour ajouter les types composites aux opérations de service.
    // Vous pouvez ajouter des fichiers XSD au projet. Une fois le projet généré, vous pouvez utiliser directement les types de données qui y sont définis, avec l'espace de noms "WcfService.ContractType".




}
