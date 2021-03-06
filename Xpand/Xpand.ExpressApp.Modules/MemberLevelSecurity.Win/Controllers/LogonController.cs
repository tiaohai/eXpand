using System.ComponentModel;
using DevExpress.ExpressApp.Actions;

namespace eXpand.ExpressApp.MemberLevelSecurity.Win.Controllers
{
    public partial class LogonController : DevExpress.ExpressApp.LogonController 
    {
        public LogonController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        protected override SimpleAction CreateAcceptAction()
        {
            var action = base.CreateAcceptAction();
            action.Executed+=Action_OnExecuted;
            action.ExecuteCompleted+=Action_OnExecuteCompleted;
            action.Executing+=Action_OnExecuting;
            
            return action;
        }

        protected override void Accept(SimpleActionExecuteEventArgs args)
        {
            base.Accept(args);
        }

        private void Action_OnExecuting(object sender, CancelEventArgs e)
        {
            
        }

        private void Action_OnExecuteCompleted(object sender, ActionBaseEventArgs e)
        {
            
        }

        private void Action_OnExecuted(object sender, ActionBaseEventArgs e)
        {
                        
        }
    }
}
