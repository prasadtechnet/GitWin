using PdfTemplator.PdfCore.Models.Pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PdfTemplator.PdfCore.Models
{
    
    public class XmlPdfTree
    {
        public string Type { get; set; }
        public HCF_TemplateModel Hcf { get; set; }
        public HC_TemplateModel Hc { get; set; }
        public CF_TemplateModel Cf { get; set; }
        public XmlTreeNode xmlTreeNode { get; set; }
    }

    public class XmlTreeNode
    {
        public string Name { get; set; }
        public string Text { get; set; }
        public List<XmlTreeNode> Childs { get; set; }
    }
}
