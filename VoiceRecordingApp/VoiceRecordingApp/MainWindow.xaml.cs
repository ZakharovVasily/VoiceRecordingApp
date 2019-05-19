using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace VoiceRecordingApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private AppViewModel _appViewModel;

        public MainWindow()
        {
            InitializeComponent();

            DataContext = new AppViewModel();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _appViewModel.StartRecord();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            _appViewModel.StopRecording();
            MessageBox.Show("Запись окончена.");
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            _appViewModel.PlayAudio();
        }
    }
}
