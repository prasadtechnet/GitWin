using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfTemplator.UI.PropertyGridClass
{
  public  class DocumentGridModel
    {
        public string Template { get; set; }
        public string Name { get; set; }
        public string Namespace { get; set; }
        public string Location { get; set; }
        
    }

    public class SectionModel
    {
        public string Name { get; set; }

    }
}
