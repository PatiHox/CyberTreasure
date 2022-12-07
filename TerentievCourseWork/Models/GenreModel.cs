using System;
using System.Collections.Generic;

namespace TerentievCourseWork.Models;

public class GenreModel
{
    public long Id { get; set; }
    public string Name { get; set; } = String.Empty;
    public string Description { get; set; } = String.Empty;
    public List<GameModel> Games { get; set; } = new();
}