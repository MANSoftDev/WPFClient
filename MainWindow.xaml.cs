using System.Windows;
using System.Windows.Controls;
using Microsoft.SharePoint.Client;
using System;

namespace WPFClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            contactDetails.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Contacts.ItemsSource = Logic.GetList();
        }

        private void Contacts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(contactDetails.Visibility != System.Windows.Visibility.Visible)
            {
                contactDetails.Visibility = System.Windows.Visibility.Visible;
            }
            if(e.AddedItems.Count != 0)
            {
                SelectedItemId = Convert.ToInt32(((ListItem)e.AddedItems[0])["ID"]);
            }
        }

        private void OnEdit(object sender, RoutedEventArgs e)
        {
            contactEdit.Visibility = System.Windows.Visibility.Visible;
            contactDetails.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void OnSave(object sender, RoutedEventArgs e)
        {
            contactEdit.Visibility = System.Windows.Visibility.Collapsed;
            contactDetails.Visibility = System.Windows.Visibility.Visible;

            DetailsFirstName.Text = EditFirstName.Text;
            DetailsTitle.Text = EditTitle.Text;
            DetailsWorkAddress.Text = EditWorkAddress.Text;
            DetailsWorkCity.Text = EditWorkCity.Text;
            DetailsWorkState.Text = EditWorkState.Text;
            DetailsWorkZip.Text = EditWorkZip.Text;

            Logic.Update(SelectedItemId, EditFirstName.Text, EditTitle.Text, EditWorkAddress.Text, 
                EditWorkCity.Text, EditWorkState.Text, EditWorkZip.Text);

            Contacts.Items.Refresh();
        }

        private void OnDelete(object sender, RoutedEventArgs e)
        {
            Logic.Delete(SelectedItemId);
            Contacts.Items.Refresh();
            Contacts.ItemsSource = Logic.GetList();
            contactDetails.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void OnAddContact(object sender, RoutedEventArgs e)
        {
            AddContact wnd = new AddContact();
            wnd.Show();
        }

        private int SelectedItemId { get; set; }


    }
}
