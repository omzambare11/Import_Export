using System.Collections.Generic;
using System.Text;
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

            // Dynamic counter
            Dictionary<string, int> entityCounts =
                new Dictionary<string, int>();

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

                    if (ent == null)
                        continue;

                    totalCount++;

                    // Get entity type name dynamically
                    string entityType =
                        ent.GetType().Name.ToUpper();

                    // Rename some common types nicely
                    switch (entityType)
                    {
                        case "DBTEXT":
                            entityType = "TEXT";
                            break;

                        case "BLOCKREFERENCE":
                            entityType = "BLOCK";
                            break;
                    }

                    // Add or increment
                    if (entityCounts.ContainsKey(entityType))
                    {
                        entityCounts[entityType]++;
                    }
                    else
                    {
                        entityCounts[entityType] = 1;
                    }
                }

                tr.Commit();
            }

            // Build output dynamically
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(
                "========== DRAWING ANALYSIS ==========\n");

            sb.AppendLine(
                $"TOTAL OBJECTS : {totalCount}\n");

            foreach (var item in entityCounts)
            {
                sb.AppendLine(
                    $"{item.Key,-20}: {item.Value}");
            }

            ResultText.Text = sb.ToString();
        }
    }
}