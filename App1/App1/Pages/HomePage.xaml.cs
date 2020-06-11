using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
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

        private void Feedback_Button_Clicked(object sender, EventArgs e)
        {
        }

        private void TDM_Clicked(object sender, EventArgs e)
        {
        }

        private void STDM_Clicked(object sender, EventArgs e)
        {
        }
    }
}