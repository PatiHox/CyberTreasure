using System.Collections.Generic;
using TerentievCourseWork.Models;

namespace TerentievCourseWork.Services;

public interface IProductDataProvider
{
    IEnumerable<GenreModel> GetGenres();
}