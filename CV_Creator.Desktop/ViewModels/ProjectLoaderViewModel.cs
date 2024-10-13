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
        private readonly IStringSanitizer _stringSanitizer;
        private readonly IProjectRepository _repositoryProj;
        private readonly IProjectCollectionDisplayService _paginationService;
        private List<CheckedProject> _loadedAllProjects;
        private readonly int _displayItemsPerPage;
        private readonly int _projectsQtyToBeSelected = 6;

        public ProjectLoaderViewModel(IProjectRepository repoProj, IProjectCollectionDisplayService paginationService, IWindowManager winService, IStringSanitizer stringSanitizer)
        {
            _winService = winService;
            _stringSanitizer = stringSanitizer;
            _repositoryProj = repoProj;
            _paginationService = paginationService;
            _displayItemsPerPage = 4;
            CurrentPage = 1;

            LoadDataAndInitPropertiesAsync();

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
        public List<CheckedProject> FilteredProjects { get; set; }

        private string _filterTechPhrase;
        public string FilterTechPhrase
        {
            get => _filterTechPhrase;
            set
            {
                _filterTechPhrase = value;
                OnPropertyChanged();
                CurrentPage = 1;
                FilteredProjects = FilterPageCollection();
                DisplayCollection = GetNewPageCollection();
                PageCount = GetPageCount();
            }
        }

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

        private void LoadDataAndInitPropertiesAsync()
        {
            LoadingData = true;
            MaxProjectsSelected = _projectsQtyToBeSelected;
            _loadedAllProjects = _repositoryProj.GetAllCheckedProjectsAsync();
            _loadedAllProjects = CleanUpHtml(_loadedAllProjects);
            FilteredProjects = _loadedAllProjects;
            DisplayCollection = GetNewPageCollection();
            PageCount = _paginationService.GetPagesCount(FilteredProjects.Count(), _displayItemsPerPage);
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
                FilteredProjects));
        }

        private List<CheckedProject> FilterPageCollection()
        {
            return _paginationService.FilterDisplayedCollection(
                    FilterTechPhrase,
                    FilteredProjects,
                    _loadedAllProjects,
                    _displayItemsPerPage,
                    CurrentPage);
        }
        private int GetPageCount()
        {
            return _paginationService.GetPagesCount(FilteredProjects.Count(), _displayItemsPerPage);
        }

        private void OnFinishClick(object obj)
        {
            ObjectResult = _repositoryProj.GetProjectsFromChecked(FilteredProjects.Where(item => item.Checked == true).ToList());
            _winService.CloseWindow(this);
        }

        private void OnSelectedCount(object obj)
        {
            SelectedCount = FilteredProjects.Where(project => project.Checked).Count();

            if (SelectedCount > _projectsQtyToBeSelected)
            {
                SelectedCount--;
                CheckedProject projectFromTheList = obj as CheckedProject;
                projectFromTheList.Checked = false;

                DisplayCollection = new ObservableCollection<CheckedProject>();
                DisplayCollection = GetNewPageCollection();
            }

            if (SelectedCount == _projectsQtyToBeSelected)
            {
                IsFinishButtonEnabled = true;
            }
            else
            {
                IsFinishButtonEnabled = false;
            }
        }

        private List<CheckedProject> CleanUpHtml(List<CheckedProject> list)
        {
            foreach (var item in list)
            {
                item.Comment = _stringSanitizer.CleanUpComment(item.Comment);
            }

            return list;
        }
    }
}
