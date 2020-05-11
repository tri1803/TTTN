using FluentValidation;
using Temp.Common.Resources;
using Temp.Service.DTO;

namespace Temp.Web.Infacestructure.Validations
{
    public class CreateAccValidate : AbstractValidator<CreateAccDto>
    {
        public CreateAccValidate()
        {
            RuleFor(reg => reg.Username).NotNull()
                .WithMessage(MessageResource.NullUsername);
            RuleFor(reg => reg.Password).NotNull()
                .WithMessage(MessageResource.NullPassword)
                .MinimumLength(6)
                .WithMessage(MessageResource.PasswordLength);
            RuleFor(reg => reg.ConfirmPass).NotNull()
                .WithMessage(MessageResource.NullPassword)                          
                .Equal(reg => reg.Password).WithMessage(MessageResource.Compare);
        }
    }
}
