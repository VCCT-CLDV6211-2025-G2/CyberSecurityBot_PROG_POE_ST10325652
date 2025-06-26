using CyberSecurityBot_PROG_POE_ST10325652.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;


namespace CyberSecurityBot_PROG_POE_ST10325652
{
    /// <summary>
    /// Interaction logic for WelcomeView.xaml
    /// </summary>
    public partial class WelcomeView : UserControl
    {
        public WelcomeView()
        {
            InitializeComponent();
            DataContext = new WelcomeViewModel();
        }
    }
}
