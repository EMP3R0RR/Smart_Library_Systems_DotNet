using AutoMapper;
using LibraryBLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryDAL.EF;
using LibraryDAL.Repos;

namespace LibraryBLL.Services
{
    public class CategoryServices
    {
        public static List<CategoryDTO> GetAll()
        {
            var repo = new CategoryRepo();
            var categories = repo.GetAll();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Category, CategoryDTO>();
            });
            var mapper = new Mapper(config);

            return mapper.Map<List<CategoryDTO>>(categories);
        }
    }
}
