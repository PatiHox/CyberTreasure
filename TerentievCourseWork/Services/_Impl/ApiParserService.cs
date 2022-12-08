using System.Collections.Generic;
using Newtonsoft.Json;
using TerentievCourseWork.Models;

namespace TerentievCourseWork.Services._Impl;

public class ApiParserService : IApiParserService
{
    public List<GenreModel> ParseGenresResponse(string json)
    {
        return JsonConvert.DeserializeObject<List<GenreModel>>(json, new JsonSerializerSettings{ NullValueHandling = NullValueHandling.Ignore }) ?? new List<GenreModel>();
    }
}