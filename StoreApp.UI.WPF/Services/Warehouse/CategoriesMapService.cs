using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using StoreApp.BLL.DTO.Warehouse;
using StoreApp.UI.WPF.Helpers;
using StoreApp.UI.WPF.Models.Warehouse;

namespace StoreApp.UI.WPF.Services.Warehouse
{
    public class CategoriesMapService
    {
        private Automapper.ObjectMapper objectMapper = Automapper.ObjectMapper.Instance;
        BLLServicesStorage services = BLLServicesStorage.Instance;

        public void CreateCategory(CategoryUI categoryUI)
        {
            CategoryDTO categorytDTO = objectMapper.Mapper.Map<CategoryDTO>(categoryUI);
            services.CategoriesService.CreateCategory(categorytDTO);
        }

        public async Task<CategoryUI> GetCategoryById(int id)
        {
            var result = await services.CategoriesService.GetCategoryById(id);
            return objectMapper.Mapper.Map<CategoryUI>(result);
        }

        public async Task<List<CategoryUI>> GetAllCategories()
        {
            var result = await services.CategoriesService.GetAllCategories();
            return objectMapper.Mapper.Map<List<CategoryUI>>(result);
        }
    }
}
