using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hastanerandevusistemi.Models
{
    [Table("RandevuAl")]
    public class RandevuAl
    {
        [Key]
        public int randID { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "Bu alan zorunludur.")]
        public string randklinik { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "Bu alan zorunludur.")]
        public string randhekim { get; set; }

        [Required(ErrorMessage = "Bu alan zorunludur.")]
        [DataType(DataType.Date)]
        public DateTime randtarih { get; set; }

        [StringLength(80)]
        [Required(ErrorMessage = "Bu alan zorunludur.")]
        public string randsaat { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "Bu alan zorunludur.")]
        public string randsahip { get; set; }
    }
}
