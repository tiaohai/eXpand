﻿using System;
using System.IO;
using System.Linq;
using System.Reflection;
using DevExpress.DashboardCommon.Native;
using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using Xpand.ExpressApp.Dashboard.BusinessObjects;

namespace Xpand.ExpressApp.Dashboard.Win.Helpers {
    public static class DashBoardDefinitionExtensions {
        static Type DashBoardObjectType(IDashboardDefinition template, DevExpress.DashboardCommon.Dashboard dashboard, ITypeWrapper typeWrapper) {
            var wrapper = template.DashboardTypes.FirstOrDefault(type => type.Caption.Equals(dashboard.DataSources.First(ds => ds.Name.Equals(typeWrapper.Caption)).Name));
            return wrapper != null ? wrapper.Type : null;
        }

        public static DevExpress.DashboardCommon.Dashboard CreateDashBoard(this IDashboardDefinition template, IObjectSpace objectSpace, bool filter) {
            var dashboard = DashboardAccessor.CreateDashboardInstance();
            try {
                LoadFromXml(template.Xml, dashboard);
                foreach (ITypeWrapper typeWrapper in template.DashboardTypes) {
                    ITypeWrapper wrapper = typeWrapper;
                    if (dashboard.DataSources.Contains(ds => ds.Name.Equals(wrapper.Caption))) {
                        Type dashBoardObjectType = DashBoardObjectType(template, dashboard, typeWrapper);
                        if (dashBoardObjectType != null) {
                            var dataSource = dashboard.DataSources.First(ds => ds.Name.Equals(typeWrapper.Caption));
                            dataSource.Data = objectSpace.GetObjects(dashBoardObjectType);
                        }
                    } else if (!dashboard.DataSources.Contains(ds => ds.Name.Equals(wrapper.Caption)))
                        dashboard.AddDataSource(typeWrapper.Caption, objectSpace.GetObjects(typeWrapper.Type));
                }
                if (filter)
                    Filter(template, dashboard);
            } catch (Exception e) {
                dashboard.Dispose();
                Tracing.Tracer.LogError(e);
            }
            return dashboard;
        }

        static void Filter(IDashboardDefinition template, DevExpress.DashboardCommon.Dashboard dashboard) {
            var types = template.DashboardTypes.Select(tw => tw.Caption);
            for (int i = dashboard.DataSources.Count - 1; i >= 0; i--)
                if (!types.Contains(dashboard.DataSources[i].Name))
                    dashboard.DataSources.RemoveAt(i);
        }

        static void LoadFromXml(string xml, DevExpress.DashboardCommon.Dashboard dashboard) {
            if (xml != null) {
                using (var me = new MemoryStream()) {
                    var sw = new StreamWriter(me);
                    sw.Write(xml);
                    sw.Flush();
                    me.Seek(0, SeekOrigin.Begin);
                    dashboard.LoadFromXml(me);
                    sw.Close();
                    me.Close();
                }
            }
        }
        /// <summary>
        /// Returns a _private_ Property Value from a given Object. Uses Reflection.
        /// Throws a ArgumentOutOfRangeException if the Property is not found.
        /// </summary>
        /// <typeparam name="T">Type of the Property</typeparam>
        /// <param name="obj">Object from where the Property Value is returned</param>
        /// <param name="propName">Propertyname as string.</param>
        /// <returns>PropertyValue</returns>
        public static T GetPrivatePropertyValue<T>(this object obj, string propName) {
            if (obj == null)
                throw new ArgumentNullException("obj");
            PropertyInfo pi = obj.GetType().GetProperty(propName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            if (pi == null)
                throw new ArgumentOutOfRangeException("propName", string.Format("Property {0} was not found in Type {1}", propName, obj.GetType().FullName));
            return (T)pi.GetValue(obj, null);
        }

    }
}
