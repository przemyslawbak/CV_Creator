using CV_Creator.Desktop.Commands;
using CV_Creator.Models;
using CV_Creator.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CV_Creator.Desktop.ViewModels
{
    public class ItControlViewModel : ViewModelBase
    {
        private readonly static string _basicFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Alicja_Zalupska_application");
        private readonly IWindowManager _windowManager;
        private readonly ITechStackProcessor _stackProcessor;
        private readonly IDataProcessor _dataProcessor;
        private readonly IFileManager _fileManager;
        private readonly IEmailManager _emailManager;
        private Func<IProjectLoaderViewModel> _projectLoaderVMCreator;
        private Func<ITechStackLoaderViewModel> _techLoaderVMCreator;

        public ItControlViewModel(
            IWindowManager windowManager,
            IDataProcessor dataProcessor,
            Func<IProjectLoaderViewModel> projectLoaderVMCreator,
            Func<ITechStackLoaderViewModel> techLoaderVMCreator,
            IFileManager fileManager,
            IEmailManager emailManager,
            ITechStackProcessor stackProcessor
            )
        {
            _windowManager = windowManager;
            _dataProcessor = dataProcessor;
            _projectLoaderVMCreator = projectLoaderVMCreator;
            _techLoaderVMCreator = techLoaderVMCreator;
            _fileManager = fileManager;
            _emailManager = emailManager;
            _stackProcessor = stackProcessor;

            OpenProjectsLoaderCommand = new AsyncCommand(async () => await OnOpenProjectsLoaderAsync());
            //OpenTechLoaderCommand = new AsyncCommand(async () => await OnOpenTechLoaderAsync());
            CreatePdfCommand = new AsyncCommand(async () => await OnCreatePdfAsync());
            OpenFilePathWindowCommand = new DelegateCommand(OnSaveFilePathWindow);
            ClearInputsCommand = new DelegateCommand(OnClearInputs);

            LoadControlsFromFile();
        }

        public ICommand OpenFilePathWindowCommand { get; private set; }
        public ICommand OpenProjectsLoaderCommand { get; private set; }
        //public ICommand OpenTechLoaderCommand { get; private set; }
        public ICommand CreatePdfCommand { get; private set; }
        public ICommand ClearInputsCommand { get; private set; }

        public List<Project> LoadedProjects { get; set; }
        public List<Technology> LoadedTechStack { get; set; }
        public string FileName { get; set; } = _basicFilePath + ".pdf";

        private bool _processingData;
        public bool ProcessingData
        {
            get => _processingData;
            set
            {
                _processingData = value;
                OnPropertyChanged();
            }
        }

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

        private string _techSelected;
        public string TechStackSelected
        {
            get => _techSelected;
            set
            {
                _techSelected = value;
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
                CompanyNameUpdated();
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
            get => string.IsNullOrEmpty(_filePath) ? FileName : _filePath;
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
            get => _sendOrSave;
            set
            {
                _sendOrSave = value;
                OnPropertyChanged();
                AreButtonsActive();
            }
        }

        private void CompanyNameUpdated()
        {
            FilePath = string.IsNullOrEmpty(CompanyName) ? _basicFilePath + ".pdf" : _basicFilePath + "_for_" + CompanyName.ToUpper() + ".pdf";
        }

        private void AreButtonsActive()
        {
            string[] allProps = { ProjectsSelected, TechStackSelected, CompanyName, PositionApplied, FilePath, EmailAddress };
            IsClearButtonEnabled = allProps.Any(prop => !string.IsNullOrEmpty(prop)) ? true : false;

            if (SendOrSave == 1)
            {
                string[] props = { ProjectsSelected, TechStackSelected, CompanyName, PositionApplied, FilePath };
                IsExecuteButtonEnabled = props.All(prop => !string.IsNullOrEmpty(prop)) ? true : false;
            }
            else
            {
                string[] props = { ProjectsSelected, TechStackSelected, CompanyName, PositionApplied, EmailAddress };
                IsExecuteButtonEnabled = props.All(prop => !string.IsNullOrEmpty(prop)) ? true : false;
            }
        }

        private void LoadControlsFromFile()
        {
            //TODO: load from file
            SendOrSave = 1;
            ProjectsSelected = string.Empty;
            TechStackSelected = string.Empty;
            CompanyName = string.Empty;
            PositionApplied = string.Empty;
            FilePath = string.Empty;
            EmailAddress = string.Empty;
        }

        private void OnClearInputs(object o)
        {
            LoadControlsFromFile();
        }

        private async Task OnCreatePdfAsync()
        {
            ProcessingData = true;

            byte[] pdfCv = _dataProcessor.ProcessPortfolio(LoadedProjects, LoadedTechStack, CompanyName, PositionApplied);

            if (SendOrSave == 1)
            {
                await _fileManager.SaveToDiskAsync(pdfCv, FilePath);
            }
            else
            {
                _emailManager.SendToAddressAsync(pdfCv, EmailAddress);
            }

            await Task.Delay(1000);

            ProcessingData = false;
        }

        private void OnSaveFilePathWindow(object o)
        {
            FilePath = _windowManager.OpenFileDialogWindow(FilePath);
        }

        private async Task OnOpenProjectsLoaderAsync()
        {
            LoadedProjects = await _windowManager.OpenResultWindowAsync(_projectLoaderVMCreator()) as List<Project>;
            LoadedTechStack = _stackProcessor.SelectTechStack(LoadedProjects);
            TechStackSelected = UpdateTechStackSelected(LoadedTechStack);

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

        private string UpdateTechStackSelected(List<Technology> loadedTechStack)
        {
            string stack = string.Empty;

            if (loadedTechStack != null)
            {
                List<string> names = new List<string>();

                foreach (var tech in loadedTechStack)
                {
                    names.Add(tech.Name);
                }

                stack = string.Join(", ", names.ToArray());
            }

            return stack;
        }
    }
}
