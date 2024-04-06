using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASM_C__PH39133.Models
{
	[Table("KhachHang")]

	public class Customers
	{
		[Key]
		public Guid IdKH { get; set; }

		[Required]
		[StringLength(15)]
		public string? DienThoai { get; set; }

		[Required]
		[StringLength(50)]
		public string? TenKH { get; set; }

		[Required]
		[StringLength(100)]
		public string? DiaChi { get; set; }

		[Required]
		[StringLength(5)]
		public string? GioiTinh { get; set; }

		[Required]
		[StringLength(30)]
		public string? MatKhau { get; set; }

		public bool IsAdmin { get; set; } = false;
		public virtual Carts? Carts { get; set; }

	}
}
