using System.Collections.Generic;
using TerentievCourseWork.Models;

namespace TerentievCourseWork.Services._Impl;

public class ProductDataProvider : IProductDataProvider
{
    public IEnumerable<GenreModel> GetGenres()
    {
        return new List<GenreModel>
        {
            new() { Name = "Action" },
            new() { Name = "Adventure" },
            new() { Name = "RPG" },
        };
    }
}