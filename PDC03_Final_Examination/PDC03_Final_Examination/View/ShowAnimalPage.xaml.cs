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
    public partial class ShowAnimalPage : ContentPage
    {
        AnimalViewModel viewModel;

        public ShowAnimalPage()
        {
            InitializeComponent();
            viewModel = new AnimalViewModel();
        }
        private void showAnimal()
        {
            var res = viewModel.GetAllAnimals().Result;
            lstData.ItemsSource = res;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            showAnimal();
        }

        private void btnAddRecord_Clicked(object sender, EventArgs e)
        {
            this.Navigation.PushAsync(new AddAnimal());
        }

        private async void lstData_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                AnimalModel obj = (AnimalModel)e.SelectedItem;
                var popupPage = new PopUpPage(obj);
                await Navigation.PushModalAsync(popupPage);

                MessagingCenter.Subscribe<PopUpPage, string>(this, "OptionSelected", async (page, selectedOption) =>
                {
                    //obj.Image = imageDirectory;
                    if (selectedOption == "Update")
                    {
                        await Navigation.PushAsync(new AddAnimal(obj));
                    }
                    else if (selectedOption == "Delete")
                    {
                        viewModel.DeleteAnimal(obj);
                        showAnimal();
                    }
                    else if (selectedOption == "Cancel")
                    {
                        showAnimal();
                    }

                    lstData.SelectedItem = null;
                    MessagingCenter.Unsubscribe<PopUpPage, string>(this, "OptionSelected");
                });
            }
        }
    }
}