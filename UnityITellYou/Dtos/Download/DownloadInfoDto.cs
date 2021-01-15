using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace UnityITellYou.Dtos.Download
{
    public class DownloadInfoDto
    {
        /// <summary>
        /// 显示大标题
        /// </summary>
        [JsonPropertyName("title")]
        public string Title { get; set; }

        /// <summary>
        /// 日期
        /// </summary>
        [JsonPropertyName("date")]
        public string Date { get; set; }

        [JsonPropertyName("hubUri")]
        public string UnityHubUri { get; set; }

        [JsonPropertyName("tag")]
        public string GithubTag { get; set; }
    }
}
