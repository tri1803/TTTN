using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Temp.DataAccess.Data;
using Temp.Service.DTO;

namespace Temp.Service.Service
{
    public interface IProductService
    {
        /// <summary>
        /// get all list user
        /// </summary>
        /// <returns></returns>
        IEnumerable<ProductDto> GetAllActive();

        IEnumerable<ProductDto> GetAllInActive();
        /// <summary>
        /// create or edit user
        /// </summary>
        /// <param name="userDto"></param>
        void Save(CreateProductDto productDto, IFormFile AvataPath);
        
        /// <summary>
        /// delete user
        /// </summary>
        void Delete(int id);
        
        /// <summary>
        /// get account by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Product GetById(int id);

        CreateProductDto GetProductEditById(int id);

        List<ProductViewModel> GetProductNews_10();

        List<ProductViewModel> Get10Phukien();

        List<CreateProductDto> GetProductWithCate(int id);
    }
}