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
                product.ProductType = (int)Common.Infrastructure.ProductType.Active;
                _unitofWork.ProductBaseService.Add(product);
                _unitofWork.Save();
            }
            else
            {
                var product = _mapper.Map<CreateProductDto, Product>(productDto);
                product.Avatar = filename;
                product.CreateDate = DateTime.Now;
                product.Status = (int) Status.Active;
                product.ProductType = (int)Common.Infrastructure.ProductType.Active;
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
            return _unitofWork.ProductBaseService.ObjectContext.FirstOrDefault(s => s.Id == id);
        }

        public CreateProductDto GetProductEditById(int id)
        {
            var product = _unitofWork.ProductBaseService.GetById(id);
            return _mapper.Map<Product, CreateProductDto>(product);
        }

        public List<ProductViewModel> GetProductNews_10()
        {
            var products = _unitofWork.ProductBaseService.ObjectContext.Include(s => s.Category).Where(s => s.Category.Name != "Phụ kiện khác").OrderByDescending(s => s.CreateDate).Take(10).ToList();
            return _mapper.Map<List<Product>, List<ProductViewModel>>(products);
        }

        public List<ProductViewModel> Get10Phukien()
        {
            var products = _unitofWork.ProductBaseService.ObjectContext.Include(s => s.Category).Where(s => s.Category.Name == "Phụ kiện khác").OrderByDescending(s => s.CreateDate).Take(10).ToList();
            return _mapper.Map<List<Product>, List<ProductViewModel>>(products);
        }

        public List<CreateProductDto> GetProductWithCate(int id)
        {
            var products = _unitofWork.ProductBaseService.ObjectContext.Where(s => s.CategoryId == id).Include(s => s.Category).ToList();
            return _mapper.Map<List<Product>, List<CreateProductDto>>(products);
        }

        public void Active(int id)
        {
            var product = _unitofWork.ProductBaseService.GetById(id);
            product.ProductType = (int)ProductType.Active;
            _unitofWork.Save();
        }

        public void InActive(int id)
        {
            var product = _unitofWork.ProductBaseService.GetById(id);
            product.ProductType = (int)ProductType.InActive;
            _unitofWork.Save();
        }

        public List<ProductViewModel> GetProductInactive()
        {
            var products = _unitofWork.ProductBaseService.ObjectContext.Where(s => s.ProductType == (int)ProductType.InActive).Include(s => s.Category).Take(10).ToList();
            return _mapper.Map<List<Product>, List<ProductViewModel>>(products);
        }
    }
}