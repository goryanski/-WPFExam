using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using StoreApp.BLL.DTO.Warehouse;

namespace StoreApp.BLL.Interfaces
{
    public interface ICategoriesService
    {
        void CreateCategory(CategoryDTO category);
        Task<CategoryDTO> GetCategoryById(int id);
    }
}
