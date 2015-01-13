using System;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using Cvte.GitRain.Configs;
using Cvte.GitRain.Properties;

namespace Cvte.GitRain.UI
{
    /// <summary>
    /// 一个用于在线获取必应图片显示的控件。
    /// </summary>
    public partial class BingImageControl : UserControl
    {
        /// <summary>
        /// 创建 <see cref="BingImageControl"/> 的新实例。
        /// </summary>
        public BingImageControl()
        {
            InitializeComponent();
            Loaded += (sender, args) => LoadBingImage();
        }

        /// <summary>
        /// 加载必应图片。
        /// </summary>
        private void LoadBingImage()
        {
            // 读取最近一次使用的必应图片文件。
            string fileName = UserConfig.Instance.BingImage.RecentImage;

            // 如果图片文件存在，则暂时使用，否则暂不使用。
            if (File.Exists(fileName))
            {
                Background = new ImageBrush(LoadImageFile(fileName))
                {
                    Stretch = TempBackgroundImageBrush.Stretch,
                };
            }
            else
            {
                fileName = String.Empty;
            }

            // 准备在异步下载完新的必应图片后，将新的图片替代旧的显示。
            Action<string> loadNewBingImage = newFileName =>
            {
                if (newFileName == null || newFileName == fileName || !File.Exists(newFileName))
                {
                    return;
                }
                TempBackgroundImageBrush.ImageSource = LoadImageFile(newFileName);
                UserConfig.Instance.BingImage.RecentImage = newFileName;
                Settings.Default.Save();
                FadeOutStoryboard.Begin();
            };

            // 异步下载必应图片。
            Task task = new Task(() =>
            {
                string newFileName;
                try
                {
                    newFileName = BingImage.GetBingImageFile();
                }
                catch (WebException)
                {
                    Dispatcher.BeginInvoke(new Action(() => ReportErrorStoryboard.Begin()));
                    return;
                }
                Dispatcher.BeginInvoke(loadNewBingImage, newFileName);
            });
            task.Start();
        }

        /// <summary>
        /// 当新图片显示完成后，将临时图层删除。
        /// </summary>
        private void OnFadeOutStoryboardCompleted(object sender, EventArgs e)
        {
            Background = TempBackgroundBorder.Background;
            Content = null;
        }

        /// <summary>
        /// 获取用于显示新图片的故事板。
        /// </summary>
        private Storyboard FadeOutStoryboard
        {
            get { return (Storyboard)FindResource("FadeOutTempBackgroundStoryboard"); }
        }

        /// <summary>
        /// 获取用于显示网络错误的故事板。
        /// </summary>
        private Storyboard ReportErrorStoryboard
        {
            get { return (Storyboard)FindResource("ReportErrorStoryboard"); }
        }

        /// <summary>
        /// 从本地加载一个文件到内存中形成 <see cref="BitmapImage"/> 对象。
        /// </summary>
        /// <param name="fileName">文件路径。</param>
        /// <returns><see cref="BitmapImage"/> 对象。</returns>
        private static BitmapImage LoadImageFile(string fileName)
        {
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            using (FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                byte[] bytes = new BinaryReader(fileStream).ReadBytes((int)fileStream.Length);
                MemoryStream stream = new MemoryStream(bytes);
                fileStream.CopyTo(stream);
                bitmap.StreamSource = stream;
            }
            bitmap.CacheOption = BitmapCacheOption.None;
            bitmap.EndInit();
            return bitmap;
        }
    }

    internal static class BingImage
    {
        internal static string GetBingImageFile()
        {
            // 获取必应主页。
            WebClient client = new WebClient();
            string html = client.DownloadString(UserConfig.Instance.BingImage.Url);

            // 获取主页背景图片下载地址。
            Match imgurlMatch = Regex.Match(html, UserConfig.Instance.BingImage.ImageUrlPattern);
            if (!imgurlMatch.Success)
            {
                return null;
            }
            string imgurl = imgurlMatch.Value;

            // 获取图片名称。
            Match imgnameMatch = Regex.Match(imgurl, UserConfig.Instance.BingImage.ImageFileNamePattern);
            Match imgextMatch = Regex.Match(imgurl, "\\.\\w*$");
            if (!imgnameMatch.Success || !imgextMatch.Success)
            {
                return null;
            }
            string imgname = imgnameMatch.Value;
            string imgext = imgextMatch.Value;

            // 生成本地路径。
            string imageFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures),
                UserConfig.Instance.BingImage.Folder);
            if (!Directory.Exists(imageFolder))
            {
                Directory.CreateDirectory(imageFolder);
            }
            string imageFilePath = Path.Combine(imageFolder, imgname + imgext);

            // 下载图片。
            if (File.Exists(imageFilePath))
            {
                return imageFilePath;
            }
            client.DownloadFile(imgurl, imageFilePath);
            return imageFilePath;
        }
    }
}
