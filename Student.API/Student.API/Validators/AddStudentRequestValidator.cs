using FluentValidation;
using Student.API.DomainModels;
using Student.API.Repositories;

namespace Student.API.Validators
{
	public class AddStudentRequestValidator : AbstractValidator<AddStudentRequest>
	{
		public AddStudentRequestValidator(IStudentRepository studentRepository)
		{
			RuleFor(x => x.FirstName).NotEmpty();
			RuleFor(x => x.LastName).NotEmpty();
			RuleFor(x => x.DateOfBirth).NotEmpty();
			RuleFor(x => x.Email)
				.NotEmpty()
				.EmailAddress();
			RuleFor(x => x.Mobile).NotEmpty();
			RuleFor(x => x.GenderId).NotEmpty().Must(id =>
			{
				var gender = studentRepository.GetGendersAsync().Result.ToList().FirstOrDefault(x => x.Id == id);
				if (gender != null) { 
				return true;
				}
				return false;




			}).WithMessage("Lütfen geçerli bir cinsiyet seçin.");
			RuleFor(x=> x.PostalAdress).NotEmpty();
			RuleFor(x=> x.PhysicalAdress).NotEmpty();


		}
	}
}
		
