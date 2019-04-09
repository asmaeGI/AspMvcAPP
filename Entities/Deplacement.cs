namespace Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Runtime.Serialization;

    [DataContract]
    [Table("Deplacement")]
    public partial class Deplacement
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Deplacement()
        {
            Colaborateur = new HashSet<Colaborateur>();
        }
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        [Required(ErrorMessage = "Ce champ est obligatoire")]
        [Column(TypeName = "date")]
        public DateTime? Date { get; set; }
        [DataMember]
        [Required(ErrorMessage = "Ce champ est obligatoire")]
        public double? Cout { get; set; }
        [DataMember]
        [Required(ErrorMessage = "Ce champ est obligatoire")]
        [StringLength(50)]
        public string Observation { get; set; }

        [DataMember]
        public virtual ICollection<Colaborateur> Colaborateur { get; set; }
    }
}
