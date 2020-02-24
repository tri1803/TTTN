
using AutoMapper;
using System.Collections.Generic;
using Temp.Service.BaseService;
using Temp.Service.DTO;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using Temp.DataAccess.Data;

namespace Temp.Service.Service
{
    public class SaleService : ISaleService
    {
        private readonly IUnitofWork _unitofWork;
        private readonly IMapper _mapper;

        public SaleService(IUnitofWork unitofWork, IMapper mapper)
        {
            _unitofWork = unitofWork;
            _mapper = mapper;
        }
        public void Create(SaleDto saleDto)
        {
            var sale = _mapper.Map<SaleDto, Sale>(saleDto);
            sale.CreateDate = DateTime.Now;
            _unitofWork.SaleBaseService.Add(sale);
            _unitofWork.Save();
        }

        public IEnumerable<SaleDto> GetAll()
        {
            var sales = _unitofWork.SaleBaseService.ObjectContext.Where(s => s.EndDate >= DateTime.Now).AsEnumerable();
            return _mapper.Map<IEnumerable<Sale>, IEnumerable<SaleDto>>(sales);
        }
    }
}
