﻿using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using eXpand.ExpressApp.AdditionalViewControlsProvider.Logic;
using eXpand.ExpressApp.Attributes;

namespace FeatureCenter.Module.LinqQuery
{
    public class AttributeRegistrator:Module.AttributeRegistrator
    {
        public override IEnumerable<Attribute> GetAttributes(ITypeInfo typesInfo) {
            if (typesInfo.Type != typeof(Customer)) yield break;
            yield return new AdditionalViewControlsRuleAttribute(Captions.ViewMessage + " " + Captions.HeaderLinqQuery, "1=1", "1=1", Captions.ViewMessageLinqQuery, Position.Bottom){ViewType = ViewType.ListView, View = "LinqQuery_ListView"};
            yield return new AdditionalViewControlsRuleAttribute(Captions.Header + " " + Captions.HeaderLinqQuery, "1=1", "1=1", Captions.HeaderLinqQuery, Position.Top) { ViewType = ViewType.ListView, View = "LinqQuery_ListView" };
            yield return new NavigationItemAttribute(Captions.Miscellaneous + "LinqQuery", "Customer_ListView_EmployeesLinq_Linq");
            yield return new CloneViewAttribute(CloneViewType.ListView, "LinqQuery_ListView");
        }
    }
}