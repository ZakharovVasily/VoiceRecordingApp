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

            DataContext = _appViewModel = new AppViewModel();

            FinishButton.Visibility = Visibility.Hidden;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _appViewModel.StartRecord();

            StartButton.Visibility = Visibility.Hidden;
            FinishButton.Visibility = Visibility.Visible;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            _appViewModel.StopRecording();
            MessageBox.Show("Запись окончена. Файл сохранен под названием : audio" 
                            + _appViewModel.FileNameCollection.Count.ToString() + ".wav");
            _appViewModel.UpdateFileNameCollection();

            StartButton.Visibility = Visibility.Visible;
            FinishButton.Visibility = Visibility.Hidden;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            _appViewModel.PlayAudio();
        }
    }
}
