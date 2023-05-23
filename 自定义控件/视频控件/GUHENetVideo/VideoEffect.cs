namespace GHIBMS.NetVideo
{
    public class VideoEffect
    {
        private uint _BrightValue = 0;
        private uint _ContrastValue = 0;
        private uint _SaturationValue = 0;
        private uint _HueValue = 0;
        //亮度
        public uint BrightValue
        {
            get { return _BrightValue; }
            set { _BrightValue = value; }
        }
        //对比度
        public uint ContrastValue
        {
            get { return _ContrastValue; }
            set { _ContrastValue = value; }
        }
        //饱和度
        public uint SaturationValue
        {
            get { return _SaturationValue; }
            set { _SaturationValue = value; }
        }
        //色度
        public uint HueValue
        {
            get { return _HueValue; }
            set { _HueValue = value; }
        }
    }
}
