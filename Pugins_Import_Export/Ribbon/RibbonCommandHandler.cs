using Autodesk.AutoCAD.ApplicationServices;
using System;
using System.Windows.Input;

namespace HelloAutoCADPlugin.Ribbon
{
    public class RibbonCommandHandler : ICommand
    {
        private readonly string _command;

        public RibbonCommandHandler(string command)
        {
            _command = command;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            var doc =
                Autodesk.AutoCAD.ApplicationServices.Application
                .DocumentManager
                .MdiActiveDocument;

            if (doc == null) return;

            doc.SendStringToExecute(_command, true, false, false);
        }
    }
}