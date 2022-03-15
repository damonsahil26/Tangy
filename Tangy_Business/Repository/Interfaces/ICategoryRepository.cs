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
        public CategoryDTO CreateCategory(CategoryDTO categoryDTO);
        public CategoryDTO UpdateCategory(CategoryDTO categoryDTO);
        public int DeleteCategory(int Id);
        public IEnumerable<CategoryDTO> GetAllCategories();
        public CategoryDTO GetCategoryById(int Id);
    }
}
