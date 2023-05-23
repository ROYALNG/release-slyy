using GHIBMS.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;

public class ListViewEx : ListView
{
    #region 虚拟模式相关操作
    //用于比较的类
    private static CaseInsensitiveComparer ObjectCompare = new CaseInsensitiveComparer();
    ///<summary>
    /// 前台行集合
    ///</summary>
    public List<ListViewItem> CurrentCacheItemsSource;

    public ListViewEx()
    {

        this.SetStyle(ControlStyles.OptimizedDoubleBuffer,//|
                                                          // ControlStyles.AllPaintingInWmPaint, //|
                                                          //ControlStyles.UserPaint,
            true);

        this.CurrentCacheItemsSource = new List<ListViewItem>();
        this.VirtualMode = true;
        this.RetrieveVirtualItem += new RetrieveVirtualItemEventHandler(listView_RetrieveVirtualItem);
    }


    /// <summary>
    /// 新增项
    /// </summary>
    /// <param name="item"></param>
    public void Add(ListViewItem item)
    {
        try
        {
            if (item == null) return;
            this.CurrentCacheItemsSource.Add(item);
            //需要用户手动刷新this.Invalidate()，用于实现批量增加一次刷新
            //this.VirtualListSize = this.CurrentCacheItemsSource.Count;
            //this.Invalidate();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }

    }
    public void Insert(int index, ListViewItem item)
    {
        try
        {
            if (item == null) return;
            this.CurrentCacheItemsSource.Insert(index, item);
            this.VirtualListSize = this.CurrentCacheItemsSource.Count;
            //this.Invalidate();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }

    }
    public new void Invalidate()
    {

        this.VirtualListSize = this.CurrentCacheItemsSource.Count;
        base.Invalidate();
    }
    public new void Clear()
    {
        base.Clear();
        this.VirtualListSize = 0;
        this.CurrentCacheItemsSource.Clear();
        this.Invalidate();
        GC.Collect();

    }
    public void SelectedAll()
    {
        foreach (ListViewItem item in CurrentCacheItemsSource)
        {
            item.EnsureVisible();
            item.Selected = true;

        }
    }
    public void RemoveAll()
    {
        this.VirtualListSize = 0;
        CurrentCacheItemsSource.Clear();
    }
    public void SelectedNo()
    {
        foreach (ListViewItem item in CurrentCacheItemsSource)
        {
            item.EnsureVisible();
            item.Selected = false;
        }
    }

    ///<summary>
    /// 虚拟模式事件
    ///</summary>
    ///<param name="sender"></param>
    ///<param name="e"></param>
    private void listView_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
    {
        try
        {
            if (this.CurrentCacheItemsSource == null || this.CurrentCacheItemsSource.Count == 0 || e.ItemIndex >= this.CurrentCacheItemsSource.Count)
            {
                return;
            }

            ListViewItem lv = this.CurrentCacheItemsSource[e.ItemIndex];
            e.Item = lv;
            // Debug.WriteLine("listView_RetrieveVirtualItem e.ItemIndex:"+e.ItemIndex);
        }
        catch (System.Exception ex)
        {

            Console.WriteLine(ex.ToString());
        }

    }

    ///<summary>
    /// 获取选中的第一行的指定tag值
    ///</summary>
    ///<param name="key"></param>
    ///<returns></returns>
    public string FirstSelectItemValue(string key)
    {
        int i = GetColumnsIndex(key);
        if (this.SelectedIndices.Count > 0)
        {
            return this.CurrentCacheItemsSource[this.SelectedIndices[0]].SubItems[i].Text;
        }
        else
            return "";
    }

    ///<summary>
    /// 获取列名的索引
    ///</summary>
    ///<param name="key"></param>
    ///<returns></returns>
    public int GetColumnsIndex(string key)
    {
        int i = 0;
        for (; i < this.Columns.Count; i++)
        {
            if (this.Columns[i].Name == key)
            {
                break;
            }
        }

        return i;
    }
    public int GetItemsConut()
    {
        return this.CurrentCacheItemsSource.Count;
    }

    ///<summary>
    /// 获取选中项
    ///</summary>
    ///<returns></returns>
    public List<ListViewItem> GetSelectItem()
    {
        List<ListViewItem> l = new List<ListViewItem>();
        foreach (var item in this.SelectedIndices)
        {
            l.Add(this.CurrentCacheItemsSource[int.Parse(item.ToString())]);
        }
        return l;

        //List<ListViewItem> l = new List<ListViewItem>();
        //try
        //{
        //    foreach (var item in this.CurrentCacheItemsSource)
        //    {
        //        if (item.Selected)
        //            l.Add(item);
        //    }


        //}
        //catch (System.Exception ex)
        //{

        //    Console.WriteLine(ex.ToString());
        //}
        // return l;

    }
    ///<summary>
    /// 获取选中项
    ///</summary>
    ///<returns></returns>
    public ListViewItem GetFirstSelectItem()
    {
        ListViewItem lvs = null;
        foreach (var item in this.SelectedIndices)
        {
            lvs = this.CurrentCacheItemsSource[int.Parse(item.ToString())];
            break;
        }
        return lvs;
    }
    public void Remove(ListViewItem item)
    {
        this.VirtualListSize = CurrentCacheItemsSource.Count - 1;
        this.CurrentCacheItemsSource.Remove(item);

        //this.Invalidate();

    }
    public void RemoveSelects()
    {

        try
        {
            List<ListViewItem> delItems = GetSelectItem();
            if (delItems.Count == CurrentCacheItemsSource.Count)
            {
                RemoveAll();
                delItems.Clear();
                return;
            }
            this.VirtualListSize = this.CurrentCacheItemsSource.Count - delItems.Count;

            for (int i = 0; i < delItems.Count; i++)
            {
                delItems[i].Selected = false;
            }
            delItems.Clear();
            this.Invalidate();
        }
        catch (Exception ex)
        {
            Logger.GetInstance().LogError(ex.ToString());
        }
    }
    public void RemoveAt(int index)
    {
        if (this.CurrentCacheItemsSource.Count > index)
        {
            this.CurrentCacheItemsSource.RemoveAt(index);
            this.VirtualListSize = this.CurrentCacheItemsSource.Count;
        }

    }

    public ListViewItem SearchListViewItemByText(string text)
    {

        foreach (ListViewItem item in CurrentCacheItemsSource)
        {
            if (item.SubItems[1].Text == text)
            {
                return item;

            }
        }
        return null;

    }

    ///<summary>
    /// 获取选中行的某列集合
    ///</summary>
    ///<param name="key"></param>
    ///<returns></returns>
    public List<string> GetListViewField(string key)
    {
        List<string> ids = new List<string>();

        foreach (var item in this.SelectedIndices)
        {
            string id = this.CurrentCacheItemsSource[int.Parse(item.ToString())].SubItems[GetColumnsIndex(key)].Text;
            ids.Add(id);
        }
        return ids;
    }

    private ListViewItemComparer mySorter;
    ///<summary>
    /// 排序
    ///</summary>
    ///<param name="e"></param>
    protected override void OnColumnClick(ColumnClickEventArgs e)
    {
        base.OnColumnClick(e);

        //string dbType = this.Columns[e.Column].Tag.ToString();

        if (this.mySorter == null)
        {
            this.mySorter = new ListViewItemComparer(e.Column, SortOrder.Ascending/*, dbType*/);
        }
        else
        {
            if (this.mySorter.SortColumn == e.Column)
            {
                if (this.mySorter.Order == SortOrder.Ascending)
                {
                    this.mySorter.Order = SortOrder.Descending;
                }
                else
                {
                    this.mySorter.Order = SortOrder.Ascending;
                }
            }
            else
            {
                this.mySorter.SortColumn = e.Column;
                this.mySorter.Order = SortOrder.Ascending;
            }

            /*this.mySorter.DbType = dbType;*/

            this.CurrentCacheItemsSource.Sort(this.mySorter);

            this.Invalidate();
        }
    }
    #endregion

    #region 普通模式下排序
    /*普通模式下排序
        public void ReLoadColumn()
        {
            this.ListViewItemSorter = new ListViewItemComparer(0, SortOrder.Ascending, this.Columns[0].Tag.ToString());
        }

        protected override void OnColumnClick(ColumnClickEventArgs e)
        {
            base.OnColumnClick(e);

            string dbType = this.Columns[e.Column].Tag.ToString();

            if (this.ListViewItemSorter == null)
            {
                this.ListViewItemSorter = new ListViewItemComparer(e.Column, SortOrder.Ascending, dbType);
            }
            else
            {
                ListViewItemComparer comparer = this.ListViewItemSorter as ListViewItemComparer;
                if (comparer.SortColumn == e.Column)
                {
                    if (comparer.Order == SortOrder.Ascending)
                    {
                        comparer.Order = SortOrder.Descending;
                    }
                    else
                    {
                        comparer.Order = SortOrder.Ascending;
                    }
                }
                else
                {
                    comparer.SortColumn = e.Column;
                    comparer.Order = SortOrder.Ascending;
                }

                //MessageBox.Show(dbType);

                comparer.DbType = dbType;

                //仅仅改变了ListViewItemSorter属性值，这里不会自动调用Sort()方法，需要显式指定执行Sort()方法实现排序。
                this.Sort();
            }
        }
        */
    #endregion

    #region ListView排序逻辑
    ///<summary>
    /// ListView排序逻辑
    ///</summary>
    private class ListViewItemComparer : System.Collections.Generic.IComparer<ListViewItem>
    {
        // public string DbType;
        public ListViewItemComparer()
        {
            this.SortColumn = 0;
            this.Order = SortOrder.None;
        }

        public ListViewItemComparer(int column)
            : this()
        {
            this.SortColumn = column;
        }

        ///<summary>
        /// 
        ///</summary>
        ///<param name="column">哪列</param>
        ///<param name="sortOrder">排序方式</param>
        ///<param name="dbType">类型</param>
        public ListViewItemComparer(int column, SortOrder sortOrder/*, string dbType*/)
            : this(column)
        {
            Order = sortOrder;
            //DbType = dbType.ToLower();
        }

        #region IComparer 成员
        public int Compare(ListViewItem x, ListViewItem y)
        {
            int result = 0;
            string c1 = "";
            string c2 = "";

            try
            {
                c1 = x.SubItems[this.SortColumn].Text;
                c2 = y.SubItems[this.SortColumn].Text;

            }
            catch
            {
                //    MessageBox.Show(ex.Message);
                return 0;
            }
            /*
        switch (DbType)
        {
            case "int":
                result = pubFun.IsInt(c1, 0) - pubFun.IsInt(c2, 0);
                break;

            case "datetime":
                DateTime t1 = pubFun.IsDate(c1, DateTime.MinValue);
                DateTime t2 = pubFun.IsDate(c2, DateTime.MinValue);

                if (DateTime.TryParse(c1, out t1) && DateTime.TryParse(c2, out t2))
                {
                    int cha1 = pubFun.IsInt((t1 - t2).TotalSeconds.ToString(), 0);
                    result = pubFun.IsInt((t1 - t2).TotalSeconds.ToString(), 0);

                    if (cha1 == 0)
                        result = 0;
                    else if (cha1 < 0)
                        result = -1;
                    else
                        result = 1;
                }
                else
                {
                    result = string.Compare(c1, c2);
                }

                break;

            case "double":
                double d1 = pubFun.IsDouble(c1, 0);
                double d2 = pubFun.IsDouble(c2, 0);

                if (d1 == d2)
                    result = 0;
                else if (d1 < d2)
                    result = -1;
                else
                    result = 1;
                break;

            default:
                result = string.Compare(c1, c2);
                break;
        }*/
            //数字列，修复数字列排序不对的问题
            if (Regex.IsMatch(c1, @"^\d+(\.\d+)?$"))
                result = ObjectCompare.Compare(pubFun.IsNumeric(c1), pubFun.IsNumeric(c2));
            else
                result = ObjectCompare.Compare(c1, c2);
            if (this.Order == SortOrder.Descending)
                return -result;
            else if (this.Order == SortOrder.Ascending)
                return result;
            else
                return 0;
        

        }
        #endregion

        ///<summary>
        /// 当前排序列
        ///</summary>
        public int SortColumn
        {
            get;
            set;
        }

        ///<summary>
        /// 当前列排序方式
        ///</summary>
        public SortOrder Order
        {
            get;
            set;
        }
    }
    #endregion
}