using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hastanerandevusistemi.Models
{
    [Table("Randevular")]
    public class Randevular
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
        public DateTime randtarih { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "Bu alan zorunludur.")]
        public string randsahip { get; set; }


    }
}
