using CulinaryBlog.Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CulinaryBlog.Domain.Models;
using CulinaryBlog.DAL.Storage;

namespace CulinaryBlog.Service.Interfaces
{
    public interface ICategoriesService
    {
        BaseResponse<List<Categories>> GetAllCategories();
    }
}
