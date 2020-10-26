using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ImportFile.Api.Inventory
{
    [ApiController]
    [Route("inventory")]
    public class ImportCsvFile : BaseAsyncEndpoint
    {
        public ImportCsvFile()
        {
        }

        [Route("import/csv")]
        public IActionResult ImportCsvFileAction([FromBody] Input input)
        {
            return Accepted();
        }

        public class Input
        {
            [Required]
            public string FileUrl { get; set; }

            public bool ContainsHeader { get; set; } = true;
        }
    }
}
