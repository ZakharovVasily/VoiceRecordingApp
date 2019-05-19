using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using VoiceRecordingApp.Annotations;

namespace VoiceRecordingApp
{
    public class Audio : INotifyPropertyChanged
    {
        private string _name;

        private string _path;

        private string _format;

        public string Name
        {
            get => _name;

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException("Имя не может быть пустое.");

                _name = value;
                OnPropertyChanged("Name");
            }
        }

        public string Path
        {
            get => _path;

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException("Путь до фойла не может быть пустым.");

                _path = value;
                OnPropertyChanged("Path");
            }
        }

        public string Format
        {
            get => _format;

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException("Имя не может быть пустое.");

                _format = value;
                OnPropertyChanged("Format");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
