using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using VSG.Controllers;

using AME.Controllers;
using AME.Views.View_Components;

namespace VSG.ViewComponents
{
    public partial class EngramUnitID : UserControl, AME.Views.View_Components.IViewComponent
    {
        private ViewComponentHelper myHelper;

        public IViewComponentHelper IViewHelper
        {
            get { return myHelper; }
        }

        private VSGController _controller;
        private int displayID = -1;
        private int parentID = -1;
        public EngramUnitID()
        {
            myHelper = new ViewComponentHelper(this);

            InitializeComponent();
        }
        public int DisplayID
        {
            get
            {
                return displayID;
            }
            set
            {
                displayID = value;
                speciesCompletionUnitCheckBox1.ComponentId = value;
                if (_controller != null)
                {
                    unitLinkBox.DisplayRootId = _controller.ScenarioId;
                    unitLinkBox.DisplayLinkType = "Scenario";
                    unitLinkBox.DisplayComponentType = "CreateEvent";
                    unitLinkBox.ConnectFromId = DisplayID;
                    unitLinkBox.ConnectRootId = DisplayID;
                    unitLinkBox.ConnectLinkType = "EngramUnitID";
                }

            }
        }
        public int ParentID
        {
            get
            {
                return parentID;
            }
            set
            {
                parentID = value;
            }
        }

        public Boolean Unit
        {
            get
            {
                return speciesCompletionUnitCheckBox1.Checked;
            }
        }

        #region IViewComponent Members

        public AME.Controllers.IController Controller
        {
            get
            {
                return _controller;
            }
            set
            {
                _controller = (VSGController)value;
                unitLinkBox.Controller = _controller;
                speciesCompletionUnitCheckBox1.Controller = _controller;
                if (displayID > 0)
                {
                    unitLinkBox.DisplayRootId = _controller.ScenarioId;
                    unitLinkBox.DisplayLinkType = "Scenario";
                    unitLinkBox.DisplayComponentType = "CreateEvent";
                    unitLinkBox.ConnectFromId = DisplayID;
                    unitLinkBox.ConnectRootId = DisplayID;
                    unitLinkBox.ConnectLinkType = "EngramUnitID";
                }
            }
        }

        public void UpdateViewComponent()
        {
            String type = _controller.GetComponentType(parentID);
            switch (type)
            {
                case "CreateEvent":
                    unitLinkBox.Enabled = false;
                    orLabel.Visible = false;
                    speciesCompletionUnitCheckBox1.Visible = false;
                    break;
                case "SpeciesCompletionEvent":
                case "LaunchEvent":
                case "WeaponLaunchEvent":
                case "RevealEvent":
                case "MoveEvent":
                case "CompletionEvent":
                case "TransferEvent":
                case "StateChangeEvent":
                    orLabel.Visible = true;
                    speciesCompletionUnitCheckBox1.Visible = true;
                    //_controller.ViewUpdateStatus = false;
                    speciesCompletionUnitCheckBox1.UpdateViewComponent();
                    //_controller.ViewUpdateStatus = true;
                    if (speciesCompletionUnitCheckBox1.Checked)
                    {
                        unitLinkBox.DeleteAllLinks();
                        unitLinkBox.Enabled = false;
                    }
                    else
                    {
                        unitLinkBox.Enabled = true;
                    }
                    break;
                default:
                    unitLinkBox.Enabled = true;
                    orLabel.Visible = false;
                    speciesCompletionUnitCheckBox1.Visible = false;
                    break;
            }

            unitLinkBox.UpdateViewComponent();
           
        }

        #endregion


    }
}