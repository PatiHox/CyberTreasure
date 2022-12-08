using System.Collections.Generic;
using TerentievCourseWork.Models;

namespace TerentievCourseWork.Services;

public interface IApiParserService
{
    List<GenreModel> ParseGenresResponse(string json);
}