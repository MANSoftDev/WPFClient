using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WPFClient
{
    /// <summary>
    /// Interaction logic for AddContact.xaml
    /// </summary>
    public partial class AddContact : Window
    {
        public AddContact()
        {
            InitializeComponent();
        }

        private void OnSave(object sender, RoutedEventArgs e)
        {
            Logic.AddContact(FirstName.Text, Title.Text, WorkAddress.Text, WorkCity.Text, WorkState.Text, WorkZip.Text);
            ShowMain();
        }

        private void OnCancel(object sender, RoutedEventArgs e)
        {
            ShowMain();
        }

        private void ShowMain()
        {
            this.Close();
        }
    }
}
