namespace Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Deplacement")]
    public partial class Deplacement
    {
        public int Id { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Date { get; set; }

        public double? Cout { get; set; }

        [StringLength(50)]
        public string Observation { get; set; }

        public int? IdC { get; set; }

        public virtual Colaborateur Colaborateur { get; set; }
    }
}
