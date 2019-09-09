using PdfTemplator.UI.Models;
using PdfTemplator.UI.PropertyGridClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PdfTemplator.UI.Business
{
   public class HTMLPreviewModule
    {
        string strHtmlTemplate = "<html><head></head><body>{0}</body></html>";
        private static Dictionary<string, string> htmlTagDictionary = new Dictionary<string, string>
        {
            { "SECTION","<div >#Val#</div>"},
            { "TABLE","<table #attr# style='border:1px solid #272727' >#Val#</table>"},
            { "ROW","<tr #attr# >#Val#<tr>"},
            { "CELL","<td #attr# style='border:1px solid #655678'>#Val#</td>"},
            { "LABEL","<span #attr# >#Val#</span>"},
            { "FIELD","<label #attr#> #Val#></label>"},
            { "IMAGE","<img #attr# />"},
            { "IMAGESUB","<div><label #lattr# #lVal#></label><img #attr# /></div>"},
            { "EMPTY","<span #attr# >&nbsp;</span>"}
        };

        public string GetHTMLTemplate(TreeView tvSource)
        {
            var strRes = "";
            try
            {
                var lsTables = new List<string>();

                foreach (TreeNode item in tvSource.Nodes[0].Nodes)
                {
                    lsTables.Add(GetHtmlEqualent(item));
                }

                strRes = string.Format(strHtmlTemplate, String.Join("",lsTables.ToArray()));
            }
            catch (Exception ex)
            {
            }
            return strRes;
        }



        private string GetHtmlEqualent(TreeNode tN)
        {
            var strRes = "";
            if(tN.Tag!=null)
            {
                var ctrl = tN.Tag as ControlPropertyModel;
                switch (ctrl.ControlType.ToUpper())
                {
                    case "SECTION":
                  
                        var strlblSec = htmlTagDictionary["SECTION"];
                        var strChildSec = "";
                        if (tN.Nodes.Count > 0)
                        {
                            var lsItems = new List<string>();
                            foreach (TreeNode item in tN.Nodes)
                            {
                                lsItems.Add(GetHtmlEqualent(item));
                            }
                            if (lsItems.Count > 0)
                                strChildSec = String.Join("<br/>", lsItems.ToArray());

                        }

                        strlblSec = strlblSec.Replace("#Val#", strChildSec);
                       
                        strRes = strlblSec;

                        break;
                    case "TABLE":
                        var objTab = ctrl.Properties as TableGridClass;

                        var strlblTab = htmlTagDictionary["TABLE"];
                        var strChildTab = "";
                        if (tN.Nodes.Count > 0)
                        {
                            var lsItems = new List<string>();
                            foreach (TreeNode item in tN.Nodes)
                            {
                                lsItems.Add(GetHtmlEqualent(item));
                            }
                            if (lsItems.Count > 0)
                                strChildTab = String.Join("<br/>", lsItems.ToArray());

                        }

                        strlblTab = strlblTab.Replace("#Val#", strChildTab);
                        strlblTab = strlblTab.Replace("#attr#", "");
                        strRes = strlblTab;

                        break;
                    case "ROW":
                        var objRow = ctrl.Properties as RowGridClass;
                        var strlblRow = htmlTagDictionary["ROW"];
                        var strChildRow = "";
                        if (tN.Nodes.Count > 0)
                        {
                            var lsItems = new List<string>();
                            foreach (TreeNode item in tN.Nodes)
                            {
                                lsItems.Add(GetHtmlEqualent(item));
                            }
                            if (lsItems.Count > 0)
                                strChildRow = String.Join("<br/>", lsItems.ToArray());

                        }

                        strlblRow = strlblRow.Replace("#Val#", strChildRow);
                        strlblRow = strlblRow.Replace("#attr#", "");
                        strRes = strlblRow;

                        break;
                    case "CELL":
                        var objCell = ctrl.Properties as CellGridCalss;
                        //childs
                        var strlblCell = htmlTagDictionary["CELL"];
                        var strChildCell = "";
                        if (tN.Nodes.Count > 0)
                        {
                            var lsItems = new List<string>();
                            foreach (TreeNode item in tN.Nodes)
                            {
                                lsItems.Add(GetHtmlEqualent(item));
                            }
                            if (lsItems.Count > 0)
                                strChildCell = String.Join("<br/>", lsItems.ToArray());

                        }
                       
                        strlblCell = strlblCell.Replace("#Val#", strChildCell);
                        strlblCell = strlblCell.Replace("#attr#", "");
                        strRes = strlblCell;
                        break;
                    case "LABEL":
                        var objlbl = ctrl.Properties as LabelCellGridClass;
                        var strlbl = htmlTagDictionary["LABEL"];
                        strlbl = strlbl.Replace("#Val#", objlbl.Name);
                        strlbl = strlbl.Replace("#attr#", "");
                        strRes = strlbl;
                        break;
                    case "FIELD":
                        var objfld = ctrl.Properties as FieldCellGridClass;
                        var strFld = htmlTagDictionary["FIELD"];
                        strFld = strFld.Replace("#Val#", objfld.FieldModel + "." + objfld.Name);
                        strFld = strFld.Replace("#attr#", "");
                        strRes = strFld;
                        break;
                    case "EMPTY":
                        var objEmpty = ctrl.Properties as EmptyCellGridClass;
                        var strlblE = htmlTagDictionary["EMPTY"];
                        strlblE = strlblE.Replace("#Val#", "&nbsp;");
                        strlblE = strlblE.Replace("#attr#", "");
                        strRes = strlblE;
                        break;
                    case "IMAGEURL":
                        var objIUrl = ctrl.Properties as ImageUrlCellGridClass;
                        var strImgU = htmlTagDictionary["IMAGE"];                       
                        strImgU = strImgU.Replace("#attr#", "Src='"+objIUrl.Src+"'");

                        strRes = strImgU;
                        break;
                    case "IMAGEBYTE":
                        var objIByte = ctrl.Properties as ImageByteCellGridClass;
                        var strImgB = htmlTagDictionary["IMAGE"];                      
                        strImgB = strImgB.Replace("#attr#", "Src=''");
                        strRes = strImgB;
                        break;
                    case "IMAGESUBURL":
                        var objISUrl = ctrl.Properties as ImageSubUrlCellGridClass;
                        var strlblImgU = htmlTagDictionary["IMAGESUB"];
                        strlblImgU = strlblImgU.Replace("#Val#", objISUrl.LabelName);
                        strlblImgU = strlblImgU.Replace("#lattr#", "");
                        strlblImgU = strlblImgU.Replace("#attr#", "Src=''");
                        strRes = strlblImgU;
                        break;
                    case "IMAGESUBBYTE":
                        var objISByte = ctrl.Properties as ImageSubByteCellGridClass;
                        var strIlbl = htmlTagDictionary["IMAGESUB"];
                        strIlbl = strIlbl.Replace("#lVal#", objISByte.LabelName);
                        strIlbl = strIlbl.Replace("#lattr#", "");
                        strIlbl = strIlbl.Replace("#attr#", "Src=''");
                        strRes = strIlbl;
                        break;
                }

            }

          

            return strRes;
        }


    }
}
