using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
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

namespace download_url_progress_bar
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new ViewModel();
        }

        //// HttpClient создается один раз на всё время работы приложения
        //private static readonly HttpClient _client = new HttpClient();

        //// Токен отмены служит для прерывания работы загрузчика в любой момент
        //private CancellationTokenSource _cts;

        //// метод универсален, проверен в .NET Core и .NET Framework
        //private async Task DownloadAndSaveFileAsync(string url, string path, IProgress<int> status, CancellationToken token)
        //{
        //    const int bufferLength = 8192;
        //    long currentPosition = File.Exists(path) ? new FileInfo(path).Length : 0;

        //    using (HttpRequestMessage request = new HttpRequestMessage { RequestUri = new Uri(url) })
        //    {
        //        request.Headers.Range = new RangeHeaderValue(currentPosition, null);
        //        using (HttpResponseMessage response = await _client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, token))
        //        {
        //            response.EnsureSuccessStatusCode();
        //            using (Stream responseStream = await response.Content.ReadAsStreamAsync())
        //            using (FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write, FileShare.None))
        //            {
        //                long contentLength = currentPosition + response.Content.Headers.ContentLength ?? 0;
        //                int progress = -1;
        //                int oldProgress;
        //                byte[] buffer = new byte[bufferLength];
        //                int bytesReceived;
        //                while ((bytesReceived = await responseStream.ReadAsync(buffer, 0, bufferLength, token).ConfigureAwait(false)) > 0)
        //                {
        //                    await fs.WriteAsync(buffer, 0, bytesReceived, token).ConfigureAwait(false);

        //                    currentPosition += bytesReceived;
        //                    oldProgress = progress;
        //                    progress = (int)(currentPosition * 100 / contentLength);
        //                    // так как значение от 0 до 100, нет особого смысла повтороно обновлять интерфейс, если значение не изменилось.
        //                    if (oldProgress != progress)
        //                    {
        //                        status?.Report(progress);
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}

        //// обратите внимание на async здесь
        //private async void button1_Click(object sender, EventArgs e)
        //{
        //    button1.Enabled = false;
        //    button2.Enabled = true;
        //    if (_cts == null)
        //    {
        //        _cts = new CancellationTokenSource();
        //        try
        //        {
        //            // укажите здесь нужный URL и путь к файлу
        //            // обратите внимение на new Progress<int>(v => progressBar1.Value = v) - оно и меняет значение прогресс бара во время загрузки
        //            await DownloadAndSaveFileAsync("https://example.org/file.txt", "file.txt", new Progress<int>(v => progressBar1.Value = v), _cts.Token);
        //        }
        //        catch (OperationCanceledException) { }
        //        catch (HttpRequestException ex)
        //        {
        //            if (ex.Message.Contains("416")) // 416 (Requested Range Not Satisfiable)
        //            {
        //                MessageBox.Show("Файл уже закачан");
        //            }
        //            else
        //            {
        //                MessageBox.Show(ex.Message + "\r\n\r\n" + ex.StackTrace, "HttpRequestException");
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show(ex.Message + "\r\n\r\n" + ex.StackTrace, "Exception");
        //        }
        //        finally
        //        {
        //            _cts.Dispose();
        //            _cts = null;
        //        }
        //        progressBar1.Value = 0;
        //    }
        //    button1.Enabled = true;
        //    button2.Enabled = false;
        //}

        //private void button2_Click(object sender, EventArgs e)
        //{
        //    if (_cts != null && !_cts.IsCancellationRequested) _cts.Cancel();
        //}

    }
}
