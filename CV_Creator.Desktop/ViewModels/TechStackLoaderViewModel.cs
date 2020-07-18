using CV_Creator.DAL;
using CV_Creator.Desktop.Commands;
using CV_Creator.Models;
using CV_Creator.Services;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CV_Creator.Desktop.ViewModels
{
    public interface ITechStackLoaderViewModel
    {

    }

    public class TechStackLoaderViewModel : ViewModelBase, ITechStackLoaderViewModel, IResultViewModel
    {
        private readonly IWindowManager _winService;
        private readonly ITechRepository _repositoryTech;
        private readonly int _maxTechnologiesToBeSelected = 15;

        public TechStackLoaderViewModel(ITechRepository repositoryTech, IWindowManager winService)
        {
            _winService = winService;
            _repositoryTech = repositoryTech;

            MaxTechSelected = _maxTechnologiesToBeSelected;
            Initialization = LoadDataAndInitPropertiesAsync();

            FinishClickCommand = new DelegateCommand(OnFinishClick);
            SelectedCountCommand = new DelegateCommand(OnSelectedCount);
        }

        public ICommand FinishClickCommand { get; private set; }
        public ICommand SelectedCountCommand { get; private set; }

        public Task Initialization { get; private set; }
        public object ObjectResult { get; set; }
        public int MaxTechSelected { get; set; }

        private bool _isFinishButtonEnabled;
        public bool IsFinishButtonEnabled
        {
            get => _isFinishButtonEnabled;
            set
            {
                _isFinishButtonEnabled = value;
                OnPropertyChanged();
            }
        }

        private int _selectedCount;
        public int SelectedCount
        {
            get => _selectedCount;
            set
            {
                _selectedCount = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<CheckedTech> _loadedTechnologies;
        public ObservableCollection<CheckedTech> LoadedTechnologies
        {
            get => _loadedTechnologies;
            set
            {
                _loadedTechnologies = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<CheckedTech> _displayCollection;
        public ObservableCollection<CheckedTech> DisplayCollection
        {
            get => _displayCollection;
            set
            {
                _displayCollection = value;
                OnPropertyChanged();
            }
        }

        private bool _loadingData;
        public bool LoadingData
        {
            get => _loadingData;
            set
            {
                _loadingData = value;
                OnPropertyChanged();
            }
        }

        private async Task LoadDataAndInitPropertiesAsync()
        {
            LoadingData = true;
            LoadedTechnologies = new ObservableCollection<CheckedTech>(await _repositoryTech.GetAllCheckedTechnologiesAsync());
            DisplayCollection = LoadedTechnologies;
            LoadingData = false;
        }

        private void OnFinishClick(object obj)
        {
            ObjectResult = _repositoryTech.GetTechnologiesFromChecked(DisplayCollection.Where(item => item.Checked == true).ToList());
            _winService.CloseWindow(this);
        }

        private void OnSelectedCount(object obj)
        {
            SelectedCount = LoadedTechnologies.Where(project => project.Checked).Count();

            if (SelectedCount > _maxTechnologiesToBeSelected)
            {
                SelectedCount--;
                CheckedTech techFromTheList = obj as CheckedTech;
                techFromTheList.Checked = false;

                DisplayCollection = new ObservableCollection<CheckedTech>();
                DisplayCollection = LoadedTechnologies;
            }

            if (SelectedCount == _maxTechnologiesToBeSelected)
            {
                IsFinishButtonEnabled = true;
            }
            else
            {
                IsFinishButtonEnabled = false;
            }
        }
    }
}
