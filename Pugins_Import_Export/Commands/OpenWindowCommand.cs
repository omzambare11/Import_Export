using System;
using System.Collections.Generic;
using System.Text;
using System;
using System.Windows.Input;

namespace HelloAutoCADPlugin.Commands
{
    public class OpenWindowCommand : ICommand
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
            ResultWindow window =
                new ResultWindow();

            window.ShowDialog();
        }
    }
}
