using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using UnityITellYou.Dtos.Download;
using UnityITellYou.Dtos.Nav;

namespace UnityITellYou.Services
{
    public class DataService : IDataService
    {
        private readonly HttpClient m_HttpClient;
        private readonly IConfiguration m_Configuration;

        private Dictionary<string, IEnumerable<DownloadInfoDto>> m_Dict_DownloadInfos = new Dictionary<string, IEnumerable<DownloadInfoDto>>();

        public DataService(HttpClient httpClient,
            IConfiguration configuration)
        {
            this.m_HttpClient = httpClient;
            this.m_Configuration = configuration;
        }


        /// <summary>
        /// 获取下载导航数据
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DownloadNavDto> GetDownloadNav()
        {
            return m_Configuration.GetSection("DownloadNav").Get<IEnumerable<DownloadNavDto>>();
        }


        public async Task<IEnumerable<DownloadInfoDto>> GetDownloadInfosAsync(string fileName)
        {
            if(m_Dict_DownloadInfos.TryGetValue(fileName, out var _infos))
            {
                return _infos;
            }

            //从网络获取
            var infos = await m_HttpClient.GetFromJsonAsync<IEnumerable<DownloadInfoDto>>($"download-data/{fileName}");
            m_Dict_DownloadInfos.Add(fileName, infos);
            return infos;
        }




    }
}
