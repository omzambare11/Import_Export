using System.Windows;

using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;

using AcApp = Autodesk.AutoCAD.ApplicationServices.Application;

namespace HelloAutoCADPlugin
{
    public partial class ResultWindow : Window
    {
        public ResultWindow()
        {
            InitializeComponent();
        }

        private void CountButton_Click(
            object sender,
            RoutedEventArgs e)
        {
            Document doc =
                AcApp.DocumentManager.MdiActiveDocument;

            Database db = doc.Database;

            int totalCount = 0;

            int lineCount = 0;
            int circleCount = 0;
            int polylineCount = 0;
            int arcCount = 0;
            int textCount = 0;
            int dimensionCount = 0;
            int blockCount = 0;

            using (Transaction tr =
                db.TransactionManager.StartTransaction())
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

            ResultText.Text =
                "========== DRAWING ANALYSIS ==========\n\n" +

                $"TOTAL OBJECTS : {totalCount}\n\n" +

                $"LINES         : {lineCount}\n" +
                $"CIRCLES       : {circleCount}\n" +
                $"POLYLINES     : {polylineCount}\n" +
                $"ARCS          : {arcCount}\n" +
                $"TEXTS         : {textCount}\n" +
                $"DIMENSIONS    : {dimensionCount}\n" +
                $"BLOCKS        : {blockCount}";
        }
    }
}