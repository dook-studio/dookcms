using System;
using System.Collections;
using System.Text;
using System.IO;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace Common
{
    public class App
    {

        //public static void Main(string[] args)
        //{

        //    //初始化CSS解析器

        //    CssDocument doc = new CssDocument();

        //    //加载现有CSS文件

        //    doc.Load(Directory.GetCurrentDirectory() + "/test.css");

        //    //修改CSS

        //    doc["body"].Attributes["font-size"] = "12px";

        //    //保存CSS文件

        //    doc.Save(Directory.GetCurrentDirectory() + "/a.css");

        //    Console.Read();

        //}

    }

    public class CssParse
    {
        private string m_source;
        private int m_idx;

        public static bool IsWhiteSpace(char ch)
        {
            return ("\t\n\r ".IndexOf(ch) != -1);
        }
        
        public void EatWhiteSpace()
        {
            while (!Eof())
            {
                if (!IsWhiteSpace(GetCurrentChar()))
                    return;
                m_idx++;
            }
        }



        public bool Eof()
        {
            return (m_idx >= m_source.Length);
        }

        public string ParseElementName()
        {
            StringBuilder element = new StringBuilder();
            EatWhiteSpace();
            while (!Eof())
            {
                if (GetCurrentChar() == '{')
                {
                    m_idx++;
                    break;
                }

                element.Append(GetCurrentChar());
                m_idx++;
            }

            EatWhiteSpace();
            return element.ToString().Trim();
        }
        

        public string ParseAttributeName()
        {
            StringBuilder attribute = new StringBuilder();
            EatWhiteSpace();
            while (!Eof())
            {
                if (GetCurrentChar() == ':')
                {
                    m_idx++;
                    break;
                }

                attribute.Append(GetCurrentChar());
                m_idx++;
            }
            
            EatWhiteSpace();
            return attribute.ToString().Trim();
        }



        public string ParseAttributeValue()
        {
            StringBuilder attribute = new StringBuilder();
            EatWhiteSpace();
            while (!Eof())
            {
                if (GetCurrentChar() == ';')
                {
                    m_idx++;
                    break;
                }
                attribute.Append(GetCurrentChar());
                m_idx++;
            }
            EatWhiteSpace();
            return attribute.ToString().Trim();
        }



        public char GetCurrentChar()
        {
            return GetCurrentChar(0);
        }



        public char GetCurrentChar(int peek)
        {
            if ((m_idx + peek) < m_source.Length)
                return m_source[m_idx + peek];
            else
                return (char)0;
        }



        public char AdvanceCurrentChar()
        {
            return m_source[m_idx++];
        }



        public void Advance()
        {
            m_idx++;
        }



        public string Source
        {
            get
            {
                return m_source;
            }

            set
            {
                m_source = value;
            }
        }



        public ArrayList Parse()
        {
            ArrayList elements = new ArrayList();
            while (!Eof())
            {
                string elementName = ParseElementName();
                if (elementName == null)
                    break;
                CssElement element = new CssElement(elementName);
                string name = ParseAttributeName();
                string value = ParseAttributeValue();
                while (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(value))
                {
                    element.Add(name, value);
                    EatWhiteSpace();
                    if (GetCurrentChar() == '}')
                    {
                        m_idx++;
                        break;
                    }
                    
                    name = ParseAttributeName();
                    value = ParseAttributeValue();
                }
                elements.Add(element);
            }            
            return elements;
        }
    }

    public class CssDocument
    {
        private string _Text;
        public string Text
        {
            get
            {
                return _Text;
            }
            set
            {
                _Text = value;
            }
        }      
  
        private ArrayList _Elements;
        public ArrayList Elements
        {
            get
            {
                return _Elements;
            }
            set
            {
                _Elements = value;
            }
        }
        
        public CssElement this[string name]
        {
            get
            {
                for (int i = 0; i < Elements.Count; i++)
                {
                    if (((CssElement)Elements[i]).Name.Equals(name))
                        return (CssElement)Elements[i];
                }
                return null;
            }
        }
        

        private string _File;
        public string File
        {
            get
            {
                return _File;
            }
            set
            {
                _File = value;
            }
        }

        public CssDocument()
        {
        }        
        public void Load(string file)
        {
            using (StreamReader sr = new StreamReader(file))
            {
                Text = sr.ReadToEnd();
                sr.Close();
            }

            CssParse parse = new CssParse();
            parse.Source = Regex.Replace(Text, @"/\*.*?\*/", "", RegexOptions.Compiled);
            Elements = parse.Parse();
        }
        
        public void Add(CssElement element)
        {
            Elements.Add(element);
        }
        
        public void Save()
        {
            Save(this.File);
        }
        
        public void Save(string file)
        {
            using (StreamWriter sw = new StreamWriter(file, false))
            {
                for (int i = 0; i < Elements.Count-1; i++)
                {
                    CssElement element = (CssElement)Elements[i];
                    sw.WriteLine(element.Name + " {");
                    foreach (string name in element.Attributes.AllKeys)
                    {
                        sw.WriteLine("\t{0}:{1};", name, element.Attributes[name]);
                    }
                    sw.WriteLine("}");
                }
                sw.Flush();
                sw.Close();
            }
        }
    }



    public class CssElement
    {

        private string _Name;
        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
            }
        }

        private NameValueCollection _Attributes;
        public NameValueCollection Attributes
        {
            get
            {
                return _Attributes;
            }
            set
            {
                _Attributes = value;
            }
        }
        public CssElement(string name)
        {
            this.Name = name;
            Attributes = new NameValueCollection();
        }
        public void Add(string attribute, string value)
        {
            Attributes[attribute] = value;
        }
    }
}