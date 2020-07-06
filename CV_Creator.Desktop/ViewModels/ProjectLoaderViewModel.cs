using CV_Creator.DAL;
using CV_Creator.Desktop.Commands;
using CV_Creator.Models;
using CV_Creator.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
        private readonly List<CheckedProject> _loadedAllProjects;
        private List<CheckedProject> _filteredProjects;
        private readonly int _displayItemsPerPage;

        public ProjectLoaderViewModel(IProjectRepository repoProj, IProjectCollectionDisplayService paginationService, IWindowManager winService)
        {
            _winService = winService;
            _repositoryProj = repoProj;
            _paginationService = paginationService;
            _loadedAllProjects = _repositoryProj.GetAllCheckedProjects();
            _filteredProjects = _loadedAllProjects;
            _displayItemsPerPage = 2;

            CurrentPage = 1;
            DisplayCollection = GetNewPageCollection();
            PageCount = _paginationService.GetPagesCount(_filteredProjects.Count(), _displayItemsPerPage);

            NextClickCommand = new DelegateCommand(OnNextClick);
            PrevClickCommand = new DelegateCommand(OnPrevClick);
            FinishClickCommand = new DelegateCommand(OnFinishClick);
        }

        public ICommand NextClickCommand { get; private set; }
        public ICommand PrevClickCommand { get; private set; }
        public ICommand FinishClickCommand { get; private set; }

        //TODO: number of added, max number

        public object ObjectResult { get; set; }

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
    }
}
