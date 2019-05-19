using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Linq;
using System.Media;
using System.Runtime.CompilerServices;
using NAudio.Wave;
using NAudio.FileFormats;
using NAudio.CoreAudioApi;
using NAudio;
using NAudio.Dsp;
using NAudio.Wave;
using VoiceRecordingApp.Annotations;

namespace VoiceRecordingApp
{
    class AppViewModel : INotifyPropertyChanged
    {
        // WaveIn - поток для записи
        private WaveIn _waveIn;

        //Класс для записи в файл
        private WaveFileWriter _writer;

        //Имя файла для записи
        private string outputFilename;

        private Audio _selectedAudio;

        private ObservableCollection<Audio> _fileNameCollection;

        public Audio SelectedAudio
        {
            get => _selectedAudio;
            set
            {
                _selectedAudio = value;
                OnPropertyChanged("SelectedAudio");
            }
        }

        public ObservableCollection<Audio> FileNameCollection
        {
            get => _fileNameCollection;
            set
            {
                _fileNameCollection = new ObservableCollection<Audio>();

                UpdateFileNameCollection();
            }
        }

        public void UpdateFileNameCollection()
        {
            _fileNameCollection.Clear();

            var filesname = Directory.GetFiles(@"Audio", "*.wav").ToList<string>();

            foreach (var t in filesname)
            {
                var audio = new Audio();

                audio.Path = t;
                
                _fileNameCollection.Add(audio);
            }
        }

        public AppViewModel()
        {
            FileNameCollection = new ObservableCollection<Audio>();
        }

        void WaveIn_DataAvailable(object sender, WaveInEventArgs e)
        {
            _writer.WriteData(e.Buffer, 0, e.BytesRecorded);
        }

        public void StartRecord()
        {
            _waveIn = new WaveIn();

            outputFilename = "Audio/audio" + FileNameCollection.Count + ".wav";

            //Дефолтное устройство для записи (если оно имеется)
            _waveIn.DeviceNumber = 0;

            //Прикрепляем к событию DataAvailable обработчик, возникающий при наличии записываемых данных
            _waveIn.DataAvailable += WaveIn_DataAvailable;

            //Прикрепляем обработчик завершения записи
            _waveIn.RecordingStopped += new EventHandler<StoppedEventArgs>(WaveIn_RecordingStopped);

            //Формат wav-файла - принимает параметры - частоту дискретизации и количество каналов(здесь mono)
            _waveIn.WaveFormat = new WaveFormat(8000, 1);

            //Инициализируем объект WaveFileWriter
            _writer = new WaveFileWriter(outputFilename, _waveIn.WaveFormat);

            _waveIn.StartRecording();
        }

        private void WaveIn_RecordingStopped(object sender, EventArgs e)
        {

            _waveIn.Dispose();
            _waveIn = null;
            _writer.Close();
            _writer = null;
        }

        public void StopRecording()
        {
            _waveIn.StopRecording();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void PlayAudio()
        {
            SoundPlayer simpleSound = new SoundPlayer(SelectedAudio.Path);
            simpleSound.Play();
        }
    }
}
