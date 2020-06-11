using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App1.Models;

namespace App1.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TDMPage2 : ContentPage
    {
        TDMA matrix;
        double[] results;
        public TDMPage2()
        {
            InitializeComponent();
            matrix = new TDMA(5);
        }

        private void Solve_Button_Clicked(object sender, EventArgs e)
        {
            if (CheckInput(matrix))
            {
                results = Solver.MatrixSolver(matrix);
                DisplayResults(results);
            }
        }

        private bool CheckInput(TDMA matrix)
        {
            int i = 1;
            matrix.reducedMatrix[0, 0] = 0.0;
            matrix.reducedMatrix[4, 2] = 0.0;
            foreach (View item in grids.Children)
            {
                if (item.GetType() == typeof(Entry))
                {
                    Entry E = (Entry)item;
                    double a;
                    bool t = double.TryParse(E.Text, out a);
                    if (t)
                    {
                        if (i < 14)
                        {
                            matrix.reducedMatrix[i / 3, i % 3] = a;
                            i++;
                        }
                        else
                        {
                            matrix.equivalentMatrix[i - 14] = a;
                            i++;
                        }
                        invalidInput.IsVisible = false;
                    }
                    else
                    {
                        string warning;
                        string position = (i > 13) ? "J" + (i - 13) : "( row" + ((i / 3) + 1) + ", column" + ((i % 3) + (i / 3)) + " )";
                        if (E.Text == "" || E.Text == null) warning = "Empty input at " + position;
                        else warning = "invalid input at " + position;
                        invalidInput.Text = warning;
                        invalidInput.IsVisible = true;
                        return false;
                    }
                }
            }
            return true;
        }

        private void DisplayResults(double[] answers)
        {
            int i = 0;
            foreach (View item in answerStack.Children)
            {
                if (item.ClassId == "ans")
                {
                    Label E = (Label)item;
                    E.Text = string.Format("{0,-10:#0.0000000000}", answers[i]);
                    i++;
                }
            }
        }

        private void Clear_Button_Clicked(object sender, EventArgs e)
        {
            foreach (View item in answerStack.Children)
            {
                if (item.ClassId == "ans")
                {
                    Label E = (Label)item;
                    E.Text = "";
                }
            }
            foreach (View item in grids.Children)
            {
                if (item.GetType() == typeof(Entry))
                {
                    Entry E = (Entry)item;
                    E.Text = "";
                }
            }
            invalidInput.IsVisible = false;
        }
    }
}