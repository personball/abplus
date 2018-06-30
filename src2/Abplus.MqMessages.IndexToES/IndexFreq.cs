namespace Abp
{
    /// <summary>
    /// 定义创建索引的频率
    /// </summary>
    public enum IndexFreq
    {
        /// <summary>
        /// 每天一个索引
        /// </summary>
        PerDay = 0,

        /// <summary>
        /// 每月一个索引
        /// </summary>
        PerMonth = 1,

        /// <summary>
        /// 每年一个索引
        /// </summary>
        PerYear = 2,

        /// <summary>
        /// 仅一个索引，索引名不加日期后缀
        /// </summary>
        None = 3
    }
}
