namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Order")]
    public partial class Order
    {
        public long ID { get; set; }

        public DateTime? CreatedDate { get; set; }

        public long? CustomerID { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Bạn chưa nhập tên người nhận hàng.")]
        public string ShipName { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Bạn chưa nhập số điện thoại người nhận hàng.")]
        public string ShipMobile { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Bạn chưa nhập địa chỉ người nhận hàng.")]
        public string ShipAddress { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Bạn chưa nhập email người nhận hàng.")]
        public string ShipEmail { get; set; }

        public int? Status { get; set; }
    }
}
