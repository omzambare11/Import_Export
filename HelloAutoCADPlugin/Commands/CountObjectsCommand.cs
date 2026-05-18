using System;
using System.Windows.Input;

using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;

namespace HelloAutoCADPlugin.Commands
{
    public class CountObjectsCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged
        {
            add { }
            remove { }
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            Document doc =
                Application.DocumentManager.MdiActiveDocument;

            Database db = doc.Database;

            int totalCount = 0;

            int lineCount = 0;
            int circleCount = 0;
            int polylineCount = 0;
            int arcCount = 0;
            int textCount = 0;
            int dimensionCount = 0;
            int blockCount = 0;

            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                BlockTable bt =
                    tr.GetObject(
                        db.BlockTableId,
                        OpenMode.ForRead
                    ) as BlockTable;

                BlockTableRecord btr =
                    tr.GetObject(
                        bt[BlockTableRecord.ModelSpace],
                        OpenMode.ForRead
                    ) as BlockTableRecord;

                foreach (ObjectId objId in btr)
                {
                    Entity ent =
                        tr.GetObject(
                            objId,
                            OpenMode.ForRead
                        ) as Entity;

                    if (ent != null)
                    {
                        totalCount++;

                        if (ent is Line)
                            lineCount++;

                        else if (ent is Circle)
                            circleCount++;

                        else if (ent is Polyline)
                            polylineCount++;

                        else if (ent is Arc)
                            arcCount++;

                        else if (ent is DBText)
                            textCount++;

                        else if (ent is Dimension)
                            dimensionCount++;

                        else if (ent is BlockReference)
                            blockCount++;
                    }
                }

                tr.Commit();
            }

            string result =
                "===== DRAWING ANALYSIS =====\n\n" +

                $"Total Objects : {totalCount}\n\n" +

                $"Lines         : {lineCount}\n" +
                $"Circles       : {circleCount}\n" +
                $"Polylines     : {polylineCount}\n" +
                $"Arcs          : {arcCount}\n" +
                $"Texts         : {textCount}\n" +
                $"Dimensions    : {dimensionCount}\n" +
                $"Blocks        : {blockCount}";

            ResultWindow window =
    new ResultWindow(); 

            window.ShowDialog();
        }
    }
}