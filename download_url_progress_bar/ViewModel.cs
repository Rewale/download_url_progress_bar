using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace download_url_progress_bar
{
    public class RelayCommand : ICommand
    {
        private Action<object> execute;
        private Func<object, bool> canExecute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return this.canExecute == null || this.canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            this.execute(parameter);
        }
    }
    class ViewModel:INotifyPropertyChanged
    {
        public BitmapImage ImageSource
        {
            get
            {
                return LoadImage(byteArray);
            }
        }
        private byte[] byteArray;
        private byte[] ByteArray
        {
            get
            {
                return byteArray;
            }
            set
            {
                byteArray = value;
                OnPropertyChanged(nameof(ImageSource));
            }
        }
        private string url;

        public string Url
        {
            get { return url; }
            set { url = value; }
        }
        private int progressBarValue = 0;

        public int ProgressBarValue
        {
            get { return progressBarValue; }
            set { 

                progressBarValue = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand clickCommand
        {
            get
            {
                return new RelayCommand(async o =>
                {
                    //Task t1 = Task.Run(() => IncreaseBar());
                    Task t2 = Task.Run(() => GetSomeImage(Url, new Progress<int>(v => ProgressBarValue = v)));
                    
                    await Task.WhenAll(new[] { t2});                   
                    //ByteArray = await GetSomeImage(Url);
                });
                
            }
        }
        public void IncreaseBar()
        {
            ProgressBarValue = 0;
            for (int i = 1; i <= 100; i++)
            {
                ProgressBarValue++;
                //Thread.Sleep(10);

            }
            //ProgressBarValue = 0;
        }
        private async void GetSomeImage(string ImageUrl, IProgress<int> status)
        {
            //Thread.Sleep(500);
            status?.Report(0);
            using (var client = new HttpClient())
            {
                ByteArray =  await client.GetByteArrayAsync(ImageUrl);                
                
                status?.Report(100);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        private static BitmapImage LoadImage(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0) return null;
            var image = new BitmapImage();
            using (var mem = new MemoryStream(imageData))
            {
                mem.Position = 0;
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = mem;
                image.EndInit();
            }
            image.Freeze();
            return image;
        }
    }
}
