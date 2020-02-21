using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Temp.Common.Infrastructure;
using Temp.DataAccess.Data;
using Temp.Service.BaseService;
using Temp.Service.DTO;

namespace Temp.Service.Service
{
    public class ProductService : IProductService
    {
        private readonly IUnitofWork _unitofWork;
        private readonly IMapper _mapper;
        private readonly IHostingEnvironment _hostingEnvironment;

        public ProductService(IUnitofWork unitofWork, IMapper mapper, IHostingEnvironment hostingEnvironment)
        {
            _unitofWork = unitofWork;
            _mapper = mapper;
            _hostingEnvironment = hostingEnvironment;
        }
        
        public IEnumerable<ProductDto> GetAllActive()
        {
            var products = _unitofWork.ProductBaseService.ObjectContext.Where(s => s.Status == (int)Status.Active).Include(s => s.Category).Include(s => s.Nsx).AsEnumerable();
            return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(products);
        }

        public IEnumerable<ProductDto> GetAllInActive()
        {
            var products = _unitofWork.ProductBaseService.ObjectContext.Where(s => s.Status == (int)Status.InActive).AsEnumerable();
            return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(products);
        }

        public void Save(CreateProductDto productDto, IFormFile AvataPath)
        {
            string uploadFile = Path.Combine(_hostingEnvironment.WebRootPath, "img");
            string filename = Guid.NewGuid().ToString() + " " + AvataPath.FileName;
            string path = Path.Combine(uploadFile, filename);
            AvataPath.CopyTo(new FileStream(path, FileMode.Create));

            if (productDto.Id <= 0)
            {
                var product = _mapper.Map<CreateProductDto, Product>(productDto);
                product.Avatar = filename;
                product.CreateDate = DateTime.Now;
                product.Status = (int) Status.Active;
                _unitofWork.ProductBaseService.Add(product);
                _unitofWork.Save();
            }
            else
            {
                var product = _mapper.Map<CreateProductDto, Product>(productDto);
                product.Avatar = filename;
                product.CreateDate = DateTime.Now;
                product.Status = (int) Status.Active;
                _unitofWork.ProductBaseService.Update(product);
                _unitofWork.Save();
            }
            
        }

        public void Delete(int id)
        {
            var product = _unitofWork.ProductBaseService.GetById(id);
            _unitofWork.ProductBaseService.Delete(product);
            _unitofWork.Save();
        }

        public Product GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public CreateProductDto GetProductEditById(int id)
        {
            var product = _unitofWork.ProductBaseService.GetById(id);
            return _mapper.Map<Product, CreateProductDto>(product);
        }

        public List<ProductViewModel> GetProductNews_10()
        {
            var products = _unitofWork.ProductBaseService.ObjectContext.OrderByDescending(s => s.CreateDate).Take(10).ToList();
            return _mapper.Map<List<Product>, List<ProductViewModel>>(products);
        }
    }
}