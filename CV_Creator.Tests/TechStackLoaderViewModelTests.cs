using CV_Creator.DAL;
using CV_Creator.Desktop.ViewModels;
using CV_Creator.Models;
using CV_Creator.Services;
using Moq;
using System.Collections.ObjectModel;
using Xunit;

namespace CV_Creator.Tests
{
    public class TechStackLoaderViewModelTests
    {
        private TechStackLoaderViewModel _viewModel;
        private readonly Mock<ITechRepository> _repositoryMock;
        private readonly Mock<IWindowManager> _winManagerMock;
        private readonly CheckedTech _sampleTech;

        public TechStackLoaderViewModelTests()
        {
            _repositoryMock = new Mock<ITechRepository>();
            _winManagerMock = new Mock<IWindowManager>();
            _sampleTech = new CheckedTech() { Checked = true };

            _viewModel = new TechStackLoaderViewModel(_repositoryMock.Object, _winManagerMock.Object);
        }

        [Theory]
        [InlineData(12, false)]
        [InlineData(11, true)]
        public void SelectedCountCommand_ExecutedWithSelectedCountNumber_ChangesCheckedPropertyAccordingly(int selected, bool expected)
        {
            _viewModel.SelectedCount = selected;
            _viewModel.DisplayCollection = new ObservableCollection<CheckedTech>()
            {
                _sampleTech
            };

            _viewModel.SelectedCountCommand.Execute(_sampleTech);

            Assert.Equal(expected, _sampleTech.Checked);
            Assert.Single(_viewModel.DisplayCollection);
        }
    }
}
