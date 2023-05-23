using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Text.RegularExpressions;
using HoneywellIPM;

namespace SOPC
{
    public partial class FormMain : Form
    {
         public void ConnectedDrive()
         {
             foreach (ChannelInfo ch in ChanList)
             {
                 switch (ch.ProtocolCode)
                 {
                     case 101:   //矩阵 HVB
                         break;
                     case 102:   //矩阵 PELCO
                         break;
                     case 103:   //矩阵 AD
                         break;
                     case 104:   //矩阵 BOSCH
                         break;
                     case 201:   //报警主机 DS7400
                         break;
                     case 202:   //报警主机 IPM
                         ConnectedIPM(ch);
                         break;

                 }
             }
         }
         public void DisConnectedDrive()
         {



         }
        #region Honeywell IPM
         public void ConnectedIPM(ChannelInfo cha)
         {
             FormIPMOCX ipm = new FormIPMOCX();
             ipm.StartupMoniter(cha.NetPort);
         }




        #endregion





    }
}
