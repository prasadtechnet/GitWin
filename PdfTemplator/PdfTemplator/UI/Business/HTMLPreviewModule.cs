using PdfTemplator.UI.Models;
using PdfTemplator.UI.PropertyGridClass;
using System;
using System.Collections.Generic;
using System.Drawing;
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
            { "TABLE","<table #attr# style='border-collapse: collapse;' >#Val#</table>"},
            { "ROW","<tr #attr# >#Val#<tr>"},
            { "CELL","<td #attr# >#Val#</td>"},
            { "LABEL","<span #attr# >#Val#</span>"},
            { "FIELD","<label #attr#> #Val#</label>"},
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
                        strlblTab = strlblTab.Replace("#attr#", GetAttr(objTab.width.ToString(),"","","","","",null,null,null,"","","","","","",objTab.spaceBefore.ToString(),objTab.spaceAfter.ToString()));
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
                        strlblCell = strlblCell.Replace("#attr#", GetAttr((objCell.Width*100).ToString(), "", objCell.HAlign.ToString(), objCell.VAlign.ToString(), objCell.RowSpan.ToString(), objCell.ColSpan.ToString(), null, null, null, objCell.PLeft.ToString(), objCell.PRight.ToString(), objCell.PTop.ToString(), objCell.PBottom.ToString(), "", "","", "",objCell.BorderPattren.ToString(),tN));
                        strRes = strlblCell;
                        break;
                    case "LABEL":
                        var objlbl = ctrl.Properties as LabelCellGridClass;
                        var strlbl = htmlTagDictionary["LABEL"];
                        strlbl = strlbl.Replace("#Val#", objlbl.Name);
                        strlbl = strlbl.Replace("#attr#", GetAttr("", objlbl.Height.ToString(), "", "", "", "", objlbl.LabelFont, objlbl.LabelColor, null, "", "", "", "", "", "", "", ""));
                        strRes = strlbl;
                        break;
                    case "FIELD":
                        var objfld = ctrl.Properties as FieldCellGridClass;
                        var strFld = htmlTagDictionary["FIELD"];
                        strFld = strFld.Replace("#Val#", objfld.FieldModel + "." + objfld.Name);
                        strFld = strFld.Replace("#attr#", GetAttr("", objfld.Height.ToString(), "", "", "", "", objfld.FieldFont, objfld.FieldColor, null, "", "", "", "", "", "", "", ""));
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
                        strImgU = strImgU.Replace("#attr#", "Src='"+objIUrl.Src+"' "+ GetAttr("", objIUrl.Height.ToString(), "", "", "", "",null, null, null, "", "", "", "", "", "", "", ""));

                        strRes = strImgU;
                        break;
                    case "IMAGEBYTE":
                        var objIByte = ctrl.Properties as ImageByteCellGridClass;
                        var strImgB = htmlTagDictionary["IMAGE"];                      
                        strImgB = strImgB.Replace("#attr#", "Src='' "+ GetAttr("", objIByte.Height.ToString(), "", "", "", "",null, null, null, "", "", "", "", "", "", "", ""));
                        strRes = strImgB;
                        break;
                    case "IMAGESUBURL":
                        var objISUrl = ctrl.Properties as ImageSubUrlCellGridClass;
                        var strlblImgU = htmlTagDictionary["IMAGESUB"];
                        strlblImgU = strlblImgU.Replace("#Val#", objISUrl.LabelName);
                        strlblImgU = strlblImgU.Replace("#lattr#", GetAttr("", objISUrl.LabelHeight.ToString(), "", "", "", "", objISUrl.LabelFont, objISUrl.LabelColor, null, "", "", "", "", "", "", "", ""));
                        strlblImgU = strlblImgU.Replace("#attr#", "Src='' " + GetAttr("", objISUrl.Height.ToString(), "", "", "", "", null,null, null, "", "", "", "", "", "", "", ""));
                        strRes = strlblImgU;
                        break;
                    case "IMAGESUBBYTE":
                        var objISByte = ctrl.Properties as ImageSubByteCellGridClass;
                        var strIlbl = htmlTagDictionary["IMAGESUB"];
                        strIlbl = strIlbl.Replace("#lVal#", objISByte.LabelName);
                        strIlbl = strIlbl.Replace("#lattr#", GetAttr("", objISByte.LabelHeight.ToString(), "", "", "", "", objISByte.LabelFont, objISByte.LabelColor, null, "", "", "", "", "", "", "", ""));
                        strIlbl = strIlbl.Replace("#attr#", "Src='' " + GetAttr("", objISByte.Height.ToString(), "", "", "", "", null,null, null, "", "", "", "", "", "", "", ""));
                        strRes = strIlbl;
                        break;
                }

            }

          

            return strRes;
        }

        private string GetAttributes(ControlPropertyModel objCT)
        {
            var strRes = "";

            try
            {
                switch (objCT.ControlType.ToUpper())
                {
                    case "SECTION":                      

                        strRes = "";
                        break;
                    case "TABLE":

                        strRes = "";
                        break;
                    case "ROW":

                        strRes = "";
                        break;
                    case "CELL":

                        strRes = "";
                        break;
                    case "LABEL":
                        strRes = "";
                        break;
                    case "FIELD":
                        strRes = "";
                        break;
                    case "EMPTY":
                        strRes = "";
                        break;
                    case "IMAGEURL":
                        strRes = "";
                        break;
                    case "IMAGEBYTE":
                        strRes = "";
                        break;
                    case "IMAGESUBURL":
                        strRes = "";
                        break;
                    case "IMAGESUBBYTE":
                        strRes = "";
                        break;
                }
            }
            catch (Exception ex)
            {
            }

            return strRes;
        }

        private string GetAttr(string width="",string height="",string halign="",string valign="",string rowspan="",string colspan="",Font font=null, Color? color =null,Color? bgColor=null,string pLeft="", string pRight="", string pTop="", string pBottom="", string mLeft = "", string mRight = "", string mTop = "", string mBottom = "",string borderPattren="",TreeNode tn=null)
        {
            var strRes = "";
            try
            {
                var lsAttr = new List<string>();
                var lsStyle = new List<string>();

                if (!String.IsNullOrEmpty(width))
                    lsStyle.Add("width:"+width+" px");
                if (!String.IsNullOrEmpty(height))
                    lsStyle.Add("height:" + height + " px");

                if (!String.IsNullOrEmpty(halign))
                    lsStyle.Add("text-align:"+(halign.Replace("ALIGN_", "").ToLower()));

                if (!String.IsNullOrEmpty(valign))
                    lsStyle.Add("vertical-align:" + (halign.Replace("ALIGN_", "").ToLower()));

                if (!String.IsNullOrEmpty(pLeft))
                    lsStyle.Add("padding-left:"+pLeft + " px");
                if (!String.IsNullOrEmpty(pRight))
                    lsStyle.Add("padding-right:"+ pRight + " px");

                if (!String.IsNullOrEmpty(pTop))
                    lsStyle.Add("padding-top:"+pTop + " px");
                if (!String.IsNullOrEmpty(pBottom))
                    lsStyle.Add("padding-bottom:"+pBottom + " px");
                //Border
                #region Border
                if (!String.IsNullOrEmpty(borderPattren))
                {
                    var brdr = borderPattren.Split('_');
                    if (brdr.Length == 4)
                    {
                        lsStyle.Add("border:1px solid black");
                    }
                    else
                    {
                        //border - style: solid;
                        //border - width: medium;
                        //border - color: red;
                        //border - left: 6px solid red;
                       
                        foreach (var item in brdr)
                        {
                            switch (item.ToUpper())
                            {
                                case "L":
                                    lsStyle.Add("border-left:1px solid black");
                                    break;
                                case "B":
                                    lsStyle.Add("border-bottom:1px solid black");
                                    break;
                                case "T":
                                    lsStyle.Add("border-top:1px solid black");
                                    break;
                                case "R":
                                    lsStyle.Add("border-right:1px solid black");
                                    break;
                            }
                        }
                    }

                }else
                {
                    lsStyle.Add("border:none");
                }
                #endregion

                    //Margin
                    if (!String.IsNullOrEmpty(mLeft))
                    lsStyle.Add("margin-left:" + mLeft + " px");
                if (!String.IsNullOrEmpty(mRight))
                    lsStyle.Add("margin-right:" + mRight + " px");
                if (!String.IsNullOrEmpty(mTop))
                    lsStyle.Add("margin-top:" + mTop + " px");
                if (!String.IsNullOrEmpty(mBottom))
                    lsStyle.Add("margin-bottom:" + mBottom + " px");


                if (font != null)
                {
                    lsStyle.Add("font-family:"+font.FontFamily.Name);
                    lsStyle.Add("font-size:"+font.Size+"px");
                    if(font.Bold)
                     lsStyle.Add("font-weight:bold");
                }
                if (color!=null)
                    lsStyle.Add("color:" + System.Drawing.ColorTranslator.ToHtml(color.Value));
                if (bgColor != null)
                    lsStyle.Add("background-color:" + System.Drawing.ColorTranslator.ToHtml(color.Value));

                if (!String.IsNullOrEmpty(rowspan))
                    lsAttr.Add(" rowspan=\""+rowspan+"\"");
                if (!String.IsNullOrEmpty(colspan))
                    lsAttr.Add(" colspan=\"" + colspan + "\"");


                //if (tn.Text.ToUpper() == "CELL")
                //{

                //    var tnTab = tn.Parent.Parent;
                //   // var tabGCell=(tnTab.Tag as ControlPropertyModel).Properties as TableGridClass;

                  

                //}

                if (lsAttr.Count > 0)
                    strRes=  String.Join("  ", lsAttr.ToArray());

                if (lsStyle.Count > 0)
                    strRes= strRes+" Style=\""+ String.Join(";", lsStyle.ToArray())+"\"";
            }
            catch (Exception ex)
            {
            }
            return strRes;
        }
    }
}
