using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CodingTest.Models
{
    public class FileModel
    {
    
        public IFormFile File { get; set; }

    }
}
