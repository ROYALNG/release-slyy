using System;
using System.Collections.Generic;
using System.Text;
using GHIBMS.Common;

namespace GHIBMS.Server
{
    [Serializable]
    public class GraphicObject

    {
        private string _controlID ;
        private string _controlClass;
        private string _associateVar;
        private DataViewFormatEnum _viewFormat;
        public GraphicObject() 
        {
            controlID = "";
            controlClass = "";
            associateVar = "";
            _viewFormat = 0;
        }
        public string controlID
        {
            get { return _controlID; }
            set { _controlID = value; }
        
        }
        public string controlClass
        {
            get { return _controlClass; }
            set { _controlClass = value; }
        }
        public string associateVar
        {
            get { return _associateVar; }
            set { _associateVar = value; }
        }
        public DataViewFormatEnum ViewFormat
        {
            get { return _viewFormat; }
            set { _viewFormat = value; }
        }
       
    }
}
