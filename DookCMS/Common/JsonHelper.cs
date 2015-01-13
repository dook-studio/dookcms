using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using System.Data;
using System.Reflection;

namespace Common
{
    public class JsonHelper
    {

        //将datatable数据转换成JSON数据
        public static string DataTableToJson(string jsonName, DataTable dt)
        {
            StringBuilder Json = new StringBuilder();
            Json.Append("[");
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Json.Append("{");
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        Json.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\"" + ":\"" + dt.Rows[i][j].ToString() + "\"");
                        if (j < dt.Columns.Count - 1)
                        {
                            Json.Append(",");
                        }
                    }
                    Json.Append("}");
                    if (i < dt.Rows.Count - 1)
                    {
                        Json.Append(",");
                    }
                }
            }
            Json.Append("]");
            return Json.ToString();
        }

        //列表数据转换到json数据
        public static string ObjectToJson<T>(string jsonName, IList<T> IL)
        {
            StringBuilder Json = new StringBuilder();
            Json.Append("[");
            if (IL.Count > 0)
            {
                for (int i = 0; i < IL.Count; i++)
                {
                    T obj = Activator.CreateInstance<T>();
                    Type type = obj.GetType();
                    PropertyInfo[] pis = type.GetProperties();
                    Json.Append("{");
                    for (int j = 0; j < pis.Length; j++)
                    {
                        Json.Append("\"" + pis[j].Name.ToString() + "\"" + ":\"" + pis[j].GetValue(IL[i], null) + "\"");
                        if (j < pis.Length - 1)
                        {
                            Json.Append(",");
                        }
                    }
                    Json.Append("}");
                    if (i < IL.Count - 1)
                    {
                        Json.Append(",");
                    }
                }
            }
            Json.Append("]");
            return Json.ToString();
        }

        #region 全局变量

        private static JSONObject _json = new JSONObject();//寄存器 
        private static readonly string _SEMICOLON = "@semicolon";//分号转义符 
        private static readonly string _COMMA = "@comma"; //逗号转义符 

        #endregion

        #region 字符串转义
        /// <summary> 
        /// 字符串转义,将双引号内的:和,分别转成_SEMICOLON和_COMMA 
        /// </summary> 
        /// <param name="text"></param> 
        /// <returns></returns> 
        private static string StrEncode(string text)
        {
            MatchCollection matches = Regex.Matches(text, "\\\"[^\\\"]+\\\"");
            foreach (Match match in matches)
            {
                text = text.Replace(match.Value, match.Value.Replace(":", _SEMICOLON).Replace(",", _COMMA));
            }
            return text;
        }

        /// <summary> 
        /// 字符串转义,将_SEMICOLON和_COMMA分别转成:和, 
        /// </summary> 
        /// <param name="text"></param> 
        /// <returns></returns> 
        private static string StrDecode(string text)
        {
            return text.Replace(_SEMICOLON, ":").Replace(_COMMA, ",");
        }

        #endregion

        #region JSON最小单元解析

        /// <summary> 
        /// 最小对象转为JSONObject 
        /// </summary> 
        /// <param name="text"></param> 
        /// <returns></returns> 
        private static JSONObject DeserializeSingletonObject(string text)
        {
            JSONObject jsonObject = new JSONObject();

            MatchCollection matches = Regex.Matches(text, "(\\\"(?<key>[^\\\"]+)\\\":\\\"(?<value>[^,\\\"]+)\\\")|(\\\"(?<key>[^\\\"]+)\\\":(?<value>[^,\\\"\\}]+))");
            foreach (Match match in matches)
            {
                string value = match.Groups["value"].Value;
                jsonObject.Add(match.Groups["key"].Value, _json.ContainsKey(value) ? _json[value] : StrDecode(value));
            }

            return jsonObject;
        }

        /// <summary> 
        /// 最小数组转为JSONArray 
        /// </summary> 
        /// <param name="text"></param> 
        /// <returns></returns> 
        private static JSONArray DeserializeSingletonArray(string text)
        {
            JSONArray jsonArray = new JSONArray();

            MatchCollection matches = Regex.Matches(text, "(\\\"(?<value>[^,\\\"]+)\")|(?<value>[^,\\[\\]]+)");
            foreach (Match match in matches)
            {
                string value = match.Groups["value"].Value;
                jsonArray.Add(_json.ContainsKey(value) ? _json[value] : StrDecode(value));
            }

            return jsonArray;
        }

        /// <summary> 
        /// 反序列化 
        /// </summary> 
        /// <param name="text"></param> 
        /// <returns></returns> 
        private static string Deserialize(string text)
        {
            text = StrEncode(text);//转义;和, 

            int count = 0;
            string key = string.Empty;
            string pattern = "(\\{[^\\[\\]\\{\\}]+\\})|(\\[[^\\[\\]\\{\\}]+\\])";

            while (Regex.IsMatch(text, pattern))
            {
                MatchCollection matches = Regex.Matches(text, pattern);
                foreach (Match match in matches)
                {
                    key = "___key" + count + "___";

                    if (match.Value.Substring(0, 1) == "{")
                        _json.Add(key, DeserializeSingletonObject(match.Value));
                    else
                        _json.Add(key, DeserializeSingletonArray(match.Value));

                    text = text.Replace(match.Value, key);

                    count++;
                }
            }
            return text;
        }

        #endregion

        #region 公共接口

        /// <summary> 
        /// 序列化JSONObject对象 
        /// </summary> 
        /// <param name="text"></param> 
        /// <returns></returns> 
        public static JSONObject DeserializeObject(string text)
        {
            _json = new JSONObject();
            return _json[Deserialize(text)] as JSONObject;
        }

        /// <summary> 
        /// 序列化JSONArray对象 
        /// </summary> 
        /// <param name="text"></param> 
        /// <returns></returns> 
        public static JSONArray DeserializeArray(string text)
        {
            _json = new JSONObject();
            return _json[Deserialize(text)] as JSONArray;
        }

        /// <summary> 
        /// 反序列化JSONObject对象 
        /// </summary> 
        /// <param name="jsonObject"></param> 
        /// <returns></returns> 
        public static string SerializeObject(JSONObject jsonObject)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{");
            foreach (KeyValuePair<string, object> kvp in jsonObject)
            {
                if (kvp.Value is JSONObject)
                {
                    sb.Append(string.Format("\"{0}\":{1},", kvp.Key, SerializeObject((JSONObject)kvp.Value)));
                }
                else if (kvp.Value is JSONArray)
                {
                    sb.Append(string.Format("\"{0}\":{1},", kvp.Key, SerializeArray((JSONArray)kvp.Value)));
                }
                else if (kvp.Value is String)
                {
                    sb.Append(string.Format("\"{0}\":\"{1}\",", kvp.Key, kvp.Value));
                }
                else
                {
                    sb.Append(string.Format("\"{0}\":\"{1}\",", kvp.Key, ""));
                }
            }
            if (sb.Length > 1)
                sb.Remove(sb.Length - 1, 1);
            sb.Append("}");
            return sb.ToString();
        }

        /// <summary> 
        /// 反序列化JSONArray对象 
        /// </summary> 
        /// <param name="jsonArray"></param> 
        /// <returns></returns> 
        public static string SerializeArray(JSONArray jsonArray)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            for (int i = 0; i < jsonArray.Count; i++)
            {
                if (jsonArray[i] is JSONObject)
                {
                    sb.Append(string.Format("{0},", SerializeObject((JSONObject)jsonArray[i])));
                }
                else if (jsonArray[i] is JSONArray)
                {
                    sb.Append(string.Format("{0},", SerializeArray((JSONArray)jsonArray[i])));
                }
                else if (jsonArray[i] is String)
                {
                    sb.Append(string.Format("\"{0}\",", jsonArray[i]));
                }
                else
                {
                    sb.Append(string.Format("\"{0}\",", ""));
                }

            }
            if (sb.Length > 1)
                sb.Remove(sb.Length - 1, 1);
            sb.Append("]");
            return sb.ToString();
        }
        #endregion

        #region Method
        /// <summary>
        /// 类对像转换成json格式
        /// </summary> 
        /// <returns></returns>
        public static string ToJson(object t)
        {
            return JsonConvert.SerializeObject(t, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Include });
        }
        /// <summary>
        /// 类对像转换成json格式
        /// </summary>
        /// <param name="t"></param>
        /// <param name="HasNullIgnore">是否忽略NULL值</param>
        /// <returns></returns>
        public static string ToJson(object t, bool HasNullIgnore)
        {
            if (HasNullIgnore)
                return JsonConvert.SerializeObject(t, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            else
                return ToJson(t);
        }
        /// <summary>
        /// json格式转换
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strJson"></param>
        /// <returns></returns>
        public static T FromJson<T>(string strJson) where T : class
        {
            if (!string.IsNullOrEmpty(strJson))
                return JsonConvert.DeserializeObject<T>(strJson);
            return null;
        }



        #endregion
    }

    /// <summary> 
    /// 取出JSON对象类 
    /// </summary> 
    public class JSONObject : Dictionary<string, object>
    {
        public new void Add(string key, object value)
        {
            System.Type t = value.GetType();

            if (t.Name == "String")
            {
                value = JSONEncode.StrEncodeForDeserialize(value.ToString());
            }

            base.Add(key, value);
        }
        public override string ToString()
        {
            return JsonHelper.SerializeObject(this);
        }
        public static JSONObject FromObject(string json)
        {
            return JsonHelper.DeserializeObject(json);
        }
    }

    /// <summary> 
    /// 取出JSON数组类 
    /// </summary> 
    public class JSONArray : List<object>
    {
        public new void Add(object item)
        {
            System.Type t = item.GetType();

            if (t.Name == "String")
            {
                item = JSONEncode.StrEncodeForDeserialize(item.ToString());
            }

            base.Add(item);
        }
        public override string ToString()
        {
            return JsonHelper.SerializeArray(this);
        }
        public JSONArray FromObject(string json)
        {
            return JsonHelper.DeserializeArray(json);
        }
    }

    /// <summary> 
    /// 字符串转义,将"{"、"}"、""" 
    /// </summary> 
    public class JSONEncode
    {
        public static readonly string _LEFTBRACES = "@leftbraces";//"{"转义符 
        public static readonly string _RIGHTBRACES = "@rightbraces";//"}"转义符 
        public static readonly string _LEFTBRACKETS = "@leftbrackets";//"["转义符 
        public static readonly string _RIGHTBRACKETS = "@rightbrackets";//"]"转义符 
        public static readonly string _DOUBLEQUOTATIONMARKS = "@doubleQuotationMarks";//"""转义符 


        #region 字符串转义
        /// <summary> 
        /// 字符串转义,将"{"、"}"、""",分别转换_LEFTBRACES、_RIGHTBRACES、_DOUBLEQUOTATIONMARKS 
        /// </summary> 
        /// <param name="text"></param> 
        /// <returns></returns> 
        public static string StrEncodeForDeserialize(string text)
        {
            return text
            .Replace("{", _LEFTBRACES)
            .Replace("}", _RIGHTBRACES)
            .Replace("[", _LEFTBRACKETS)
            .Replace("]", _RIGHTBRACKETS)
            .Replace("\"", _DOUBLEQUOTATIONMARKS);
        }

        /// <summary> 
        /// 字符串转义,将_LEFTBRACES、_RIGHTBRACES、_DOUBLEQUOTATIONMARKS,分别转换"{"、"}"、""" 
        /// </summary> 
        /// <param name="text"></param> 
        /// <returns></returns> 
        public static string StrDecodeForDeserialize(string text)
        {
            return text.Replace(_LEFTBRACES, "{")
            .Replace(_RIGHTBRACES, "}")
            .Replace(_LEFTBRACKETS, "[")
            .Replace(_RIGHTBRACKETS, "]")
            .Replace(_DOUBLEQUOTATIONMARKS, "\"");
        }
        #endregion
    }
}