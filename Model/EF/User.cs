namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User
    {
        public long ID { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage ="Vui lòng nhập UserName của bạn.")]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        [StringLength(32)]
        [Required(ErrorMessage = "Vui lòng nhập Password của bạn.")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password")]
        [DisplayName("Xác nhận mật khẩu.")]
        public string ConfirmPassword { get; set; }

        [StringLength(50)]
        public string Salt { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Vui lòng nhập họ và tên của bạn.")]
        public string Name { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Vui lòng nhập địa chỉ của bạn.")]
        public string Address { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Vui lòng nhập email của bạn.")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Làm ơn nhập đúng email của bạn.")]
        public string Email { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Vui lòng nhập số điện thoại của bạn.")]
        public string Phone { get; set; }

        public DateTime? CreateDate { get; set; }

        [StringLength(50)]
        public string CreateBy { get; set; }

        public DateTime? ModifiledDate { get; set; }

        [StringLength(50)]
        public string ModifiledBy { get; set; }

        public bool? Status { get; set; }
    }
}
