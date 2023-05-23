using DHNetSDK;
using System;

namespace GHIBMS.VideoPlayback
{
    [Serializable]
    public class DvrFindData
    {
        /// <summary>
        /// 文件名
        ///     char sFileName[100];
        /// </summary>
        private string fileName = "";
        public string FileName
        {
            get { return fileName; }
            set { fileName = value; }

        }
        /// <summary>
        /// 文件的开始时间
        /// </summary>
        private DateTime startTime = DateTime.Now;
        public DateTime StartTime
        {
            get { return startTime; }
            set { startTime = value; }
        }
        /// <summary>
        /// 文件的结束时间
        /// </summary>
        private DateTime stopTime;
        public DateTime StopTime
        {
            get { return stopTime; }
            set { stopTime = value; }
        }
        /// <summary>
        /// 文件的大小
        /// </summary>
        private string fileSize;
        public string FileSize
        {
            get { return fileSize; }
            set { fileSize = value; }
        }

        /// <summary>
        /// 文件是否被锁定，9000设备支持，1－此文件已经被锁定；0－正常的文件
        /// </summary>
        private byte locked;
        public byte Locked
        {
            get { return locked; }
            set { locked = value; }
        }
        /// <summary>
        /// 文件状态 0 未播放 1 正在播放 2 已经播放
        /// </summary>
        private int playState = 0;
        public int PlayState
        {
            get { return playState; }
            set { playState = value; }
        }
        /// <summary>
        /// 大华 录像文件
        /// </summary>
        private NET_RECORDFILE_INFO recordFile;
        public NET_RECORDFILE_INFO RecordFile
        {
            get { return recordFile; }
            set { recordFile = value; }
        }


        public void Clone(DvrFindData data)
        {
            this.fileName = data.fileName;
            this.startTime = data.startTime;
            this.stopTime = data.stopTime;
            this.fileSize = data.fileSize;
            this.locked = data.locked;
        }
    }
}
