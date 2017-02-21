using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MiddleWareBussinessObjects.LDLFileBO;
using System.IO;

namespace UploadLDLTool
{
    public partial class Mainfrm : Form
    {
        public Mainfrm()
        {
            InitializeComponent();
        }

        private void btnLoadFile_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog fileDlg = new OpenFileDialog())
            {
                fileDlg.Title = "Select Layout Definition File";
                fileDlg.Filter = @"All Files|*.*|Layout Definition Files|*.LDL";
                if (fileDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string ldlFile = fileDlg.FileName;

                    fileDlg.Title = "Select Absolute Position File";
                    fileDlg.Filter = @"All Files|*.*|Absolute Position Files|*.satloc";
                    fileDlg.FileName = "NemGeographicCoordinates.satloc";
                    if (fileDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        parseFile(ldlFile, fileDlg.FileName);
                    }

                }
            }

        }

        private  void parseFile(string name,string staticDataFile)
        {
            try
            {
                string[] fileContent = File.ReadAllLines(name);
                string[] absoluteFileContent = File.ReadAllLines(staticDataFile);
                LDLParser.GetParser().ParseText(fileContent, absoluteFileContent);
                int nTripples = LDLParser.GetParser().GetAsTripples().Count;
                this.txtOutput.Text = string.Format("Completed, {0} triples created", nTripples);
            }
            catch (Exception ex)
            {
                this.txtOutput.Text += ex.Message + Environment.NewLine;
            }

        }
    }
}
