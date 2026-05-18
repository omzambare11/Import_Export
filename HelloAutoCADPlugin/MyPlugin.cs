using Autodesk.AutoCAD.Runtime;
using Autodesk.Windows;
using HelloAutoCADPlugin.Commands;
using HelloAutoCADPlugin.Ribbon;

namespace HelloAutoCADPlugin
{
    public class MyPlugin : IExtensionApplication
    {
        public void Initialize()
        {
            CreateRibbon();
        }

        public void Terminate()
        {
        }

        private void CreateRibbon()
        {
            RibbonControl ribbon = ComponentManager.Ribbon;

            if (ribbon == null)
                return;

            RibbonTab tab = new RibbonTab
            {
                Title = "OM TAB",
                Id = "OM_TAB"
            };

            ribbon.Tabs.Add(tab);

            RibbonPanelSource panelSource = new RibbonPanelSource
            {
                Title = "My Tools"
            };

            RibbonPanel panel = new RibbonPanel
            {
                Source = panelSource
            };

            tab.Panels.Add(panel);

            panelSource.Items.Add(new RibbonButton
            {
                Text = "Say Hello",
                ShowText = true,
                CommandHandler = new HelloCommand()
            });

            panelSource.Items.Add(new RibbonButton
            {
                Text = "Count Objects",
                ShowText = true,
                CommandHandler = new OpenWindowCommand()
            });

            panelSource.Items.Add(new RibbonButton
            {
                Text = "ImportExport",
                ShowText = true,
                Size = RibbonItemSize.Large,
                CommandHandler = new RibbonCommandHandler("OPENIMPORTEXPORT ")
            });
        }
    }
}