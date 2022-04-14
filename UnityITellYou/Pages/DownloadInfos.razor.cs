using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using UnityITellYou.Dtos.Download;
using UnityITellYou.Services;

namespace UnityITellYou.Pages;

public class DownloadInfosModel : ComponentBase
{
    [Parameter]
    public string Id { get; set; } = null!;
    public string? Title { get; set; }

    public string? FileName { get; set; }

    [Inject]
    public IDataService m_DataService { get; set; } = null!;


    public bool Loading { get; set; }

    public IEnumerable<DownloadInfoDto>? DownloadInfos { get; set; }

    //protected override async Task OnInitializedAsync()
    //{
        
    //}

    protected override async Task OnParametersSetAsync()
    {
        Loading = true;
        var nav = m_DataService.GetDownloadNav().FirstOrDefault(dto => dto.Id == this.Id);
        if(nav is not null)
        {
            Title = nav.Title;
            FileName = nav.File;

            DownloadInfos = await m_DataService.GetDownloadInfosAsync(FileName);
        }

        Loading = false;
    }
}
