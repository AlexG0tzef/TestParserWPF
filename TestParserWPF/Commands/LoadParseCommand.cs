using MyTestOr.Commands.AsyncCommand;
using System;
using System.Collections.Generic;
using System.Linq;
using OfficeOpenXml;
using System.Text;
using System.Threading.Tasks;
using TestParserWPF.MVVM.ViewModels;
using Microsoft.Win32;
using System.IO;
using ExcelDataReader;
using System.Data;

namespace TestParserWPF.Commands
{
    public class LoadParseCommand : AsyncBaseCommand
    {
        private readonly MainViewModel _MainViewModel;

        public LoadParseCommand(MainViewModel mainViewModel)
        {
            _MainViewModel = mainViewModel;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            var file = GetSelectedFilesFromDialog().Result.FirstOrDefault();
            _MainViewModel.FileName = file;
            _MainViewModel.ListUrl = File.ReadAllText(file).Split("\r\n").ToList();

        }

        private async Task<string[]> GetSelectedFilesFromDialog()
        {
            string[]? answ = null;

            
            OpenFileDialog dial = new OpenFileDialog();
            dial.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            dial.ShowDialog();
            return dial.FileNames;
        }

        public override bool CanExecute(object parameter)
        {
            return base.CanExecute(parameter);
        }
    }
}
