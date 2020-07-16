using CV_Creator.DAL;
using CV_Creator.Desktop.Commands;
using CV_Creator.Models;
using CV_Creator.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CV_Creator.Desktop.ViewModels
{
    public interface IProjectLoaderViewModel
    {

    }

    public class ProjectLoaderViewModel : ViewModelBase, IProjectLoaderViewModel, IResultViewModel
    {
        private readonly IWindowManager _winService;
        private readonly IProjectRepository _repositoryProj;
        private readonly IProjectCollectionDisplayService _paginationService;
        private List<CheckedProject> _loadedAllProjects;
        private List<CheckedProject> _filteredProjects;
        private readonly int _displayItemsPerPage;
        private readonly int _maxProjectsToBeSelected = 6;

        public ProjectLoaderViewModel(IProjectRepository repoProj, IProjectCollectionDisplayService paginationService, IWindowManager winService)
        {
            _winService = winService;
            _repositoryProj = repoProj;
            _paginationService = paginationService;
            _displayItemsPerPage = 4;
            CurrentPage = 1;

            Initialization = LoadDataAndInitPropertiesAsync();

            NextClickCommand = new DelegateCommand(OnNextClick);
            PrevClickCommand = new DelegateCommand(OnPrevClick);
            FinishClickCommand = new DelegateCommand(OnFinishClick);
            SelectedCountCommand = new DelegateCommand(OnSelectedCount);
        }

        public ICommand NextClickCommand { get; private set; }
        public ICommand PrevClickCommand { get; private set; }
        public ICommand FinishClickCommand { get; private set; }
        public ICommand SelectedCountCommand { get; private set; }

        public Task Initialization { get; private set; }
        public object ObjectResult { get; set; }
        public int MaxProjectsSelected { get; set; }

        private string _filterTechPhrase;
        public string FilterTechPhrase
        {
            get => _filterTechPhrase;
            set
            {
                _filterTechPhrase = value;
                OnPropertyChanged();
                CurrentPage = 1;
                _filteredProjects = FilterPageCollection();
                DisplayCollection = GetNewPageCollection();
                PageCount = GetPageCount();
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

        private int _pageCount;
        public int PageCount
        {
            get => _pageCount;
            set
            {
                _pageCount = value;
                OnPropertyChanged();
            }
        }

        private int _currentPage;
        public int CurrentPage
        {
            get => _currentPage;
            set
            {
                _currentPage = value;
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

        private ObservableCollection<CheckedProject> _displayCollection;
        public ObservableCollection<CheckedProject> DisplayCollection
        {
            get => _displayCollection;
            set
            {
                _displayCollection = value;
                OnPropertyChanged();
            }
        }

        private async Task LoadDataAndInitPropertiesAsync()
        {
            LoadingData = true;
            MaxProjectsSelected = _maxProjectsToBeSelected;
            _loadedAllProjects = await _repositoryProj.GetAllCheckedProjectsAsync();
            _filteredProjects = _loadedAllProjects;
            DisplayCollection = GetNewPageCollection();
            PageCount = _paginationService.GetPagesCount(_filteredProjects.Count(), _displayItemsPerPage);
            LoadingData = false;
        }

        private void OnNextClick(object obj)
        {
            if (CurrentPage < PageCount)
            {
                CurrentPage++;
                DisplayCollection = GetNewPageCollection();
            }
        }

        private void OnPrevClick(object obj)
        {
            if (CurrentPage > 1)
            {
                CurrentPage--;
                DisplayCollection = GetNewPageCollection();
            }
        }

        private ObservableCollection<CheckedProject> GetNewPageCollection()
        {
            return new ObservableCollection<CheckedProject>(_paginationService.GetDisplayResults(
                CurrentPage,
                _displayItemsPerPage,
                _filteredProjects));
        }

        private List<CheckedProject> FilterPageCollection()
        {
            return _paginationService.FilterDisplayedCollection(
                    FilterTechPhrase,
                    _filteredProjects,
                    _loadedAllProjects,
                    _displayItemsPerPage,
                    CurrentPage);
        }
        private int GetPageCount()
        {
            return _paginationService.GetPagesCount(_filteredProjects.Count(), _displayItemsPerPage);
        }

        private void OnFinishClick(object obj)
        {
            ObjectResult = _repositoryProj.GetProjectsFromChecked(_filteredProjects.Where(item => item.Checked == true).ToList());
            _winService.CloseWindow(this);
        }

        private void OnSelectedCount(object obj)
        {
            if (SelectedCount < _maxProjectsToBeSelected)
            {
                SelectedCount = _filteredProjects.Where(project => project.Checked).Count();
            }
            else
            {
                CheckedProject projectFromTheList = obj as CheckedProject;
                projectFromTheList.Checked = false;
            }

            DisplayCollection = GetNewPageCollection();
        }
    }
}
