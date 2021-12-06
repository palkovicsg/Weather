using Microsoft.AspNetCore.Http;

namespace Weather.Dto
{
    public class CSVFileDto
    {
        public IFormFile File { get; set; }
    }
}
