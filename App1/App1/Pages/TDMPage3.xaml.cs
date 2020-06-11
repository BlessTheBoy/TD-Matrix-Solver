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
    public partial class TDMPage3 : ContentPage
    {
        TDMA matrix;
        double[] results;
        public TDMPage3(int size)
        {
            InitializeComponent();
            matrix = new TDMA(size);
            this.BindingContext = this;

            question.Text = "is J3 = J4 = ... = J" + (size - 2) + " ?";
            for (int i = 4; i < size - 1; i++)
            {
                Label Name = new Label { Text = "J" + i, FontSize = 14, TextColor = Color.White };
                Entry nameValue = new Entry { WidthRequest = 80, HorizontalTextAlignment = TextAlignment.Start, FontSize = 13, TextColor = Color.FromHex("#149414"), Keyboard = Keyboard.Numeric };
                equivalentStack.Children.Add(Name, 0, i - 4);
                equivalentStack.Children.Add(nameValue, 1, i - 4);
            }

            for (int i = 1; i <= size; i++)
            {
                Label Name = new Label { TextType = TextType.Html, Text = "N<sub><small>" + i + "</small></sub>:", FontSize = 18, TextColor = Color.White, FontAttributes = FontAttributes.Bold };
                Label Ans = new Label { ClassId = "ans", FontSize = 18, Text = "", TextColor = Color.White, MinimumWidthRequest = 100 };
                answerStack.Children.Add(Name, 0, i - 1);
                answerStack.Children.Add(Ans, 1, i - 1);
            }

            Label N1 = new Label { TextType = TextType.Html, Text = "N<sub><small>" + (matrix.Size - 1) + "</small></sub> ", FontSize = 14, TextColor = Color.White };
            Label N2 = new Label { TextType = TextType.Html, Text = "N<sub><small>" + (matrix.Size) + "</small></sub> ", FontSize = 14, TextColor = Color.White };
            grids.Children.Add(N1, 12, 6);
            grids.Children.Add(N2, 12, 7);

            string j1 = "J" + (size - 1);
            string j2 = "J" + size;
            Entry J1 = new Entry { Placeholder = j1 };
            Entry J2 = new Entry { Placeholder = j2 };
            grids.Children.Add(J1, 16, 6);
            grids.Children.Add(J2, 16, 7);
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
            int ms = (matrix.Size * 3) - 2;
            matrix.reducedMatrix[0, 0] = 0.0;
            matrix.reducedMatrix[(matrix.Size - 1), 2] = 0.0;
            foreach (View item in grids.Children)
            {
                if (item.GetType() == typeof(Entry))
                {
                    Entry E = (Entry)item;
                    double a;
                    bool t = double.TryParse(E.Text, out a);
                    if (t)
                    {
                        if (i < 8)
                        {
                            matrix.reducedMatrix[i / 3, i % 3] = a;
                            i++;
                        }
                        else if (i == 8)
                        {
                            matrix.reducedMatrix[i / 3, i % 3] = a;
                            for (int j = 0; j < (matrix.Size - 5); j++)
                            {
                                for (int k = 0; k < 3; k++)
                                {
                                    matrix.reducedMatrix[j + 3, k] = matrix.reducedMatrix[j + 2, k];
                                }
                            }
                            i = ms - 4;
                        }
                        else if (i >= ms - 4 && i <= ms)
                        {
                            matrix.reducedMatrix[i / 3, i % 3] = a;
                            i++;
                        }
                        else if (i > ms && i < (ms + 3))
                        {
                            matrix.equivalentMatrix[i - (ms + 1)] = a;
                            i++;
                        }
                        else if (i == ms + 3)
                        {
                            matrix.equivalentMatrix[2] = a;
                            i++;
                            if (!equivalentInput.IsVisible)
                            {
                                for (int k = 0; k < (matrix.Size - 5); k++)
                                {
                                    matrix.equivalentMatrix[k + 3] = a;
                                }
                            }
                            i += (matrix.Size - 5);
                        }
                        else
                        {
                            matrix.equivalentMatrix[i - (ms + 1)] = a;
                            i++;
                        }
                        invalidInput.IsVisible = false;
                    }
                    else
                    {
                        string warning;
                        string position = (i > ms) ? "J" + (i - ms) : "( row" + ((i / 3) + 1) + ", column" + ((i % 3) + (i / 3)) + " )";
                        if (E.Text == "" || E.Text == null) warning = "Empty input at " + position;
                        else warning = "invalid input at " + position;
                        invalidInput.Text = warning;
                        invalidInput.IsVisible = true;
                        return false;
                    }
                }
            }
            if (equivalentInput.IsVisible)
            {
                int r = 3;
                foreach (View item in equivalentStack.Children)
                {
                    if (item.GetType() == typeof(Entry))
                    {
                        Entry E = (Entry)item;
                        double a;
                        bool t = double.TryParse(E.Text, out a);
                        if (t)
                        {
                            matrix.equivalentMatrix[r] = a;
                            r++;
                        }
                        else
                        {
                            string warning;
                            string position = "J" + (r + 1);
                            if (E.Text == "" || E.Text == null) warning = "Empty input at " + position;
                            else warning = "invalid input at " + position;
                            invalidInput.Text = warning;
                            invalidInput.IsVisible = true;
                            return false;
                        }
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
            askIfReapeated.IsVisible = true;
            equivalentInput.IsVisible = false;
        }

        private void Yes_Button_Clicked(object sender, EventArgs e)
        {
            askIfReapeated.IsVisible = false;
            equivalentInput.IsVisible = false;
            solveButton.IsVisible = true;
        }

        private void No_Button_Clicked(object sender, EventArgs e)
        {
            askIfReapeated.IsVisible = false;
            equivalentInput.IsVisible = true;
            solveButton.IsVisible = true;
        }
    }
}