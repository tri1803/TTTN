using System.Collections.Generic;
using Temp.Service.DTO;

namespace Temp.Service.Service
{
    public interface ISaleService
    {
        IEnumerable<SaleDto> GetAll();

        void Create(SaleDto saleDto);
    }
}
