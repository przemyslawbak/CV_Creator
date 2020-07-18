using CV_Creator.DAL;
using CV_Creator.Desktop.ViewModels;
using CV_Creator.Models;
using CV_Creator.Services;
using Moq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xunit;

namespace CV_Creator.Tests
{
    public class ProjectLoaderViewModelTests
    {
        private ProjectLoaderViewModel _viewModel;
        private readonly Mock<IProjectRepository> _repositoryMock;
        private readonly Mock<IProjectCollectionDisplayService> _paginationServiceMock;
        private readonly Mock<IWindowManager> _winManagerMock;
        private readonly Mock<IStringSanitizer> _stringSanitizerMock;
        private readonly CheckedProject _sampleProjectChecked;

        public ProjectLoaderViewModelTests()
        {
            _repositoryMock = new Mock<IProjectRepository>();
            _paginationServiceMock = new Mock<IProjectCollectionDisplayService>();
            _winManagerMock = new Mock<IWindowManager>();
            _stringSanitizerMock = new Mock<IStringSanitizer>();

            _sampleProjectChecked = new CheckedProject() { Checked = true };
            _paginationServiceMock.Setup(mock => mock.GetDisplayResults(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<List<CheckedProject>>())).Returns(new List<CheckedProject>() { _sampleProjectChecked });
            _repositoryMock.Setup(mock => mock.GetAllCheckedProjectsAsync()).ReturnsAsync(new List<CheckedProject>() { });

            _viewModel = new ProjectLoaderViewModel(_repositoryMock.Object, _paginationServiceMock.Object, _winManagerMock.Object, _stringSanitizerMock.Object);
        }

        [Theory]
        [InlineData(7, false, true, 6)]
        [InlineData(6, true, true, 6)]
        [InlineData(5, true, false, 5)]
        public void SelectedCountCommand_ExecutedWithSelectedCountNumber_ChangesCheckedPropertyAccordingly(int selected, bool expectedChecked, bool expectedButtonActive, int expectedCheckedQty)
        {
            for (int i = 0; i < selected; i++)
            {
                _viewModel.FilteredProjects.Add(new CheckedProject() { Checked = true });
            }

            _viewModel.SelectedCountCommand.Execute(_viewModel.FilteredProjects[0]);
            int checkedQtyResult = _viewModel.FilteredProjects.Where(project => project.Checked).Count();

            Assert.Equal(expectedChecked, _viewModel.FilteredProjects[0].Checked);
            Assert.Equal(expectedButtonActive, _viewModel.IsFinishButtonEnabled);
            Assert.Equal(expectedCheckedQty, checkedQtyResult);
        }

        [Fact]
        public void SelectedCountCommand_Executed_GetDisplayResultsIsCalledOnce()
        {
            _viewModel.DisplayCollection = new ObservableCollection<CheckedProject>()
            {
                _sampleProjectChecked
            };

            _viewModel.SelectedCountCommand.Execute(_sampleProjectChecked);

            _paginationServiceMock.Verify(mock => mock.GetDisplayResults(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<List<CheckedProject>>()), Times.Once);
        }
    }
}
