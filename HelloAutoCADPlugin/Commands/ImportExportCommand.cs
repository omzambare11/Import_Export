using Autodesk.AutoCAD.Runtime;

namespace HelloAutoCADPlugin.Commands
{
    public class ImportExportCommand
    {
        [CommandMethod("OPENIMPORTEXPORT")]
        public void Open()
        {
            ImportExportWindow win = new ImportExportWindow();
            win.ShowDialog();
        }
    }
}