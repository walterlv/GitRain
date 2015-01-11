using System;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Cvte.GitRain.IO
{
    [DebuggerDisplay("[{Type}] {Line}")]
    public class IniLine
    {
        private const string RevervedNameNode = "~node~";
        private const string RevervedNameKey = "~key~";
        private const string RevervedNameValue = "~value~";
        private const string RevervedNameComment = "~comment~";

        private const string IniEmptyLinePattern = "^\\s*";

        private const string IniCommentLinePattern = "^(\\s*[;#])(.*)";
        private const string IniCommentResultPattern = "(?<=(^\\s*[;#]\\s*))(.*)";
        private const string IniCommentFormatPattern = "$1~comment~";

        private const string IniNodeLinePattern = "^(\\s*\\[)(\\w*)(\\])\\s*$";
        private const string IniNodeResultPattern = "(?<=(\\s*\\[))(\\w*)(?=(\\])\\s*)";
        private const string IniNodeFormatResult = "$1~node~$3";

        private const string IniKeyValueLinePattern = "^(\\s*)([\\w]+)(\\s*=\\s*)(.*)";
        private const string IniKeyResultPattern = "(?<=(^(\\s*)))([\\w]+)(?=((\\s*=\\s*)(.*)))";
        private const string IniValueResultPattern = "(?<=(^(\\s*)([\\w]+)(\\s*=\\s*)))(.*)";
        private const string IniKeyValueFormatPattern = "$1~key~$3~value~";

        private readonly Match _match;
        private string _node;
        private string _key;
        private string _value;
        private string _comment;

        public string Line { get; private set; }
        public IniLineType Type { get; private set; }

        public string Node
        {
            get { return _node; }
            set
            {
                ThrowIfTypeNotValid(IniLineType.Node);
                _node = value;
                Line = _match.Result(IniNodeFormatResult)
                    .Replace(RevervedNameNode, _node);
            }
        }

        public string Key
        {
            get { return _key; }
            set
            {
                ThrowIfTypeNotValid(IniLineType.KeyValue);
                _key = value;
                Line = _match.Result(IniKeyValueFormatPattern)
                    .Replace(RevervedNameKey, _key)
                    .Replace(RevervedNameValue, _value);
            }
        }

        public string Value
        {
            get { return _value; }
            set
            {
                ThrowIfTypeNotValid(IniLineType.KeyValue);
                _value = value;
                Line = _match.Result(IniKeyValueFormatPattern)
                    .Replace(RevervedNameKey, _key)
                    .Replace(RevervedNameValue, _value);
            }
        }

        public string Comment
        {
            get { return _comment; }
            set
            {
                ThrowIfTypeNotValid(IniLineType.Comment);
                _comment = value;
                Line = _match.Result(IniCommentFormatPattern)
                    .Replace(RevervedNameComment, _key);
            }
        }

        public IniLine(string line)
        {
            Line = line;

            IniLineType type;
            _match = Match(line, out type);
            Type = type;

            switch (type)
            {
                case IniLineType.Comment:
                    _comment = Regex.Match(line, IniCommentResultPattern).Value.Trim();
                    break;
                case IniLineType.Node:
                    _node = Regex.Match(line, IniNodeResultPattern).Value.Trim();
                    break;
                case IniLineType.KeyValue:
                    _key = Regex.Match(line, IniKeyResultPattern).Value.Trim();
                    _value = Regex.Match(line, IniValueResultPattern).Value.Trim();
                    break;
            }
        }

        public IniLine(IniLineType type)
        {
            if (type != IniLineType.Empty)
            {
                throw new ArgumentException("此构造方法只能创建 Empty 类型的行。");
            }
            Line = String.Empty;
        }

        public IniLine(IniLineType type, string commentOrNode)
        {
            if (type != IniLineType.Comment || type != IniLineType.Node)
            {
                throw new ArgumentException("此构造方法只能创建 Comment 和 Node 类型的行。");
            }
            switch (type)
            {
                case IniLineType.Comment:
                    _comment = commentOrNode;
                    Line = String.Format("# {0}", _comment);
                    break;
                case IniLineType.Node:
                    _node = commentOrNode;
                    Line = String.Format("[{0}]", _comment);
                    break;
            }
        }

        public IniLine(IniLineType type, string key, string value)
        {
            if (type != IniLineType.KeyValue)
            {
                throw new ArgumentException("此构造方法只能创建 KeyValue 类型的行。");
            }
            _key = key;
            _value = value;
            Line = String.Format("{0} = {1}", _key, _value);
        }

        public override string ToString()
        {
            return Line;
        }

        private static Match Match(string line, out IniLineType type)
        {
            Match match = Regex.Match(line, IniEmptyLinePattern);
            if (match.Success)
            {
                type = IniLineType.Empty;
                return match;
            }
            match = Regex.Match(line, IniCommentLinePattern);
            if (match.Success)
            {
                type = IniLineType.Comment;
                return match;
            }
            match = Regex.Match(line, IniKeyValueLinePattern);
            if (match.Success)
            {
                type = IniLineType.KeyValue;
                return match;
            }
            match = Regex.Match(line, IniNodeLinePattern);
            if (match.Success)
            {
                type = IniLineType.Node;
                return match;
            }
            type = IniLineType.Invalid;
            return null;
        }

        [DebuggerStepThrough]
        private void ThrowIfTypeNotValid(IniLineType type)
        {
            if (Type != type)
            {
                Debug.WriteIf(false, type);
                throw new ArgumentException("ini 文件不支持修改行类型", "value");
            }
        }
    }
}
