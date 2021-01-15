using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnityITellYou.Dtos.Nav
{
    public class DownloadNavDto
    {
        /// <summary>
        /// 显示标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 对应的Json文件
        /// </summary>
        public string File { get; set; }
    }
}
