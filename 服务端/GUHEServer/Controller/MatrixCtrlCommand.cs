using System;
using System.Collections.Generic;
using System.Text;
using GHIBMS.Common;

namespace GHIBMS.Server
{
    public class MatrixCtrlCommand
    {
        private ChannelInfo channell;
        public ChannelInfo Channel
        {
            set { channell = value; }
            get { return channell; }
        }
        private ControllerInfo controller;
        public ControllerInfo Conntroller
        {
            set { controller = value; }
            get { return controller; }
        }
        //发送命令二进制序列
        private string outBuffer;
        public string OutBuffer
        {
            set { outBuffer = value; }
            get { return outBuffer; }

        }
        private MatrixCommandEnum command;
        public MatrixCommandEnum Command
        {
            set { command = value; }
            get { return command; }
        }
        private int param1;
        public int Param1
        {
            set { param1 = value; }
            get { return param1; }
        }
        private int param2;
        public int Param2
        {
            set { param2 = value; }
            get { return param2; }
        }
        private int param3;
        public int Param3
        {
            set { param3 = value; }
            get { return param3; }
        }
        private int param4;
        public int Param4
        {
            set { param4= value; }
            get { return param4; }
        }
        private int param5;
        public int Param5
        {
            set { param5 = value; }
            get { return param5; }
        }
    }
}
