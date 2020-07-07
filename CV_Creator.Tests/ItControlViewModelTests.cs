using CV_Creator.Desktop.ViewModels;
using CV_Creator.Models;
using CV_Creator.Services;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace CV_Creator.Tests
{
    public class ItControlViewModelTests
    {
        private ItControlViewModel _viewModel;
        private readonly Mock<IWindowManager> _windowManagerMock;
        private readonly Mock<IDataProcessor> _dataProcessorMock;
        private readonly Mock<IFileManager> _fileManagerMock;
        private readonly Mock<IEmailManager> _emailManagerMock;

        public ItControlViewModelTests()
        {
            _windowManagerMock = new Mock<IWindowManager>();
            _dataProcessorMock = new Mock<IDataProcessor>();
            _fileManagerMock = new Mock<IFileManager>();
            _emailManagerMock = new Mock<IEmailManager>();

            _viewModel = new ItControlViewModel(
                _windowManagerMock.Object,
                _dataProcessorMock.Object,
                ProjectLoaderViewModel,
                _fileManagerMock.Object,
                _emailManagerMock.Object);
        }

        private IProjectLoaderViewModel ProjectLoaderViewModel()
        {
            var projectLoaderViewModelMock = new Mock<IProjectLoaderViewModel>();

            return projectLoaderViewModelMock.Object;
        }

        [Fact]
        public void OpenFilePathWindowCommand_Executed_CallsOpenFileDialogWindowOnce()
        {
            _viewModel.OpenFilePathWindowCommand.Execute(null);

            _windowManagerMock.Verify(mock => mock.OpenFileDialogWindow(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void OpenProjectsLoaderCommand_Executed_CallsOpenResultWindowOnce()
        {
            _viewModel.OpenProjectsLoaderCommand.Execute(null);

            _windowManagerMock.Verify(mock => mock.OpenResultWindowAsync(It.IsAny<object>()), Times.Once);
        }

        [Fact]
        public void OpenResultWindow_ReturnsValue_ProjectsSelectedIsString()
        {
            _viewModel.ProjectsSelected = string.Empty;
            _windowManagerMock.Setup(window => window.OpenResultWindowAsync(It.IsAny<object>())).ReturnsAsync(new List<Project>() { new Project() { Name = "Test project" }, new Project() { Name = "Second project" } });

            _viewModel.OpenProjectsLoaderCommand.Execute(null);

            Assert.Equal("Test project, Second project", _viewModel.ProjectsSelected);
        }

        [Fact]
        public void OpenResultWindow_ReturnsNull_ProjectsSelectedIsEmpty()
        {
            _viewModel.ProjectsSelected = string.Empty;
            _windowManagerMock.Setup(window => window.OpenResultWindowAsync(It.IsAny<object>())).ReturnsAsync(null);

            _viewModel.OpenProjectsLoaderCommand.Execute(null);

            Assert.Equal(string.Empty, _viewModel.ProjectsSelected);
        }
    }
}
