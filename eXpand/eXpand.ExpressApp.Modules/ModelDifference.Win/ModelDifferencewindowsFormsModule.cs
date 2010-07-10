using System.IO;
using DevExpress.ExpressApp.Win.SystemModule;

namespace eXpand.ExpressApp.ModelDifference.Win{
    public sealed class ModelDifferenceWindowsFormsModule : ModelDifferenceBaseModule
    {
        public ModelDifferenceWindowsFormsModule()
        {
            RequiredModuleTypes.Add(typeof(ModelDifferenceModule));
            RequiredModuleTypes.Add(typeof(SystemWindowsFormsModule));
        }

        private bool? _persistentApplicationModelUpdated=false;

        public override string GetPath() {
            return Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath);
        }

        protected override bool? PersistentApplicationModelUpdated{
            get { return _persistentApplicationModelUpdated; }
            set { _persistentApplicationModelUpdated = value; }
        }
    }
}