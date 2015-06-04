using System;
using System.Data;
using System.Web.UI;
using System.Collections;

namespace web_app.common
{
    public class HierarchicalDataSet : IHierarchicalDataSource
    {
        DataSet dataSet;
        string idColumnName;
        string parentIdColumnName;
        private long rootIdVal;

        /// <summary>
        /// The constructor of the class
        /// </summary>
        /// <param name="dataSet">The dataset that contains the data</param>
        /// <param name="idColumnName">The Primary key column name</param>
        /// <param name="parentidColumnName">The Parent Primary key column name that identifies the Parent-Child relationship</param>
        public HierarchicalDataSet(DataSet dataSet, string idColumnName, string parentIdColumnName, long rootIdVal)
        {
            this.dataSet = dataSet;
            this.idColumnName = idColumnName;
            this.parentIdColumnName = parentIdColumnName;
            this.rootIdVal = rootIdVal;
        }

        public event EventHandler DataSourceChanged; // never used here

        public HierarchicalDataSourceView GetHierarchicalView(string viewPath)
        {
            return new DataSourceView(this, viewPath, rootIdVal);
        }

        #region supporting methods
        DataRowView GetParentRow(DataRowView row)
        {
            dataSet.Tables[0].DefaultView.RowFilter = String.Format("{0} = {1}", idColumnName, row[parentIdColumnName]);
            DataRowView parentRow = dataSet.Tables[0].DefaultView[0];
            dataSet.Tables[0].DefaultView.RowFilter = "";
            return parentRow;
        }

        string GetChildrenViewPath(string viewPath, DataRowView row)
        {
            return viewPath + "\\" + row[idColumnName];
        }

        bool HasChildren(DataRowView row)
        {
            dataSet.Tables[0].DefaultView.RowFilter = String.Format("{0} = {1}", parentIdColumnName, row[idColumnName]);
            bool hasChildren = dataSet.Tables[0].DefaultView.Count > 0;
            dataSet.Tables[0].DefaultView.RowFilter = "";
            return hasChildren;
        }

        string GetParentViewPath(string viewPath)
        {
            return viewPath.Substring(0, viewPath.LastIndexOf("\\"));
        }
        #endregion

        #region private classes that implement further interfaces
        class DataSourceView : HierarchicalDataSourceView
        {
            HierarchicalDataSet hDataSet;
            string viewPath;
            private long rootIdVal;

            public DataSourceView(HierarchicalDataSet hDataSet, string viewPath, long rootIdVal)
            {
                this.hDataSet = hDataSet;
                this.viewPath = viewPath;
                this.rootIdVal = rootIdVal;
            }

            public override IHierarchicalEnumerable Select()
            {
                return new HierarchicalEnumerable(hDataSet, viewPath, rootIdVal);
            }
        }

        class HierarchicalEnumerable : IHierarchicalEnumerable
        {
            HierarchicalDataSet hDataSet;
            string viewPath;
            private long rootIdVal;

            public HierarchicalEnumerable(HierarchicalDataSet hDataSet, string viewPath, long rootIdVal)
            {
                this.hDataSet = hDataSet;
                this.viewPath = viewPath;
                this.rootIdVal = rootIdVal;
            }

            public IHierarchyData GetHierarchyData(object enumeratedItem)
            {
                DataRowView row = (DataRowView)enumeratedItem;
                return new HierarchyData(hDataSet, viewPath, row, rootIdVal);
            }

            public IEnumerator GetEnumerator()
            {
                if (viewPath == "")
                    hDataSet.dataSet.Tables[0].DefaultView.RowFilter = String.Format("{0} = " + rootIdVal, hDataSet.parentIdColumnName);
                else
                {
                    string lastID = viewPath.Substring(viewPath.LastIndexOf("\\") + 1);
                    hDataSet.dataSet.Tables[0].DefaultView.RowFilter = String.Format("{0} = {1}", hDataSet.parentIdColumnName, lastID);
                }

                IEnumerator i = hDataSet.dataSet.Tables[0].DefaultView.GetEnumerator();
                hDataSet.dataSet.Tables[0].DefaultView.RowFilter = "";
                return i;
            }
        }

        class HierarchyData : IHierarchyData
        {
            HierarchicalDataSet hDataSet;
            DataRowView row;
            string viewPath;
            private long rootIdVal;

            public HierarchyData(HierarchicalDataSet hDataSet, string viewPath, DataRowView row, long rootIdVal)
            {
                this.hDataSet = hDataSet;
                this.viewPath = viewPath;
                this.row = row;
                this.rootIdVal = rootIdVal;
            }

            public IHierarchicalEnumerable GetChildren()
            {
                return new HierarchicalEnumerable(hDataSet, hDataSet.GetChildrenViewPath(viewPath, row), rootIdVal);
            }

            public IHierarchyData GetParent()
            {
                return new HierarchyData(hDataSet, hDataSet.GetParentViewPath(viewPath), hDataSet.GetParentRow(row), rootIdVal);
            }

            public bool HasChildren
            {
                get
                {
                    return hDataSet.HasChildren(row);
                }
            }

            public object Item
            {
                get
                {
                    return row;
                }
            }

            public string Path
            {
                get
                {
                    return viewPath;
                }
            }

            public string Type
            {
                get
                {
                    return typeof(DataRowView).ToString();
                }
            }
        }
        #endregion
    }
}