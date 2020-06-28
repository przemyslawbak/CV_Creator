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

            _viewModel = new ItControlViewModel(_windowManagerMock.Object, _fileManagerMock.Object);
        }

        [Fact]
        public void ClearInputsCommand_Executed_CallsGetDefaultPdfPathOnce()
        {
            _viewModel.ClearInputsCommand.Execute(null);

            _fileManagerMock.Verify(mock => mock.GetDefaultPdfPath(), Times.Once);
        }

        [Fact]
        public void OpenFilePathWindowCommand_Executed_CallsOpenFileDialogWindowOnce()
        {
            _viewModel.OpenFilePathWindowCommand.Execute(null);

            _windowManagerMock.Verify(mock => mock.OpenFileDialogWindow(It.IsAny<string>()), Times.Once);
        }
    }
}
