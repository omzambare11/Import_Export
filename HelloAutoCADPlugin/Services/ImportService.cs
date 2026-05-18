using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using System.IO;

namespace HelloAutoCADPlugin.Services
{
    public static class ImportService
    {
        public static void ImportDwg(string filePath)
        {
            Document doc =
                Application.DocumentManager.MdiActiveDocument;

            Database db = doc.Database;

            Editor ed = doc.Editor;

            try
            {
                using (Database sourceDb = new Database(false, true))
                {
                    sourceDb.ReadDwgFile(filePath, FileShare.ReadWrite, true, "");

                    using (Transaction tr =
                        db.TransactionManager.StartTransaction())
                    {
                        BlockTable bt =
                            (BlockTable)tr.GetObject(db.BlockTableId, OpenMode.ForRead);

                        BlockTableRecord modelSpace =
                            (BlockTableRecord)tr.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite);

                        ObjectIdCollection ids =
                            new ObjectIdCollection();

                        BlockTable sourceBt =
                            (BlockTable)sourceDb.BlockTableId.GetObject(OpenMode.ForRead);

                        BlockTableRecord sourceModel =
                            (BlockTableRecord)sourceBt[BlockTableRecord.ModelSpace].GetObject(OpenMode.ForRead);

                        foreach (ObjectId id in sourceModel)
                        {
                            ids.Add(id);
                        }

                        IdMapping mapping = new IdMapping();

                        db.WblockCloneObjects(
                            ids,
                            modelSpace.ObjectId,
                            mapping,
                            DuplicateRecordCloning.Replace,
                            false);

                        tr.Commit();
                    }
                }

                ed.WriteMessage($"\nDWG Imported: {filePath}");
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage($"\nImport Failed: {ex.Message}");
            }
        }
    }
}