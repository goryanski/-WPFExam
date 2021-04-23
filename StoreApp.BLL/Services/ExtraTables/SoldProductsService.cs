﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreApp.BLL.DTO.ExtraTables;
using StoreApp.BLL.Interfaces;
using StoreApp.DAL.Entities.ExtraTables;
using StoreApp.DAL.Interfaces;

namespace StoreApp.BLL.Services.ExtraTables
{
    public class SoldProductsService : ISoldProductsService
    {
        private IUnitOfWork uow;
        private Automapper.ObjectMapper objectMapper = Automapper.ObjectMapper.Instance;

       
        public SoldProductsService(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public async Task CreateProduct(SoldProductDTO product)
        {
            await Task.Run(async () =>
            {
                var result = objectMapper.Mapper.Map<SoldProduct>(product);
                await uow.SoldProductsRepository.Create(result);
            });
        }

        public async Task<SoldProductDTO> GetProductById(int id)
        {
            var result = await uow.SoldProductsRepository.Get(id);
            return objectMapper.Mapper.Map<SoldProductDTO>(result);
        }

        public async Task<List<SoldProductDTO>> GetAllProducts()
        {
            var result = await uow.SoldProductsRepository.GetAll();
            return objectMapper.Mapper.Map<List<SoldProductDTO>>(result);
        }

        public async Task<List<SoldProductDTO>> GetLastProducts()
        {
            var products = await uow.SoldProductsRepository.GetAll();
            var result = products.OrderByDescending(p => p.SoldDate)
                .Take(15)
                .ToList();

            return objectMapper.Mapper.Map<List<SoldProductDTO>>(result);
        }

        public async Task<List<SoldProductDTO>> GetProductsByRange(DateTime dateFrom, DateTime dateTo)
        {
            var result = await uow.SoldProductsRepository.GetAll(p => p.SoldDate >= dateFrom && p.SoldDate <= dateTo);
            return objectMapper.Mapper.Map<List<SoldProductDTO>>(result);
        }
    }
}
