using System.ComponentModel.DataAnnotations;

namespace hastanerandevusistemi.Models
{
    public class HastaneClass
    {
        [Key]
        public int hastid { get; set; }
        public string? hastil { get; set; }
        public string? hastilce { get; set; }
        public string? hastisim { get; set; }

    }
}
