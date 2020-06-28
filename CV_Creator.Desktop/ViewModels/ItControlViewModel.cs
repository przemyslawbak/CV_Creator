using CV_Creator.Desktop.Commands;
using CV_Creator.Services;
using System;
using System.Linq;
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

            OpenProjectsLoaderCommand = new AsyncCommand(async () => await OnOpenProjectsLoaderAsync());
            OpenFilePathWindowCommand = new DelegateCommand(OnSaveFilePathWindow);
            ExecuteOperationCommand = new DelegateCommand(OnExecuteOperation);
            ClearInputsCommand = new DelegateCommand(OnClearInputs);

            //TODO: load latest from the file at first
        }

        public ICommand OpenFilePathWindowCommand { get; private set; }
        public ICommand OpenProjectsLoaderCommand { get; private set; }
        public ICommand ExecuteOperationCommand { get; private set; }
        public ICommand ClearInputsCommand { get; private set; }

        private bool _isExecuteButtonEnabled;
        public bool IsExecuteButtonEnabled
        {
            get => _isExecuteButtonEnabled;
            set
            {
                _isExecuteButtonEnabled = value;
                OnPropertyChanged();
            }
        }

        private bool _isClearButtonEnabled;
        public bool IsClearButtonEnabled
        {
            get => _isClearButtonEnabled;
            set
            {
                _isClearButtonEnabled = value;
                OnPropertyChanged();
            }
        }

        private string _projectsSelected;
        public string ProjectsSelected
        {
            get => _projectsSelected;
            set
            {
                _projectsSelected = value;
                OnPropertyChanged();
                AreButtonsActive();
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
                AreButtonsActive();
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
                AreButtonsActive();
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
                AreButtonsActive();
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

        private void AreButtonsActive()
        {
            string[] props = { ProjectsSelected, CompanyName, PositionApplied, EmailAddress };

            IsClearButtonEnabled = props.Any(prop => !string.IsNullOrEmpty(prop)) ? true : false;

            IsExecuteButtonEnabled = props.All(prop => !string.IsNullOrEmpty(prop)) ? true : false;
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

        private void OnSaveFilePathWindow(object o)
        {
            FilePath = _windowManager.OpenFileDialogWindow(FilePath);
        }

        private async Task OnOpenProjectsLoaderAsync()
        {
            throw new NotImplementedException();
        }
    }
}
