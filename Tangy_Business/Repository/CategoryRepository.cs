using AutoMapper;
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

        public CategoryDTO CreateCategory(CategoryDTO categoryDTO)
        {
            var category = _mapper.Map<CategoryDTO, Category>(categoryDTO);
            category.CreatedDate = DateTime.UtcNow;
            var addedObj = _dbContext.Categories.Add(category);
            _dbContext.SaveChanges();
            return _mapper.Map<Category, CategoryDTO>(addedObj.Entity);
        }

        public int DeleteCategory(int Id)
        {
            var category = _dbContext.Categories.FirstOrDefault(x => x.Id == Id);
            if (category != null)
            {
                var deletedObj = _dbContext.Categories.Remove(category);
                return _dbContext.SaveChanges();
            }
            return 0;
        }

        public IEnumerable<CategoryDTO> GetAllCategories()
        {
            return _mapper.Map<IEnumerable<Category>, IEnumerable<CategoryDTO>>(_dbContext.Categories);
        }

        public CategoryDTO GetCategoryById(int Id)
        {
            var category= _dbContext.Categories.FirstOrDefault(x => x.Id == Id);
            if(category != null)
            {
                return _mapper.Map<Category,CategoryDTO>(category);
            }
         return new CategoryDTO();
        }

        public CategoryDTO UpdateCategory(CategoryDTO categoryDTO)
        {
            var category=_dbContext.Categories.FirstOrDefault(x => x.Id == categoryDTO.Id);
            if(category != null)
            {
                _dbContext.Categories.Update(_mapper.Map<CategoryDTO,Category>(categoryDTO));
                _dbContext.SaveChanges();
                return _mapper.Map<Category, CategoryDTO>(category);
            }

            return new CategoryDTO();
        }
    }
}
