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
    /// NumberingControl.xaml の相互作用ロジック
    /// </summary>
    public partial class NumberingControl : UserControl
    {
        public NumberingControl()
        {
            InitializeComponent();
        }

        private void _btConv_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(_tbPattern.Text)) return;
            var r = new Regex(_tbPattern.Text);
            int num = int.Parse(_tbStart.Text);
            if (string.IsNullOrEmpty(_tbFormat.Text))
            {
                _tbContent.Text = r.Replace(_tbContent.Text, m =>
                {
                    return (num++).ToString();
                });
            }
            else
            {
                _tbContent.Text = r.Replace(_tbContent.Text, m =>
                {
                    return (num++).ToString(_tbFormat.Text);
                });
            }
        }
    }
}
