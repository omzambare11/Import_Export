using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;

namespace HelloAutoCADPlugin.Services
{
    public static class ExportService
    {
        public static void ExportDwg(string filePath)
        {
            Document doc =
                Application.DocumentManager.MdiActiveDocument;

            Editor ed = doc.Editor;

            try
            {
                doc.Database.SaveAs(filePath, true, DwgVersion.Current, doc.Database.SecurityParameters);

                ed.WriteMessage($"\nDWG Exported: {filePath}");
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage($"\nExport Failed: {ex.Message}");
            }
        }
    }
}