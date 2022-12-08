using System.Collections.Generic;
using System.Threading.Tasks;
using TerentievCourseWork.Models;

namespace TerentievCourseWork.Services;

public interface IWebRequestService
{
    Task<List<GenreModel>> GetGenres();
}