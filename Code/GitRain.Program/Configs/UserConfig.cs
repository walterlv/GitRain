using System;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using Cvte.GitRain.Data;

namespace Cvte.GitRain.Configs
{
    public class UserConfig
    {
        public static UserConfig Instance = new UserConfig();

        public GitRepoEntry RecentRepo { get; private set; }
        public BingImageEntry BingImage { get; private set; }

        private readonly string _userConfigDirectory;
        private readonly string _localFileName;

        private UserConfig()
        {
            _userConfigDirectory = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "GitRain", "User");
            _localFileName = Path.Combine(_userConfigDirectory, "Config.xml");
            Load();
        }

        private void Load()
        {
            if (!Directory.Exists(_userConfigDirectory))
            {
                Directory.CreateDirectory(_userConfigDirectory);
            }
            if (!File.Exists(_localFileName))
            {
                LoadDefault();
                return;
            }
            using (FileStream stream = new FileStream(_localFileName,
                FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                try
                {
                    var document = XDocument.Load(stream);
                    XElement root = document.Root;
                    LoadApp(root);
                    LoadUser(root);
                    LoadUI(root);
                }
                catch (XmlException)
                {
                    LoadDefault();
                }
            }
        }

        public void Save()
        {
            if (!Directory.Exists(_userConfigDirectory))
            {
                Directory.CreateDirectory(_userConfigDirectory);
            }
            using (FileStream stream = new FileStream(_localFileName,
                FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                XDocument document;
                try
                {
                    document = XDocument.Load(stream);
                }
                catch (XmlException)
                {
                    document = new XDocument(new XElement("configuration"));
                }
                XElement root = document.Root;
                SaveApp(root);
                SaveUser(root);
                SaveUI(root);
                document.Save(stream);
            }
        }

        private void LoadDefault()
        {
            BingImage = new BingImageEntry();
        }

        private void LoadApp(XElement root)
        {

        }

        private void LoadUser(XElement root)
        {

        }

        private void LoadUI(XElement root)
        {
            XElement imageElement = root.Element("UI").Element("BackgroundImage").Element("BingImage");
            BingImage = new BingImageEntry
            {
                Url = imageElement.Attribute("Url").Value,
                Folder = imageElement.Attribute("Folder").Value,
                RecentImage = imageElement.Attribute("RecentImage").Value,
                ImageUrlPattern = imageElement.Attribute("ImageUrlPattern").Value,
                ImageFileNamePattern = imageElement.Attribute("ImageFileNamePattern").Value
            };
        }

        private void SaveApp(XElement root)
        {

        }

        private void SaveUser(XElement root)
        {

        }

        private void SaveUI(XElement root)
        {
            XElement imageElement = GetElement(true, root, "UI", "BackgroundImage", "BingImage");
            imageElement.SetAttributeValue("Url", BingImage.Url);
            imageElement.SetAttributeValue("Folder", BingImage.Folder);
            imageElement.SetAttributeValue("RecentImage", BingImage.RecentImage);
            imageElement.SetAttributeValue("ImageUrlPattern", BingImage.ImageUrlPattern);
            imageElement.SetAttributeValue("ImageFileNamePattern", BingImage.ImageFileNamePattern);
        }

        private XElement GetElement(bool createIfNotExists, [NotNull] XElement root, params string[] path)
        {
            if (root == null) throw new ArgumentNullException("root");
            XElement lastElement = root;
            foreach (string name in path)
            {
                XElement element = lastElement.Element(name);
                if (element != null)
                {
                    lastElement = element;
                    continue;
                }
                if (!createIfNotExists)
                {
                    return null;
                }
                lastElement.Add(new XElement(name));
                lastElement = lastElement.Element(name);
            }
            return lastElement;
        }
    }
}
