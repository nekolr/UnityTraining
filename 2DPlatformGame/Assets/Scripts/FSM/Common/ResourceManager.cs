using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace FSM.Common
{
    /// <summary>
    /// 资源管理器
    /// </summary>
    public class ResourceManager
    {
        /// <summary>
        /// 配置分隔符
        /// </summary>
        private const char Separator = '>';

        /// <summary>
        /// 状态起始字符
        /// </summary>
        private const string StateStartCharacter = "[";

        /// <summary>
        /// 配置映射字典
        /// </summary>
        public Dictionary<string, Dictionary<string, string>> ConfigMapper { get; private set; }

        /// <summary>
        /// 加载配置
        /// </summary>
        public void LoadConfig(string filename)
        {
            // 读取配置文件
            string fileContent = ReadConfigFile(filename);
            // 解析配置
            this.ConfigMapper = ParseConfig(fileContent);
        }

        /// <summary>
        /// 解析配置
        /// </summary>
        /// <param name="fileContent">配置文件内容</param>
        /// <returns>配置字典</returns>
        private Dictionary<string, Dictionary<string, string>> ParseConfig(string fileContent)
        {
            var configMapper = new Dictionary<string, Dictionary<string, string>>();
            var lastLine = "";

            using (StringReader reader = new StringReader(fileContent))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    line = line.Trim();

                    if (!string.IsNullOrEmpty(line))
                    {
                        if (line.StartsWith(StateStartCharacter))
                        {
                            line = line.Substring(1, line.Length - 2);
                            lastLine = line;
                            configMapper.Add(line, new Dictionary<string, string>());
                        }
                        else
                        {
                            string[] value = line.Split(Separator);
                            configMapper[lastLine].Add(value[0], value[1]);
                        }
                    }
                }
            }

            return configMapper;
        }

        /// <summary>
        /// 读取配置文件
        /// </summary>
        /// <param name="filename">文件名称</param>
        /// <returns>配置文件内容</returns>
        private string ReadConfigFile(string filename)
        {
            var configFile = Resources.Load<TextAsset>(filename);
            return configFile.text;
        }
    }
}