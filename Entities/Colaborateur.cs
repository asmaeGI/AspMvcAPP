namespace Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    
    [Table("Colaborateur")]
    public partial class Colaborateur
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Colaborateur()
        {
            DemandeVisa = new HashSet<DemandeVisa>();
            Deplacement = new HashSet<Deplacement>();
        }
        
        public int Id { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Ce champ est obligatoire")]
        public string Nom { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Ce champ est obligatoire")]
        public string Prenom { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Ce champ est obligatoire")]
        public string Cin { get; set; }

        [StringLength(50)]
        [EmailAddress(ErrorMessage = "Format d'adress mail est invalide")]
        [Required(ErrorMessage = "Ce champ est obligatoire")]
        public string Email { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Ce champ est obligatoire")]
        public string Poste { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Ce champ est obligatoire")]
        public string Equipe { get; set; }

        [Required(ErrorMessage = "Ce champ est obligatoire")]
        public int? NombreDeplacement { get; set; }

        [Required(ErrorMessage = "Ce champ est obligatoire")]
        public int? Anciennete { get; set; }

        [Column(TypeName = "date")]
        [Required(ErrorMessage = "Ce champ est obligatoire")]
        public DateTime? DateValiditeVisa { get; set; }

        [Column(TypeName = "date")]
        [Required(ErrorMessage = "Ce champ est obligatoire")]
        public DateTime? DateFinVisa { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Ce champ est obligatoire")]
        public string Sexe { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Ce champ est obligatoire")]
        public string Login { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Ce champ est obligatoire")]

        public string Password { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DemandeVisa> DemandeVisa { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Deplacement> Deplacement { get; set; }
    }
}
