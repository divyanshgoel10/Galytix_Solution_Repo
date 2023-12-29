using System;
using System.Linq;

namespace Galytix.WebApi.Models
{
    public class GwpEntry
    {
        public string Country { get; set; }
        public string LineOfBusiness { get; set; }
        public double[] Values { get; set; }
        public double Y2000 { get; set; }

        public double GetAverage()
        {
            return Values.Where(v => v > 0).Average();
        }

        public static GwpEntry FromCsv(string csvLine)
        {
            try
            {
                string[] values = csvLine.Split(',');

                // Log the entire CSV line
                Console.WriteLine($"CSV Line: {csvLine}");

                // Check if there is a value at the expected index for Y2000
                if (values.Length > 13 && !string.IsNullOrWhiteSpace(values[13]))
                {
                    // Log the value being assigned to Y2000
                    Console.WriteLine($"Y2000 Value: {values[13]}");

                    return new GwpEntry
                    {
                        Country = values[0],
                        LineOfBusiness = values[3].ToLowerInvariant(),
                        Values = values.Skip(4).Select(double.Parse).ToArray(),
                        Y2000 = double.Parse(values[13]),
                        // Continue with other property assignments
                    };
                }
                else
                {
                    // Handle the case where Y2000 value is missing or empty
                    throw new FormatException("Y2000 value is missing or empty.");
                }
            }
            catch (FormatException ex)
            {
                // Log the exception or handle it appropriately
                Console.WriteLine($"Error parsing CSV line: {csvLine}");
                throw new FormatException("Error parsing CSV line. Invalid numeric value.", ex);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                Console.WriteLine($"Error parsing CSV line: {csvLine}");
                throw new FormatException("Error parsing CSV line.", ex);
            }
        }
    }
}
