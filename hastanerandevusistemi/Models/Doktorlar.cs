using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hastanerandevusistemi.Models
{
    [Table("Doktorlar")]
    public class Doktorlar
    {
        [Key]
        public int DoktorID { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "Bu alan zorunludur.")]
        public string klinik { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "Bu alan zorunludur.")]      
        public string isim { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "Bu alan zorunludur.")]
        public string durum { get; set; }

    }
}
