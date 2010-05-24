﻿using System.ComponentModel;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;

namespace eXpand.ExpressApp.SystemModule
{

    public interface IModelClassEditMode : IModelNode
    {
        [Category("eXpand")]
        [Description("Control detail view default edit mode")]
        ViewEditMode ViewEditMode { get; set; }
    }
    public interface IModelDetailViewEditMode : IModelNode
    {
        [Category("eXpand")]
        [ModelValueCalculator("((IModelClassEditMode)ModelClass)", "ViewEditMode")]
        [Description("Control detail view default edit mode")]
        ViewEditMode ViewEditMode { get; set; }
    }
    
    public class ViewEditModeController : ViewController<DetailView>, IModelExtender
    {

        protected override void OnActivated()
        {
            base.OnActivated();
            var attributeValue = ((IModelDetailViewEditMode)View.Model).ViewEditMode;
            View.ViewEditMode = attributeValue;
        }

        void IModelExtender.ExtendModelInterfaces(ModelInterfaceExtenders extenders)
        {
            extenders.Add<IModelClass, IModelClassEditMode>();
            extenders.Add<IModelDetailView, IModelDetailViewEditMode>();
        }
    }
}