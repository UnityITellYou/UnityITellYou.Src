using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Io;
using GetUnityDownloadInfos.Dtos;
using System.Linq;
using Nekonya;
using AngleSharp.Html.Dom;
using System.IO;
using System.Text.Json;
using System.Text;

namespace GetUnityDownloadInfos
{
    class Program
    {

        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var handler = new HttpClientHandler
            {
                Proxy = new WebProxy("127.0.0.1", 1080),
                UseProxy = true
            };

            var angleSharpConfig = Configuration.Default
                .WithRequesters(handler)
                .WithDefaultLoader();

            var context = BrowsingContext.New(angleSharpConfig);

            var address = "https://unity3d.com/get-unity/download/archive";

            var document = await context.OpenAsync(address);

            var archives= document.GetElementsByClassName("version archive");
            Console.WriteLine("archives 长度: " + archives.Length);

            var rootDir = Directory.GetCurrentDirectory();
            List<DownloadNavDto> DownloadNavDtoList = new List<DownloadNavDto>();

            foreach (var archive in archives)
            {
                if(TryGetNavInfoById(archive.Id, out var nav))
                {
                    DownloadNavDtoList.Add(nav);
                    var details = GetDownloadDetail(archive);

                    //写出文件
                    
                    var download_data_dir = Path.Combine(rootDir, "Result", "download-data");
                    var json_path = Path.Combine(download_data_dir, nav.File);

                    var json_str = JsonSerializer.Serialize(details.Select(d => d as DownloadInfoDto).ToList());

                    //检查目录
                    if (!Directory.Exists(download_data_dir))
                    {
                        Directory.CreateDirectory(download_data_dir);
                    }

                    if (File.Exists(json_path))
                        File.Delete(json_path);

                    File.WriteAllText(json_path, json_str, Encoding.UTF8);
                }
            }


            //最后输出nav的数组
            var nav_json_str = JsonSerializer.Serialize(DownloadNavDtoList);
            var nav_dir = Path.Combine(rootDir, "Result", "nav");
            var nav_path = Path.Combine(nav_dir, "nav.json");
            //检查目录
            if (!Directory.Exists(nav_dir))
                Directory.CreateDirectory(nav_dir);
            if (File.Exists(nav_path))
                File.Delete(nav_path);

            //写出
            File.WriteAllText(nav_path, nav_json_str, Encoding.UTF8);


            Console.WriteLine("完了");
            Console.ReadLine();
        }


        private static bool TryGetNavInfoById(string id, out DownloadNavDto nav)
        {
            switch (id)
            {
                default:
                    nav = null;
                    return false;
                case "version-2021":
                    nav = new DownloadNavDto { Title = "Unity 2021.x", File = "2021.x.json" };
                    return true;
                case "version-2020":
                    nav = new DownloadNavDto { Title = "Unity 2020.x", File = "2020.x.json" };
                    return true;
                case "version-2019":
                    nav = new DownloadNavDto { Title = "Unity 2019.x", File = "2019.x.json" };
                    return true;
                case "version-2018":
                    nav = new DownloadNavDto { Title = "Unity 2018.x", File = "2018.x.json" };
                    return true;
                case "version-2017":
                    nav = new DownloadNavDto { Title = "Unity 2017.x", File = "2017.x.json" };
                    return true;
                case "version-5":
                    nav = new DownloadNavDto { Title = "Unity 5.x", File = "5.x.json" };
                    return true;
                case "version-4":
                    nav = new DownloadNavDto { Title = "Unity 4.x", File = "4.x.json" };
                    return true;
            }


        }

        private static IEnumerable<DownloadDetailDto> GetDownloadDetail(IElement archive)
        {
            var result = new List<DownloadDetailDto>();

            var archive_items = archive.GetElementsByClassName("g12 pt0 pb0");

            foreach(var item in archive_items)
            {
                var detail = new DownloadDetailDto();
                //获取名称
                detail.Title = item.GetElementsByTagName("h4")?.FirstOrDefault()?.InnerHtml;
                if (detail.Title.IsNullOrEmpty())
                    continue;

                //获取日期
                detail.Date = item.GetElementsByClassName("mb0 cl")?.FirstOrDefault()?.InnerHtml;

                //获取Hub Uri
                var hubUriAElement = item.GetElementsByClassName("btn bg-gr left fw500 unityhub")?.FirstOrDefault();
                if(hubUriAElement != null)
                {
                    var hubUriHtmlElement = hubUriAElement as IHtmlAnchorElement;
                    detail.UnityHubUri = hubUriHtmlElement.Href;
                }

                //种子文件
                //Todo 以后再说吧


                result.Add(detail);
            }

            return result;
        }

    }
}
