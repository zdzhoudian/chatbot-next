using System;
using System.Windows.Input;

namespace NextUI
{
    public class UICommand : ICommand
    {
        private readonly Func<object, bool> _canExecuteFunc;
        private readonly Action<object> _executeFunc;

        public UICommand(Action<object> executeFunc, Func<object, bool> canExecuteFunc = null)
        {
            _canExecuteFunc = canExecuteFunc;
            _executeFunc = executeFunc;
        }

        public event EventHandler CanExecuteChanged;

        public void OnCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        public bool CanExecute(object parameter)
        {
            if (_executeFunc == null)
            {
                return false;
            }
            if (_canExecuteFunc == null)
            {
                return true;
            }
            return _canExecuteFunc(parameter);
        }

        public void Execute(object parameter)
        {
            if (!CanExecute(parameter))
            {
                return;
            }
            _executeFunc?.Invoke(parameter);
        }
    }

    public class UICommand<T> : ICommand
    {
        private readonly Func<T, bool> _canExecuteFunc;
        private readonly Action<T> _executeFunc;

        public UICommand(Action<T> executeFunc, Func<T, bool> canExecuteFunc = null)
        {
            _canExecuteFunc = canExecuteFunc;
            _executeFunc = executeFunc;
        }

        public event EventHandler CanExecuteChanged;

        public void OnCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        public bool CanExecute(object parameter)
        {
            if (_executeFunc == null)
            {
                return false;
            }
            if (_canExecuteFunc == null)
            {
                return true;
            }
            return _canExecuteFunc(ConvertToT(parameter));
        }

        public void Execute(object parameter)
        {
            var parT = ConvertToT(parameter);
            if (!CanExecute(parT))
            {
                return;
            }
            _executeFunc.Invoke(parT);
        }

        public T ConvertToT(object parameter)
        {
            if (parameter == null)
            {
                return default;
            }
            if (parameter is T parT)
            {
                return parT;
            }
            try
            {
                var typeT = typeof(T);
                if (typeT.IsEnum)
                {
                    if (parameter is string)
                    {
                        return (T)Enum.Parse(typeT, parameter.ToString());
                    }
                    if (parameter is int)
                    {
                        return (T)Enum.ToObject(typeT, (int)parameter);
                    }
                }
                else
                {
                    return (T)Convert.ChangeType(parameter, typeT);
                }
            }
            catch
            {
                //noting...
            }
            return default;
        }
    }
}
