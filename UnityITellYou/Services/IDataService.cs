using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityITellYou.Dtos.Download;
using UnityITellYou.Dtos.Nav;

namespace UnityITellYou.Services
{
    public interface IDataService
    {
        Task<IEnumerable<DownloadInfoDto>?> GetDownloadInfosAsync(string fileName);

        /// <summary>
        /// 获取导航栏数据
        /// </summary>
        /// <returns></returns>
        IEnumerable<DownloadNavDto> GetDownloadNav();
    }
}
