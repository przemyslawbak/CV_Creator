using CV_Creator.Desktop.ViewModels;
using CV_Creator.Services;
using Moq;
using Xunit;

namespace CV_Creator.Tests
{
    public class ItControlViewModelTests
    {
        private ItControlViewModel _viewModel;
        private readonly Mock<IWindowManager> _windowManagerMock;
        private readonly Mock<IFileManager> _fileManagerMock;

        public ItControlViewModelTests()
        {
            _windowManagerMock = new Mock<IWindowManager>();
            _fileManagerMock = new Mock<IFileManager>();

            _viewModel = new ItControlViewModel(_windowManagerMock.Object, _fileManagerMock.Object, ProjectLoaderViewModel);
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

            _windowManagerMock.Verify(mock => mock.OpenResultWindow(It.IsAny<object>()), Times.Once);
        }

        //TODO: what returns OpenResultWindow

        //TODO: what returns OpenFileDialogWindow
    }
}
