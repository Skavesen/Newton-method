using Flee.PublicTypes;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using VP_LW_4.Expression_Tree;

namespace VP_LW_4
{
    public partial class MathMethods : Form
    {
        public MathMethods()
        {
            InitializeComponent();

            dataGridView1.DataSource = table;
            table.Columns.Add("№", typeof(double));
            table.Columns.Add("Xn", typeof(double));
            table.Columns.Add("F(Xn)", typeof(double));
            table.Columns.Add("F'(Xn)", typeof(double));
            table.Columns.Add("F(Xn)/m", typeof(double));
            table.Columns.Add("F(Xn)-((F(Xn)/F'(Xn))", typeof(double));
            table.Columns.Add("E stop test", typeof(string));
        }

        private int iterator_i;
        DataTable table = new DataTable();
        Double Xn, Xn1, Xo, error, Xi;
        double a, b, Acc, h, X, m, chislo;
        string proverka = "+";
        private void ComputButton_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(AccBox.Text) || string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox3.Text))
            {
                MessageBox.Show("Prázdne riadky nie sú povolené");
            }
            else
            {
                a = Convert.ToDouble(textBox1.Text/*, System.Globalization.CultureInfo.InvariantCulture*/);
                b = Convert.ToDouble(textBox3.Text/*, System.Globalization.CultureInfo.InvariantCulture*/);
                Acc = Convert.ToDouble(AccBox.Text/*, System.Globalization.CultureInfo.InvariantCulture*/);
                
                if (a < b)
                {
                    chislo = b;
                }
                else
                { 
                    chislo = a;
                }
                
                if (radioButton1.Checked == true)
                {
                    //ListOFres.Text = N_tangent(F1, a, Acc).ToString();
                    textBox4.Text = "(3 * (x ^ 2) + 1,8 * x + 1,1)";
                    textBox6.Text = "(6 * x + 1,8)";
                    //textBox6.Text = "(6 * x + 1.8)";
                    writeDataTable(F1);
                    //  buildGraph(Acc, a, b);
                }
                if (radioButton2.Checked == true)
                {
                    //ListOFres.Text = N_tangent(F2, a, Acc).ToString();
                    textBox4.Text = "(((3 * (x ^ 2) )  + (2 * (2 * x) ) )  - 2)";
                    textBox6.Text = "((3 * (2 * x) )  + 4)";
                    writeDataTable(F2);
                    //  buildGraph(Acc, a, b);
                }
                if (radioButton3.Checked == true)
                {
                    //ListOFres.Text = N_tangent(F3, a, Acc).ToString();
                    textBox4.Text = "((3 * (x ^ 2) )  - 12)";
                    textBox6.Text = "(3 * (2 * x) )";
                    writeDataTable(F3);
                    // buildGraph(Acc, a, b);
                }
                if (radioButton4.Checked == true)
                {
                    //ListOFres.Text = N_tangent(F4, a, Acc).ToString();
                    textBox4.Text = "((((2 * (5 * (x ^ 4) ) )  + (5 * (4 * (x ^ 3) ) ) )  - (10 * (2 * x) ) )  + 10)";
                    textBox6.Text = "(((2 * (5 * (4 * (x ^ 3) ) ) )  + (5 * (4 * (3 * (x ^ 2) ) ) ) )  - 20)";
                    writeDataTable(F4);
                    //buildGraph(Acc, a, b);
                }
                if (radioButton5.Checked == true)
                {
                    //исходная формула
                    string str = textBox2.Text;
                    //конечная формула (после преобразований)
                    //этот вид формулы понятен для C#
                    string dst;

                    //делегат, созданный по конечной формуле
                    Formula formula = CreateFormula(str, out dst);
                    //ListOFres.Text = N_tangent(F5, a, Acc).ToString();

                    deivate();
                    writeDataTabl(F5);
                    // buildGraph(Acc, a, b);
                }
                if (radioButton6.Checked == true)
                {
                    //ListOFres.Text = N_tangent(F6, a, Acc).ToString();
                    textBox4.Text = "(8 * x ^ 3 + 18 * x ^ 2 + 10 * x)";
                    textBox6.Text = "(24 * x ^ 2 + 36 * x + 10)";
                    writeDataTable(F6);
                    // buildGraph(Acc, a, b);
                }
                if (radioButton7.Checked == true)
                {
                    //ListOFres.Text = N_tangent(F7, a, Acc).ToString();
                    textBox4.Text = "(cos(x)  + 2)";
                    textBox6.Text = "(sin(x)  * -1)";
                    writeDataTable(F7);
                    // buildGraph(Acc, a, b);
                }
                if (radioButton8.Checked == true)
                {
                    //ListOFres.Text = N_tangent(F8, a, Acc).ToString();
                    textBox4.Text = "(2 * ln(x) - 2)";
                    textBox6.Text = "(2 / x)";
                    writeDataTable(F8);
                    // buildGraph(Acc, a, b);
                }
                if (radioButton9.Checked == true)
                {
                    //ListOFres.Text = N_tangent(F9, a, Acc).ToString();
                    textBox4.Text = "((3 * (x ^ 2) )  - 8)";
                    textBox6.Text = "(3 * (2 * x) )";
                    writeDataTable(F9);
                    //  buildGraph(Acc, a, b);
                }
                if (radioButton10.Checked == true)
                {
                    //ListOFres.Text = N_tangent(F10, a, Acc).ToString();
                    textBox4.Text = "((3 * (x ^ 2) )  + (3 * (2 * x) ) )";
                    textBox6.Text = "((3 * (2 * x) )  + 6)";
                    writeDataTable(F10);
                    // buildGraph(Acc, a, b);
                }
                if (radioButton11.Checked == true)
                {
                    //ListOFres.Text = N_tangent(F11, a, Acc).ToString();
                    textBox4.Text = "(((5 * (3 * (x ^ 2) ) )  + (2 * (2 * x) ) )  - 15)";
                    textBox6.Text = "((5 * (3 * (2 * x) ) )  + 4)";
                    writeDataTable(F11);
                    //buildGraph(Acc, a, b);
                }
                if (radioButton12.Checked == true)
                {
                    //ListOFres.Text = N_tangent(F12, a, Acc).ToString();
                    textBox4.Text = "((3 * (x ^ 2) )  - 7)";
                    textBox6.Text = "(3 * (2 * x) )";
                    writeDataTable(F12);
                    //buildGraph(Acc, a, b);
                }
            }
        }
        private void chart_GetToolTipText(object sender, ToolTipEventArgs e)
        {
            // Если текст в подсказке уже есть, то ничего не меняем.
            if (!String.IsNullOrWhiteSpace(e.Text))
                return;

            Console.WriteLine(e.HitTestResult.ChartElementType);

            switch (e.HitTestResult.ChartElementType)
            {
                case ChartElementType.DataPoint:
                case ChartElementType.DataPointLabel:
                case ChartElementType.Gridlines:
                case ChartElementType.Axis:
                case ChartElementType.TickMarks:
                case ChartElementType.PlottingArea:
                    // Первый ChartArea
                    var area = chart1.ChartAreas[0];

                    // Его относительные координаты (в процентах от размеров Chart)
                    var areaPosition = area.Position;

                    // Переводим в абсолютные
                    var areaRect = new RectangleF(areaPosition.X * chart1.Width / 100, areaPosition.Y * chart1.Height / 100,
                        areaPosition.Width * chart1.Width / 100, areaPosition.Height * chart1.Height / 100);

                    // Область построения (в процентах от размеров area)
                    var innerPlot = area.InnerPlotPosition;

                    double x = area.AxisX.Minimum +
                                (area.AxisX.Maximum - area.AxisX.Minimum) * (e.X - areaRect.Left - innerPlot.X * areaRect.Width / 100) /
                                (innerPlot.Width * areaRect.Width / 100);
                    double y = area.AxisY.Maximum -
                                (area.AxisY.Maximum - area.AxisY.Minimum) * (e.Y - areaRect.Top - innerPlot.Y * areaRect.Height / 100) /
                                (innerPlot.Height * areaRect.Height / 100);

                    Console.WriteLine("{0:F2} {1:F2}", x, y);
                    e.Text = String.Format("{0:F2} {1:F2}", x, y);
                    break;
            }
        }
        //построение графика
        void buildGraph(double Acc, double a, double b)
        {
            h = Math.Pow(Acc, 0.25);//по правилу Рунге
            int n = (int)((b - a) / h);
            X = a;
            chart1.Series[0].ChartType = SeriesChartType.Spline;//Spline
            chart1.Series[1].ChartType = SeriesChartType.Point;
            chart1.Series[1].ToolTip = "X = #VALX, Y = #VALY";
            chart1.GetToolTipText += chart_GetToolTipText;
            chart1.ChartAreas[0].AxisX.Interval = 2;
            chart1.ChartAreas[0].AxisX.Minimum = -8;
            chart1.ChartAreas[0].AxisX.Maximum = 10;

            chart1.ChartAreas[0].AxisY.Minimum = -6;
            chart1.ChartAreas[0].AxisY.Maximum = 10;
            chart1.ChartAreas[0].AxisY.Interval = 2;

            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();
            for (int i = 0; i <= n; i++)
            {
                X = X + h;
                if (radioButton1.Checked == true)
                {
                    double proverka = F1(X);
                    chart1.Series[0].Points.AddXY(X, F1(X));
                    chart1.Series[1].Points.AddXY(-0.5, 0);
                    chart1.Series[1].Points.AddXY(3.5, 0);
                }
                if (radioButton2.Checked == true)
                {
                    double proverka = F2(X);
                    chart1.Series[0].Points.AddXY(X, F2(X));
                    chart1.Series[1].Points.AddXY(-4, 0);
                    chart1.Series[1].Points.AddXY(0, 0);
                }
                if (radioButton3.Checked == true)
                {
                    double proverka = F3(X);
                    chart1.Series[0].Points.AddXY(X, F3(X));
                    chart1.Series[1].Points.AddXY(-5.5, 0);
                    chart1.Series[1].Points.AddXY(-1.5, 0);
                }
                if (radioButton4.Checked == true)
                {
                    double proverka = F4(X);
                    chart1.Series[0].Points.AddXY(X, F4(X));
                    chart1.Series[1].Points.AddXY(-2, 0);
                    chart1.Series[1].Points.AddXY(2.5, 0);
                }
                if (radioButton5.Checked == true)
                {
                    string str = textBox2.Text;
                    string dst;
                    Formula formula = CreateFormula(str, out dst);
                    chart1.Series[0].Points.AddXY(X, F5(X));
                }
                if (radioButton6.Checked == true)
                {
                    double proverka = F6(X);
                    chart1.Series[0].Points.AddXY(X, F6(X));
                    chart1.Series[1].Points.AddXY(0, 0);
                    chart1.Series[1].Points.AddXY(2, 0);
                }
                if (radioButton7.Checked == true)
                {
                    double proverka = F7(X);
                    chart1.Series[0].Points.AddXY(X, F7(X));
                    chart1.Series[1].Points.AddXY(-2, 0);
                    chart1.Series[1].Points.AddXY(2, 0);
                }
                if (radioButton8.Checked == true)
                {
                    double proverka = F8(X);
                    chart1.Series[0].Points.AddXY(X, F8(X));
                    chart1.Series[1].Points.AddXY(0, 0);
                    chart1.Series[1].Points.AddXY(2.2, 0);
                    chart1.Series[1].Points.AddXY(4.2, 0);
                }
                if (radioButton9.Checked == true)
                {
                    double proverka = F9(X);
                    chart1.Series[0].Points.AddXY(X, F9(X));

                    chart1.Series[1].Points.AddXY(-5, 0);
                    chart1.Series[1].Points.AddXY(-2, 0);
                }
                if (radioButton10.Checked == true)
                {
                    double proverka = F10(X);
                    chart1.Series[0].Points.AddXY(X, F10(X));
                    chart1.Series[1].Points.AddXY(-4, 0);
                    chart1.Series[1].Points.AddXY(-2, 0);
                    chart1.Series[1].Points.AddXY(2, 0);
                }
                if (radioButton11.Checked == true)
                {
                    double proverka = F11(X);
                    chart1.Series[0].Points.AddXY(X, F11(X));
                    chart1.Series[1].Points.AddXY(-0.5, 0);
                    chart1.Series[1].Points.AddXY(2.5, 0);
                }
                if (radioButton12.Checked == true)
                {
                    double proverka = F12(X);
                    chart1.Series[0].Points.AddXY(X, F12(X));
                    chart1.Series[1].Points.AddXY(-2, 0);
                    chart1.Series[1].Points.AddXY(-1, 0);
                    chart1.Series[1].Points.AddXY(2, 0);
                    chart1.Series[1].Points.AddXY(4.5, 0);
                }
            }
            if (radioButton5.Checked == true)
            {
                string str = textBox2.Text;
                string dst;
                Formula formula = CreateFormula(str, out dst);
                chart1.Series[1].Points.AddXY((N_tangent(F5, a, Acc)) - 1.5, 0);
                chart1.Series[1].Points.AddXY((N_tangent(F5, a, Acc)) + 1.5, 0);
            }
        }
        void buildGraphh(double Acc, double a, double b)
        {
            h = Math.Pow(Acc, 0.25);//по правилу Рунге
            int n = (int)((b - a) / h);
            X = a;
            chart1.Series[0].ChartType = SeriesChartType.Spline;//Spline
            chart1.Series[1].ChartType = SeriesChartType.Point;
            chart1.Series[1].ToolTip = "X = #VALX, Y = #VALY";
            chart1.GetToolTipText += chart_GetToolTipText;
            chart1.ChartAreas[0].AxisX.Interval = 2;
            chart1.ChartAreas[0].AxisX.Minimum = -10;
            chart1.ChartAreas[0].AxisX.Maximum = 10;


            chart1.ChartAreas[0].AxisY.Minimum = -30;
            chart1.ChartAreas[0].AxisY.Maximum = 40;
            chart1.ChartAreas[0].AxisY.Interval = 6;

            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();
            for (int i = 0; i <= n; i++)
            {
                //X = X + h;
                //X = X + 0.5;
                if (radioButton1.Checked == true)
                {
                    double proverka = F1(X);
                    chart1.Series[0].Points.AddXY(X, F1(X));
                    chart1.Series[1].Points.AddXY(-0.5, 0);
                    chart1.Series[1].Points.AddXY(3.5, 0);
                }
                if (radioButton2.Checked == true)
                {
                    double proverka = F2(X);
                    chart1.Series[0].Points.AddXY(X, F2(X));
                    chart1.Series[1].Points.AddXY(-4, 0);
                    chart1.Series[1].Points.AddXY(0, 0);
                }
                if (radioButton3.Checked == true)
                {
                    double proverka = F3(X);
                    chart1.Series[0].Points.AddXY(X, F3(X));
                    chart1.Series[1].Points.AddXY(-5.5, 0);
                    chart1.Series[1].Points.AddXY(-1.5, 0);
                }
                if (radioButton4.Checked == true)
                {
                    double proverka = F4(X);
                    chart1.Series[0].Points.AddXY(X, F4(X));
                    chart1.Series[1].Points.AddXY(-2, 0);
                    chart1.Series[1].Points.AddXY(2.5, 0);
                }
                if (radioButton5.Checked == true)
                {
                    string str = textBox2.Text;
                    string dst;
                    Formula formula = CreateFormula(str, out dst);
                    chart1.Series[0].Points.AddXY(X, F5(X));
                }
                if (radioButton6.Checked == true)
                {
                    double proverka = F6(X);
                    chart1.Series[0].Points.AddXY(X, F6(X));
                    chart1.Series[1].Points.AddXY(0, 0);
                    chart1.Series[1].Points.AddXY(2, 0);
                }
                if (radioButton7.Checked == true)
                {
                    double proverka = F7(X);
                    chart1.Series[0].Points.AddXY(X, F7(X));
                    chart1.Series[1].Points.AddXY(-2, 0);
                    chart1.Series[1].Points.AddXY(2, 0);
                }
                if (radioButton8.Checked == true)
                {
                    double proverka = F8(X);
                    chart1.Series[0].Points.AddXY(X, F8(X));
                    chart1.Series[1].Points.AddXY(0, 0);
                    chart1.Series[1].Points.AddXY(2.2, 0);
                    chart1.Series[1].Points.AddXY(4.2, 0);
                }
                if (radioButton9.Checked == true)
                {
                    double proverka = F9(X);
                    chart1.Series[0].Points.AddXY(X, F9(X));

                    chart1.Series[1].Points.AddXY(-5, 0);
                    chart1.Series[1].Points.AddXY(-2, 0);
                }
                if (radioButton10.Checked == true)
                {
                    double proverka = F10(X);
                    chart1.Series[0].Points.AddXY(X, F10(X));
                    chart1.Series[1].Points.AddXY(-4, 0);
                    chart1.Series[1].Points.AddXY(-2, 0);
                    chart1.Series[1].Points.AddXY(2, 0);
                }
                if (radioButton11.Checked == true)
                {
                    double proverka = F11(X);
                    chart1.Series[0].Points.AddXY(X, F11(X));
                    chart1.Series[1].Points.AddXY(-0.5, 0);
                    chart1.Series[1].Points.AddXY(2.5, 0);
                }
                if (radioButton12.Checked == true)
                {
                    double proverka = F12(X);
                    chart1.Series[0].Points.AddXY(X, F12(X));
                    chart1.Series[1].Points.AddXY(-2, 0);
                    chart1.Series[1].Points.AddXY(-1, 0);
                    chart1.Series[1].Points.AddXY(2, 0);
                    chart1.Series[1].Points.AddXY(4.5, 0);
                }
                X = X + h;
            }
            if (radioButton5.Checked == true)
            {
                string str = textBox2.Text;
                string dst;
                Formula formula = CreateFormula(str, out dst);
                chart1.Series[1].Points.AddXY((N_tangent(F5, a, Acc)) - 1.5, 0);
                chart1.Series[1].Points.AddXY((N_tangent(F5, a, Acc)) + 1.5, 0);
            }
        }
        void buildGraphhh(double Acc, double a, double b)
        {
            h = Math.Pow(Acc, 0.25);//по правилу Рунге
            int n = (int)((b - a) / h);
            X = a;
            chart1.Series[0].ChartType = SeriesChartType.Spline;//Spline
            chart1.Series[1].ChartType = SeriesChartType.Point;
            chart1.Series[1].ToolTip = "X = #VALX, Y = #VALY";
            chart1.GetToolTipText += chart_GetToolTipText;
            chart1.ChartAreas[0].AxisX.Interval = 1;
            chart1.ChartAreas[0].AxisX.Minimum = a;
            chart1.ChartAreas[0].AxisX.Maximum = b;


            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();
            for (int i = 0; i <= n; i++)
            {
                if (radioButton1.Checked == true)
                {
                    double proverka = F1(X);
                    chart1.Series[0].Points.AddXY(X, F1(X));
                    chart1.Series[1].Points.AddXY(-0.5, 0);
                    chart1.Series[1].Points.AddXY(3.5, 0);
                }
                if (radioButton2.Checked == true)
                {
                    double proverka = F2(X);
                    chart1.Series[0].Points.AddXY(X, F2(X));
                    chart1.Series[1].Points.AddXY(-4, 0);
                    chart1.Series[1].Points.AddXY(0, 0);
                }
                if (radioButton3.Checked == true)
                {
                    double proverka = F3(X);
                    chart1.Series[0].Points.AddXY(X, F3(X));
                    chart1.Series[1].Points.AddXY(-5.5, 0);
                    chart1.Series[1].Points.AddXY(-1.5, 0);
                }
                if (radioButton4.Checked == true)
                {
                    double proverka = F4(X);
                    chart1.Series[0].Points.AddXY(X, F4(X));
                    chart1.Series[1].Points.AddXY(-2, 0);
                    chart1.Series[1].Points.AddXY(2.5, 0);
                }
                if (radioButton5.Checked == true)
                {
                    string str = textBox2.Text;
                    string dst;
                    Formula formula = CreateFormula(str, out dst);
                    chart1.Series[0].Points.AddXY(X, F5(X));
                }
                if (radioButton6.Checked == true)
                {
                    double proverka = F6(X);
                    chart1.Series[0].Points.AddXY(X, F6(X));
                    chart1.Series[1].Points.AddXY(0, 0);
                    chart1.Series[1].Points.AddXY(2, 0);
                }
                if (radioButton7.Checked == true)
                {
                    double proverka = F7(X);
                    chart1.Series[0].Points.AddXY(X, F7(X));
                    chart1.Series[1].Points.AddXY(-2, 0);
                    chart1.Series[1].Points.AddXY(2, 0);
                }
                if (radioButton8.Checked == true)
                {
                    double proverka = F8(X);
                    chart1.Series[0].Points.AddXY(X, F8(X));
                    chart1.Series[1].Points.AddXY(0, 0);
                    chart1.Series[1].Points.AddXY(2.2, 0);
                    chart1.Series[1].Points.AddXY(4.2, 0);
                }
                if (radioButton9.Checked == true)
                {
                    double proverka = F9(X);
                    chart1.Series[0].Points.AddXY(X, F9(X));

                    chart1.Series[1].Points.AddXY(-5, 0);
                    chart1.Series[1].Points.AddXY(-2, 0);
                }
                if (radioButton10.Checked == true)
                {
                    double proverka = F10(X);
                    chart1.Series[0].Points.AddXY(X, F10(X));
                    chart1.Series[1].Points.AddXY(-4, 0);
                    chart1.Series[1].Points.AddXY(-2, 0);
                    chart1.Series[1].Points.AddXY(2, 0);
                }
                if (radioButton11.Checked == true)
                {
                    double proverka = F11(X);
                    chart1.Series[0].Points.AddXY(X, F11(X));
                    chart1.Series[1].Points.AddXY(-0.5, 0);
                    chart1.Series[1].Points.AddXY(2.5, 0);
                }
                if (radioButton12.Checked == true)
                {
                    double proverka = F12(X);
                    chart1.Series[0].Points.AddXY(X, F12(X));
                    chart1.Series[1].Points.AddXY(-2, 0);
                    chart1.Series[1].Points.AddXY(-1, 0);
                    chart1.Series[1].Points.AddXY(2, 0);
                    chart1.Series[1].Points.AddXY(4.5, 0);
                }
                X = X + h;
            }
            if (radioButton5.Checked == true)
            {
                string str = textBox2.Text;
                string dst;
                Formula formula = CreateFormula(str, out dst);
                chart1.Series[1].Points.AddXY((N_tangent(F5, a, Acc)) - 1.5, 0);
                chart1.Series[1].Points.AddXY((N_tangent(F5, a, Acc)) + 1.5, 0);
            }
        }
        public string GetFromTextBox(string tbName)
        {
            TextBox textBox = this.Controls.Find(tbName, true).FirstOrDefault() as TextBox;
            return textBox?.Text ?? "";
        }
        #region parser
        private static Formula CreateFormula(string src, out string dst)
        {
            string code =
            @"using System;
 
         static class Code
         {
             public static double Formula(double x, double y, double z)
             {
                 return {source};
             }
         }";
            string s = @"-?[\d.]+|\#\d+|[a-z]";
            string[] patterns =
            {
            $@"(?<A>sin|cos|log)?\((?:{s})\)",
            $@"({s})\^({s})",
            $@"(?:{s})[*/](?:{s})",
            $@"(?:{s})[+-](?:{s})"
         };
            src = Regex.Replace(src, @"\s+", "").ToLower().Replace(',', '.');
            Dictionary<string, string> dic = new Dictionary<string, string>();
            Func<string, string> f1 = null;
            f1 = s1 =>
            {
                foreach (string p in patterns)
                {
                    var m = Regex.Match(s1, p);
                    if (m.Success)
                    {
                        string value = m.Value;
                        if (m.Groups["A"].Success)
                        {
                            value = value.Replace("sin", "Math.Sin");
                            value = value.Replace("cos", "Math.Cos");
                            value = value.Replace("log", "Math.Log");
                        }
                        if (m.Groups[1].Success && m.Groups[2].Success)
                        {
                            value = $"Math.Pow({m.Groups[1]},{m.Groups[2]})";
                        }
                        string key = $"#{dic.Count}";
                        dic.Add(key, value);
                        string ss = s1.Substring(0, m.Index) + key + s1.Substring(m.Index + m.Length);
                        return f1(ss);
                    }
                }
                return s1;
            };
            Func<string, string> f2 = null;
            f2 = s1 =>
            {
                string ss = Regex.Replace(s1, @"\#\d+", x => dic[x.Value]);
                return ss == s1 ? ss : f2(ss);
            };
            dst = f2(f1(src));
            code = code.Replace("{source}", dst);
            var compiler = CodeDomProvider.CreateProvider("C#");
            CompilerParameters parameters = new CompilerParameters();
            parameters.GenerateInMemory = true;
            var result = compiler.CompileAssemblyFromSource(parameters, code);
            if (result.Errors.Count == 0)
            {
                var assembly = result.CompiledAssembly;
                var type = assembly.GetType("Code");
                var method = type.GetMethod("Formula");
                return (Formula)Delegate.CreateDelegate(typeof(Formula), method);
            }
            return null;
        }
        public delegate double Formula(double x, double y, double z);
        #endregion
        //построить график
        private void button2_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox3.Text))
            {
                buildGraphh(0.1, -10, 10);
            }
            else
            {
                a = double.Parse(textBox1.Text/*, System.Globalization.CultureInfo.InvariantCulture*/);
                b = double.Parse(textBox3.Text/*, System.Globalization.CultureInfo.InvariantCulture*/);

                buildGraphh(1, -10, 10);
            }
        }
        #region solution
        public delegate double FunctionX(double x);
        delegate double PointIn(double a, double b);
        public double F1(double x)
        {
            return Math.Pow(x, 3) + 0.9 * Math.Pow(x, 2) + 1.1 * x - 7.8;
        }
        public double F2(double x)
        {
            return Math.Pow(x, 3) + 2 * Math.Pow(x, 2) - 4 * x - 5;
        }
        public double F3(double x)
        {
            return Math.Pow(x, 3) - 12 * x + 1;
        }
        public double F4(double x)
        {
            return 2 * Math.Pow(x, 5) + 5 * Math.Pow(x, 4) - 10 * Math.Pow(x, 2) + 10 * x - 3;
        }
        public double F5(double x)
        {
            var text = GetFromTextBox("textBox2");
            // Определить контекст нашего выражения
            ExpressionContext context = new ExpressionContext();
            // Разрешить выражению использовать все статические общедоступные методы System.Math
            context.Imports.AddType(typeof(Math));
            // Определите переменную int
            context.Variables["x"] = x;
            // Создание динамического выражения, которое вычисляет объект
            IDynamicExpression eDynamic = context.CompileDynamic(text);
            // Вычисление выражения
            double result = Convert.ToDouble(eDynamic.Evaluate());
            return result;
        }
        public double F6(double x)
        {
            return 2 * Math.Pow(x, 4) + 6 * Math.Pow(x, 3) + 5 * Math.Pow(x, 2) - 0.5;
        }
        public double F7(double x)
        {
            return Math.Sin(x) + 2 * x - 2;
        }
        public double F8(double x)
        {
            return 2 * x * Math.Log(x) - 4 * x + 5.3;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            table.Rows.Clear();
            AccBox.Text = "";
            textBox1.Text = "";
            textBox3.Text = "";
            ListOFres.Text = "";
            textBox11.Text = "";
            textBox4.Text = "";
            textBox8.Text = "";
            textBox7.Text = "";
            textBox5.Text = "";
            textBox9.Text = "";
            textBox10.Text = "";
            textBox6.Text = "";
            textBox12.Text = "";
            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();
            iterator_i = 0;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
            {
            char number = e.KeyChar;
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && number != 8 && number != 44 && number != 45 && number != 46) //цифры, клавиша BackSpace и запятая в ASCII
            {
                e.Handled = true;
            }
        }

        private void AccBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && number != 8 && number != 44 && number != 46) //цифры, клавиша BackSpace и запятая в ASCII
            {
                e.Handled = true;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("1.Môžete používať iba čísla\n2.Nemôžete používať písmena\n3.Desatinné čísla musia byť zadané pomocou čiarky");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("1.Môžete používať iba čísla\n2.Nemôžete používať písmena\n3.Nemôžete používať záporne hodnoty\n4.Desatinné čísla musia byť zadané pomocou čiarky");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MessageBox.Show("1.Znak násobenia je *\n2.Znak stupňa je ^\n3.Sínus je sin\n4.Kosínus je cos\n5.Logaritmus je ln\n6.Desatinné čísla musia byť zadané pomocou čiarky\n7.Premenná môže byť iba X\n8.X vedľa sínusu / kosínu / logaritmu by mal byť vždy v zátvorkách");
        }

        public double F9(double x)
        {
            return Math.Pow(x, 3) - 8 * x + 15;
        }
        public double F10(double x)
        {
            return Math.Pow(x, 3) + 3 * Math.Pow(x, 2) - 3;
        }
        public double F11(double x)
        {
            return 5 * Math.Pow(x, 3) + 2 * Math.Pow(x, 2) - 15 * x - 6;
        }
        public double F12(double x)
        {
            return Math.Pow(x, 3) - 7 * x - 7;
        }       
        public double Fdiv(double x)
        {
            var text = GetFromTextBox("textBox6");
            // Определить контекст нашего выражения
            ExpressionContext context = new ExpressionContext();
            // Разрешить выражению использовать все статические общедоступные методы System.Math
            context.Imports.AddType(typeof(Math));
            // Определите переменную int
            context.Variables["x"] = x;
            // Создание динамического выражения, которое вычисляет объект
            IDynamicExpression eDynamic = context.CompileDynamic(text);
            // Вычисление выражения
            double result = Convert.ToDouble(eDynamic.Evaluate());
            return result;
        }
        //первая производная функции
        public double Fdif(FunctionX F, double x)
        {
            double h, fr, fl, fc;

            //h = 0.1; // шаг, с которым вычисляем производную
            h = 0.001; // шаг, с которым вычисляем производную
            // приближенно вычисляем первую производную различными способами
            //fl = (F(x) - F(x - h)) / h; // левая
            //fr = (F(x + h) - F(x)) / h; // правая
            fc = (F(x + h) - F(x - h)) / (2 * h); // центральная
            //return fr;
            return fc;
        }
        public double N_tangent(FunctionX F, double x, double E)    // "Метод Ньютона" 
        {
            double x1;
            x1 = x - F(x) / Fdif(F, x);
            while (Math.Abs(F(x) / Fdif(F, x)) > E)
            {
                x = x1;
                x1 = x - F(x) / Fdif(F, x);
            }
            return x;
        }
        #endregion
        //расчёт в таблице
        private void writeDataTable(FunctionX F)
        {
            int row = dataGridView1.Rows.Count;
            if (row == 1 || iterator_i==0)
            {
                buildGraphhh(1, a, b);
                table.Rows.Clear();
                textBox11.Text = "";
                if (F(a) * Fdiv(a) > 0)
                {
                    Xo = double.Parse(textBox1.Text);
                    Xi = double.Parse(textBox3.Text);
                }
                else
                {
                    Xo = double.Parse(textBox3.Text);
                    Xi = double.Parse(textBox1.Text);
                }
                Xn = Xo;
                textBox12.Text = ""+Xn;

                
                if (Fdif(F, a) > Fdif(F, b))
                {
                    chislo = b;
                }
                else
                {
                    chislo = a;
                }

                Xn1 = Convert.ToDouble(string.Format("{0:0.000}", Xn - ((F(Xn) / (Fdif(F, Xo))))));
                error = Math.Abs(Xn1 - Xn);
                m = Fdif(F, chislo);
                textBox11.Text += "m = min|F'(x)|" + Environment.NewLine;
                textBox11.Text += "m = min|F'(" + chislo + ")|" + Environment.NewLine;
               // textBox11.Text += "m = " + F(chislo) + Environment.NewLine;
                textBox11.Text += "m = " + Fdif(F, chislo) + Environment.NewLine;
                textBox11.Text += "Potom" + Environment.NewLine;
                textBox11.Text += "Xn+1 = Xn - F(Xn) / F'(Xn)" + Environment.NewLine;
                textBox11.Text += "dosadenie hodnôt" + Environment.NewLine;
            }
            if (/*iterator_i < 10*/proverka == "+")
            {
                if (string.Format("{0:0.000}", error) != string.Format("{0:0.000}", 0.0000))
                {
                    if (iterator_i != 0)
                    {

                        Xn = Xn1;
                        Xn1 = Convert.ToDouble(string.Format("{0:0.000000}", Xn - ((F(Xn) / (Fdif(F, Xo))))));

                        error = Math.Abs(Xn1 - Xn);
                    }
                    if (((Xn - Acc) * (Xn + Acc)) > 0)
                    {
                        proverka = "+";
                    }
                    else
                    {
                        proverka = "-";
                    }
                    if (iterator_i == 18)
                    {
                        proverka = "-";
                    }
                    table.Rows.Add((iterator_i).ToString(), string.Format("{0:0.000000}", Xn), string.Format("{0:0.000}", F(Xn)), string.Format("{0:0.000}", Fdif(F, Xn)), string.Format("{0:0.0000}", Math.Abs(F(Xn) / m)), Xn1.ToString(), proverka);
                    textBox11.Text += "X" + (iterator_i + 1).ToString() + " = " + string.Format("{0:0.000000}", Xn) + " - (" + string.Format("{0:0.000}", F(Xn)) + " / " + string.Format("{0:0.000000}", Fdif(F, Xn)) + ") = " + Xn1.ToString() + Environment.NewLine;
                    iterator_i++;
                    chart1.Series[1].Points.Clear();
                    chart1.Series[1].Points.AddXY(Xn, 0);
                    
                    //chart1.Series[1].Points.AddXY(Xn1, 0);
                }
                else
                {
                    proverka = "-";
                    Xn = Xn1;
                    Xn1 = Convert.ToDouble(string.Format("{0:0.000000}", Xn - ((F(Xn) / (Fdif(F, Xo))))));
                    table.Rows.Add((iterator_i).ToString(), string.Format("{0:0.000000}", Xn), string.Format("{0:0.000}", F(Xn)), string.Format("{0:0.000}", Fdif(F, Xn)), string.Format("{0:0.0000}", Math.Abs(F(Xn) / m)), Xn1.ToString(), proverka);
                    textBox11.Text += "X" + (iterator_i + 1).ToString() + " = " + string.Format("{0:0.000000}", Xn) + " - (" + string.Format("{0:0.000}", F(Xn)) + " / " + string.Format("{0:0.000}", Fdif(F, Xn)) + ") = " + Xn1.ToString() + Environment.NewLine;
                    iterator_i++;
                    if (F == F7 && (Xn - Math.Abs(F(Xn) / m))==0)
                    {
                        chart1.Series[0].Points.Clear();
                        chart1.Series[1].Points.Clear();
                        buildGraph(1, -10, 10);
                        //chart1.Series[1].Points.Clear();
                        chart1.Series[1].Points.AddXY(0, 0);
                    }
                    else
                    {
                        chart1.Series[1].Points.AddXY((Xn - Math.Abs(F(Xn) / m)), 0);
                    }
                    
                    //chart1.Series[1].Points.AddXY(Xn, 0);
                    //chart1.Series[1].Points.AddXY(Xn1, 0);
                    MessageBox.Show("Koreň nájdený");
                    iterator_i = 0; proverka = "+";
                    ListOFres.Text = "" + (Xn - Math.Abs(F(Xn) / m));
                }
            }
            else
            {
                if (F==F7 && (Xn - Math.Abs(F(Xn) / m)) == 0)
                {
                    chart1.Series[0].Points.Clear();
                    chart1.Series[1].Points.Clear();
                    buildGraph(1, -10, 10);
                    //chart1.Series[1].Points.Clear();
                    chart1.Series[1].Points.AddXY(0, 0);
                }
                else
                {
                    chart1.Series[1].Points.Clear();
                    chart1.Series[1].Points.AddXY((Xn - Math.Abs(F(Xn) / m)), 0);
                }
                MessageBox.Show("Koreň nájdený");
                iterator_i = 0; proverka = "+";
                ListOFres.Text = "" + (Xn - Math.Abs(F(Xn) / m));
            }
            if (F(a) * F(b) < 0)
            {
                textBox5.Text = string.Format("{0:0.00}", F(a)) + " * " + string.Format("{0:0.00}", F(b)) + " < 0";
            }
            else
            {
                textBox5.Text = "Nie:" + string.Format("{0:0.00}", F(a)) + " * " + string.Format("{0:0.00}", F(b)) + " > 0";
                MessageBox.Show("Tento interval nemá korene.");
                table.Rows.Clear();
                AccBox.Text = "";
                textBox1.Text = "";
                textBox3.Text = "";
                //ListOFres.Text = "";
                //textBox11.Text = "";
                //textBox4.Text = "";
                //textBox8.Text = "";
                //textBox7.Text = "";
                //textBox5.Text = "";
                //textBox9.Text = "";
                //textBox10.Text = "";
                //textBox6.Text = "";
                //textBox12.Text = "";
                chart1.Series[0].Points.Clear();
                chart1.Series[1].Points.Clear();
                iterator_i = 0;
            }
            if (Math.Abs(Fdiv(a)) > 0)
            {
                textBox7.Text = string.Format("{0:0.00}",Math.Abs(Fdiv(a))) + " > 0";
            }
            else
            {
                textBox7.Text = "Nie:" + Fdiv(a);
            }
            if (Math.Abs(Fdiv(b)) > 0)
            {
                textBox8.Text = string.Format("{0:0.00}", Math.Abs(Fdiv(b))) + " > 0";
            }
            else
            {
                textBox8.Text = "Nie:" + Fdiv(b);
            }
            if (F(a) * Fdiv(a) > 0)
            {
                textBox10.Text = string.Format("{0:0.00}", F(a)) + " * " + string.Format("{0:0.00}", Fdiv(a)) + " > 0";
            }
            else
            {
                textBox10.Text = "Nie:" + string.Format("{0:0.00}", F(a)) + " * " + string.Format("{0:0.00}", Fdiv(a));
            }
            if (F(b) * Fdiv(b) > 0)
            {
                textBox9.Text = string.Format("{0:0.00}", F(b)) + " * " + string.Format("{0:0.00}", Fdiv(b)) + " > 0";
            }
            else
            {
                textBox9.Text = "Nie:" + string.Format("{0:0.00}", F(b)) + " * " + string.Format("{0:0.00}", Fdiv(b));
            }

            textBox11.Text += "|X" + (iterator_i).ToString() + "- α| ≤ |F(X" + (iterator_i).ToString() + ")| / m" + Environment.NewLine;
            textBox11.Text += "|X" + (iterator_i).ToString() + "- α| ≤ " + Math.Abs(F(Xn)) + " / " + m + Environment.NewLine;
            textBox11.Text += "|" + Xn + "- α| = " + Math.Abs(F(Xn) / m) + Environment.NewLine;
            textBox11.Text += "α = " + (Xn - Math.Abs(F(Xn) / m)) + Environment.NewLine;
        }
        private void writeDataTabl(FunctionX F)
        {
            int row = dataGridView1.Rows.Count;
            if (row > 0)
            {
                buildGraph(1, -10, 10);
                table.Rows.Clear();
                textBox11.Text = "";
                if (F(a) * Fdiv(a) > 0)
                {
                    Xo = double.Parse(textBox1.Text/*, System.Globalization.CultureInfo.InvariantCulture*/);
                }
                else
                {
                    Xo = double.Parse(textBox3.Text/*, System.Globalization.CultureInfo.InvariantCulture*/);
                }
                Xn = Xo;
                textBox12.Text = "" + Xn;

                Xn1 = Convert.ToDouble(string.Format("{0:0.000000}", Xn - ((F(Xn) / (Fdif(F, Xo))))));
                error = Math.Abs(Xn1 - Xn);
                m = F(chislo);
                textBox11.Text += "m = min|F'(x)|" + Environment.NewLine;
                textBox11.Text += "m = min|F'(" + chislo + ")|" + Environment.NewLine;
                // textBox11.Text += "m = " + F(chislo) + Environment.NewLine;
                textBox11.Text += "m = " + Fdif(F, chislo) + Environment.NewLine;
                textBox11.Text += "Potom" + Environment.NewLine;
                textBox11.Text += "Xn+1 = Xn - F(Xn) / F'(Xn)" + Environment.NewLine;
                textBox11.Text += "dosadenie hodnôt" + Environment.NewLine;
            }
            for (iterator_i = 0; iterator_i < 18; iterator_i++)
            {
                if (string.Format("{0:0.000}", error) != string.Format("{0:0.000}", 0.0000) && proverka == "+")
                {
                    if (iterator_i != 0)
                    {

                        Xn = Xn1;
                        Xn1 = Convert.ToDouble(string.Format("{0:0.000000}", Xn - ((F(Xn) / (Fdif(F, Xo))))));

                        error = Math.Abs(Xn1 - Xn);
                    }
                    if (((Xn - Acc) * (Xn + Acc)) > 0)
                    {
                        proverka = "+";
                    }
                    else
                    {
                        proverka = "-";
                    }
                    if (iterator_i == 18)
                    {
                        proverka = "-";
                    }
                    table.Rows.Add((iterator_i).ToString(), string.Format("{0:0.000000}", Xn), string.Format("{0:0.000}", F(Xn)), string.Format("{0:0.000}", Fdif(F, Xn)), string.Format("{0:0.0000}", Math.Abs(F(Xn) / m)), Xn1.ToString(), proverka);
                    textBox11.Text += "X" + (iterator_i + 1).ToString() + " = " + string.Format("{0:0.000000}", Xn) + " - (" + string.Format("{0:0.000}", F(Xn)) + " / " + string.Format("{0:0.000}", Fdif(F, Xn)) + ") = " + Xn1.ToString() + Environment.NewLine;
                    chart1.Series[1].Points.Clear();
                    chart1.Series[1].Points.AddXY((Xn - Math.Abs(F(Xn) / m)), 0);

                    //chart1.Series[1].Points.AddXY(Xn1, 0);
                }
                else
                {
                    proverka = "-";
                    table.Rows.Add((iterator_i).ToString(), string.Format("{0:0.000000}", Xn), string.Format("{0:0.000}", F(Xn)), string.Format("{0:0.000}", Fdif(F, Xn)), string.Format("{0:0.0000}", Math.Abs(F(Xn) / m)), Xn1.ToString(), proverka);
                    textBox11.Text += "X" + (iterator_i + 1).ToString() + " = " + string.Format("{0:0.000000}", Xn) + " - (" + string.Format("{0:0.000}", F(Xn)) + " / " + string.Format("{0:0.000}", Fdif(F, Xn)) + ") = " + Xn1.ToString() + Environment.NewLine;
                    chart1.Series[1].Points.Clear();
                    chart1.Series[1].Points.AddXY((Xn - Math.Abs(F(Xn) / m)), 0);

                    MessageBox.Show("Koreň nájdený");
                    iterator_i = 0; proverka = "+";
                    ListOFres.Text = "" + (Xn - Math.Abs(F(Xn) / m));
                    break;
                }
            }
            if (F(a) * F(b) < 0)
            {
                textBox5.Text = string.Format("{0:0.00}", F(a)) + " * " + string.Format("{0:0.00}", F(b)) + " < 0";
            }
            else
            {
                textBox5.Text = "Nie:" + string.Format("{0:0.00}", F(a)) + " * " + string.Format("{0:0.00}", F(b)) + " > 0";
            }
            if (Math.Abs(Fdiv(a)) > 0)
            {
                textBox7.Text = string.Format("{0:0.00}", Math.Abs(Fdiv(a))) + " > 0";
            }
            else
            {
                textBox7.Text = "Nie:" + Fdiv(a);
            }
            if (Math.Abs(Fdiv(b)) > 0)
            {
                textBox8.Text = string.Format("{0:0.00}", Math.Abs(Fdiv(b))) + " > 0";
            }
            else
            {
                textBox8.Text = "Nie:" + Fdiv(b);
            }
            if (F(a) * Fdiv(a) > 0)
            {
                textBox10.Text = string.Format("{0:0.00}", F(a)) + " * " + string.Format("{0:0.00}", Fdiv(a)) + " > 0";
            }
            else
            {
                textBox10.Text = "Nie:" + string.Format("{0:0.00}", F(a)) + " * " + string.Format("{0:0.00}", Fdiv(a));
            }
            if (F(b) * Fdiv(b) > 0)
            {
                textBox9.Text = string.Format("{0:0.00}", F(b)) + " * " + string.Format("{0:0.00}", Fdiv(b)) + " > 0";
            }
            else
            {
                textBox9.Text = "Nie:" + string.Format("{0:0.00}", F(b)) + " * " + string.Format("{0:0.00}", Fdiv(b));
            }

            textBox11.Text += "|X" + (iterator_i).ToString() + "- α| ≤ |F(X" + (iterator_i).ToString() + ")| / m" + Environment.NewLine;
            textBox11.Text += "|X" + (iterator_i).ToString() + "- α| ≤ " + Math.Abs(F(Xn)) + " / " + m + Environment.NewLine;
            textBox11.Text += "|" + Xn + "- α| = " + Math.Abs(F(Xn) / m) + Environment.NewLine;
            textBox11.Text += "α = " + (Xn - Math.Abs(F(Xn) / m)) + Environment.NewLine;
        }
        void deivate()
        {
            try
            {
                ExpressionTree tree = new ExpressionTree(textBox2.Text);
                textBox4.Text = tree.GetDerivative().GetInFixNotation();
                string MyString = textBox4.Text;
                char[] MyChar = { ' ', ')'};
                string NewString = MyString.TrimEnd(MyChar);
                tree = new ExpressionTree(NewString);
                textBox6.Text = tree.GetDerivative().GetInFixNotation();
            }
            catch(Exception e)
            {
                //MessageBox.Show(e.ToString());
            }
        }
    }
}
