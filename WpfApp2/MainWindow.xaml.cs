using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace WpfApp2
{

    public partial class MainWindow : Window
    {
        private readonly ApiClient _apiClient;
        private ObservableCollection<Membre> _membres;

        public MainWindow()
        {
            InitializeComponent();
            _apiClient = new ApiClient();
            _membres = new ObservableCollection<Membre>();
            MembresListBox.ItemsSource = _membres;
        }

        private async void FetchMembersButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<Membre> membres = await _apiClient.GetMembresAsync();
                _membres.Clear();
                foreach (Membre membre in membres)
                {
                    _membres.Add(membre);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private async void DeleteMembreButton_Click(object sender, RoutedEventArgs e)
        {
            Membre membre = (Membre)MembresListBox.SelectedItem;
            if (membre != null)
            {
                try
                {
                    bool result = await _apiClient.DeleteMembreAsync(membre.Id);
                    if (result)
                    {
                        _membres.Remove(membre);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting member: {ex.Message}");
                }
            }
        }
    }
}
