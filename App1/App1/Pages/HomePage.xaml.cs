using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.OpenWhatsApp;

namespace App1.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        NavigationPage matrixPage;
        public HomePage()
        {
            InitializeComponent();
            this.BindingContext = this;
        }
        public string Instructions { get => instructions; }
        public string Note { get => note; }
        public string Feedback { get => feedback; }
        public string Copyright { get => copyright; }
        public static string instructions = "TDM stands for Tri-Diagonal matrix and STDM stands for Space and Time Dependent Tri-Diagonal Matrix that can be used to solve matrix generated from conduction, convection, diffusion problems and the likes.";
        public static string note = "This app can solve tridiagonal matrix of any size but not any other non-tri-diagonal matrix.";
        public static string feedback = "we would love to hear your recommendations and feedbacks. Click the button below.";
        public static string copyright = "designed by BlessTheBoy ©2020";

        private async void Feedback_Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                Chat.Open("+2348174538295", @"Hi A.B.
My name is...
The Matrix Solver app is... and could be better if...");
            }
            catch (Exception)
            {
                await DisplayAlert("Not Installed", "Whatsapp not installed!", "ok");
            }
        }

        private async void TDM_Clicked(object sender, EventArgs e)
        {
            var sizePage = new SizePrompt(this);
            sizePage.type = "TDM";
            await Navigation.PushAsync(sizePage);
        }

        private async void STDM_Clicked(object sender, EventArgs e)
        {
            var sizePage = new SizePrompt(this);
            sizePage.type = "STDM";
            await Navigation.PushAsync(sizePage);
        }

        public void CreatePage(string type, int size)
        {
            if (type == "TDM")
            {
                if (size == 3)
                {
                    matrixPage = new NavigationPage(new TDMPage());
                }
                else if (size == 4)
                {
                    matrixPage = new NavigationPage(new TDMPage1());
                }
                else if (size == 5)
                {
                    matrixPage = new NavigationPage(new TDMPage2());
                }
                else if (size > 5)
                {
                    matrixPage = new NavigationPage(new TDMPage3(size));
                }
            }
            else
            {
                if (size == 3)
                {
                    matrixPage = new NavigationPage(new STDMPage());
                }
                else if (size == 4)
                {
                    matrixPage = new NavigationPage(new STDMPage1());
                }
                else if (size == 5)
                {
                    matrixPage = new NavigationPage(new STDMPage2());
                }
                else if (size > 5)
                {
                    matrixPage = new NavigationPage(new STDMPage3(size));
                }
            }
        }

        public async void LaunchPage()
        {
            await Navigation.PushAsync(matrixPage);
        }
    }
}