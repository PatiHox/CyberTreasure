using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TerentievCourseWork.Models;

namespace TerentievCourseWork.Services._Impl;

public class WebRequestService : IWebRequestService
{
    private const string ApiUrl = "http://localhost:5000/api/";
    private IApiParserService _apiParserService;
    
    public WebRequestService(IApiParserService apiParserService)
    {
        _apiParserService = apiParserService;
    }

    public async Task<List<GenreModel>> GetGenres()
    {
        string genres;
        // var uri = new Uri("pack://application:,,,/genres.json", UriKind.RelativeOrAbsolute);
        // StreamResourceInfo streamResourceInfo = Application.GetResourceStream(uri) ?? throw new InvalidOperationException();
        await using (Stream fs = File.OpenRead(@"C:\Users\PatiBook\RiderProjects\TerentievCourseWork\TerentievCourseWork\genres.json"))
        {
            var streamReader = new StreamReader(fs);
            genres = await streamReader.ReadToEndAsync();
        }

        var parsedList = _apiParserService.ParseGenresResponse(genres);
        
        return parsedList;
    }
}