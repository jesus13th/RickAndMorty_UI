using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RickAndMorty.Models;
using RickAndMorty.Services;

using Xamarin.Forms;

namespace RickAndMorty {
    public partial class MainPage : ContentPage {
        public List<Result> Characters = new List<Result>();
        private RickAndMortyService rickAndMortyService;
        private bool canNavigate = true;

        public MainPage() {
            InitializeComponent();
            rickAndMortyService = new RickAndMortyService();
        }
        protected override void OnAppearing() {
            base.OnAppearing();
            GetResults();
        }
        private async void GetResults() {
            canNavigate = false;
            var async_characters = await rickAndMortyService.GetCharacters();
            characterList.ItemsSource = async_characters.results;
            counterLbl.Text = $"Page {rickAndMortyService.counter}/{async_characters.info.pages}";

            previousBtn.IsEnabled = rickAndMortyService.counter > 1;
            nextBtn.IsEnabled = rickAndMortyService.counter < async_characters.info.pages;
            canNavigate = true;
        }

        private void nextBtn_Clicked(object sender, EventArgs e) {
            if (canNavigate) {
                rickAndMortyService.counter++;
                GetResults();
            }
        }

        private void previousBtn_Clicked(object sender, EventArgs e) {
            if (canNavigate) {
                rickAndMortyService.counter--;
                GetResults();
            }
        }
    }
}
