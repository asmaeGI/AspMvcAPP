namespace Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DemandeVisa")]
    public partial class DemandeVisa
    {
        public int Id { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Date { get; set; }

        [StringLength(50)]
        public string Status { get; set; }

        public int? Dure { get; set; }

        [StringLength(50)]
        public string Observation { get; set; }

        public int? IdC { get; set; }

        public virtual Colaborateur Colaborateur { get; set; }
    }
}
