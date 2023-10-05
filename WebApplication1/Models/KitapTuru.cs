using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class KitapTuru
    {
        [Key]   //Primary Key
        public int Id { get; set; }

        [Required(ErrorMessage ="Kitap Türü Adı Boş Bırakılamaz!")]   //Not null
        [MaxLength(25)]
        [DisplayName("Kitap Türü Adı")]
        public string Ad { get; set; }
    }
}
