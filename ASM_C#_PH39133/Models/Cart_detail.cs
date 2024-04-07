using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASM_C__PH39133.Models
{
	[Table("GioHangCT")]

	public class Cart_detail
	{
		[Key]
		public Guid IdGioHangCT { get; set; }

		[Required]
		[MaxLength(100)]
		public Guid IdGioHang { get; set; }

		[Required]
		public Guid MaSP { get; set; }

		[Required]
		public int SoLuong { get; set; }

		[ForeignKey("MaSP")]
		public virtual Products? Products { get; set; }

		[ForeignKey("IdGioHang")]
		public virtual Carts? Carts { get; set; }
	}
}
