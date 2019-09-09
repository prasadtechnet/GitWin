using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfTemplator.UI.Master
{
    public static class Master
    {
       
        private static Dictionary<string, int> AlignmentDictionary = new Dictionary<string, int>
        {
            { "ALIGN_LEFT",0},
            { "ALIGN_CENTER",1},
            { "ALIGN_RIGHT",2},
            { "ALIGN_TOP",4},
            { "ALIGN_MIDDLE",5},
            { "ALIGN_BOTTOM",6}
        };
        public static List<string> GetSections(string template)
        {
            var lsSections = new List<string>();

            switch (template.ToUpper())
            {
                case "CUSTOM":
                    lsSections.Add("Content");                          
                    break;
                case "HCF":
                    lsSections.Add("PageHeader");
                    lsSections.Add("Content");
                    lsSections.Add("PageFooter");
                    break;
                case "HC":
                    lsSections.Add("PageHeader");
                    lsSections.Add("Content");
                    break;
                case "CF":
                    lsSections.Add("Content");
                    lsSections.Add("PageFooter");
                    break;
            }

            return lsSections;
        }

        public static List<string> DirectAddingControls()
        {
            return new List<string> {
                "Row",
                "EmptyCell"               
            };
        }

        public static List<string> GetAlignments()
        {
          
            return new List<string> {
                "ALIGN_LEFT",
                "ALIGN_CENTER",
                "ALIGN_RIGHT",
                "ALIGN_TOP",
                "ALIGN_MIDDLE",
                "ALIGN_BOTTOM"
            };
        }

        public static int GetAlignmentNumber(string Name)
        {
            return AlignmentDictionary[Name];
        }
    }
}
