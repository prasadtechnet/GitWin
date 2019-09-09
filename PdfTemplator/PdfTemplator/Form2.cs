using PdfTemplator.UI.PropertyGridClass;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PdfTemplator
{
    public partial class Form2 : Form
    {

        #region Varaibles
        string lblobj = "";
        LabelCellGridClass _lbl;
        FieldCellGridClass _fld;
        TableGridClass _tbl;
        ImageUrlCellGridClass _imgUrl;
        ImageByteCellGridClass _imgByte;
        #endregion
        public Form2()
        {
            InitializeComponent();
        }
   
        private void Form2_Load(object sender, EventArgs e)
        {
            //set,Get ,serialise,get back-- done
            //validations
            //dynamic dropdown for model properties--done

         //   pgTest.SelectedObject = new RnD1();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            var objData = pgTest.SelectedObject;
            switch (cbControl.SelectedItem.ToString().ToUpper())
            {
                case "LABEL":
                    _lbl = (LabelCellGridClass)objData;
                    lblobj = Newtonsoft.Json.JsonConvert.SerializeObject(_lbl);
                    pgTest.SelectedObject = null;
                    break;
                case "FIELD":
                    _fld =  (FieldCellGridClass)objData;
                    break;
                case "TABLE":
                    _tbl = (TableGridClass)objData;
                    break;
                case "IMAGEURL":
                    _imgUrl = (ImageUrlCellGridClass)objData;
                    break;
                case "IMAGEBYTE":
                    _imgByte = (ImageByteCellGridClass)objData;
                    break;
            }


        }

        private void btnGetback_Click(object sender, EventArgs e)
        {
            switch (cbControl.SelectedItem.ToString().ToUpper())
            {
                case "LABEL":
                    pgTest.SelectedObject = (LabelCellGridClass)Newtonsoft.Json.JsonConvert.DeserializeObject<LabelCellGridClass>(lblobj);
                   // pgTest.SelectedObject = _lbl;
                    break;
                case "FIELD":
                    pgTest.SelectedObject = _fld;
                    break;
                case "TABLE":
                    pgTest.SelectedObject = _tbl;
                    break;
                case "IMAGEURL":
                    pgTest.SelectedObject = _imgUrl;
                    break;
                case "IMAGEBYTE":
                    pgTest.SelectedObject = _imgByte;
                    break;
            }
        }

        private void btnSet_Click(object sender, EventArgs e)
        {
            if (cbControl.SelectedIndex > -1)
            {
                pgTest.SelectedObject = GetPropertyObject(cbControl.SelectedItem.ToString());
            }
        }

        private object GetPropertyObject(string control)
        {
            object objData = null;
            switch (control.ToUpper())
            {
                case "LABEL":
                    objData = new LabelCellGridClass() {ControlType="label",LabelFont=new Font("Arial",10f,FontStyle.Regular),LabelColor=Color.Black };
                    break;
                case "FIELD":
                    objData = new FieldCellGridClass();
                    break;
                case "TABLE":
                    objData = new CellGridCalss();
                    break;
                case "IMAGEURL":
                    objData = new ImageUrlCellGridClass();
                    break;
                case "IMAGEBYTE":
                    objData = new ImageByteCellGridClass();
                    break;
            }
            return objData;
        }



      

       
    }

    public class RnD1
    {
        private String _formatString = null;
        [Category("Display")]
        [DisplayName("Format String")]
        [Description("Format string governing display of data values.")]
        [DefaultValue("")]
        [TypeConverter(typeof(FormatStringConverter))]
        public String FormatString { get { return _formatString; } set { _formatString = value; } }
    }
    public class FormatStringConverter : StringConverter
    {
        public override Boolean GetStandardValuesSupported(ITypeDescriptorContext context) { return true; }
        public override Boolean GetStandardValuesExclusive(ITypeDescriptorContext context) { return true; }
        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
        
            return new StandardValuesCollection(GetProps());
        }

        private List<String> GetProps()
        {
            List<String> list = new List<String>();
            list.Add("");
            list.Add("Currency");
            list.Add("Scientific Notation");
            list.Add("General Number");
            list.Add("Number");
            list.Add("Percent");
            list.Add("Time");
            list.Add("Date");
            return list;
        }
    }



}
