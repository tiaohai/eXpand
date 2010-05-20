using System;
using System.ComponentModel;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Utils;
using DevExpress.XtraEditors;

namespace eXpand.ExpressApp.Win.SystemModule
{
    public interface IModelDetailViewTabStopForReadOnly : IModelNode
    {
        [Category("eXpand")]
        bool TabOverReadOnlyEditors { get; set; }
    }

    public class ReadOnlyTabStopController : ViewController<DetailView>,IModelExtender
    {


        void IModelExtender.ExtendModelInterfaces(ModelInterfaceExtenders extenders)
        {
            extenders.Add<IModelDetailView, IModelDetailViewTabStopForReadOnly>();
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            IModelNode viewsNode = View.Model.Parent;
            if (viewsNode != null)
            {
                var modelViewsExt = viewsNode as IModelDetailViewTabStopForReadOnly;
                if (modelViewsExt != null)
                {
                    if (modelViewsExt.TabOverReadOnlyEditors)
                    {
                        var detailView = View;
                        CheckControlsVisibility(detailView);
                    }
                }
            }
        }
        private void CheckControlsVisibility(DetailView detailView)
        {
            Guard.ArgumentNotNull(detailView, "detailView");
            foreach (PropertyEditor propertyEditor in detailView.GetItems<PropertyEditor>())
            {
                var editor = propertyEditor.Control as BaseEdit;
                if (editor != null) {
                    Boolean controlHasTabStop = editor.TabStop;
                    Boolean revisedTabStop = controlHasTabStop && (!editor.Properties.ReadOnly);
                    if (revisedTabStop != controlHasTabStop) {
                        editor.TabStop = false;
                    }
                }
                
            }
        }

    }
}