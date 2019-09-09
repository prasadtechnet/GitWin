using PdfTemplator.UI.Models;
using PdfTemplator.UI.PropertyGridClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfTemplator.UI.Business
{
    public class PropertyGridManager
    {
        public static object GetPropertyGridObject(string controlType)
        {
            object objData = null;
            try
            {
                switch (controlType.ToUpper())
                {
                    case "LABEL":
                        objData = new LabelCellGridClass();
                        break;
                    case "FIELD":
                        objData = new FieldCellGridClass();
                        break;
                    case "EMPTY":
                        objData = new EmptyCellGridClass();
                        break;
                    case "TABLE":
                        objData = new TableGridClass();
                        break;
                    case "ROW":
                        objData = new RowGridClass();
                        break;                  
                    case "CELL":
                        objData = new CellGridCalss();
                        break;
                    case "IMAGEURL":
                        objData = new ImageUrlCellGridClass();
                        break;
                    case "IMAGEBYTE":
                        objData = new ImageByteCellGridClass();
                        break;
                    case "IMAGESUBURL":
                        objData = new ImageSubUrlCellGridClass();
                        break;
                    case "IMAGESUBBYTE":
                        objData = new ImageSubByteCellGridClass();
                        break;
                }
            }
            catch (Exception ex)
            {

            }
           
            return objData;
        }
      

        public static string GetModelFieldName(ControlPropertyModel objInput)
        {
            var strModel = "";
            try
            {
                switch (objInput.ControlType.ToUpper())
                {
                    case "FIELD":
                        var objF = objInput.Properties as FieldCellGridClass;
                        strModel = objF.FieldModel + "-" + objF.Name;
                        break;
                    case "IMAGEBYTE":
                        var objIB = objInput.Properties as ImageByteCellGridClass;
                        strModel = objIB.ImageFieldModel + "-" + objIB.Name;
                        break;
                    case "IMAGESUBBYTE":
                        var objISB = objInput.Properties as ImageSubByteCellGridClass;
                        strModel = objISB.ImageFieldModel + "-" + objISB.Name;
                        break;
                }
            }
            catch (Exception ex)
            {
            }
            return strModel;
        }
    }
}
