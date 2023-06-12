using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using PDC03_Final_Examination.Model;
using PDC03_Final_Examination.ViewModel;

namespace PDC03_Final_Examination.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopUpPage : ContentPage
    {
        public string SelectedOption { get; private set; }
        private AnimalModel _animalModel;
        public PopUpPage(AnimalModel animalModel)
        {
            InitializeComponent();
            _animalModel = animalModel;
            imagePicture.Source = _animalModel.Image;
        }

        private void UpdateButton_Clicked(object sender, EventArgs e)
        {
            SelectedOption = "Update";
            ClosePopup();
        }

        private void DeleteButton_Clicked(object sender, EventArgs e)
        {
            SelectedOption = "Delete";
            ClosePopup();
        }
        private void CancelButton_Clicked(object sender, EventArgs e)
        {
            SelectedOption = "Cancel";
            ClosePopup();
        }

        private async void ClosePopup()
        {
            await Navigation.PopModalAsync();
            MessagingCenter.Send(this, "OptionSelected", SelectedOption);
        }
    }
}