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
    [Table("DemandeVisa")]
    public partial class DemandeVisa
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        [Required(ErrorMessage = "Ce champ est obligatoire")]
        [Column(TypeName = "date")]
        public DateTime Date { get; set; }
        [DataMember]
        [Required(ErrorMessage = "Ce champ est obligatoire")]
        [StringLength(50)]
        public string Status { get; set; }
        [DataMember]
        [RequiredIf("Status", "Acceptee", ErrorMessage = "Ce champ est obligatoire")]
        public DateTime? DateValiditeVisa { get; set; }
        [DataMember]
        [RequiredIf("Status", "Acceptee", ErrorMessage = "Ce champ est obligatoire")]
        public DateTime? DateFinVisa { get; set; }
        [DataMember]
        [RequiredIf("Status", "Refusee", ErrorMessage = "Ce champ est obligatoire")]
        [StringLength(500)]
        public string Observation { get; set; }
        [DataMember]

        public int? IdC { get; set; }
                [DataMember]

        public virtual Colaborateur Colaborateur { get; set; }
    }
}
