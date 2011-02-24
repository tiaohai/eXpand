﻿using System;

namespace Xpand.Persistent.Base.General {

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class TooltipAttribute : Attribute, ICustomAttribute {
        readonly string _value;

        public TooltipAttribute(string value) {
            _value = value;
        }


        string ICustomAttribute.Name {
            get { return "Tooltip"; }
        }

        public string Value {
            get { return _value; }
        }
    }
}