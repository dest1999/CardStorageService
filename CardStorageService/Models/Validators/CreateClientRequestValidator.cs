using CardStorageService.Models.Requests;
using FluentValidation;

namespace CardStorageService.Models.Validators
{
    public class CreateClientRequestValidator : AbstractValidator<CreateClientRequest>
    {
        public CreateClientRequestValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.Surname).NotEmpty();
        }
    }
}
