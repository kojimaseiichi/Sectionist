using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;

namespace Sectionist
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void _btConv_Click(object sender, RoutedEventArgs e)
        {
            List<int> sec = new List<int>();
            if (string.IsNullOrEmpty(_tbStart.Text) == false)
            {
                foreach (var a in Regex.Split(_tbStart.Text, @"\."))
                    sec.Add(int.Parse(a));
                sec[sec.Count - 1]--;
            }

            var lines = Regex.Split(_tbContent.Text, @"\r\n|\r|\n");
            StringBuilder sb = new StringBuilder();
            bool blank = true;
            foreach(var line in lines)
            {
                StringBuilder nl = new StringBuilder();
                var cells = Regex.Split(line, @"\t");
                for (int c = 0; c < cells.Length; c++)
                {
                    blank = true;
                    if (string.IsNullOrEmpty(cells[c]) == false)
                    {
                        Regex r = new Regex(@"(?<s>([1-9１-９]+\.)*[1-9１-９]+|[#＃・]+)\s*(?<n>\w+)");
                        var m = r.Match(cells[c]);
                        if (m.Groups["s"].Success)
                        {
                            if (sec.Count == c + 1)
                                sec[sec.Count - 1]++;
                            else if (sec.Count < c + 1)
                                sec.Add(1);
                            else
                            {
                                sec.RemoveAt(sec.Count - 1);
                                sec[sec.Count - 1]++;
                            }
                            if (m.Groups["n"].Success)
                                nl.Append("" + _join(sec) + "  " + m.Groups["n"].Value);
                            else
                                nl.Append(_join(sec));
                            blank = false;
                            break;
                        }
                    }
                    nl.Append("\t");
                }
                if (blank == false)
                    sb.AppendLine(nl.ToString());
            }
            _tbContent.Text = sb.ToString();
        }

        private string _join(List<int> sec)
        {
            return sec.ConvertAll(x => x.ToString()).Aggregate((a, b) => a + "." + b);
        }
    }
}
