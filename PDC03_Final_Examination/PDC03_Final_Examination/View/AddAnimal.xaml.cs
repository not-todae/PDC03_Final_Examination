using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using PDC03_Final_Examination.Model;
using PDC03_Final_Examination.ViewModel;
using Xamarin.Essentials;
using System.IO;

namespace PDC03_Final_Examination.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddAnimal : ContentPage
    {
        AnimalViewModel _viewModel;
        bool _isUpdate;
        int animalID;
        string imageDirectory;
        public AddAnimal()
        {
            InitializeComponent();
            _viewModel = new AnimalViewModel();
            _isUpdate = false;
        }
        public AddAnimal(AnimalModel obj)
        {
            InitializeComponent();
            _viewModel = new AnimalViewModel();

            if (obj != null)
            {
                animalID = obj.Id;
                txtCode.Text = obj.Code;
                txtCharacteristics.Text = obj.Characteristics;
                txtSpecies.Text = obj.Species;
                txtHabitat.Text = obj.Habitat;
                pckThreat.SelectedItem = obj.Threat;
                _isUpdate = true;

                if (!string.IsNullOrEmpty(obj.Image))
                {
                    imageDirectory = obj.Image;
                }
            }
        }

        private async void btnSaveUpdate_Clicked(object sender, EventArgs e)
        {
            AnimalModel obj = new AnimalModel();
            obj.Code = txtCode.Text;
            obj.Characteristics = txtCharacteristics.Text;
            obj.Species = txtSpecies.Text;
            obj.Habitat = txtHabitat.Text;
            obj.Threat = (string)pckThreat.SelectedItem;
            obj.Image = imageDirectory;

            if (_isUpdate)
            {
                obj.Id = animalID;
                await _viewModel.UpdateAnimal(obj);
            }
            else
            {
                _viewModel.InsertAnimal(obj);
            }
            await this.Navigation.PopAsync();
        }
        private async void UploadImage_Clicked(object sender, EventArgs e)
        {
            try
            {
                var mediaOptions = new MediaPickerOptions
                {
                    Title = "Select Animal Image"
                };

                var selectedImageFile = await MediaPicker.PickPhotoAsync(mediaOptions);

                if (selectedImageFile != null)
                {

                    // Save the image file to the app's local storage
                    string fileName = Path.GetFileName(selectedImageFile.FileName);
                    string destinationPath = Path.Combine(App.ImageFolderPath, fileName);

                    using (var stream = await selectedImageFile.OpenReadAsync())
                    {
                        using (var fileStream = File.Create(destinationPath))
                        {
                            await stream.CopyToAsync(fileStream);
                        }
                    }

                    // Save the image directory in the model
                    imageDirectory =    Path.Combine(App.ImageFolderPath, fileName);
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions
            }

        }
    }
}