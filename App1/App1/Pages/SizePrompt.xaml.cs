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
    public partial class SizePrompt : ContentPage
    {
        HomePage p;
        public SizePrompt(HomePage mainPage)
        {
            InitializeComponent();
            p = mainPage;
        }
        public string type { get; set; }
        public int size;

        private async void Enter_Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                size = int.Parse(this.sizeEntry.Text);
                if (size < 3)
                {
                    errorMessage.IsVisible = true;
                    return;
                }
                p.CreatePage(this.type, this.size);
                await Navigation.PopAsync();
                p.LaunchPage();

            }
            catch (FormatException)
            {
                errorMessage.IsVisible = true;
            }
        }

        private async void Cancel_Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

    }
}