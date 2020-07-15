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
    public partial class STDMPage2 : ContentPage
    {
        TDMA matrix;
        private double[,] reduced = new double[5, 3];
        private double[] results;
        private double finalTime;
        private double step;
        private double[] a;
        private double[] b;
        private StringBuilder[] ans;
        public STDMPage2()
        {
            InitializeComponent();
            matrix = new TDMA(5);
            results = new double[5];
            a = new double[5];
            b = new double[5];

            ans = new StringBuilder[6];
            for (int i = 0; i < 6; i++)
            {
                ans[i] = new StringBuilder();
            }

            for (int i = 1; i < 6; i++)
            {
                Label Name = new Label { Text = "C" + i, FontSize = 14, TextColor = Color.White };
                Entry nameValue = new Entry { WidthRequest = 80, HorizontalTextAlignment = TextAlignment.Start, FontSize = 14, TextColor = Color.FromHex("#149414"), Keyboard = Keyboard.Numeric };
                initialValues.Children.Add(Name, 0, i - 1);
                initialValues.Children.Add(nameValue, 1, i - 1);
            }
        }


        private void Solve_Button_Clicked(object sender, EventArgs e)
        {
            if (CheckInput(matrix) && ValidateInitials())
            {
                for (int i = 1; i < (finalTime / step) + 1; i++)
                {
                    SetReduced();
                    matrix.equivalentMatrix = CalculateEquivalent(results);
                    results = Solver.MatrixSolver(matrix);
                    CummulateResults(results, i);
                }
                DisplayResults();
            }
        }

        private void SetReduced()
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    matrix.reducedMatrix[i, j] = reduced[i, j];
                }
            }
        }

        private bool CheckInput(TDMA matrix)
        {
            int i = 1;
            reduced[0, 0] = 0.0;
            reduced[4, 2] = 0.0;
            foreach (View item in grids.Children)
            {
                if (item.GetType() == typeof(Entry))
                {
                    Entry E = (Entry)item;
                    double u;
                    bool t = double.TryParse(E.Text, out u);
                    if (t)
                    {
                        if (i < 14)
                        {
                            reduced[i / 3, i % 3] = u;
                            i++;
                            invalidInput.IsVisible = false;
                        }
                        else
                        {
                            if (E.ClassId == "a")
                            {
                                a[(i - 14) / 2] = u;
                            }
                            else if (E.ClassId == "b")
                            {
                                b[(i - 15) / 2] = u;
                            }
                            i++;
                            invalidInput.IsVisible = false;
                        }

                    }
                    else
                    {
                        string warning;
                        string position = (i < 14) ? ("( row" + ((i / 3) + 1) + ", column" + ((i % 3) + (i / 3)) + " )") : ((i % 2 == 0) ? "a" + (i - 12) / 2 : "b" + (i - 13) / 2);
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

        private bool ValidateInitials()
        {
            int i = 0;
            foreach (View item in initialValues.Children)
            {
                if (item.GetType() == typeof(Entry))
                {
                    Entry E = (Entry)item;
                    double a;
                    bool t = double.TryParse(E.Text, out a);
                    if (t)
                    {
                        results[i] = a;
                        i++;
                        invalidInput.IsVisible = false;
                    }
                    else
                    {
                        string warning;
                        string position = (i + 1).ToString();
                        if (E.Text == "" || E.Text == null) warning = "Empty initial value at " + position;
                        else warning = "invalid initial value at C" + position;
                        invalidInput.Text = warning;
                        invalidInput.IsVisible = true;
                        return false;
                    }
                }
            }
            if (!double.TryParse(Step.Text, out step))
            {
                if (Step.Text == "" || Step.Text == null) invalidInput.Text = "Enter time step, dT";
                else invalidInput.Text = "Invalid time step, dT";
                invalidInput.IsVisible = true;
                return false;
            }
            if (!double.TryParse(FinalTime.Text, out finalTime))
            {
                if (FinalTime.Text == "" || FinalTime.Text == null) invalidInput.Text = "Enter final time, T";
                else invalidInput.Text = "Invalid final time, T";
                invalidInput.IsVisible = true;
                return false;
            }

            return true;
        }

        private void DisplayResults()
        {
            int i = 0;
            foreach (View item in answerStack.Children)
            {
                if (item.GetType() == typeof(Label))
                {
                    Label E = (Label)item;
                    string u = ans[i].ToString();
                    E.Text = E.Text + u;
                    i++;
                }
            }
        }

        private void CummulateResults(double[] nswers, int time)
        {
            ans[0].Append("\n" + (time * step).ToString());
            for (int y = 0; y < 5; y++)
            {
                string ansT = string.Format("\n {0,-16:0.000000}", nswers[y]);
                ans[y + 1].Append(ansT);
            }
        }

        private void Clear_Button_Clicked(object sender, EventArgs e)
        {
            foreach (View item in initialValues.Children)
            {
                if (item.GetType() == typeof(Entry))
                {
                    Entry E = (Entry)item;
                    E.Text = "";
                }
            }
            FinalTime.Text = "";
            Step.Text = "";
            foreach (View item in grids.Children)
            {
                if (item.GetType() == typeof(Entry))
                {
                    Entry E = (Entry)item;
                    E.Text = "";
                }
            }
            invalidInput.IsVisible = false;
            int o = 0;
            foreach (View item in answerStack.Children)
            {
                if (item.GetType() == typeof(Label))
                {
                    Label E = (Label)item;
                    if (o == 0)
                    {
                        E.Text = "Time";
                        o++;
                        continue;
                    }
                    string u = string.Format("C{0}", o);
                    E.Text = u;
                    o++;
                }
            }
            for (int i = 0; i < 5; i++)
            {
                ans[i] = new StringBuilder();
            }
        }

        private double[] CalculateEquivalent(double[] answer)
        {
            double[] values = new double[5];
            for (int i = 0; i < 5; i++)
            {
                values[i] = (a[i] * answer[i]) + b[i];
            }
            return values;
        }
    }
}