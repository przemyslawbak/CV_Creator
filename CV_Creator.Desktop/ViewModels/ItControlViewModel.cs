using CV_Creator.Desktop.Commands;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CV_Creator.Desktop.ViewModels
{
    public class ItControlViewModel : ViewModelBase
    {
        public ItControlViewModel()
        {
            SendOrSave = 1;
            OpenProjectsLoaderAsync = new AsyncCommand(async () => await OnOpenProjectsLoader());

            //TODO: remove later
            EmailAddress = "email";
            FilePath = "file";
        }

        private async Task OnOpenProjectsLoader()
        {
            throw new NotImplementedException(); //TODO: await window opened and model update from window manager
        }

        public ICommand OpenProjectsLoaderAsync { get; private set; }

        private string _projectsSelected;
        public string ProjectsSelected
        {
            get => _projectsSelected;
            set
            {
                _projectsSelected = value;
                OnPropertyChanged();
            }
        }

        private string _companyName;
        public string CompanyName
        {
            get => _companyName;
            set
            {
                _companyName = value;
                OnPropertyChanged();
            }
        }

        private string _positionApplied;
        public string PositionApplied
        {
            get => _positionApplied;
            set
            {
                _positionApplied = value;
                OnPropertyChanged();
            }
        }

        private string _filePath;
        public string FilePath
        {
            get => _filePath;
            set
            {
                _filePath = value;
                OnPropertyChanged();
            }
        }

        private string _emailAddress;
        public string EmailAddress
        {
            get => _emailAddress;
            set
            {
                _emailAddress = value;
                OnPropertyChanged();
            }
        }

        private int _sendOrSave;
        public int SendOrSave
        {
            get
            {
                return _sendOrSave;
            }
            set
            {
                _sendOrSave = value;
                OnPropertyChanged();
                VisibilityUpdate(_sendOrSave);
            }
        }

        private bool _isEmailAddressVisible;
        public bool IsSendingEmailVisible
        {
            get
            {
                return _isEmailAddressVisible;
            }
            set
            {
                _isEmailAddressVisible = value;
                OnPropertyChanged();
            }
        }

        private bool _isFilePathVisible;
        public bool IsFIleSavingVisible
        {
            get
            {
                return _isFilePathVisible;
            }
            set
            {
                _isFilePathVisible = value;
                OnPropertyChanged();
            }
        }

        private void VisibilityUpdate(int _sendOrSave)
        {
            if (_sendOrSave == 1)
            {
                IsFIleSavingVisible = true;
                IsSendingEmailVisible = false;
            }
            else
            {
                IsFIleSavingVisible = false;
                IsSendingEmailVisible = true;
            }
        }
    }
}
