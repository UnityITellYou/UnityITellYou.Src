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

        /// <summary>
        /// 页面组件之间的导航跳转用这个
        /// </summary>
        public string Id { get; set; }
    }
}
