using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASM_C__PH39133.Models
{
	[Table("GioHang")]

	public class Carts
	{
		[Key]
		public Guid IdGioHang { get; set; }

		[Required]
		public Guid IDKH { get; set; }

		[ForeignKey("IDKH")]
		public virtual Customers? Customers { get; set; }
		public virtual ICollection<Cart_detail>? Cart_Details { get; set; }


	}
}
