namespace Entities
{
    using Foolproof;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Runtime.Serialization;

    [DataContract]
    [Table("Colaborateur")]
    public partial class Colaborateur
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Colaborateur()
        {
            DemandeVisa = new HashSet<DemandeVisa>();
        }
        [DataMember]
        [Key]
        public int IdCol { get; set; }
        [DataMember]
        [StringLength(50)]
        [RequiredIf("Role", "Colaborateur", ErrorMessage = "Ce champ est obligatoire")]
        public string Nom { get; set; }
        [DataMember]
        [RequiredIf("Role", "Colaborateur", ErrorMessage = "Ce champ est obligatoire")]
        [StringLength(50)]
        public string Prenom { get; set; }
        [DataMember]
        [Required(ErrorMessage = "Ce champ est obligatoire")]
        [StringLength(50)]
        public string Role { get; set; }
        [DataMember]
        [RequiredIf("Role", "Colaborateur", ErrorMessage = "Ce champ est obligatoire")]
        [StringLength(50)]
        public string Cin { get; set; }
        [DataMember]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [Required(ErrorMessage = "Ce champ est obligatoire")]
        [StringLength(50)]
        public string Email { get; set; }
        [DataMember]
        [RequiredIf("Role", "Colaborateur", ErrorMessage = "Ce champ est obligatoire")]
        [StringLength(50)]
        public string Poste { get; set; }
        [DataMember]
        [RequiredIf("Role", "Colaborateur", ErrorMessage = "Ce champ est obligatoire")]
        [StringLength(50)]
        public string Equipe { get; set; }
        [DataMember]
        [RequiredIf("Role", "Colaborateur", ErrorMessage = "Ce champ est obligatoire")]
        public int? NombreDeplacement { get; set; }
        [DataMember]
        [RequiredIf("Role", "Colaborateur", ErrorMessage = "Ce champ est obligatoire")]
        public int? Anciennete { get; set; }
        [DataMember]
        [Column(TypeName = "date")]
        public DateTime? DateValiditeVisa { get; set; }
        [DataMember]
        [Column(TypeName = "date")]
        public DateTime? DateFinVisa { get; set; }
        [DataMember]
        [RequiredIf("Role", "Colaborateur", ErrorMessage = "Ce champ est obligatoire")]
        [StringLength(50)]
        public string Sexe { get; set; }
        [DataMember]
        [Required(ErrorMessage = "Ce champ est obligatoire")]
        [StringLength(50)]
        public string Login { get; set; }
        [DataMember]
        [Required(ErrorMessage = "Ce champ est obligatoire")]
        [StringLength(50)]
        public string Password { get; set; }
        [DataMember]
        [Column(TypeName = "date")]
        public DateTime? DateConnection { get; set; }
        [DataMember]

        public int? IdD { get; set; }

        public virtual Deplacement Deplacement { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DemandeVisa> DemandeVisa { get; set; }
    }
}
