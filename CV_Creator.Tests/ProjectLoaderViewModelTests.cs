using CV_Creator.DAL;
using CV_Creator.Desktop.ViewModels;
using CV_Creator.Models;
using CV_Creator.Services;
using Moq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xunit;

namespace CV_Creator.Tests
{
    public class ProjectLoaderViewModelTests
    {
        private ProjectLoaderViewModel _viewModel;
        private readonly Mock<IProjectRepository> _repositoryMock;
        private readonly Mock<IProjectCollectionDisplayService> _paginationServiceMock;
        private readonly Mock<IWindowManager> _winManagerMock;
        private readonly CheckedProject _sampleProject;

        public ProjectLoaderViewModelTests()
        {
            _repositoryMock = new Mock<IProjectRepository>();
            _paginationServiceMock = new Mock<IProjectCollectionDisplayService>();
            _winManagerMock = new Mock<IWindowManager>();

            _sampleProject = new CheckedProject() { Checked = true };
            _paginationServiceMock.Setup(mock => mock.GetDisplayResults(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<List<CheckedProject>>())).Returns(new List<CheckedProject>() { _sampleProject });
            _repositoryMock.Setup(mock => mock.GetAllCheckedProjectsAsync()).ReturnsAsync(new List<CheckedProject>() { _sampleProject });

            _viewModel = new ProjectLoaderViewModel(_repositoryMock.Object, _paginationServiceMock.Object, _winManagerMock.Object);
        }

        [Theory]
        [InlineData(6, false)]
        [InlineData(5, true)]
        public void SelectedCountCommand_ExecutedWithSelectedCountNumber_ChangesCheckedPropertyAccordingly(int selected, bool expected)
        {
            _viewModel.SelectedCount = selected;
            _viewModel.DisplayCollection = new ObservableCollection<CheckedProject>()
            {
                _sampleProject
            };

            _viewModel.SelectedCountCommand.Execute(_sampleProject);

            Assert.Equal(expected ,_sampleProject.Checked);
            Assert.Single(_viewModel.DisplayCollection);
        }
    }
}
