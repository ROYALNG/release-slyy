using System;
using System.Collections.Generic;
using System.Text;

namespace GHIBMS.Server
{   
    [Serializable]
    public class GraphicObjectMngr
    {
        private string _formName="";
        private string _projectName="";
        private List<GraphicObject> _GraphicObjectsList = new List<GraphicObject>();
        public GraphicObjectMngr()
        {
          
        }
        public string FormName
        {
            get { return _formName; }
            set { _formName = value; }
        }
        public string ProjectName
        {
            get {return _projectName;}
            set {_projectName=value;}
        }
        public List<GraphicObject> GraphicObjectsList
        {
            get
            {
                return _GraphicObjectsList;
            }
            set
            {
                _GraphicObjectsList = value;
            }
        }

    }
}
