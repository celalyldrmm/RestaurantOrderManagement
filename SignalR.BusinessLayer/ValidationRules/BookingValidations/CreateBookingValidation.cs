using FluentValidation;
using SignalR.DtoLayer.BookingDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.BusinessLayer.ValidationRules.BookingValidations
{
    public class CreateBookingValidation : AbstractValidator<CreateBookingDto>
    {
        public CreateBookingValidation()
        {

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("İsim alanı boş geçilemez!")
                .MinimumLength(5).WithMessage("Lütfen isim alanına en az 5 karakter giriniz.")
                .MaximumLength(50).WithMessage("Lütfen isim alanına en fazla 50 karakter giriniz.");


            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Telefon alanı boş geçilemez!");


            RuleFor(x => x.Mail)
                .NotEmpty().WithMessage("Mail alanı boş geçilemez!")
                .EmailAddress().WithMessage("Lütfen geçerli bir email adresi giriniz.");


            RuleFor(x => x.PersonCount)
                .NotEmpty().WithMessage("Kişi sayısı alanı boş geçilemez!")
                .GreaterThan(0).WithMessage("Kişi sayısı pozitif bir değer olmalıdır.");


            RuleFor(x => x.Date)
                .NotEmpty().WithMessage("Tarih alanı boş bırakılamaz!")
                .GreaterThanOrEqualTo(DateTime.Now.Date).WithMessage("Tarih bugünden önce olamaz!");



            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Lütfen açıklama alanına en fazla 500 karakter giriniz.");
        }

    }
}
