﻿using Xpand.ExpressApp.Logic.Conditional.Logic;
using Xpand.ExpressApp.Logic.Security.Improved;

namespace Xpand.ExpressApp.Logic.Conditional.Security.Improved {
    public abstract class ConditionalLogicRulePermission : LogicRulePermission, IConditionalLogicRule {
        protected ConditionalLogicRulePermission(string operation, IConditionalLogicRule logicRule)
            : base(operation, logicRule) {
            NormalCriteria = logicRule.NormalCriteria;
            EmptyCriteria = logicRule.EmptyCriteria;
        }
        public string NormalCriteria { get; set; }
        public string EmptyCriteria { get; set; }
    }
}
