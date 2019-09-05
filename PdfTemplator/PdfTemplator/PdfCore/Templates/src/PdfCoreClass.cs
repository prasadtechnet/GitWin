using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfTemplator.PdfCore.Templates
{
    #region Pdf Core abstract

    public abstract class PdfDesignCore
    {

        #region Cell Methods

        protected iTextSharp.text.pdf.PdfPCell GetStringCell(string strValue, string fontFamilyName,float fontSize, int fontweight, iTextSharp.text.Color fontColor, int h_align, int v_align, float height, float p_left, float p_right, float p_top, float p_btm, string borderPattren, int rowspan, int colspan, iTextSharp.text.Color bgColor = null)
        {            
            iTextSharp.text.pdf.PdfPCell pthCell = new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase(new iTextSharp.text.Chunk(strValue, GetFont(fontFamilyName, fontSize, fontweight, fontColor))));
            try
            {
                pthCell.VerticalAlignment = v_align;
                pthCell.HorizontalAlignment = h_align;
                if (height > 0f)
                    pthCell.FixedHeight = height;

                if (rowspan > 0)
                    pthCell.Rowspan = rowspan;

                if (colspan > 0)
                    pthCell.Colspan = colspan;


                pthCell.BackgroundColor = bgColor == null ? iTextSharp.text.Color.WHITE : bgColor;

                if (p_btm >= 0f)
                    pthCell.PaddingBottom = p_btm;
                if (p_top >= 0f)
                    pthCell.PaddingTop = p_top;
                if (p_left >= 0f)
                    pthCell.PaddingLeft = p_left;
                if (p_right >= 0f)
                    pthCell.PaddingRight = p_right;


                pthCell.Border = GetBorderSides(borderPattren);

              
            }
            catch (Exception ex)
            {

            }
            return pthCell;
        }
   
        protected iTextSharp.text.pdf.PdfPCell PhraseCell(iTextSharp.text.pdf.PdfPTable table, int hAlign, int vAlign= iTextSharp.text.pdf.PdfPCell.ALIGN_TOP, iTextSharp.text.Color borderColor=null)
        {
            iTextSharp.text.pdf.PdfPCell cell = new iTextSharp.text.pdf.PdfPCell(table);
            try
            {
                cell.BorderColor = borderColor==null ? iTextSharp.text.Color.WHITE : borderColor;
                cell.HorizontalAlignment = hAlign;
                cell.VerticalAlignment = vAlign;
                cell.PaddingBottom = 2f;
                cell.PaddingTop = 0f;
            }
            catch (Exception ex)
            {
            }
          
            return cell;
        }

        protected iTextSharp.text.pdf.PdfPCell PhraseCell(iTextSharp.text.Phrase phrase, float height, int hAlign, int vAlign = iTextSharp.text.pdf.PdfPCell.ALIGN_TOP, iTextSharp.text.Color backColor = null, iTextSharp.text.Color borderColor = null)
        {
            iTextSharp.text.pdf.PdfPCell cell = new iTextSharp.text.pdf.PdfPCell(phrase);
            try
            {
                cell.BorderColor = iTextSharp.text.Color.WHITE;
                cell.BackgroundColor = backColor;
                cell.FixedHeight = height;
                cell.VerticalAlignment = vAlign;
                cell.HorizontalAlignment = hAlign;
                cell.PaddingBottom = 2f;
                cell.PaddingTop = 0f;
            }
            catch (Exception ex)
            {
            }
           
            return cell;
        }

        protected iTextSharp.text.pdf.PdfPCell ImageCell(string path, float scale, int hAlign, int vAlign= iTextSharp.text.pdf.PdfPCell.ALIGN_TOP, iTextSharp.text.Color backColor = null)
        {
            iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(GetImagePath(path));//HttpContext.Current.Server.MapPath(path)
            image.ScalePercent(scale);
            iTextSharp.text.pdf.PdfPCell cell = new iTextSharp.text.pdf.PdfPCell(image);
            cell.BorderColor = backColor==null?iTextSharp.text.Color.WHITE:backColor;
            cell.VerticalAlignment = vAlign;
            cell.HorizontalAlignment = hAlign;
            cell.PaddingBottom = 0f;
            cell.PaddingTop = 0f;
            return cell;
        }
        protected iTextSharp.text.pdf.PdfPCell ImageCell(byte[] path, float scale, int hAlign, int vAlign = iTextSharp.text.pdf.PdfPCell.ALIGN_TOP, iTextSharp.text.Color backColor = null)
        {
            iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(path);       
            image.ScalePercent(scale);
            iTextSharp.text.pdf.PdfPCell cell = new iTextSharp.text.pdf.PdfPCell(image);
            cell.BorderColor = backColor == null ? iTextSharp.text.Color.WHITE : backColor;
            cell.VerticalAlignment = vAlign;
            cell.HorizontalAlignment = hAlign;
            cell.PaddingBottom = 0f;
            cell.PaddingTop = 0f;
            return cell;
        }

        protected iTextSharp.text.pdf.PdfPCell ImageCell(string path, float scale, float height, int hAlign, int vAlign= iTextSharp.text.pdf.PdfPCell.ALIGN_TOP, float p_left=0f, float p_right = 0f, float p_top = 0f, float p_btm = 0f, string borderPattren="T,R,B")
        {
            iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(GetImagePath(path));//HttpContext.Current.Server.MapPath(path)
            image.ScalePercent(scale);

            iTextSharp.text.pdf.PdfPCell cell = new iTextSharp.text.pdf.PdfPCell(image);
            cell.BorderColor = iTextSharp.text.Color.BLACK;
            cell.VerticalAlignment = iTextSharp.text.pdf.PdfPCell.ALIGN_TOP;
            cell.HorizontalAlignment = hAlign;
            //cell.PaddingBottom = 0f;
            //cell.PaddingTop = 0f;

            cell.Border = GetBorderSides(borderPattren);

            
            if (p_btm >= 0f)
                cell.PaddingBottom = p_btm;
            if (p_top >= 0f)
                cell.PaddingTop = p_top;
            if (p_left >= 0f)
                cell.PaddingLeft = p_left;
            if (p_right >= 0f)
                cell.PaddingRight = p_right;


            return cell;
        }

        protected iTextSharp.text.pdf.PdfPCell ImageCell(string path, string SubHeading, string FontFamily, float fontSize = 10f, float scale = 50f, float Img_Height = 50f, float lbl_Height = 20f, int lbl_hAlign = iTextSharp.text.pdf.PdfCell.ALIGN_MIDDLE, int fontWeight = iTextSharp.text.Font.NORMAL, float totalWidth = 250f, float abWidth = 210, int rowSpan = 1, int colSpan = 1)
        {

            iTextSharp.text.pdf.PdfPTable tabSig = new iTextSharp.text.pdf.PdfPTable(1);
            iTextSharp.text.pdf.PdfPCell cell = null;
            try
            {

                tabSig.TotalWidth = totalWidth;
                tabSig.LockedWidth = true;
                tabSig.SetWidths(new float[] { 1.0f });

                iTextSharp.text.pdf.PdfPCell cellSig = GetStringCell(SubHeading, FontFamily, fontSize, fontWeight, iTextSharp.text.Color.BLACK, iTextSharp.text.pdf.PdfCell.ALIGN_LEFT, lbl_hAlign, lbl_Height, 5f, -1f, 4f, 4f, "", 1, 1);

                tabSig.AddCell(cellSig);

                if (path != null)
                {
                    iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(path);
                    //image.ScaleAbsolute(220.0f,50.0f);
                    //image.ScalePercent(scale);
                    image.ScaleAbsolute(abWidth, scale);
                    //  image.ScalePercent(scale);

                    iTextSharp.text.pdf.PdfPCell cellImg = new iTextSharp.text.pdf.PdfPCell(image);

                    cellImg.VerticalAlignment = iTextSharp.text.pdf.PdfPCell.ALIGN_TOP;
                    cellImg.BorderColor = iTextSharp.text.Color.WHITE;
                    cellImg.HorizontalAlignment = iTextSharp.text.pdf.PdfPCell.ALIGN_CENTER;
                    cellImg.FixedHeight = Img_Height;
                    cellImg.PaddingBottom = 0f;
                    cellImg.PaddingTop = 1f;
                    // cellImg.Top = 2f;
                    tabSig.AddCell(cellImg);

                }

                cell = new iTextSharp.text.pdf.PdfPCell(tabSig);
                cell.BorderColor = iTextSharp.text.Color.BLACK;
                cell.HorizontalAlignment = iTextSharp.text.pdf.PdfPCell.ALIGN_LEFT;
                cell.VerticalAlignment = iTextSharp.text.pdf.PdfPCell.ALIGN_MIDDLE;
                cell.PaddingTop = 1f;
                cell.PaddingBottom = 1f;
                cell.Rowspan = rowSpan;
                cell.Colspan = colSpan;


            }
            catch (Exception ex)
            {

            }
            return cell;

         

        }


        protected iTextSharp.text.pdf.PdfPCell ImageCell(byte[] path,string SubHeading,string FontFamily,float fontSize=10f, float scale=50f, float Img_Height=50f, float lbl_Height=20f ,int lbl_hAlign= iTextSharp.text.pdf.PdfCell.ALIGN_MIDDLE, int fontWeight= iTextSharp.text.Font.NORMAL, float totalWidth = 250f, float abWidth = 210, int rowSpan = 1, int colSpan = 1)
        {
            iTextSharp.text.pdf.PdfPTable tabSig = new iTextSharp.text.pdf.PdfPTable(1);
            iTextSharp.text.pdf.PdfPCell cell = null;
            try
            {

                tabSig.TotalWidth = totalWidth;
                tabSig.LockedWidth = true;
                tabSig.SetWidths(new float[] { 1.0f });

                iTextSharp.text.pdf.PdfPCell cellSig = GetStringCell(SubHeading, FontFamily, fontSize, fontWeight, iTextSharp.text.Color.BLACK,iTextSharp.text.pdf.PdfCell.ALIGN_LEFT, lbl_hAlign, lbl_Height, 5f, -1f, 4f, 4f,"", 1, 1);

                tabSig.AddCell(cellSig);

                if (path != null)
                {
                    iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(path);
                    //image.ScaleAbsolute(220.0f,50.0f);
                    //image.ScalePercent(scale);
                    image.ScaleAbsolute(abWidth, scale);
                    //  image.ScalePercent(scale);

                    iTextSharp.text.pdf.PdfPCell cellImg = new iTextSharp.text.pdf.PdfPCell(image);

                    cellImg.VerticalAlignment = iTextSharp.text.pdf.PdfPCell.ALIGN_TOP;
                    cellImg.BorderColor = iTextSharp.text.Color.WHITE;
                    cellImg.HorizontalAlignment = iTextSharp.text.pdf.PdfPCell.ALIGN_CENTER;
                    cellImg.FixedHeight = Img_Height;
                    cellImg.PaddingBottom = 0f;
                    cellImg.PaddingTop = 1f;
                    // cellImg.Top = 2f;
                    tabSig.AddCell(cellImg);

                }

                cell = new iTextSharp.text.pdf.PdfPCell(tabSig);
                cell.BorderColor = iTextSharp.text.Color.BLACK;
                cell.HorizontalAlignment = iTextSharp.text.pdf.PdfPCell.ALIGN_LEFT;
                cell.VerticalAlignment = iTextSharp.text.pdf.PdfPCell.ALIGN_MIDDLE;
                cell.PaddingTop = 1f;
                cell.PaddingBottom = 1f;
                cell.Rowspan = rowSpan;
                cell.Colspan = colSpan;
            }
            catch (Exception ex)
            {

            }
            return cell;
        }

        protected iTextSharp.text.pdf.PdfPCell ImageCell(string path, float scale, int align, int rowSpan, int colSpan)
        {
            iTextSharp.text.pdf.PdfPCell cellImg = null;
            try
            {
                if (path != null)
                {
                    iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(GetImagePath(path));//HttpContext.Current.Server.MapPath(path)
                    image.ScalePercent(scale);                                  

                    cellImg = new iTextSharp.text.pdf.PdfPCell(image);

                    cellImg.VerticalAlignment = iTextSharp.text.pdf.PdfPCell.ALIGN_TOP;
                    // cellImg.BorderColor = iTextSharp.text.Color.WHITE;
                    cellImg.HorizontalAlignment = iTextSharp.text.pdf.PdfPCell.ALIGN_CENTER;
                    cellImg.FixedHeight = 40f;
                    cellImg.PaddingBottom = 0f;
                    cellImg.PaddingTop = 1f;
                    // cellImg.Top = 2f;
                    if (rowSpan > 0)
                        cellImg.Rowspan = rowSpan;

                    if (colSpan > 0)
                        cellImg.Colspan = colSpan;

                }

            }
            catch (Exception ex)
            {

            }
            return cellImg;
        }




        #endregion

        #region Local Pdf Methods

        private int GetBorderSides(string pattren)
        {
            var intRes = -1;
            switch (pattren.ToUpper())
            {
                case "L":
                    intRes = iTextSharp.text.pdf.PdfPCell.LEFT_BORDER;
                    break;
                case "B":
                    intRes = iTextSharp.text.pdf.PdfPCell.BOTTOM_BORDER;
                    break;
                case "R":
                    intRes = iTextSharp.text.pdf.PdfPCell.RIGHT_BORDER;
                    break;
                case "T":
                    intRes = iTextSharp.text.pdf.PdfPCell.TOP_BORDER;
                    break;
                case "L,T":
                    intRes= iTextSharp.text.pdf.PdfPCell.LEFT_BORDER | iTextSharp.text.pdf.PdfPCell.TOP_BORDER;
                    break;
                case "L,B":                                     
                        intRes = iTextSharp.text.pdf.PdfPCell.LEFT_BORDER | iTextSharp.text.pdf.PdfPCell.BOTTOM_BORDER;
                    break;
                case "R,B":
                       intRes = iTextSharp.text.pdf.PdfPCell.RIGHT_BORDER | iTextSharp.text.pdf.PdfPCell.BOTTOM_BORDER;
                    break;
                case "L,R":
                        intRes = iTextSharp.text.pdf.PdfPCell.RIGHT_BORDER | iTextSharp.text.pdf.PdfPCell.LEFT_BORDER;
                    break;
                case "R,T":
                        intRes = iTextSharp.text.pdf.PdfPCell.RIGHT_BORDER | iTextSharp.text.pdf.PdfPCell.TOP_BORDER;
                    break;

                case "T,R,B":
                    intRes = iTextSharp.text.pdf.PdfPCell.RIGHT_BORDER | iTextSharp.text.pdf.PdfPCell.TOP_BORDER | iTextSharp.text.pdf.PdfPCell.BOTTOM_BORDER;
                    break;
                case "B,L,T":
                    intRes = iTextSharp.text.pdf.PdfPCell.LEFT_BORDER | iTextSharp.text.pdf.PdfPCell.TOP_BORDER | iTextSharp.text.pdf.PdfPCell.BOTTOM_BORDER;
                    break;
                case "R,B,L":
                    intRes = iTextSharp.text.pdf.PdfPCell.LEFT_BORDER | iTextSharp.text.pdf.PdfPCell.RIGHT_BORDER | iTextSharp.text.pdf.PdfPCell.BOTTOM_BORDER;
                    break;
                case "L,T,R":
                    intRes = iTextSharp.text.pdf.PdfPCell.LEFT_BORDER | iTextSharp.text.pdf.PdfPCell.RIGHT_BORDER | iTextSharp.text.pdf.PdfPCell.TOP_BORDER;
                    break;

                case "":
                    intRes = 0;
                    break;
                case "L,T,R,B":
                default:
                    intRes = iTextSharp.text.pdf.PdfPCell.LEFT_BORDER | iTextSharp.text.pdf.PdfPCell.BOTTOM_BORDER | iTextSharp.text.pdf.PdfPCell.TOP_BORDER | iTextSharp.text.pdf.PdfPCell.RIGHT_BORDER;
                    break;

            }

            return intRes;
        }
        private iTextSharp.text.Font GetFont(string fontFamilyName, float fontSize, int fontweight, iTextSharp.text.Color fontColor)
        {
            return iTextSharp.text.FontFactory.GetFont(fontFamilyName, fontSize, fontweight, fontColor);
        }

        #endregion

        #region Local Methods    

        private string GetImagePath(string path)
        {
            var strFilePath = "";
            try
            {
                // //web app
                // strFilePath = HttpContext.Current.Server.MapPath(path);
                //win app
              
               strFilePath = AppDomain.CurrentDomain.BaseDirectory + "images\\" + path;

            }
            catch (Exception ex)
            {
            }
            return strFilePath;
        }
        private iTextSharp.text.Color GetColor(string strCondition)
        {
            iTextSharp.text.Color clrRes = iTextSharp.text.Color.BLACK;

            if (strCondition != null && strCondition.Contains("FCOLOR"))
            {
                var clrV = strCondition.Split(';').Where(x => x.Contains("FCOLOR")).FirstOrDefault();
                if (clrV != null)
                {
                    clrRes = new iTextSharp.text.Color(System.Drawing.ColorTranslator.FromHtml(clrV.Split(':')[1]));
                }
            }
            return clrRes;
        }

        private int GetFontWeight(string strCondition)
        {
            int intRes = iTextSharp.text.Font.NORMAL;

            if (strCondition != null && strCondition.Contains("FW"))
            {
                var clrV = strCondition.Split(';').Where(x => x.Contains("FW")).FirstOrDefault();
                if (clrV != null)
                {
                    if (clrV.Split(':')[1] == "BOLD")
                        intRes = iTextSharp.text.Font.BOLD;
                }
            }
            return intRes;
        }

        private float GetFontSize(string strCondition, float defaultVal = 10f)
        {


            if (strCondition != null && strCondition.Contains("FS"))
            {
                var clrV = strCondition.Split(';').Where(x => x.Contains("FS")).FirstOrDefault();
                if (clrV != null)
                {
                    return Convert.ToSingle(clrV.Split(':')[1]);
                }
            }
            return defaultVal;
        }

        private float GetHeight(string strContent, bool fullorHalf)
        {
            float res = 16f;
            int no = (strContent.Length / (fullorHalf ? 90 : 45)) + 1;

            res = res * no;
            return res;
        }
        
        #endregion
    }


    #endregion
}
