using System;

namespace MshotDigitsCamControl
{
    [Serializable]
    public class MshotParaInfo
    {
        public MshotParaInfo()
        {
            DeviceIndex = -1;
            DeviceName = "";
            AEFlag = true;
            AEModeIndex = -1;
            AEModeName = "";
            AETimeCount = 0;
            GrainCount = -1;
            FlinkIndex = -1;
            FlinkName = "";
            ROIPxIndex = -1;
            ROIPxName = "";
        }
        /// <summary>
        /// 设备索引
        /// </summary>
        public int DeviceIndex { get; set; }
        /// <summary>
        /// 设备名
        /// </summary>
        public string DeviceName { get; set; }
        /// <summary>
        /// 启用自动曝光
        /// </summary>
        public bool AEFlag { get; set; }
        /// <summary>
        /// 自动曝光模式索引
        /// </summary>
        public int AEModeIndex { get; set; }
        /// <summary>
        /// 自动曝光模式名
        /// </summary>
        public string AEModeName { get; set; }
        /// <summary>
        /// 自动曝光时间
        /// </summary>
        public int AETimeCount { get; set; }
        /// <summary>
        /// 模拟增益调节
        /// </summary>
        public int GrainCount { get; set; }
        /// <summary>
        /// 抗频闪设置
        /// </summary>
        public int FlinkIndex { get; set; }
        /// <summary>
        /// 抗频闪名称
        /// </summary>
        public string FlinkName { get; set; }
        /// <summary>
        /// 分辨率索引
        /// </summary>
        public int ROIPxIndex { get; set; }
        /// <summary>
        /// 分辨率
        /// </summary>
        public string ROIPxName { get; set; }
    }
}
