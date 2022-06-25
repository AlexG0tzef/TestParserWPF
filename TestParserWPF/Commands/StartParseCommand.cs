using AngleSharp.Html.Parser;
using MyTestOr.Commands.AsyncCommand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestParserWPF.Core;
using TestParserWPF.MVVM.ViewModels;

namespace TestParserWPF.Commands
{
    public class StartParseCommand : AsyncBaseCommand
    {
        private readonly MainViewModel _MainViewModel;

        public StartParseCommand(MainViewModel mainViewModel)
        {
            _MainViewModel = mainViewModel;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _MainViewModel.Parser.Start();
        }

        public override bool CanExecute(object parameter)
        {
            return base.CanExecute(parameter);
        }
    }
}
