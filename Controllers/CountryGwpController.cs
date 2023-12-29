using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Galytix.WebApi.Models; // Add the correct namespace for CountryGwpRequest and GwpEntry
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Galytix.WebApi.Controllers
{
    [ApiController]
    [Route("api/gwp")]
    public class CountryGwpController : ControllerBase
    {
        private readonly ILogger<CountryGwpController> _logger;

        public CountryGwpController(ILogger<CountryGwpController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        [Route("avg")]
        public async Task<IActionResult> CalculateAverage([FromBody] CountryGwpRequest request)
{
    try
    {
        // Load data from CSV file
        var data = await LoadDataFromCsv("Data/gwpByCountry.csv");

        // Filter data based on the request
        var filteredData = data
            .Where(entry =>
                entry.Country == request.Country &&
                request.lob.Contains(entry.LineOfBusiness.ToLowerInvariant()))
            .ToList();

        // Calculate average for each line of business
        var result = new Dictionary<string, double>();
        foreach (var lob in request.lob)
        {
            var total = filteredData
                .Where(entry => entry.LineOfBusiness.ToLowerInvariant() == lob)
                .Sum(entry => entry.GetAverage());

            result.Add(lob, total / filteredData.Count);
        }

        return Ok(result);
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, "An error occurred in CalculateAverage");
        return BadRequest($"An error occurred: {ex.Message}");
    }
}
        private async Task<List<GwpEntry>> LoadDataFromCsv(string filePath)
        {
            var lines = await System.IO.File.ReadAllLinesAsync(filePath);
            return lines.Skip(1).Select(line => GwpEntry.FromCsv(line)).ToList();
        }
    }
}
