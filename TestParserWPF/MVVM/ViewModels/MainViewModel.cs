using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TestParserWPF.Commands;
using TestParserWPF.Core;

namespace TestParserWPF.MVVM.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        #region Parser
        private Parser<List<string>> _parser;
        public Parser<List<string>> Parser
        {
            get => _parser;
            set
            {
                _parser = value;
                OnPropertyChanged(nameof(Parser));
            }
        }
        #endregion

        #region ListURL
        private List<string> _listUrl;
        public List<string> ListUrl
        {
            get => _listUrl;
            set
            {
                _listUrl = value;
                Parser.ListUrl = value;
                OnPropertyChanged(nameof(ListUrl));
            }
        }
        #endregion

        #region ListData
        private ObservableCollection<string> _listData = new ObservableCollection<string>();
        public ObservableCollection<string> ListData
        {
            get => _listData;
            set
            {
                _listData = value;
                OnPropertyChanged(nameof(ListData));
            }
        }
        #endregion

        #region FileName
        private string _fileName;
        public string FileName
        {
            get => _fileName;
            set
            {
                _fileName = value;
                OnPropertyChanged(nameof(FileName));
            }
        }
        #endregion

        #region Commands
        public ICommand StartCommand { get; set; }
        public ICommand StopCommand { get; set; }
        public ICommand LoadCommand { get; set; }
        #endregion

        public MainViewModel()
        {
            Parser = new Parser<List<string>>
            (
                new SitesParser(),
                ListUrl
            );
            Parser.OnCompleted += Parser_OnCompleted;
            Parser.OnNewData += Parser_OnNewData;
            LoadCommand = new LoadParseCommand(this);
            StartCommand = new StartParseCommand(this);
            StopCommand = new StopParseCommand(this);
        }

        private void Parser_OnNewData(object arg1, List<string> arg2)
        {
            ListData.Add($"{Parser.CurURL} - {arg2.Count}");
        }

        private void Parser_OnCompleted(object obj)
        {
            
        }
    }
}
