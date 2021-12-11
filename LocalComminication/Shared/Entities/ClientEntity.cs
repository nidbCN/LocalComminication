namespace LocalComminication.Server.Entities
{
#nullable disable
    public class ClientEntity
    {
        /// <summary>
        /// 连接代码
        /// </summary>
        public ushort Code { get; set; }
        
        /// <summary>
        /// IP地址
        /// </summary>
        public string Address { get; set; }
        
        /// <summary>
        /// 端口
        /// </summary>
        public ushort Port { get; set; }
        
        /// <summary>
        /// 备注
        /// </summary>
        public string Note { get; set; }
    }
#nullable restore
}
