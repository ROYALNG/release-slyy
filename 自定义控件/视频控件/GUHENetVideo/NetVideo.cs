using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace GHIBMS.NetVideo
{
    public partial class NetVideo : UserControl
    {
        #region 属性
        private int matrixID = 1;
        public int MatrixID
        {
            get { return matrixID; }
            set { matrixID = value; }
        }
        private int camID = 1;
        public int CamID
        {
            get { return camID; }
            set
            {
                camID = value;
            }
        }
        private int userID = -1;
        public int UserID
        {
            get { return userID; }
            set { userID = value; }
        }
        private int channel = 0;
        public int Channel
        {
            get { return channel; }
            set { channel = value; }
        }
        #endregion
        public NetVideo()
        {
            InitializeComponent();

          
        }
    }
}
