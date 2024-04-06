using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASM_C__PH39133.Models
{
	[Table("DanhMuc")]

	public class Categories
	{
		[Key]
		public Guid IdDanhMuc { get; set; }

		[Required]
		[MaxLength(100)]
		public string? TenDanhMuc { get; set; }

		[Required]
		public string? MoTa { get; set; }

		public virtual ICollection<Products>? Products { get; set; }

	}
}
