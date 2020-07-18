using CV_Creator.DAL;
using CV_Creator.Desktop.ViewModels;
using CV_Creator.Models;
using CV_Creator.Services;
using Moq;
using System.Collections.ObjectModel;
using System.Linq;
using Xunit;

namespace CV_Creator.Tests
{
    public class TechStackLoaderViewModelTests
    {
        private TechStackLoaderViewModel _viewModel;
        private readonly Mock<ITechRepository> _repositoryMock;
        private readonly Mock<IWindowManager> _winManagerMock;

        public TechStackLoaderViewModelTests()
        {
            _repositoryMock = new Mock<ITechRepository>();
            _winManagerMock = new Mock<IWindowManager>();

            _viewModel = new TechStackLoaderViewModel(_repositoryMock.Object, _winManagerMock.Object);
        }

        [Theory]
        [InlineData(16, false, true, 15)]
        [InlineData(15, true, true, 15)]
        [InlineData(14, true, false, 14)]
        public void SelectedCountCommand_ExecutedWithSelectedCountNumber_ChangesCheckedPropertyAccordingly(int selected, bool expectedChecked, bool expectedButtonActive, int expectedCheckedQty)
        {
            _viewModel.LoadedTechnologies = new ObservableCollection<CheckedTech>();
            for (int i = 0; i < selected; i++)
            {
                _viewModel.LoadedTechnologies.Add(new CheckedTech() { Checked = true });
            }

            _viewModel.SelectedCountCommand.Execute(_viewModel.LoadedTechnologies[0]);
            int checkedQtyResult = _viewModel.LoadedTechnologies.Where(project => project.Checked).Count();

            Assert.Equal(expectedChecked, _viewModel.LoadedTechnologies[0].Checked);
            Assert.Equal(expectedButtonActive, _viewModel.IsFinishButtonEnabled);
            Assert.Equal(expectedCheckedQty, checkedQtyResult);
        }
    }
}
