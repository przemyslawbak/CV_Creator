using System;

namespace CV_Creator.Desktop.ViewModels
{
    public class MainViewModel
    {
        public MainViewModel(ItControlViewModel _itControlViewModel, OffshoreControlViewModel _offshoreControlViewModel)
        {
            ItControlViewModel = _itControlViewModel;
            OffshoreControlViewModel = _offshoreControlViewModel;
        }

        public ItControlViewModel ItControlViewModel { get; set; }
        public OffshoreControlViewModel OffshoreControlViewModel { get; set; }
    }
}
