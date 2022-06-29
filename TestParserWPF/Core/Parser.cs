using AngleSharp.Html.Parser;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TestParserWPF.Core
{
    public class Parser<T> : INotifyPropertyChanged where T : class
    {
        #region ListURL
        private List<string> _listUrl;
        public List<string> ListUrl
        {
            get => _listUrl;
            set
            {
                _listUrl = value;
                OnPropertyChanged(nameof(ListUrl));
            }
        }
        #endregion

        #region Parser
        private IParser<T> _parser;
        public IParser<T> parser
        {
            get => _parser;
            set
            {
                if (_parser != value)
                {
                    _parser = value;
                    OnPropertyChanged(nameof(parser));
                }
            }
        }
        #endregion

        #region curURL
        private string _curURL;
        public string CurURL
        {
            get => _curURL;
            set
            {
                _curURL = value;
                OnPropertyChanged(nameof(CurURL));
            }
        }
        #endregion

        private LoaderHTML loader = new LoaderHTML();

        #region IsActive
        private bool _isActive;
        public bool isActive
        {
            get => _isActive;
            set
            {
                if (_isActive != value)
                {
                    _isActive = value;
                    OnPropertyChanged(nameof(isActive));
                }
            }
        }
        #endregion

        public event Action<object, T> OnNewData;
        public event Action<object> OnCompleted;

        public Parser(IParser<T> _parser, List<string> _listUrl)
        {
            parser = _parser;
            ListUrl = _listUrl;
        }

        public void Start()
        {
            isActive = true;
            Work();
        }
        public void Stop()
        {
            isActive = false;
        }
        private async void Work()
        {
            if (ListUrl != null)
            {
                foreach (var url in ListUrl)
                {
                    if (!isActive)
                    {
                        OnCompleted?.Invoke(this);
                        return;
                    }
                    CurURL = url;
                    var source = await loader.GetSourceByUrl(url);
                    var domParser = new HtmlParser();

                    var document = await domParser.ParseDocumentAsync(source);
                    var result = parser.Parse(document);

                    OnNewData?.Invoke(this, result);
                }
                OnCompleted?.Invoke(this);
                isActive = false;
            }
            else
            {
                MessageBox.Show("Не выбран файл!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #region INotifyPropertyChanged
        protected void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
    }
}
