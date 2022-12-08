using System.Collections.Generic;
using System.Collections.ObjectModel;
using TerentievCourseWork.Models;

namespace TerentievCourseWork.Services;

public interface IProductDataProvider
{
    ObservableCollection<GenreModel> Genres { get; }
}