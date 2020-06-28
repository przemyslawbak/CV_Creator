using CV_Creator.Desktop.Commands;
using CV_Creator.Services;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CV_Creator.Desktop.ViewModels
{
    public class ItControlViewModel : ViewModelBase
    {
        private readonly IWindowManager _windowManager;
        private readonly IFileManager _fileManager;
        public ItControlViewModel(IWindowManager windowManager, IFileManager fileManager)
        {
            _windowManager = windowManager;
            _fileManager = fileManager;

            OpenProjectsLoader = new AsyncCommand(async () => await OnOpenProjectsLoader());
            OpenFilePathWindow = new AsyncCommand(async () => await OnOpenFilePathWindow());
            ExecuteOperation = new DelegateCommand(OnExecuteOperation);
            ClearInputs = new DelegateCommand(OnClearInputs);

            //TODO: remove later
            SendOrSave = 1;
            EmailAddress = "email";
            FilePath = "file";
        }

        private void OnClearInputs(object o)
        {
            SendOrSave = 1;
            ProjectsSelected = string.Empty;
            CompanyName = string.Empty;
            PositionApplied = string.Empty;
            FilePath = _fileManager.GetDefaultPdfPath();
            EmailAddress = string.Empty;
        }

        private void OnExecuteOperation(object o)
        {
            throw new NotImplementedException();
        }

        private async Task OnOpenFilePathWindow()
        {
            throw new NotImplementedException();
        }

        private async Task OnOpenProjectsLoader()
        {
            throw new NotImplementedException();
        }

        public ICommand OpenFilePathWindow { get; private set; }
        public ICommand OpenProjectsLoader { get; private set; }
        public ICommand ExecuteOperation { get; private set; }
        public ICommand ClearInputs { get; private set; }

        private string _projectsSelected;
        public string ProjectsSelected
        {
            get => _projectsSelected;
            set
            {
                _projectsSelected = value;
                OnPropertyChanged();
            }
        }

        private string _companyName;
        public string CompanyName
        {
            get => _companyName;
            set
            {
                _companyName = value;
                OnPropertyChanged();
            }
        }

        private string _positionApplied;
        public string PositionApplied
        {
            get => _positionApplied;
            set
            {
                _positionApplied = value;
                OnPropertyChanged();
            }
        }

        private string _filePath;
        public string FilePath
        {
            get => _filePath;
            set
            {
                _filePath = value;
                OnPropertyChanged();
            }
        }

        private string _emailAddress;
        public string EmailAddress
        {
            get => _emailAddress;
            set
            {
                _emailAddress = value;
                OnPropertyChanged();
            }
        }

        private int _sendOrSave;
        public int SendOrSave
        {
            get
            {
                return _sendOrSave;
            }
            set
            {
                _sendOrSave = value;
                OnPropertyChanged();
                VisibilityUpdate(_sendOrSave);
            }
        }

        private bool _isEmailAddressVisible;
        public bool IsSendingEmailVisible
        {
            get
            {
                return _isEmailAddressVisible;
            }
            set
            {
                _isEmailAddressVisible = value;
                OnPropertyChanged();
            }
        }

        private bool _isFilePathVisible;
        public bool IsFIleSavingVisible
        {
            get
            {
                return _isFilePathVisible;
            }
            set
            {
                _isFilePathVisible = value;
                OnPropertyChanged();
            }
        }

        private void VisibilityUpdate(int _sendOrSave)
        {
            if (_sendOrSave == 1)
            {
                IsFIleSavingVisible = true;
                IsSendingEmailVisible = false;
            }
            else
            {
                IsFIleSavingVisible = false;
                IsSendingEmailVisible = true;
            }
        }
    }
}
