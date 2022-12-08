using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Resources;
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
        var uri = new Uri("pack://application:,,,/genres.json", UriKind.RelativeOrAbsolute);
        StreamResourceInfo streamResourceInfo = Application.GetResourceStream(uri) ?? throw new InvalidOperationException();
        StreamReader streamReader = new StreamReader(streamResourceInfo.Stream);
        var genres = await streamReader.ReadToEndAsync();
        
        
        var parsedList = _apiParserService.ParseGenresResponse(genres);
        
        return parsedList;
    }
}