using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfTemplator.PdfCore.Models
{
   public class TemplateResponse
    {
        public bool  Status { get; set; }
        public string Message { get; set; }

        public string PdfTemplateFileName { get; set; }
        public string PdfTemplateFileContent { get; set; }

        public string PdfModelsFileName { get; set; }
        public string PdfModelsFileContent { get; set; }

    }

    public class ResponseModel
    {
        public bool Status { get; set; }
        public string Message { get; set; }
    }
}
