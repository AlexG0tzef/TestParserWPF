using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTestOr.Commands.AsyncCommand
{
    public abstract class AsyncBaseCommand : CommandBase
    {
        #region IsExecute
        private bool _IsExecute;
        private bool IsExecute
        {
            get => _IsExecute;
            set
            {
                _IsExecute = value;
                OnCanExecuteChanged();
            }
        }
        #endregion

        public override bool CanExecute(object parameter)
        {
            return !IsExecute && base.CanExecute(parameter);
        }

        public override async void Execute(object parameter)
        {
            IsExecute = true;
            try
            {
                await ExecuteAsync(parameter);
            }
            finally
            {
                IsExecute = false;
            }
            
        }

        public abstract Task ExecuteAsync(object parameter);
    }
}
