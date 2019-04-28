using System;
using System.Collections.Generic;
using System.Windows;
using System.IO;
using System.Windows.Controls;
using MahApps.Metro.Controls;
using MahApps.Metro.Actions;
using MahApps.Metro.Behaviours;
using MahApps.Metro.Converters;
using MahApps.Metro;

namespace WpfApp5
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        String Number1 = "0", Number2 = "";
        List<string> expression = new List<string>();
        Operator flag = Operator.none;
        enum Operator { add, sub, mul, div, rem,none };
        public MainWindow()
        {
            InitializeComponent();
            Resources = Application.Current.Resources;
        }

        private void Button_number_Click(object sender, RoutedEventArgs e)
        {
            Numberinput(Convert.ToString((sender as Button).Content));
        }
        private void Button_flag_Click(object sender, RoutedEventArgs e)
        {
            Flaginput(Convert.ToString((sender as Button).Content));
        }
        private void Button_equ_Click(object sender, RoutedEventArgs e)
        {
            if (Number1 == "" || Number2 == "")
            {
                Number1 = "";
                Number2 = "";
                flag = Operator.none;
                return;
            }
            switch (flag)
            {
                case Operator.add:
                    label1.Content = Convert.ToString(Convert.ToDouble(Number1) + Convert.ToDouble(Number2));
                    expression.Add(Number1 + "+" + Number2 + "=" + label1.Content);
                    break;
                case Operator.sub:
                    label1.Content = Convert.ToString(Convert.ToDouble(Number1) - Convert.ToDouble(Number2));
                    expression.Add(Number1 + "-" + Number2 + "=" + label1.Content);
                    break;
                case Operator.mul:
                    label1.Content = Convert.ToString(Convert.ToDouble(Number1) * Convert.ToDouble(Number2));
                    expression.Add(Number1 + "×" + Number2 + "=" + label1.Content);
                    break;
                case Operator.div:
                    label1.Content = Convert.ToString(Convert.ToDouble(Number1) / Convert.ToDouble(Number2));
                    expression.Add(Number1 + "÷" + Number2 + "=" + label1.Content);
                    break;
                case Operator.rem:
                    label1.Content=Convert.ToString(Convert.ToDouble(Number1)%Convert.ToDouble(Number2));
                    expression.Add(Number1 + "%" + Number2 + "=" + label1.Content);
                    break;
            }
            Number1 = Convert.ToString(label1.Content);
            //Number2 = "";
            //flag = Operator.none;
        }
        private void Button_clear_Click(object sender, RoutedEventArgs e)
        {
            Number1 = "0";
            Number2 = "";
            flag = Operator.none;
            label1.Content = 0;
        }
        private void Button_del_Click(object sender, RoutedEventArgs e)
        {
            if (flag == Operator.none)
            {
                if (Number1.Length > 0)
                {
                    if (Number1.Length > 1)
                    {
                        Number1 = Number1.Remove(Number1.Length - 1);
                        label1.Content = Number1;
                    }
                    else
                    {
                        Number1 = Number1.Remove(Number1.Length - 1);
                        label1.Content = 0;
                    }
                }
                else
                    label1.Content = 0;
            }
            else
            {
                if (Number2.Length > 0)
                {
                    if (Number2.Length > 1)
                    {
                        Number2 = Number2.Remove(Number2.Length - 1);
                        label1.Content = Number2;
                    }
                    else
                    {
                        Number2 = Number2.Remove(Number2.Length - 1);
                        label1.Content = 0;
                    }
                }
                else
                    label1.Content = 0;
            }
        }
        private void MenuItem_save_Click(object sender, RoutedEventArgs e)
        {
            FileStream resultfile = new FileStream("result.txt", FileMode.Append);
            StreamWriter streamWriter = new StreamWriter(resultfile);
            foreach (string a in expression)
            {
                streamWriter.WriteLine(a);
            }
            streamWriter.Close();
        }
        private void Numberinput(string content)
        {
            if (flag == Operator.none)
            {
                if (Number1.Contains(".") && content == ".")
                {
                    return;
                }
                if (Number1 == "0" && content == "0")
                {
                    return;
                }
                if (Number1 == "0" && content == ".")
                {
                    Number1 = "0" + content;
                    label1.Content = Number1;
                    return;
                }
                if (Number1 == "0" && content != "0")
                {
                    Number1 = content;
                    label1.Content = Number1;
                    return;
                }
                Number1 = Number1 + content;
                label1.Content = Number1;
            }
            else
            {
                if (Number2.Contains(".") && content == ".")
                {
                    return;
                }
                if (Number2 == "0" && content == "0")
                {
                    return;
                }
                if (Number2 == "0" && content == ".")
                {
                    Number2 = "0" + content;
                    label1.Content = Number2;
                    return;
                }
                if (Number2 == "0" && content != "0")
                {
                    Number2 = content;
                    label1.Content = Number2;
                    return;
                }
                Number2 = Number2 + content;
                label1.Content = Number2;
            }
        }
        private void Flaginput(string content)
        {
            switch (content)
            {
                case "+":
                    flag = Operator.add;
                    break;
                case "-":
                    flag = Operator.sub;
                    break;
                case "×":
                    flag = Operator.mul;
                    break;
                case "÷":
                    flag = Operator.div;
                    break;
                case "%":
                    flag = Operator.rem;
                    break;
            }
        }

        private void Button_rec_Click(object sender, RoutedEventArgs e)
        {
            if (Number1 != "0" && flag == Operator.none) 
            {
                Number1 = Convert.ToString(1 / Convert.ToDouble(Number1));
                label1.Content = Number1;
            }
            if (Number2 != "0" && flag != Operator.none)
            {
                Number2 = Convert.ToString(1 / Convert.ToDouble(Number2));
                label1.Content = Number2;
            }
            else
            {
                return;
            }
        }
        private void Button_squ_Click(object sender, RoutedEventArgs e)
        {
            if(flag==Operator.none)
            {
                Number1 = Convert.ToString(Convert.ToDouble(Number1) * Convert.ToDouble(Number1));
                label1.Content = Number1;
            }
            else
            {
                Number2 = Convert.ToString(Convert.ToDouble(Number2) * Convert.ToDouble(Number2));
                label1.Content = Number2;
            }
        }

        private void Button_sqrt_Click(object sender, RoutedEventArgs e)
        {
            if(flag==Operator.none)
            {
              if(Convert.ToDouble(Number1)>=0)
                {
                    Number1 = Convert.ToString(System.Math.Sqrt(Convert.ToDouble(Number1)));
                    label1.Content = Number1;
                }
              else
                {
                    label1.Content = "暂不支持此功能";
                }
            }
            else
            {
                if (Convert.ToDouble(Number2) >= 0)
                {
                    Number2 = Convert.ToString(System.Math.Sqrt(Convert.ToDouble(Number2)));
                    label1.Content = Number2;
                }
                else
                {
                    label1.Content = "暂不支持此功能";
                }
            }
        }

        private void Button_opp_Click(object sender, RoutedEventArgs e)
        {
            if(flag==Operator.none)
            {
                Number1 = Convert.ToString(-Convert.ToDouble(Number1));
                label1.Content = Number1;
            }
            else
            {
                Number2 = Convert.ToString(-Convert.ToDouble(Number2));
                label1.Content = Number2;
            }
        }

        private void Button_ce_Click(object sender, RoutedEventArgs e)
        {
            if(flag==Operator.none)
            {
                Number1 = "0";
                label1.Content = Number1;
            }
            else
            {
                Number2 = "0";
                label1.Content = Number2;
            }
        }

        private void MenuItem_darkthemeChange_Click(object sender, RoutedEventArgs e)
        {
            Tuple<AppTheme, Accent> theme = ThemeManager.DetectAppStyle(this);
            ThemeManager.ChangeAppStyle(this, theme.Item2, ThemeManager.GetAppTheme("BaseDark"));
        }

        private void MenuItem_lightthemeChange_Click(object sender, RoutedEventArgs e)
        {
                Tuple<AppTheme, Accent> theme = ThemeManager.DetectAppStyle(this);
                ThemeManager.ChangeAppStyle(this, theme.Item2, ThemeManager.GetAppTheme("BaseLight"));

        }






















    }






}
