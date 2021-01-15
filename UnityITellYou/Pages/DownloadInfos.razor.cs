using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using UnityITellYou.Dtos.Download;
using UnityITellYou.Services;

namespace UnityITellYou.Pages
{
    public class DownloadInfosModel : ComponentBase
    {
        [Parameter]
        public string Title { get; set; }

        [Parameter]
        public string FileName { get; set; }

        [Inject]
        public IDataService m_DataService { get; set; }


        public bool Loading { get; set; }

        public IEnumerable<DownloadInfoDto> DownloadInfos { get; set; }

        protected override async Task OnInitializedAsync()
        {
            
        }

        protected override async Task OnParametersSetAsync()
        {
            Loading = true;
            DownloadInfos = await m_DataService.GetDownloadInfosAsync(FileName);
            Loading = false;
        }
    }
}
