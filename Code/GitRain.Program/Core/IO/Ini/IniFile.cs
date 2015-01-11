using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Cvte.GitRain.IO
{
    public class IniFile
    {
        private readonly FileInfo _file;
        private readonly List<IniLine> _lines;

        public string FullName
        {
            get { return _file.FullName; }
        }

        public IniFile(string file)
        {
            _file = new FileInfo(file);
            _lines = _file.Exists
                ? File.ReadAllLines(_file.FullName).Select(x => new IniLine(x)).ToList()
                : new List<IniLine>();
        }

        public string this[string noteKey]
        {
            get
            {
                if (!noteKey.Contains('.'))
                {
                    throw new ArgumentException("错误的访问格式，应该为 Node.Key。");
                }
                string[] keyValue = noteKey.Split('.');
                Debug.Assert(keyValue.Length == 2);
                return Get(keyValue[0], keyValue[1]);
            }
            set
            {
                if (!noteKey.Contains('.'))
                {
                    throw new ArgumentException("错误的访问格式，应该为 Node.Key。");
                }
                string[] keyValue = noteKey.Split('.');
                Debug.Assert(keyValue.Length == 2);
                Set(keyValue[0], keyValue[1], value);
            }
        }

        public string Get(string node, string key)
        {
            IniKeyValueLineInfo info = FindLine(node, key);
            return info.Exists ? _lines[info.LineIndex].Value : null;
        }

        public void Set(string node, string key, string value)
        {
            IniKeyValueLineInfo info = FindLine(node, key);
            if (info.Exists)
            {
                _lines[info.LineIndex].Value = value;
            }
            else if (info.NodeLineIndex < 0)
            {
                _lines.Add(new IniLine(IniLineType.Node, node));
                _lines.Add(new IniLine(IniLineType.KeyValue, key, value));
            }
            else
            {
                _lines.Insert(info.NodeLastKeyValueLineIndex, new IniLine(IniLineType.KeyValue, key, value));
            }
        }

        [NotNull]
        private IniKeyValueLineInfo FindLine([NotNull] string node, [NotNull] string key)
        {
            if (node == null) throw new ArgumentNullException("node");
            if (key == null) throw new ArgumentNullException("key");
            int nodeIndex = _lines.FindIndex(x => x.Type == IniLineType.Node && x.Node == node);
            if (nodeIndex < 0 || nodeIndex == _lines.Count - 1)
            {
                return new IniKeyValueLineInfo(false);
            }

            int nextNodeIndex = _lines.FindIndex(nodeIndex + 1, x => x.Type == IniLineType.Node);
            if (nextNodeIndex < 0)
            {
                nextNodeIndex++;
            }
            int keyIndex = _lines.FindIndex(nodeIndex + 1, nextNodeIndex - nodeIndex - 1,
                x => x.Type == IniLineType.KeyValue && x.Key == key);
            int lastKeyIndex = _lines.FindLastIndex(nodeIndex + 1, nextNodeIndex - nodeIndex - 1,
                x => x.Type == IniLineType.KeyValue);
            return keyIndex < 0
                ? new IniKeyValueLineInfo(false, nodeIndex, lastKeyIndex < 0 ? nodeIndex : lastKeyIndex)
                : new IniKeyValueLineInfo(true, nodeIndex, keyIndex, lastKeyIndex);
        }

        public void Save()
        {
            if (_file.Exists)
            {
                // TODO 对于已存在的文件进行写入。
            }
            else
            {
                File.WriteAllLines(FullName, _lines.Select(x => x.ToString()));
            }
        }

        private class IniKeyValueLineInfo
        {
            public bool Exists { get; private set; }
            public int NodeLineIndex { get; private set; }
            public int LineIndex { get; private set; }
            public int NodeLastKeyValueLineIndex { get; private set; }

            public IniKeyValueLineInfo(bool exists)
            {
                if (exists)
                {
                    throw new ArgumentException("只有在找不到行信息时才使用此构造方法。");
                }
                Exists = false;
                NodeLineIndex = -1;
                LineIndex = -1;
                NodeLastKeyValueLineIndex = -1;
            }
            public IniKeyValueLineInfo(bool exists, int nodeLineIndex, int nodeLastKeyValueLineIndex)
            {
                if (exists)
                {
                    throw new ArgumentException("只有在找不到行信息时才使用此构造方法。");
                }
                Exists = false;
                NodeLineIndex = nodeLineIndex;
                LineIndex = -1;
                NodeLastKeyValueLineIndex = nodeLastKeyValueLineIndex;
            }
            public IniKeyValueLineInfo(bool exists, int nodeLineIndex, int lineIndex, int nodeLastKeyValueLineIndex)
            {
                Exists = exists;
                NodeLineIndex = nodeLineIndex;
                LineIndex = lineIndex;
                NodeLastKeyValueLineIndex = nodeLastKeyValueLineIndex;
            }
        }
    }
}
