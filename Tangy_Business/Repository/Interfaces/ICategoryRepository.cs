using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tangy_Models.DTO;

namespace Tangy_Business.Repository.Interfaces
{
    public interface ICategoryRepository
    {
        public Task<CategoryDTO> CreateCategory(CategoryDTO categoryDTO);
        public Task<CategoryDTO> UpdateCategory(CategoryDTO categoryDTO);
        public Task<int> DeleteCategory(int Id);
        public Task<IEnumerable<CategoryDTO>> GetAllCategories();
        public Task<CategoryDTO> GetCategoryById(int Id);
    }
}
