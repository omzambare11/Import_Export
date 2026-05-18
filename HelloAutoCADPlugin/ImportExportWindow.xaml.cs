using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Microsoft.Win32;
using System.Windows;

namespace HelloAutoCADPlugin
{
    public partial class ImportExportWindow : Window
    {
        public ImportExportWindow()
        {
            InitializeComponent();
        }

        // ================= IMPORT DWG (OPEN FILE) =================

        private void Import_Click(
            object sender,
            RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();

            dlg.Filter = "DWG Files (*.dwg)|*.dwg";

            if (dlg.ShowDialog() != true)
                return;

            try
            {
                // OPEN DWG AS NEW DRAWING

                Document doc =
                    Autodesk.AutoCAD.ApplicationServices.Application
                    .DocumentManager
                    .Open(dlg.FileName, false);

                if (doc != null)
                {
                    Autodesk.AutoCAD.ApplicationServices.Application
                        .DocumentManager
                        .MdiActiveDocument = doc;
                }

                MessageBox.Show("Import Successful");

                // 🔥 AUTO CLOSE WINDOW

                this.Close();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(
                    "Import/Open Error: " + ex.Message
                );
            }
        }

        // ================= EXPORT DWG =================

        private void Export_Click(
            object sender,
            RoutedEventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();

            dlg.Filter = "DWG Files (*.dwg)|*.dwg";

            dlg.FileName = "Export.dwg";

            if (dlg.ShowDialog() != true)
                return;

            try
            {
                Document doc =
                    Autodesk.AutoCAD.ApplicationServices.Application
                    .DocumentManager
                    .MdiActiveDocument;

                // SAVE CURRENT DRAWING

                doc.Database.SaveAs(
                    dlg.FileName,
                    true,
                    DwgVersion.Current,
                    doc.Database.SecurityParameters);

                MessageBox.Show("Export Successful");

                // 🔥 AUTO CLOSE WINDOW

                this.Close();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(
                    "Export Error: " + ex.Message
                );
            }
        }
    }
}