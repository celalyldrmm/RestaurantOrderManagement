using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRWebUI.Dtos.BookingDtos
{
    public class CreateBookingDto
    {
        [Required(ErrorMessage = "Adınız ve soyadınız zorunludur!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Telefon numarası zorunludur!")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Geçerli bir telefon numarası giriniz!")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Mail adresi zorunludur!")]
        [EmailAddress(ErrorMessage = "Geçerli bir mail adresi giriniz!")]
        public string Mail { get; set; }

        [Required(ErrorMessage = "Geçerli bir kişi sayısı seçiniz!")]
        public int? PersonCount { get; set; }

        [Required(ErrorMessage = "Geçerli bir tarih seçiniz!")]
        [DataType(DataType.Date)]
        public DateTime? Date { get; set; }
        public string Description { get; set; }

    }
}
