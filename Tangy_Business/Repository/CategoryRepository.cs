using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tangy_Business.Repository.Interfaces;
using Tangy_DataAccess;
using Tangy_DataAccess.Data;
using Tangy_Models.DTO;

namespace Tangy_Business.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public CategoryRepository(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<CategoryDTO> CreateCategory(CategoryDTO categoryDTO)
        {
            var category = _mapper.Map<CategoryDTO, Category>(categoryDTO);
            category.CreatedDate = DateTime.UtcNow;
            var addedObj = _dbContext.Categories.Add(category);
            await _dbContext.SaveChangesAsync();
            return _mapper.Map<Category, CategoryDTO>(addedObj.Entity);
        }

        public async Task<int> DeleteCategory(int Id)
        {
            var category = await _dbContext.Categories.FirstOrDefaultAsync(x => x.Id == Id);
            if (category != null)
            {
                var deletedObj = _dbContext.Categories.Remove(category);
                return await _dbContext.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<IEnumerable<CategoryDTO>> GetAllCategories()
        {
            return _mapper.Map<IEnumerable<Category>, IEnumerable<CategoryDTO>>(_dbContext.Categories);
        }

        public async Task<CategoryDTO> GetCategoryById(int Id)
        {
            var category= await _dbContext.Categories.FirstOrDefaultAsync(x => x.Id == Id);
            if(category != null)
            {
                return _mapper.Map<Category,CategoryDTO>(category);
            }
         return new CategoryDTO();
        }

        public async Task<CategoryDTO> UpdateCategory(CategoryDTO categoryDTO)
        {
            var category=await _dbContext.Categories.FirstOrDefaultAsync(x => x.Id == categoryDTO.Id);
            if(category != null)
            {   
                category.Name = categoryDTO.Name;
                _dbContext.Categories.Update(category);
                await _dbContext.SaveChangesAsync();
                return _mapper.Map<Category, CategoryDTO>(category);
            }

            return new CategoryDTO();
        }
    }
}
