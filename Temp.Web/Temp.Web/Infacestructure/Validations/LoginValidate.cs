using FluentValidation;
using Temp.Common.Resources;
using Temp.Service.DTO;

namespace Temp.Web.Infrastructure.Validations
{
    public class LoginValidate : AbstractValidator<LogInDto>
    {
        public LoginValidate()
        {
            RuleFor(s => s.Password).NotNull()
                .WithMessage(MessageResource.NullPassword)
                .MinimumLength(3)
                .WithMessage(MessageResource.PasswordLength)
                .MaximumLength(50);
            RuleFor(reg => reg.Username).NotNull()
                .WithMessage(MessageResource.NullUsername);         
        }
    }
}
