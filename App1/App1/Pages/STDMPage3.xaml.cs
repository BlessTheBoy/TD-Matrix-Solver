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
    public partial class STDMPage3 : ContentPage
    {
        TDMA matrix;
        private double[,] reduced;
        private double[] results;
        private double finalTime;
        private double step;
        private double[] a;
        private double[] b;
        bool checka;
        bool checkb;
        public STDMPage3(int size)
        {
            InitializeComponent();
            this.BindingContext = this;



            matrix = new TDMA(size);
            results = new double[size];
            a = new double[size];
            b = new double[size];
            reduced = new double[size, 3];

            Label V1 = new Label { TextType = TextType.Html, Text = "C<sup><small>n+1</small></sup><sub><small>" + (matrix.Size - 1) + "</small></sub> ", FontSize = 14, TextColor = Color.White };
            Label V2 = new Label { TextType = TextType.Html, Text = "C<sup><small>n+1</small></sup><sub><small>" + (matrix.Size) + "</small></sub> ", FontSize = 14, TextColor = Color.White };
            grids.Children.Add(V1, 12, 6);
            grids.Children.Add(V2, 12, 7);

            Label C1 = new Label { TextType = TextType.Html, Text = "C<sup><small>n</small></sup><sub><small>" + (matrix.Size - 1) + "</small></sub> ", FontSize = 14, TextColor = Color.White };
            Label C2 = new Label { TextType = TextType.Html, Text = "C<sup><small>n</small></sup><sub><small>" + (matrix.Size) + "</small></sub> ", FontSize = 14, TextColor = Color.White };
            grids.Children.Add(C1, 17, 6);
            grids.Children.Add(C2, 17, 7);

            string a1 = "a" + (size - 1);
            string a2 = "a" + size;
            Entry A1 = new Entry { Placeholder = a1, ClassId = "a" };
            Entry A2 = new Entry { Placeholder = a2, ClassId = "a" };
            grids.Children.Add(A1, 16, 6);
            grids.Children.Add(A2, 16, 7);

            string b1 = "b" + (size - 1);
            string b2 = "b" + size;
            Entry B1 = new Entry { Placeholder = b1, ClassId = "b" };
            Entry B2 = new Entry { Placeholder = b2, ClassId = "b" };
            grids.Children.Add(B1, 18, 6);
            grids.Children.Add(B2, 18, 7);



            askInitialValues.Text = "Enter initial values of C1 to C" + size;
            int end;
            if (size % 2 == 0) end = size / 2;
            else end = (size / 2) + 1;
            for (int i = 1; i <= end; i++)
            {
                Label Name = new Label { Text = "C" + i, FontSize = 14, TextColor = Color.White };
                Entry nameValue = new Entry { WidthRequest = 50, HorizontalTextAlignment = TextAlignment.Start, FontSize = 13, TextColor = Color.FromHex("#149414"), Keyboard = Keyboard.Numeric };
                initialValues1.Children.Add(Name, 0, i - 1);
                initialValues1.Children.Add(nameValue, 1, i - 1);
            }
            for (int i = end + 1; i <= size; i++)
            {
                Label Name = new Label { Text = "C" + i, FontSize = 14, TextColor = Color.White };
                Entry nameValue = new Entry { WidthRequest = 50, HorizontalTextAlignment = TextAlignment.Start, FontSize = 13, TextColor = Color.FromHex("#149414"), Keyboard = Keyboard.Numeric };
                initialValues2.Children.Add(Name, 0, i - (end + 1));
                initialValues2.Children.Add(nameValue, 1, i - (end + 1));
            }



            questiona.Text = "is a3 = a4 = ... = a" + (size - 2) + " ? ";
            entera.Text = "Enter a4 to a" + (size - 2);
            int enda;
            if ((size - 5) % 2 == 0) enda = ((size - 5) / 2) + 3;
            else enda = ((size - 5) / 2) + 4;
            for (int i = 4; i <= enda; i++)
            {
                Label Name = new Label { Text = "a" + i, FontSize = 14, TextColor = Color.White };
                Entry nameValue = new Entry { WidthRequest = 50, HorizontalTextAlignment = TextAlignment.Start, FontSize = 13, TextColor = Color.FromHex("#149414"), Keyboard = Keyboard.Numeric };
                aValues1.Children.Add(Name, 0, i - 4);
                aValues1.Children.Add(nameValue, 1, i - 4);
            }
            for (int i = enda + 1; i < (size - 1); i++)
            {
                Label Name = new Label { Text = "a" + i, FontSize = 14, TextColor = Color.White };
                Entry nameValue = new Entry { WidthRequest = 50, HorizontalTextAlignment = TextAlignment.Start, FontSize = 13, TextColor = Color.FromHex("#149414"), Keyboard = Keyboard.Numeric };
                aValues2.Children.Add(Name, 0, i - (enda + 1));
                aValues2.Children.Add(nameValue, 1, i - (enda + 1));
            }


            questionb.Text = "is b3 = b4 = ... = b" + (size - 2) + " ? ";
            enterb.Text = "Enter b4 to b" + (size - 2);
            int endb;
            if ((size - 5) % 2 == 0) endb = ((size - 5) / 2) + 3;
            else endb = ((size - 5) / 2) + 4;
            for (int i = 4; i <= endb; i++)
            {
                Label Name = new Label { Text = "b" + i, FontSize = 14, TextColor = Color.White };
                Entry nameValue = new Entry { WidthRequest = 50, HorizontalTextAlignment = TextAlignment.Start, FontSize = 13, TextColor = Color.FromHex("#149414"), Keyboard = Keyboard.Numeric };
                bValues1.Children.Add(Name, 0, i - 4);
                bValues1.Children.Add(nameValue, 1, i - 4);
            }
            for (int i = endb + 1; i < (size - 1); i++)
            {
                Label Name = new Label { Text = "b" + i, FontSize = 14, TextColor = Color.White };
                Entry nameValue = new Entry { WidthRequest = 50, HorizontalTextAlignment = TextAlignment.Start, FontSize = 13, TextColor = Color.FromHex("#149414"), Keyboard = Keyboard.Numeric };
                bValues2.Children.Add(Name, 0, i - (endb + 1));
                bValues2.Children.Add(nameValue, 1, i - (endb + 1));
            }
            StringBuilder ansHead = new StringBuilder();
            ansHead.Append("           ");
            for (int i = 1; i <= size; i++)
            {
                ansHead.Append(string.Format("C{0,-28:00}", i));
            }
            ansTitle.Text = ansHead.ToString();

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
                    DisplayResults(results, i);
                }
            }
        }

        private void SetReduced()
        {
            for (int i = 0; i < matrix.Size; i++)
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
            int aindex = 0;
            int bindex = 0;
            int ms = (matrix.Size * 3) - 2;
            reduced[0, 0] = 0.0;
            reduced[(matrix.Size - 1), 2] = 0.0;
            foreach (View item in grids.Children)
            {
                if (item.GetType() == typeof(Entry))
                {
                    Entry E = (Entry)item;
                    double u;
                    bool t = double.TryParse(E.Text, out u);
                    if (t)
                    {
                        if (i <= ms)
                        {
                            if (i < 8)
                            {
                                reduced[i / 3, i % 3] = u;
                                i++;
                            }
                            else if (i == 8)
                            {
                                reduced[i / 3, i % 3] = u;
                                for (int j = 0; j < (matrix.Size - 5); j++)
                                {
                                    for (int k = 0; k < 3; k++)
                                    {
                                        reduced[j + 3, k] = reduced[j + 2, k];
                                    }
                                }
                                i = ms - 4;
                            }
                            else if (i >= ms - 4)
                            {
                                reduced[i / 3, i % 3] = u;
                                i++;
                            }
                        }
                        else
                        {
                            if (E.ClassId == "a")
                            {
                                a[aindex++] = u;
                                if (aindex == 3 && !checka)
                                {
                                    for (int k = 0; k < (matrix.Size - 5); k++)
                                    {
                                        a[aindex] = u;
                                    }
                                }
                            }
                            else if (E.ClassId == "b")
                            {
                                b[bindex++] = u;
                                if (bindex == 3 && !checkb)
                                {
                                    for (int k = 0; k < (matrix.Size - 5); k++)
                                    {
                                        b[bindex] = u;
                                    }
                                }
                            }
                            i++;
                        }
                        invalidInput.IsVisible = false;
                    }
                    else
                    {
                        string warning;
                        string position = (i <= ms) ? ("( row" + ((i / 3) + 1) + ", column" + ((i % 3) + (i / 3)) + " )") : ((i % 2 == 0) ? "a" + (i - (ms - 1)) / 2 : "b" + (i - ms) / 2);
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
            foreach (View item in initialValues1.Children)
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
                        if (E.Text == "" || E.Text == null) warning = "Empty initial value at C" + position;
                        else warning = "invalid initial value at C" + position;
                        invalidInput.Text = warning;
                        invalidInput.IsVisible = true;
                        return false;
                    }
                }
            }
            foreach (View item in initialValues2.Children)
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
                        if (E.Text == "" || E.Text == null) warning = "Empty initial value at C" + position;
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
            if (checka)
            {
                i = 3;
                foreach (View item in aValues1.Children)
                {
                    if (item.GetType() == typeof(Entry))
                    {
                        Entry E = (Entry)item;
                        double u;
                        bool t = double.TryParse(E.Text, out u);
                        if (t)
                        {
                            a[i] = u;
                            i++;
                            invalidInput.IsVisible = false;
                        }
                        else
                        {
                            string warning;
                            string position = (i + 1).ToString();
                            if (E.Text == "" || E.Text == null) warning = "Empty value at a" + position;
                            else warning = "invalid value at a" + position;
                            invalidInput.Text = warning;
                            invalidInput.IsVisible = true;
                            return false;
                        }
                    }
                }
                foreach (View item in aValues2.Children)
                {
                    if (item.GetType() == typeof(Entry))
                    {
                        Entry E = (Entry)item;
                        double u;
                        bool t = double.TryParse(E.Text, out u);
                        if (t)
                        {
                            a[i] = u;
                            i++;
                            invalidInput.IsVisible = false;
                        }
                        else
                        {
                            string warning;
                            string position = (i + 1).ToString();
                            if (E.Text == "" || E.Text == null) warning = "Empty value at a" + position;
                            else warning = "invalid value at a" + position;
                            invalidInput.Text = warning;
                            invalidInput.IsVisible = true;
                            return false;
                        }
                    }
                }
            }
            if (checkb)
            {
                i = 3;
                foreach (View item in bValues1.Children)
                {
                    if (item.GetType() == typeof(Entry))
                    {
                        Entry E = (Entry)item;
                        double u;
                        bool t = double.TryParse(E.Text, out u);
                        if (t)
                        {
                            b[i] = u;
                            i++;
                            invalidInput.IsVisible = false;
                        }
                        else
                        {
                            string warning;
                            string position = (i + 1).ToString();
                            if (E.Text == "" || E.Text == null) warning = "Empty value at b" + position;
                            else warning = "invalid value at b" + position;
                            invalidInput.Text = warning;
                            invalidInput.IsVisible = true;
                            return false;
                        }
                    }
                }
                foreach (View item in bValues2.Children)
                {
                    if (item.GetType() == typeof(Entry))
                    {
                        Entry E = (Entry)item;
                        double u;
                        bool t = double.TryParse(E.Text, out u);
                        if (t)
                        {
                            b[i] = u;
                            i++;
                            invalidInput.IsVisible = false;
                        }
                        else
                        {
                            string warning;
                            string position = (i + 1).ToString();
                            if (E.Text == "" || E.Text == null) warning = "Empty value at b" + position;
                            else warning = "invalid value at b" + position;
                            invalidInput.Text = warning;
                            invalidInput.IsVisible = true;
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        private void DisplayResults(double[] nswers, int time)
        {
            timeLabel.Text = timeLabel.Text + (time * step).ToString() + "\n";
            StringBuilder ans = new StringBuilder();
            for (int y = 0; y < matrix.Size; y++)
            {
                ans.Append(string.Format("{0,-18:00000.000000}", nswers[y]));
            }
            string newS = ans.ToString();
            ansLabel.Text += (newS + "\n");
        }

        private void Clear_Button_Clicked(object sender, EventArgs e)
        {
            foreach (View item in initialValues1.Children)
            {
                if (item.GetType() == typeof(Entry))
                {
                    Entry E = (Entry)item;
                    E.Text = "";
                }
            }
            foreach (View item in initialValues2.Children)
            {
                if (item.GetType() == typeof(Entry))
                {
                    Entry E = (Entry)item;
                    E.Text = "";
                }
            }
            foreach (View item in aValues1.Children)
            {
                if (item.GetType() == typeof(Entry))
                {
                    Entry E = (Entry)item;
                    E.Text = "";
                }
            }
            foreach (View item in aValues2.Children)
            {
                if (item.GetType() == typeof(Entry))
                {
                    Entry E = (Entry)item;
                    E.Text = "";
                }
            }
            foreach (View item in bValues1.Children)
            {
                if (item.GetType() == typeof(Entry))
                {
                    Entry E = (Entry)item;
                    E.Text = "";
                }
            }
            foreach (View item in bValues2.Children)
            {
                if (item.GetType() == typeof(Entry))
                {
                    Entry E = (Entry)item;
                    E.Text = "";
                }
            }
            timeLabel.Text = "";
            ansLabel.Text = "";
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
            askIfReapeateda.IsVisible = true;
            aValueInput.IsVisible = false;
            askIfReapeatedb.IsVisible = false;
            bValueInput.IsVisible = false;
            solveButton.IsVisible = false;
        }

        private double[] CalculateEquivalent(double[] answer)
        {
            double[] values = new double[matrix.Size];
            for (int i = 0; i < 5; i++)
            {
                values[i] = (a[i] * answer[i]) + b[i];
            }
            return values;
        }

        private void Yesa_Button_Clicked(object sender, EventArgs e)
        {
            askIfReapeateda.IsVisible = false;
            aValueInput.IsVisible = false;
            askIfReapeatedb.IsVisible = true;
        }

        private void Noa_Button_Clicked(object sender, EventArgs e)
        {
            askIfReapeateda.IsVisible = false;
            aValueInput.IsVisible = true;
            askIfReapeatedb.IsVisible = true;
            checka = true;
        }

        private void Yesb_Button_Clicked(object sender, EventArgs e)
        {
            askIfReapeatedb.IsVisible = false;
            bValueInput.IsVisible = false;
            solveButton.IsVisible = true;
        }

        private void Nob_Button_Clicked(object sender, EventArgs e)
        {
            askIfReapeatedb.IsVisible = false;
            bValueInput.IsVisible = true;
            checkb = true;
            solveButton.IsVisible = true;
        }

    }
}