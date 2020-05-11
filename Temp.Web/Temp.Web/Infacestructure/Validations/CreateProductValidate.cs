using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Temp.Service.DTO;

namespace Temp.Web.Infacestructure.Validations
{
    public class CreateProductValidate : AbstractValidator<CreateProductDto>
    {
        public CreateProductValidate()
        {
            RuleFor(reg => reg.Name).NotNull()
                .WithMessage("Vui lòng nhập tên sản phẩm");
            RuleFor(reg => reg.Amount).NotNull()
                 .WithMessage("Vui lòng nhập số lượng sản phẩm");
            RuleFor(reg => reg.Amount).NotNull()
                 .WithMessage("Vui lòng nhập giá sản phẩm");
        }
    }
}
