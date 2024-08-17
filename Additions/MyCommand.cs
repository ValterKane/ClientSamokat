using System.Windows.Input;

namespace ClientSamokat.Additions
{
    internal class MyCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        private readonly Action<object> action;

        public MyCommand(Action<object> myAction)
        {
            action = myAction;
        }

        public bool CanExecute(object? parameter)
        {
            if (parameter == null)
                return false;
            else
                return true;
        }

        public void Execute(object? parameter)
        {
            action?.Invoke(parameter);
        }
    }
}
