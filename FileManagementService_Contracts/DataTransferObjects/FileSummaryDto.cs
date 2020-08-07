using System;
using System.Collections.Generic;
using System.Text;

namespace RedSky.FileManagement.Contracts.DataTransferObjects
{
    public class FileSummaryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public DateTime Uploaded { get; set; }
        public string downloadLink { get; set; }
    }
}
