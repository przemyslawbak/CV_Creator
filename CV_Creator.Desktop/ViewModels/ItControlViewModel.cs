using CV_Creator.Desktop.Commands;
using CV_Creator.Models;
using CV_Creator.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CV_Creator.Desktop.ViewModels
{
    public class ItControlViewModel : ViewModelBase
    {
        private readonly IWindowManager _windowManager;
        private readonly IDataProcessor _dataProcessor;
        private readonly IFileManager _fileManager;
        private readonly IEmailManager _emailManager;
        private Func<IProjectLoaderViewModel> _projectLoaderVMCreator;

        public ItControlViewModel(
            IWindowManager windowManager,
            IDataProcessor dataProcessor,
            Func<IProjectLoaderViewModel> projectLoaderVMCreator,
            IFileManager fileManager,
            IEmailManager emailManager
            )
        {
            _windowManager = windowManager;
            _dataProcessor = dataProcessor;
            _projectLoaderVMCreator = projectLoaderVMCreator;
            _fileManager = fileManager;
            _emailManager = emailManager;

            OpenProjectsLoaderCommand = new AsyncCommand(async () => await OnOpenProjectsLoaderAsync());
            OpenFilePathWindowCommand = new DelegateCommand(OnSaveFilePathWindow);
            ExecuteOperationCommand = new DelegateCommand(OnExecuteOperation);
            ClearInputsCommand = new DelegateCommand(OnClearInputs);

            LoadControlsFromFile();
        }

        public ICommand OpenFilePathWindowCommand { get; private set; }
        public ICommand OpenProjectsLoaderCommand { get; private set; }
        public ICommand ExecuteOperationCommand { get; private set; }
        public ICommand ClearInputsCommand { get; private set; }

        public List<Project> LoadedProjects { get; set; }

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
                AreButtonsActive();
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
                AreButtonsActive();
            }
        }

        private void AreButtonsActive()
        {
            string[] allProps = { ProjectsSelected, CompanyName, PositionApplied, FilePath, EmailAddress };
            IsClearButtonEnabled = allProps.Any(prop => !string.IsNullOrEmpty(prop)) ? true : false;

            if (SendOrSave == 1)
            {
                string[] props = { ProjectsSelected, CompanyName, PositionApplied, FilePath };
                IsExecuteButtonEnabled = props.All(prop => !string.IsNullOrEmpty(prop)) ? true : false;
            }
            else
            {
                string[] props = { ProjectsSelected, CompanyName, PositionApplied, EmailAddress };
                IsExecuteButtonEnabled = props.All(prop => !string.IsNullOrEmpty(prop)) ? true : false;
            }
        }

        private void LoadControlsFromFile()
        {
            //TODO: load from file
            SendOrSave = 1;
            ProjectsSelected = string.Empty;
            CompanyName = string.Empty;
            PositionApplied = string.Empty;
            FilePath = string.Empty;
            EmailAddress = string.Empty;
        }

        private void OnClearInputs(object o)
        {
            SendOrSave = 1;
            ProjectsSelected = string.Empty;
            CompanyName = string.Empty;
            PositionApplied = string.Empty;
            FilePath = string.Empty;
            EmailAddress = string.Empty;
        }

        private void OnExecuteOperation(object o)
        {
            byte[] pdfCv = _dataProcessor.ProcessPortfolio(LoadedProjects);

            if (SendOrSave == 1)
            {
                _fileManager.SaveToDiskAsync(pdfCv, FilePath);
            }
            else
            {
                _emailManager.SendToAddressAsync(pdfCv, EmailAddress);
            }
        }

        private void OnSaveFilePathWindow(object o)
        {
            FilePath = _windowManager.OpenFileDialogWindow(FilePath);
        }

        private async Task OnOpenProjectsLoaderAsync()
        {
            LoadedProjects = await _windowManager.OpenResultWindowAsync(_projectLoaderVMCreator()) as List<Project>;

            ProjectsSelected = string.Empty;

            if (LoadedProjects != null)
            {
                List<string> names = new List<string>();

                foreach (var project in LoadedProjects)
                {
                    names.Add(project.Name);
                }

                ProjectsSelected = string.Join(", ", names.ToArray());
            }
        }
    }
}
