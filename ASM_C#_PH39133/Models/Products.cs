using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace ASM_C__PH39133.Models
{
	[Table("SanPham")]
	public class Products
	{
		[Key]
		public Guid MaSP { get; set; }

		[Required]
		[MaxLength(50)]
		public string? TenSP { get; set; }

		[Required]
		[MaxLength(20)]
		public string? IMEI { get; set; }

		[Required]
		public string? HinhAnh { get; set; }

		[Required]
		public int SoLuong { get; set; }

		[Required]
		[Range(0, double.MaxValue)]
		public double DonGia{ get; set; }

		[Required]
		public string? Mota{ get; set; }


		public Guid IdDanhMuc { get; set; }

		[ForeignKey("IdDanhMuc")]
		public virtual Categories? Categories { get; set; }


	}
}
