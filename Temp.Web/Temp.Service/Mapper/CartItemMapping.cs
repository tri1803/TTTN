using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Temp.DataAccess.Data;
using Temp.Service.DTO;

namespace Temp.Service.Mapper
{
    public class CartItemMapping : Profile
    {
        public CartItemMapping()
        {
            CreateMap<CartDetail, CartItemDto>();

            CreateMap<CartItemDto, CartDetail> ();
        }
    }
}
