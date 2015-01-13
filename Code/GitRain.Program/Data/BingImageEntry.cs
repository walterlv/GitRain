using System;

namespace Cvte.GitRain.Data
{
    public class BingImageEntry : NotificationObject
    {
        public const string DefaultImageUrlPattern =
            "(?<=(g_img={.*?url:'))http://.*?\\.jpg(?=('.*}))|(?<=(<div\\sid=\"bgDiv\".*?style='.*?background-image:\\s*url\\(\"))(http://.*?\\.jpg)(?=(\"\\);.*?'.*?>))";

        public const string DefaultUrl = "http://cn.bing.com/";
        public const string DefaultFolder = "必应";
        public const string DefaultImageFileNamePattern = "(?<=/)\\w*(?=_.*?\\.jpg)";
        public const double DefaultOpacity = 0.1;
        public const double DefaultBlurRadius = 30;

        private string _url;
        private string _folder;
        private string _recentImage;
        private string _imageUrlPattern;
        private string _imageFileNamePattern;
        private double _opacity;
        private double _blurRadius;

        public string Url
        {
            get { return _url; }
            set { SetProperty(ref _url, value); }
        }

        public string Folder
        {
            get { return _folder; }
            set { SetProperty(ref _folder, value); }
        }

        public string RecentImage
        {
            get { return _recentImage; }
            set { SetProperty(ref _recentImage, value); }
        }

        public string ImageUrlPattern
        {
            get { return _imageUrlPattern; }
            set { SetProperty(ref _imageUrlPattern, value); }
        }

        public string ImageFileNamePattern
        {
            get { return _imageFileNamePattern; }
            set { SetProperty(ref _imageFileNamePattern, value); }
        }

        public double Opacity
        {
            get { return _opacity; }
            set { SetProperty(ref _opacity, value); }
        }

        public double BlurRadius
        {
            get { return _blurRadius; }
            set { SetProperty(ref _blurRadius, value); }
        }

        public BingImageEntry()
        {
            _url = DefaultUrl;
            _folder = DefaultFolder;
            _recentImage = String.Empty;
            _imageUrlPattern = DefaultImageUrlPattern;
            _imageFileNamePattern = DefaultImageFileNamePattern;
            _opacity = DefaultOpacity;
            _blurRadius = DefaultBlurRadius;
        }
    }
}
