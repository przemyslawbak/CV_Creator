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
        private readonly Mock<ITechStackProcessor> _stackProcessor;

        public ItControlViewModelTests()
        {
            _windowManagerMock = new Mock<IWindowManager>();
            _dataProcessorMock = new Mock<IDataProcessor>();
            _fileManagerMock = new Mock<IFileManager>();
            _emailManagerMock = new Mock<IEmailManager>();
            _stackProcessor = new Mock<ITechStackProcessor>();

            _viewModel = new ItControlViewModel(
                _windowManagerMock.Object,
                _dataProcessorMock.Object,
                ProjectLoaderViewModel,
                TechLoaderViewModel,
                _fileManagerMock.Object,
                _emailManagerMock.Object,
                _stackProcessor.Object);
        }

        private IProjectLoaderViewModel ProjectLoaderViewModel()
        {
            var projectLoaderViewModelMock = new Mock<IProjectLoaderViewModel>();

            return projectLoaderViewModelMock.Object;
        }

        private ITechStackLoaderViewModel TechLoaderViewModel()
        {
            var techLoaderViewModelMock = new Mock<ITechStackLoaderViewModel>();

            return techLoaderViewModelMock.Object;
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

        [Fact]
        public void ExecuteOperationCommand_Executed_CallsProcessPortfolioOnce()
        {
            _viewModel.CreatePdfCommand.Execute(null);

            _dataProcessorMock.Verify(mock => mock.ProcessPortfolio(It.IsAny<List<Project>>(), It.IsAny<List<Technology>>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void ExecuteOperationCommand_ExecutedWhenSendOrSaveIsSave_CallsSaveToDiskAsyncOnce()
        {
            _viewModel.SendOrSave = 1;

            _viewModel.CreatePdfCommand.Execute(null);

            _fileManagerMock.Verify(mock => mock.SaveToDiskAsync(It.IsAny<byte[]>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void ExecuteOperationCommand_ExecutedWhenSendOrSaveIsSend_CallsSaveToDiskAsyncOnce()
        {
            _viewModel.SendOrSave = 2;

            _viewModel.CreatePdfCommand.Execute(null);

            _emailManagerMock.Verify(mock => mock.SendToAddressAsync(It.IsAny<byte[]>(), It.IsAny<string>()), Times.Once);
        }
    }
}
