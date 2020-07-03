using CV_Creator.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace CV_Creator.Services
{
    public class WindowManager : IWindowManager
    {
        /// <summary>
        /// Constructor, creates instance of OpenedViews list if not created yet.
        /// </summary>
        public WindowManager()
        {
            if (OpenedViews == null)
            {
                OpenedViews = new List<WindowModel>();
            }
        }

        /// <summary>
        /// List of currently opened WindowModels.
        /// </summary>
        public List<WindowModel> OpenedViews { get; set; }

        /// <summary>
        /// Opens window for specific view model using name convention. Attaches event handler for Window.Closed.
        /// </summary>
        /// <param name="viewModel">View model name</param>
        public void OpenWindow(object viewModel)
        {
            if (!CheckIfAlreadyOpened(viewModel))
            {
                WindowModel model = CreateWindoModel(viewModel);

                model.OpenedWindow.Closed += new EventHandler((s, e) => WindowClosed(s, e, model));
                model.OpenedWindow.Show();
            }
        }

        /// <summary>
        /// OpenFileDialog wrapper.
        /// </summary>
        /// <param name="filePath">Initial file path</param>
        /// <returns>String path</returns>
        public string OpenFileDialogWindow(string filePath)
        {
            string result = filePath;

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.DefaultExt = ".pdf"; // Default file extension
            saveFileDialog.Filter = "Text documents (.pdf)|*.pdf"; // Filter files by extension
            saveFileDialog.InitialDirectory = filePath;
            saveFileDialog.RestoreDirectory = true;
            bool? dialog = saveFileDialog.ShowDialog();

            // Process save file dialog box results
            if (dialog == true)
            {
                // Save document
                result = saveFileDialog.FileName;
            }

            return result;
        }

        /// <summary>
        /// Opens new window for specific view model using name convention. Window may return object. Attaches event handler for Window.Closed.
        /// </summary>
        /// <param name="viewModel">View model name</param>
        /// <returns>Object result</returns>
        public async Task<object> OpenResultWindow(object viewModel)
        {
            if (!CheckIfAlreadyOpened(viewModel))
            {
                WindowModel model = CreateWindoModel(viewModel);

                model.OpenedWindow.Closed += new EventHandler((s, e) => ResultWindowClosed(s, e, model));
                model.OpenedWindow.ShowDialog();

                while (!model.IsValueReturned)
                    await Task.Delay(100);

                return model.ReturnedObjectResult;
            }

            return null;
        }

        /// <summary>
        /// Verify if WindowModel can be found in OpenedViews.
        /// </summary>
        /// <param name="viewModel">View model related to the window</param>
        /// <returns>Bool type, if the window is opened already or not</returns>
        private bool CheckIfAlreadyOpened(object viewModel)
        {
            return OpenedViews.Select(model => model.AssignedViewModel.GetType() == viewModel.GetType()).FirstOrDefault();
        }

        /// <summary>
        /// Extracts window name from passed view model object and adds new WindowModel to OpenedViews list.
        /// </summary>
        /// <param name="viewModel">View model object</param>
        /// <returns>WindowModel object</returns>
        private WindowModel CreateWindoModel(object viewModel)
        {
            var modelType = viewModel.GetType();
            var windowTypeName = modelType.Name.Replace("ViewModel", "Window");
            var windowTypes = from t in modelType.Assembly.GetTypes()
                              where t.IsClass && t.Name == windowTypeName
                              select t;

            //TODO: if not found window

            WindowModel model = GetWindowModelFromWindowName(windowTypes.Single(), viewModel);
            OpenedViews.Add(model);

            return model;
        }

        /// <summary>
        /// Handler  for modal dialog window Closed event, triggered when dialog window is about to close.
        /// </summary>
        /// <param name="sender">Window object</param>
        /// <param name="model">>Window model object related to the window</param>
        private void ResultWindowClosed(object sender, EventArgs args, WindowModel model)
        {
            Window window = (Window)sender;
            window.Closed -= new EventHandler((s, e) => ResultWindowClosed(s, e, model));

            OpenedViews.Remove(model);

            var vm = window.DataContext as IResultViewModel;
            model.ReturnedObjectResult = vm.ObjectResult;
            model.IsValueReturned = true;
        }

        /// <summary>
        /// Creates instance of new window basing on window type, returns new instance of WindowModel object.
        /// </summary>
        /// <param name="type">Window type</param>
        /// <param name="viewModel">View model related to the window</param>
        /// <returns>WindowModel object</returns>
        private WindowModel GetWindowModelFromWindowName(Type type, object viewModel)
        {
            Window window = (Window)Activator.CreateInstance(type);
            window.DataContext = viewModel;

            WindowModel model = new WindowModel()
            {
                OpenedWindow = window,
                AssignedViewModel = viewModel,
                IsValueReturned = false,
                ReturnedDialogResult = null,
                ReturnedObjectResult = null,
                ReturnedFilePathResult = null
            };

            return model;
        }

        /// <summary>
        /// Handler for Closed event, triggered when window is about to close.
        /// </summary>
        /// <param name="sender">Window object</param>
        /// <param name="model">Window model object related to the window</param>
        private void WindowClosed(object sender, EventArgs args, WindowModel model)
        {
            Window window = (Window)sender;
            window.Closed -= new EventHandler((s, e) => WindowClosed(s, e, model));

            OpenedViews.Remove(model);
        }
    }
}
