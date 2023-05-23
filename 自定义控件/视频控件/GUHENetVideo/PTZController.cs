using GHIBMS.Common;
using odm.core;
using onvif.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using utils;

namespace GHIBMS.NetVideo
{
    public enum OnvifPtzCommand
    {
        Center,
        Left,
        Upleft,
        Up,
        UpRight,
        Right,
        DownRight,
        Down,
        DownLeft,
        ZoomIn,
        ZoomOut,
        Stop
    };
    public class PTZController
    {


        private OnvifPtzCommand _previousCommand;
        //private SerialPort _serialPort;

        public bool IsContinuous
        {
            get
            {
                return true;
            }
        }
        #region ONVIF
        private Profile _ptzProfile;
        private INvtSession _nvtSession;
        #endregion

        //private uint _addr;


        //private HttpWebRequest _request;
        const double Arc = Math.PI / 8;
        //private string _nextcommand = "";

        //private bool _ptzNull;

        private string _address = "";
        int _profileid = -1;
        private string _login = "";
        private string _password = "";
        float _speed = 20;


        public PTZController(string address, int profileid, string login, string password, int speed)
        {
            _address = address;
            _profileid = profileid;
            _login = login;
            _password = password;
            _speed = speed;
        }
        public int Speed
        {
            set
            {
                _speed = value;
            }
        }


        public void AddPreset(string name)
        {
            if (PTZProfile != null)
            {
                try
                {
                    PTZSession.SetPreset(PTZProfile.token, name, null).RunSynchronously();
                }
                catch (Exception ex)
                {
                    //MainForm.LogExceptionToFile(ex);
                }
            }
        }

        public void DeletePreset(string name)
        {
            if (PTZProfile != null)
            {
                try
                {
                    var l = PTZSession.GetPresets(PTZProfile.token).RunSynchronously();
                    string t = "";
                    foreach (var p in l)
                    {
                        if (p.name == name)
                        {
                            t = p.token;
                            break;
                        }
                    }

                    if (t != "")
                        PTZSession.RemovePreset(PTZProfile.token, t).RunSynchronously();
                }
                catch (Exception ex)
                {
                    //MainForm.LogExceptionToFile(ex);
                }
            }
        }

        public void SendPTZDirection(double angle)
        {
            //if (_cameraControl.Camobject.settings.ptzrotate90)
            //{
            //    angle -= (Math.PI / 2);
            //    if (angle < -Math.PI)
            //    {
            //        angle += (2 * Math.PI);
            //    }
            //}
            //if (_cameraControl.Camobject.ptz != -1)
            //{
            //    //don't flip digital controls
            //    if (_cameraControl.Camobject.settings.ptzflipx)
            //    {
            //        if (angle <= 0)
            //            angle = -Math.PI - angle;
            //        else
            //            angle = Math.PI - angle;
            //    }
            //    if (_cameraControl.Camobject.settings.ptzflipy)
            //    {
            //        angle = angle * -1;
            //    }
            //}



            //if (PTZSettings == null)
            {
                var cmd = OnvifPtzCommand.Stop;
                if (angle < Arc && angle > -Arc)
                {
                    cmd = OnvifPtzCommand.Left;
                }
                if (angle >= Arc && angle < 3 * Arc)
                {
                    cmd = OnvifPtzCommand.Upleft;
                }
                if (angle >= 3 * Arc && angle < 5 * Arc)
                {
                    cmd = OnvifPtzCommand.Up;
                }
                if (angle >= 5 * Arc && angle < 7 * Arc)
                {
                    cmd = OnvifPtzCommand.UpRight;
                }
                if (angle >= 7 * Arc || angle < -7 * Arc)
                {
                    cmd = OnvifPtzCommand.Right;
                }
                if (angle <= -5 * Arc && angle > -7 * Arc)
                {
                    cmd = OnvifPtzCommand.DownRight;
                }
                if (angle <= -3 * Arc && angle > -5 * Arc)
                {
                    cmd = OnvifPtzCommand.Down;
                }
                if (angle <= -Arc && angle > -3 * Arc)
                {
                    cmd = OnvifPtzCommand.DownLeft;
                }

                //switch (_cameraControl.Camobject.ptz)
                //{
                //    default:
                //        var p = _cameraControl.Camera.ZPoint;
                //        p.X -= Convert.ToInt32(15 * Math.Cos(angle));
                //        p.Y -= Convert.ToInt32(15 * Math.Sin(angle));
                //        _cameraControl.Camera.ZPoint = p;
                //        break;
                //    case -5://ONVIF
                ProcessOnvif(cmd);
                //        break;
                //}
                return;
            }



            //string command = PTZSettings.Commands.Center;
            //string diag = "";

            //if (angle < Arc && angle > -Arc)
            //{
            //    command = PTZSettings.Commands.Left;

            //}
            //if (angle >= Arc && angle < 3 * Arc)
            //{
            //    command = PTZSettings.Commands.LeftUp;
            //    diag = "leftup";
            //}
            //if (angle >= 3 * Arc && angle < 5 * Arc)
            //{
            //    command = PTZSettings.Commands.Up;
            //}
            //if (angle >= 5 * Arc && angle < 7 * Arc)
            //{
            //    command = PTZSettings.Commands.RightUp;
            //    diag = "rightup";
            //}
            //if (angle >= 7 * Arc || angle < -7 * Arc)
            //{
            //    command = PTZSettings.Commands.Right;
            //}
            //if (angle <= -5 * Arc && angle > -7 * Arc)
            //{
            //    command = PTZSettings.Commands.RightDown;
            //    diag = "rightdown";
            //}
            //if (angle <= -3 * Arc && angle > -5 * Arc)
            //{
            //    command = PTZSettings.Commands.Down;
            //}
            //if (angle <= -Arc && angle > -3 * Arc)
            //{
            //    command = PTZSettings.Commands.LeftDown;
            //    diag = "leftdown";
            //}

            //if (String.IsNullOrEmpty(command)) //some PTZ cameras don't have diagonal controls, this fixes that
            //{
            //    switch (diag)
            //    {
            //        case "leftup":
            //            _nextcommand = PTZSettings.Commands.Up;
            //            SendPTZCommand(PTZSettings.Commands.Left);
            //            break;
            //        case "rightup":
            //            _nextcommand = PTZSettings.Commands.Up;
            //            SendPTZCommand(PTZSettings.Commands.Right);
            //            break;
            //        case "rightdown":
            //            _nextcommand = PTZSettings.Commands.Down;
            //            SendPTZCommand(PTZSettings.Commands.Right);
            //            break;
            //        case "leftdown":
            //            _nextcommand = PTZSettings.Commands.Down;
            //            SendPTZCommand(PTZSettings.Commands.Left);
            //            break;
            //    }
            //}
            //else
            //    SendPTZCommand(command);

        }
        public void SendPTZCommand(PTZCmdCodeEnum command)
        {
            OnvifPtzCommand cmd = OnvifPtzCommand.Stop;
            switch (command)
            {
                case PTZCmdCodeEnum.PAN_LEFT:
                    cmd = OnvifPtzCommand.Left;
                    break;
                case PTZCmdCodeEnum.PAN_RIGHT:
                    cmd = OnvifPtzCommand.Right;
                    break;
                case PTZCmdCodeEnum.TILT_UP:
                    cmd = OnvifPtzCommand.Up;
                    break;
                case PTZCmdCodeEnum.TILT_DOWN:
                    cmd = OnvifPtzCommand.Down;
                    break;
                case PTZCmdCodeEnum.UP_LEFT:
                    cmd = OnvifPtzCommand.Upleft;
                    break;
                case PTZCmdCodeEnum.UP_RIGHT:
                    cmd = OnvifPtzCommand.UpRight;
                    break;
                case PTZCmdCodeEnum.DOWN_LEFT:
                    cmd = OnvifPtzCommand.DownLeft;
                    break;
                case PTZCmdCodeEnum.DOWN_RIGHT:
                    cmd = OnvifPtzCommand.DownRight;
                    break;
                case PTZCmdCodeEnum.ZOOM_IN:
                    cmd = OnvifPtzCommand.ZoomIn;
                    break;
                case PTZCmdCodeEnum.ZOOM_OUT:
                    cmd = OnvifPtzCommand.ZoomOut;
                    break;
                //case (uint)PTZCmdCodeEnum.GOTO_PRESET:
                //    cmd = PTZ_CONTROL.PTZ_POINT_MOVE_CONTROL;
                //    break;
                //case (uint)PTZCmdCodeEnum.SET_PRESET:
                //    cmd = PTZ_CONTROL.PTZ_POINT_SET_CONTROL;
                //    break;
                //case (uint)PTZCmdCodeEnum.CLE_PRESET:
                //    cmd = PTZ_CONTROL.PTZ_POINT_DEL_CONTROL;
                //    break;
                //case (uint)PTZCmdCodeEnum.FILL_PRE_SEQ:
                //    cmd = PTZ_CONTROL.EXTPTZ_ADDTOLOOP;
                //    break;
                //case (uint)PTZCmdCodeEnum.CLE_PRE_SEQ:
                //    cmd = PTZ_CONTROL.EXTPTZ_DELFROMLOOP;
                //    break;
                //case (uint)PTZCmdCodeEnum.RUN_SEQ:
                //    cmd = PTZ_CONTROL.EXTPTZ_STARTLINESCAN;
                //    break;
                //case (uint)PTZCmdCodeEnum.STOP_SEQ:
                //    cmd = PTZ_CONTROL.EXTPTZ_CLOSELINESCAN;
                //    break;
                default:
                    cmd = OnvifPtzCommand.Stop;
                    break;

            }
            SendPTZCommand(cmd, false);
        }

        public void SendPTZCommand(OnvifPtzCommand command)
        {
            SendPTZCommand(command, false);
        }


        public void SendPTZCommand(OnvifPtzCommand command, bool wait)
        {

            switch (command)
            {
                //case OnvifPtzCommand.Left:
                //    SendPTZDirection(0d);
                //    break;
                //case OnvifPtzCommand.Upleft:
                //    SendPTZDirection(Math.PI / 4);
                //    break;
                //case OnvifPtzCommand.Up:
                //    SendPTZDirection(Math.PI / 2);
                //    break;
                //case OnvifPtzCommand.UpRight:
                //    SendPTZDirection(3 * Math.PI / 4);
                //    break;
                //case OnvifPtzCommand.Right:
                //    SendPTZDirection(Math.PI);
                //    break;
                //case OnvifPtzCommand.DownRight:
                //    SendPTZDirection(-3 * Math.PI / 4);
                //    break;
                //case OnvifPtzCommand.Down:
                //    SendPTZDirection(-Math.PI / 2);
                //    break;
                //case OnvifPtzCommand.DownLeft:
                //    SendPTZDirection(-Math.PI / 4);
                //    break;
                default:
                    ProcessOnvif(command);
                    break;
            }



            //Rectangle r = _cameraControl.Camera.ViewRectangle;
            //if (r != Rectangle.Empty)
            //{
            //    double angle = 0;
            //    bool isangle = true;
            //    switch (command)
            //    {
            //        case PtzCommand.Left:
            //            angle = 0;
            //            break;
            //        case PtzCommand.Upleft:
            //            angle = Math.PI / 4;
            //            break;
            //        case PtzCommand.Up:
            //            angle = Math.PI / 2;
            //            break;
            //        case PtzCommand.UpRight:
            //            angle = 3 * Math.PI / 4;
            //            break;
            //        case PtzCommand.Right:
            //            angle = Math.PI;
            //            break;
            //        case PtzCommand.DownRight:
            //            angle = -3 * Math.PI / 4;
            //            break;
            //        case PtzCommand.Down:
            //            angle = -Math.PI / 2;
            //            break;
            //        case PtzCommand.DownLeft:
            //            angle = -Math.PI / 4;
            //            break;
            //        case PtzCommand.ZoomIn:
            //            isangle = false;
            //            _cameraControl.Camera.ZFactor += 0.2f;
            //            break;
            //        case PtzCommand.ZoomOut:
            //            isangle = false;
            //            var f = _cameraControl.Camera.ZFactor;
            //            f -= 0.2f;
            //            if (f < 1)
            //                f = 1;
            //            _cameraControl.Camera.ZFactor = f;
            //            break;
            //        case PtzCommand.Center:
            //            isangle = false;
            //            _cameraControl.Camera.ZFactor = 1;
            //            break;
            //        case PtzCommand.Stop:
            //            isangle = false;
            //            break;

            //    }
            //    if (isangle)
            //    {
            //        var p = _cameraControl.Camera.ZPoint;
            //        p.X -= Convert.ToInt32(15 * Math.Cos(angle));
            //        p.Y -= Convert.ToInt32(15 * Math.Sin(angle));
            //        _cameraControl.Camera.ZPoint = p;
            //    }

            //}
        }


        private Profile PTZProfile
        {
            get
            {
                if (PTZSession == null)
                    return null;

                if (_ptzProfile != null)
                    return _ptzProfile;

                int profileid = _profileid;//云台ID

                Profile[] profiles = new Profile[0];
                try
                {
                    profiles = PTZSession.GetProfiles().RunSynchronously();
                }
                catch (Exception ex)
                {
                    //MainForm.LogExceptionToFile(ex);
                    return null;
                }
                if (profiles.Length > profileid)
                    _ptzProfile = profiles[profileid];
                return _ptzProfile;
            }
        }
        private INvtSession PTZSession
        {
            get
            {
                if (_nvtSession != null)
                    return _nvtSession;

                string addr = _address;
                //"http://192.168.0.1:8080/onvif/service";
                DeviceDescriptionHolder ddh = null;
                if (ddh == null)
                {
                    Uri u;
                    if (!Uri.TryCreate(addr, UriKind.Absolute, out u))
                    {
                        return null;
                    }
                    ddh = new DeviceDescriptionHolder { Uris = new[] { u }, Address = "" };
                    ddh.Address += u.DnsSafeHost + "; ";
                    ddh.Address = ddh.Address.TrimEnd(new[] { ';', ' ' });
                    if (ddh.Address == "")
                    {
                        ddh.IsInvalidUris = true;
                        ddh.Address = "Invalid Uri";
                    }
                    ddh.Name = u.AbsoluteUri;
                    ddh.Location = "Unknown";
                    ddh.DeviceIconUri = null;
                }


                ddh.Account = new NetworkCredential { UserName = _login, Password = _password };
                var sessionFactory = new NvtSessionFactory(ddh.Account);


                try { _nvtSession = sessionFactory.CreateSession(ddh.Uris[0]); }
                catch (Exception ex)
                {
                    //MainForm.LogExceptionToFile(ex);
                    return null;
                }
                //ddh.Name = _nvtSession
                return _nvtSession;
            }
        }

        public void ResetONVIF()
        {
            _ptzProfile = null;
            //_ptzSettings = null;
        }

        void ProcessOnvif(OnvifPtzCommand command)
        {
            if (PTZProfile != null)
            {
                //var speed = PTZProfile.ptzConfiguration.defaultPTZSpeed;
                //string spacePT = PTZProfile.ptzConfiguration.defaultContinuousPanTiltVelocitySpace;
                //string spaceZ = PTZProfile.ptzConfiguration.defaultContinuousZoomVelocitySpace;

                Vector2D panTilt = null;
                Vector1D zoom = null;
                try
                {
                    switch (command)
                    {
                        case OnvifPtzCommand.Left:
                            panTilt = new Vector2D { space = null, x = -(_speed / 1000f), y = 0 };
                            break;
                        case OnvifPtzCommand.Upleft:
                            panTilt = new Vector2D { space = null, x = -(_speed / 1000f), y = (_speed / 1000f) };
                            break;
                        case OnvifPtzCommand.Up:
                            panTilt = new Vector2D { space = null, x = 0, y = (_speed / 1000f) };
                            break;
                        case OnvifPtzCommand.UpRight:
                            panTilt = new Vector2D { space = null, x = (_speed / 1000f), y = (_speed / 1000f) };
                            break;
                        case OnvifPtzCommand.Right:
                            panTilt = new Vector2D { space = null, x = (_speed / 1000f), y = 0 };
                            break;
                        case OnvifPtzCommand.DownRight:
                            panTilt = new Vector2D { space = null, x = (_speed / 1000f), y = -(_speed / 1000f) };
                            break;
                        case OnvifPtzCommand.Down:
                            panTilt = new Vector2D { space = null, x = 0, y = -(_speed / 1000f) };
                            break;
                        case OnvifPtzCommand.DownLeft:
                            panTilt = new Vector2D { space = null, x = -(_speed / 1000f), y = -(_speed / 1000f) };
                            break;
                        case OnvifPtzCommand.ZoomIn:
                            zoom = new Vector1D { space = null, x = (_speed / 1000f) };
                            break;
                        case OnvifPtzCommand.ZoomOut:
                            zoom = new Vector1D { space = null, x = -(_speed / 1000f) };
                            break;
                        case OnvifPtzCommand.Center:
                            ProcessOnvifCommand("");
                            return;
                        case OnvifPtzCommand.Stop:
                            PTZSession.Stop(PTZProfile.token, true, true).RunSynchronously();
                            return;
                    }
                    PTZSession.ContinuousMove(PTZProfile.token, new PTZSpeed() { panTilt = panTilt, zoom = zoom },
                                              null).RunSynchronously();
                }
                catch (Exception ex)
                {
                    //MainForm.LogExceptionToFile(ex);
                }
            }
        }

        enum PTZMoveModes
        {
            Absolute,
            Relative,
            Continuous
        }


        void ProcessOnvifCommand(string name)
        {
            if (PTZProfile != null)
            {
                try
                {
                    var l = PTZSession.GetPresets(PTZProfile.token).RunSynchronously();
                    string t = "";
                    foreach (var p in l)
                    {
                        if (p.name == name)
                        {
                            t = p.token;
                            break;
                        }
                    }

                    if (t != "")
                        PTZSession.GotoPreset(PTZProfile.token, t, null).RunSynchronously();



                }
                catch (Exception ex)
                {
                    //MainForm.LogExceptionToFile(ex);
                }

            }
        }

        public string[] ONVIFPresets
        {
            get
            {
                var pl = new List<string>();
                try
                {
                    if (PTZProfile != null && PTZSession != null)
                    {

                        var presets = PTZSession.GetPresets(PTZProfile.token).RunSynchronously();
                        pl.AddRange(presets.Select(p => p.name));

                    }
                }
                catch (Exception ex)
                {
                    //MainForm.LogExceptionToFile(ex);
                }
                return pl.ToArray();
            }
        }



        public void SendPTZCommand(string cmd)
        {
            SendPTZCommand(cmd, false);
        }

        public void SendPTZCommand(string cmd, bool wait)
        {
            if (String.IsNullOrEmpty(cmd))
                return;

            //if (_request != null)
            //{
            //    if (!wait)
            //        return;
            //    _request.Abort();
            //}

            ProcessOnvifCommand(cmd);

            return;

            #region 以下不执行

            //Uri uri;
            //bool absURL = false;

            //string url = _cameraControl.Camobject.settings.videosourcestring;

            //if (_cameraControl.Camobject.settings.ptzurlbase.Contains("://"))
            //{
            //    url = _cameraControl.Camobject.settings.ptzurlbase;
            //    absURL = true;
            //}

            //if (cmd.Contains("://"))
            //{
            //    Uri uriTemp;
            //    if (Uri.TryCreate(cmd, UriKind.RelativeOrAbsolute, out uriTemp))
            //    {
            //        absURL = uriTemp.IsAbsoluteUri;
            //        if (absURL)
            //            url = cmd;
            //    }
            //}

            //try
            //{
            //    uri = new Uri(url);
            //}
            //catch (Exception e)
            //{
            //    //MainForm.LogExceptionToFile(e);
            //    return;
            //}
            //if (uri.Scheme == Uri.UriSchemeFile)
            //    return;

            //if (!absURL)
            //{
            //    url = uri.AbsoluteUri.Replace(uri.PathAndQuery, "/");

            //    if (ptz.portSpecified && ptz.port > 0)
            //    {
            //        url = url.ReplaceFirst(":" + uri.Port + "/", ":" + ptz.port + "/");
            //    }

            //    if (!uri.Scheme.ToLower().StartsWith("http")) //allow http and https
            //    {
            //        url = url.ReplaceFirst(uri.Scheme + "://", "http://");
            //    }

            //    url = url.Trim('/');

            //    if (!cmd.StartsWith("/"))
            //    {
            //        url += _cameraControl.Camobject.settings.ptzurlbase;

            //        if (cmd != "")
            //        {
            //            if (!url.EndsWith("/"))
            //            {
            //                string ext = "?";
            //                if (url.IndexOf("?", StringComparison.Ordinal) != -1)
            //                    ext = "&";
            //                url += ext + cmd;
            //            }
            //            else
            //            {
            //                url += cmd;
            //            }

            //        }
            //    }
            //    else
            //    {
            //        url += cmd;
            //    }
            //}
            //else
            //{
            //    if (!String.IsNullOrEmpty(cmd))
            //    {
            //        if (!cmd.Contains("://"))
            //        {
            //            if (!url.EndsWith("/"))
            //            {
            //                string ext = "?";
            //                if (url.IndexOf("?", StringComparison.Ordinal) != -1)
            //                    ext = "&";
            //                url += ext + cmd;
            //            }
            //            else
            //            {
            //                url += cmd;
            //            }
            //        }
            //        else
            //        {
            //            url = cmd;
            //        }

            //    }
            //}


            //string un = _cameraControl.Camobject.settings.login;
            //string pwd = _cameraControl.Camobject.settings.password;

            //if (!String.IsNullOrEmpty(_cameraControl.Camobject.settings.ptzusername))
            //{
            //    un = _cameraControl.Camobject.settings.ptzusername;
            //    pwd = _cameraControl.Camobject.settings.ptzpassword;
            //}
            //else
            //{
            //    if (_cameraControl.Camobject.settings.login == string.Empty)
            //    {

            //        //get from url
            //        if (!String.IsNullOrEmpty(uri.UserInfo))
            //        {
            //            string[] creds = uri.UserInfo.Split(':');
            //            if (creds.Length >= 2)
            //            {
            //                un = creds[0];
            //                pwd = creds[1];
            //            }
            //        }
            //    }
            //}

            //if (!String.IsNullOrEmpty(ptz.AppendAuth))
            //{
            //    if (url.IndexOf("?", StringComparison.Ordinal) == -1)
            //        url += "?" + ptz.AppendAuth;
            //    else
            //        url += "&" + ptz.AppendAuth;

            //}

            //url = url.Replace("[USERNAME]", Uri.EscapeDataString(un));
            //url = url.Replace("[PASSWORD]", Uri.EscapeDataString(pwd));
            //url = url.Replace("[CHANNEL]", _cameraControl.Camobject.settings.ptzchannel);

            //_request = (HttpWebRequest)WebRequest.Create(url);
            //_request.Timeout = 5000;
            //_request.AllowAutoRedirect = true;
            //_request.KeepAlive = true;
            //_request.SendChunked = false;
            //_request.AllowWriteStreamBuffering = true;
            //_request.UserAgent = _cameraControl.Camobject.settings.useragent;
            //if (_cameraControl.Camobject.settings.usehttp10)
            //    _request.ProtocolVersion = HttpVersion.Version10;
            ////

            ////get credentials

            //// set login and password

            //string authInfo = "";
            //if (!String.IsNullOrEmpty(un))
            //{
            //    authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(Uri.EscapeDataString(un) + ":" + Uri.EscapeDataString(pwd)));
            //    _request.Headers["Authorization"] = "Basic " + authInfo;
            //}

            //string ckies = _cameraControl.Camobject.settings.cookies ?? "";
            //if (!String.IsNullOrEmpty(ckies))
            //{
            //    if (!ckies.EndsWith(";"))
            //        ckies += ";";
            //}
            //if (!String.IsNullOrEmpty(ptz.Cookies))
            //    ckies += ptz.Cookies;

            //if (!String.IsNullOrEmpty(ckies))
            //{
            //    ckies = ckies.Replace("[USERNAME]", un);
            //    ckies = ckies.Replace("[PASSWORD]", pwd);
            //    ckies = ckies.Replace("[CHANNEL]", _cameraControl.Camobject.settings.ptzchannel);
            //    ckies = ckies.Replace("[AUTH]", authInfo);
            //    var myContainer = new CookieContainer();
            //    string[] coll = ckies.Split(';');
            //    foreach (var ckie in coll)
            //    {
            //        if (!String.IsNullOrEmpty(ckie))
            //        {
            //            string[] nv = ckie.Split('=');
            //            if (nv.Length == 2)
            //            {
            //                var cookie = new Cookie(nv[0].Trim(), nv[1].Trim());
            //                myContainer.Add(new Uri(_request.RequestUri.ToString()), cookie);
            //            }
            //        }
            //    }
            //    _request.CookieContainer = myContainer;
            //}

            //if (ptz.POST)
            //{

            //    var i = url.IndexOf("?", StringComparison.Ordinal);
            //    if (i > -1 && i < url.Length)
            //    {
            //        var encoding = new ASCIIEncoding();
            //        string postData = url.Substring(i + 1);
            //        byte[] data = encoding.GetBytes(postData);

            //        _request.Method = "POST";
            //        _request.ContentType = "application/x-www-form-urlencoded";
            //        _request.ContentLength = data.Length;

            //        using (Stream stream = _request.GetRequestStream())
            //        {
            //            stream.Write(data, 0, data.Length);
            //        }
            //    }
            //}


            //var myRequestState = new RequestState { Request = _request };
            //_request.BeginGetResponse(FinishPTZRequest, myRequestState);

            #endregion
        }


        #region Nested type: RequestState

        public class RequestState
        {
            // This class stores the request state of the request.
            public WebRequest Request;
            public WebResponse Response;

            public RequestState()
            {
                Request = null;
                Response = null;
            }
        }

        #endregion
    }

    public class DeviceDescriptionHolder
    {
        public Uri[] Uris;
        public string Address;
        public bool IsInvalidUris;
        public NetworkCredential Account;
        public string Name, Location, DeviceIconUri;
        public Profile[] Profiles;
        public string URL;
    }

    public static class ExternClass
    {
        public static string ReplaceFirst(this string text, string search, string replace)
        {
            int pos = text.IndexOf(search, StringComparison.Ordinal);
            if (pos < 0)
            {
                return text;
            }
            return text.Substring(0, pos) + replace + text.Substring(pos + search.Length);
        }
    }
}
