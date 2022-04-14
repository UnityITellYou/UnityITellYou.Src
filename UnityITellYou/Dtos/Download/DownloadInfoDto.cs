using System.Text.Json.Serialization;

namespace UnityITellYou.Dtos.Download;

public class DownloadInfoDto
{
    /// <summary>
    /// 显示大标题
    /// </summary>
    [JsonPropertyName("title")]
    public string Title { get; set; } = default!;

    /// <summary>
    /// 日期
    /// </summary>
    [JsonPropertyName("date")]
    public string Date { get; set; } = default!;

    [JsonPropertyName("hubUri")]
    public string UnityHubUri { get; set; } = default!;

    [JsonPropertyName("tag")]
    public string GithubTag { get; set; } = default!;
}
