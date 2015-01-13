using System;

namespace Cvte.GitRain.Data
{
    public class BingImageEntry
    {
        public const string DefaultImageUrlPattern =
            "(?<=(g_img={.*?url:'))http://.*?\\.jpg(?=('.*}))|(?<=(<div\\sid=\"bgDiv\".*?style='.*?background-image:\\s*url\\(\"))(http://.*?\\.jpg)(?=(\"\\);.*?'.*?>))";

        public const string DefaultUrl = "http://cn.bing.com/";
        public const string DefaultFolder = "必应";
        public const string DefaultImageFileNamePattern = "(?<=/)\\w*(?=_.*?\\.jpg)";
        public const double DefaultOpacity = 0.1;
        public const double DefaultBlurRadius = 30;

        public string Url { get; set; }
        public string Folder { get; set; }
        public string RecentImage { get; set; }
        public string ImageUrlPattern { get; set; }
        public string ImageFileNamePattern { get; set; }
        public double Opacity { get; set; }
        public double BlurRadius { get; set; }

        public BingImageEntry()
        {
            Url = DefaultUrl;
            Folder = DefaultFolder;
            RecentImage = String.Empty;
            ImageUrlPattern = DefaultImageUrlPattern;
            ImageFileNamePattern = DefaultImageFileNamePattern;
            Opacity = DefaultOpacity;
            BlurRadius = DefaultBlurRadius;
        }

        //public BingImageEntry(string url, string folder, string recentImage, string imageUrlPattern,
        //    string imageFileNamePattern, double? opacity, double? blurRadius)
        //{
        //    Url = String.IsNullOrEmpty(url) ? DefaultUrl : url;
        //    Folder = String.IsNullOrEmpty(folder) ? DefaultFolder : folder;
        //    RecentImage = recentImage ?? String.Empty;
        //    ImageUrlPattern = String.IsNullOrEmpty(imageUrlPattern) ? DefaultImageUrlPattern : imageUrlPattern;
        //    ImageFileNamePattern = String.IsNullOrEmpty(imageFileNamePattern)
        //        ? DefaultImageFileNamePattern
        //        : imageFileNamePattern;
        //    Opacity = opacity ?? DefaultOpacity;
        //    BlurRadius = blurRadius ?? DefaultBlurRadius;
        //}
    }
}
