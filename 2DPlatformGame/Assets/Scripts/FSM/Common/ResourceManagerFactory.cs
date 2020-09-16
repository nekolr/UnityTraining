using System.Collections.Generic;

namespace FSM.Common
{
    /// <summary>
    /// 资源管理器工厂类
    /// </summary>
    public static class ResourceManagerFactory
    {
        /// <summary>
        /// 缓存
        /// </summary>
        private static readonly Dictionary<string, ResourceManager> CacheResourceManagers;

        static ResourceManagerFactory()
        {
            CacheResourceManagers = new Dictionary<string, ResourceManager>();
        }

        /// <summary>
        /// 通过配置文件获取配置信息
        /// </summary>
        /// <param name="filename">配置文件名称</param>
        /// <returns>配置映射信息</returns>
        public static Dictionary<string, Dictionary<string, string>> GetConfigMapper(string filename)
        {
            if (!CacheResourceManagers.ContainsKey(filename))
            {
                ResourceManager resourceManager = new ResourceManager();
                resourceManager.LoadConfig(filename);
                CacheResourceManagers.Add(filename, resourceManager);
            }

            return CacheResourceManagers[filename].ConfigMapper;
        }
    }
}