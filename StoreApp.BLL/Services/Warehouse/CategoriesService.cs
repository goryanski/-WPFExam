using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using StoreApp.BLL.DTO.Warehouse;
using StoreApp.BLL.Interfaces;
using StoreApp.DAL.Entities.Warehouse;
using StoreApp.DAL.Interfaces;

namespace StoreApp.BLL.Services.Warehouse
{
    public class CategoriesService : ICategoriesService
    {
        private IUnitOfWork uow;
        private Automapper.ObjectMapper objectMapper = Automapper.ObjectMapper.Instance;

        public CategoriesService(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public void CreateCategory(CategoryDTO category)
        {
            Task.Run(async () =>
            {
                var result = objectMapper.Mapper.Map<Category>(category);
                await uow.CategoriesRepository.Create(result);
            });
        }

        public async Task<CategoryDTO> GetCategoryById(int id)
        {
            var result = await uow.CategoriesRepository.Get(id);
            return objectMapper.Mapper.Map<CategoryDTO>(result);
        }

        public async Task<List<CategoryDTO>> GetAllCategories()
        {
            var result = await uow.CategoriesRepository.GetAll();
            return objectMapper.Mapper.Map<List<CategoryDTO>>(result);
        }
    }
}
