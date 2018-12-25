namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Slide")]
    public partial class Slide
    {
        public int ID { get; set; }

        [StringLength(250)]
        public string Image { get; set; }

        public int? DisplayOrder { get; set; }

        [StringLength(250)]
        public string Link { get; set; }

        [StringLength(50)]
        public string Description { get; set; }

        public DateTime? CreateDate { get; set; }

        [StringLength(50)]
        public string CreateBy { get; set; }

        public DateTime? ModifiledDate { get; set; }

        [StringLength(50)]
        public string ModifiledBy { get; set; }

        public bool? Status { get; set; }
    }
}
