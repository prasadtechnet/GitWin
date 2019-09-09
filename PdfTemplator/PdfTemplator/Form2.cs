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
        public Form2()
        {
            InitializeComponent();
        }
   
        private void Form2_Load(object sender, EventArgs e)
        {
            TreeNode trN = new TreeNode("Document");
            treeView1.Nodes.Add(trN);
        }

      
        private void TreeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
         
        }


        private void button1_Click(object sender, EventArgs e)
        {

            //Customer bill = new Customer();
            //// Assign values to the properties  
            //bill.Age = 50;
            //bill.Address = " 114 Maple Drive ";
            //bill.DateOfBirth = Convert.ToDateTime("2019/09/09");
            //bill.SSN = "123-345-3566";
            //bill.Email = "bill@aol.com";
            //bill.Name = "Bill Smith";
            //// Sets the the grid with the customer instance to be  
            //// browsed  
            //propertyGrid1.SelectedObject = bill;

            TableGridClass objTab = new TableGridClass();
            objTab.Name = "";

            propertyGrid1.SelectedObject = objTab;
            // treeView1.SelectedNode.Nodes.Add(new TreeNode("Label") { Tag=new TNM {ControlType="LABEL" } });
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // treeView1.SelectedNode.Nodes.Add(new TreeNode("Field") { Tag = new TNM { ControlType = "FIELD" } });
            treeView1.SelectedNode.Remove();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode != null)
            {
                var objTab = (TableGridClass)propertyGrid1.SelectedObject;
                var tn = new TreeNode(objTab.ControlType + ":" + objTab.Name) {Tag=objTab};
                treeView1.SelectedNode.Nodes.Add(tn);
            }

       
        }
    }

    public class TNM
    {

        public object Properties { get; set; }
        public string ControlType { get; set; }
      
    }
}
