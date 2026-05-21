using System;
using System.Windows.Input;

using Autodesk.AutoCAD.ApplicationServices.Core;

namespace HelloAutoCADPlugin.Commands
{
    public class HelloCommand : ICommand
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
            Application.ShowAlertDialog(
                "Hello From Plugin 🚀"
            );
        }
    }
}