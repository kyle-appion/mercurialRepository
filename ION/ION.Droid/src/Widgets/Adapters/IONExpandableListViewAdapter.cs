namespace ION.Droid.Widgets.Adapters {

  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;

  using Android.App;
  using Android.Content;
  using Android.OS;
  using Android.Runtime;
  using Android.Views;
  using Android.Widget;

  /// <summary>
  /// This adapter is a wrapper around a BaseExpandableListAdapter that will
  /// provide a common foundation implementation for list adapters. A great
  /// deal of support and features are provided by this adapter, making sub-
  /// classes not need to worry about trivial things such as updating a
  /// particular row or organizing the children of the adapter.
  /// </summary>
  /// <para>
  /// Something to note about the IONExpandableListViewAdapter: it is
  /// designed to be the ONLY adapter that you ever create for a list
  /// view. This is why it needs a list view as a constructor parameter. Failure
  /// to abide by one adapter, one list view demand will cause the list view to
  /// display quite unexpected results.
  /// </para>
  /// <para>
  /// Unless said otherwise, all items within this adapter are not considered
  /// to have stable ids.
  /// </para>
  /// <para>
  /// The only methods that this adapter does not address are the actual view
  /// inflation methods.
  /// </para>
  public abstract class IONExpandableListViewAdapter<Group, Child> : BaseExpandableListAdapter, Handler.ICallback {
    /// <summary>
    /// The message that will update a group row within the adapter. Arg0 is the
    /// property that is necessary for this message to work. Arg0 is the group
    /// row.
    /// </summary>
    private const int MSG_UPDATE_GROUP_ROW = 1;
    /// <summary>
    /// The message that will update a child row within the adapter. Both Arg0
    /// and Arg1 are necessary for this message to work. Arg0 is the group the
    /// child belongs to and Arg1 is the child row within the group.
    /// </summary>
    private const int MSG_UPDATE_CHILD_ROW = 2;

    // Overridden from BaseExpandableListAdapter
    public override int GroupCount {
      get {
        return __groups.Count;
      }
    }

    // Overridden from BaseExpandableListAdapter
    public override bool HasStableIds {
      get {
        return false;
      }
    }

    /// <summary>
    /// Queries a group with the given index.
    /// </summary>
    /// <param name="groupPosition"></param>
    /// <returns></returns>
    public Group this[int groupPosition] {
      get {
        return __groups[groupPosition];
      }
    }

    /// <summary>
    /// Queries a child under the given group at the given position.
    /// </summary>
    /// <param name="groupPosition"></param>
    /// <param name="childPosition"></param>
    /// <returns></returns>
    public Child this[int groupPosition, int childPosition] {
      get {
        return __children[__groups[groupPosition]][childPosition];
      }
    }

    /// <summary>
    /// Queries a child under the given group.
    /// </summary>
    /// <param name="group"></param>
    /// <returns></returns>
    public Child this[Group group, int childPosition] {
      get {
        return __children[group][childPosition];
      }
    }

    /// <summary>
    /// The list view that this adapter is providing content for.
    /// </summary>
    public ExpandableListView list { get; set; }

    /// <summary>
    /// Queries the Android context that the adapter is living in.
    /// </summary>
    public Context context {
      get {
        return list.Context;
      }
    }

    /// <summary>
    /// The list of all the groups that are within the adapter.
    /// </summary>
    private List<Group> __groups = new List<Group>();
    /// <summary>
    /// The dictionary that will store all of the children within the group. We
    /// store the children in a dictionary as opposed to a list for one simple
    /// reason: sorting the groups. With this method, we can sort the groups,
    /// and not botch the ordering of the children.
    /// </summary>
    private Dictionary<Group, List<Child>> __children = new Dictionary<Group, List<Child>>();
    /// <summary>
    /// The handler that we will post updates to.
    /// </summary>
    private Handler __handler;

    /// <summary>
    /// Creates a new IONExpandableListViewAdapter. The adapter will automatically
    /// bind itself to the provided list.
    /// </summary>
    /// <param name="list"></param>
    public IONExpandableListViewAdapter(ExpandableListView list) {
      this.list = list;
      this.list.SetAdapter(this);
      __handler = new Handler(this);
    }

    // Overridden from BaseExpandableListAdapter
    public override bool AreAllItemsEnabled() {
      return true;
    }

    // Overridden from BaseExpandableListAdapter
    public override Java.Lang.Object GetGroup(int groupPosition) {
      throw new NotImplementedException("Xamarin is a shit company with shit engineers who are too lazy to properly bind shit. Use the indexer [int groupPosition] instead.");
    }

    // Overridden from BaseExpandableListAdapter
    public override long GetGroupId(int groupPosition) {
      return (long)groupPosition;
    }

    // Overridden from BaseExpandableListAdapter
    public override Java.Lang.Object GetChild(int groupPosition, int childPosition) {
      throw new NotImplementedException("Xamarin is a shit company with shit engineers who are too lazy to properly bind shit. Use the indexer [int groupPosition, int childPosition] instead.");
    }

    /// <summary>
    /// Returns the id that can be used to describe a child. This is simply the
    /// group ORed into the lower half of a long and the child position ORed into
    /// the higher half.
    /// </summary>
    /// <param name="groupPosition"></param>
    /// <param name="childPosition"></param>
    /// <returns></returns>
    // Overridden from BaseExpandableListAdapter
    public override long GetChildId(int groupPosition, int childPosition) {
      return GetGroupId(groupPosition) | (long)(childPosition << 32);
    }

    // Overridden from BaseExpandableListAdapter
    public override int GetChildrenCount(int groupPosition) {
      return __children[__groups[groupPosition]].Count;
    }

    // Overridden from BaseExpandableListAdapter
    public bool HandleMessage(Message msg) {
      switch (msg.What) {
        case MSG_UPDATE_GROUP_ROW: {
          UpdateGroup(msg.Arg1);
          return true;
        }
        case MSG_UPDATE_CHILD_ROW: {
          UpdateChild(msg.Arg1, msg.Arg2);
          return true;
        }
        default: {
          return false;
        }
      }
    }

    /// <summary>
    /// Always returns true.
    /// </summary>
    /// <param name="groupPosition"></param>
    /// <param name="childPosition"></param>
    /// <returns></returns>
    /// // Overridden from BaseExpandableListAdapter
    public override bool IsChildSelectable(int groupPosition, int childPosition) {
      return true;
    }

    /// <summary>
    /// called when the adapter is cleared.
    /// </summary>
    public virtual void OnClear() {
      // Nope
    }

    /// <summary>
    /// Queries the index of the group within the adapter.
    /// </summary>
    /// <param name="group"></param>
    /// <returns></returns>
    public int IndexOf(Group group) {
      return __groups.IndexOf(group);
    }

    /// <summary>
    /// Queries the index of the child within the given group.
    /// </summary>
    /// <param name="group"></param>
    /// <param name="child"></param>
    /// <returns></returns>
    public int IndexOf(Group group, Child child) {
      return __children[group].IndexOf(child);
    }

    /// <summary>
    /// Posts an update that will update the given group view.
    /// </summary>
    /// <param name="group"></param>
    public void PostUpdate(Group group) {
      PostUpdate(IndexOf(group));
    }

    /// <summary>
    /// Posts an update that will update the given child row.
    /// </summary>
    /// <param name="group"></param>
    /// <param name="child"></param>
    public void PostUpdate(Group group, Child child) {
      PostUpdate(IndexOf(group), IndexOf(group, child));
    }

    /// <summary>
    /// Posts an update that will update the given group view.
    /// </summary>
    /// <param name="groupPosition"></param>
    public void PostUpdate(int groupPosition) {
      __handler.SendMessage(__handler.ObtainMessage(MSG_UPDATE_GROUP_ROW, groupPosition, 0));
      // TODO ahodder@appioninc.com: Update the group row.
    }

    /// <summary>
    /// Post an update that will update the given child.
    /// </summary>
    /// <param name="groupPosition"></param>
    /// <param name="childPosition"></param>
    public void PostUpdate(int groupPosition, int childPosition) {
      // TODO ahodder@appioninc.com: Update the child row.
      __handler.SendMessage(__handler.ObtainMessage(MSG_UPDATE_CHILD_ROW, groupPosition, childPosition));
    }

    /// <summary>
    /// Queries whether or not the adapter contains the given group.
    /// </summary>
    /// <param name="group"></param>
    /// <returns></returns>
    public bool ContainsGroup(Group group) {
      return __groups.Contains(group);
    }

    /// <summary>
    /// Queries whether or not the adapter contains the given child.
    /// </summary>
    /// <param name="group"></param>
    /// <param name="child"></param>
    /// <returns></returns>
    public bool ContainsChild(Group group, Child child) {
      if (ContainsGroup(group)) {
        return __children[group].Contains(child);
      } else {
        return false;
      }
    }

    /// <summary>
    /// Attempts to add the group to the adapter.
    /// </summary>
    /// <param name="group"></param>
    /// <returns>True if the group was added, false otherwise.</returns>
    public bool Add(Group group) {
      return Add(group, GroupCount);
    }

    /// <summary>
    /// Attempts to insert the group at the given index. If the index would place
    /// the group out of bounds, then the group will be added to the end.
    /// </summary>
    /// <param name="group"></param>
    /// <param name="index"></param>
    /// <returns>True if the group was added, false otherwise.</returns>
    public bool Add(Group group, int index) {
      if (ContainsGroup(group)) {
        return false;
      }

      if (index > GroupCount) {
        __groups.Add(group);
      } else {
        __groups.Insert(index, group);
      }

      // Ensure that we have a list for the groups children.
      if (!__children.ContainsKey(group)) {
        __children[group] = new List<Child>();
      }

      // Notify the list view that we changed.
      NotifyDataSetChanged();

      return true;
    }

    /// <summary>
    /// Attempts to add the child to the adapter.
    /// </summary>
    /// <param name="group"></param>
    /// <param name="child"></param>
    /// <returns>True if the child was added, false otherwise.</returns>
    public bool Add(Group group, Child child) {
      return Add(group, child, GetChildrenCount(IndexOf(group)));
    }

    /// <summary>
    /// Attempts to insert the child at the given index within its group. If
    /// the index would place the child out of bounds, then the child will be
    /// added to the end.
    /// </summary>
    /// <param name="group"></param>
    /// <param name="child"></param>
    /// <param name="index"></param>
    /// <returns>True if the group was added, false otherwise.</returns>
    public bool Add(Group group, Child child, int index) {
      if (ContainsChild(group, child)) {
        return false;
      }

      List<Child> list = __children[group];

      if (index > list.Count) {
        list.Add(child);
      } else {
        list.Insert(index, child);
      }

      // Notify the list view that we changed.
      NotifyDataSetChanged();

      return true;
    }

    /// <summary>
    /// Attempts to add all of the given groups to the adapter.
    /// </summary>
    /// <param name="groups"></param>
    /// <returns>A list of all the groups that were not added to the adapter.</returns>
    public List<Group> AddAll(ICollection<Group> groups) {
      List<Group> ret = new List<Group>();

      foreach (Group group in groups) {
        if (!Add(group)) {
          ret.Add(group);
        }
      }

      return ret;
    }

    /// <summary>
    /// Attempts to add all of the children to the adapter.
    /// </summary>
    /// <param name="group"></param>
    /// <param name="children"></param>
    /// <returns>A list of all the children that were not added to the adapter.</returns>
    public List<Child> AddAll(Group group, ICollection<Child> children) {
      List<Child> ret = new List<Child>();

      foreach (Child child in children) {
        if (! Add(group, child)) {
          ret.Add(child);
        }
      }

      return ret;
    }

    /// <summary>
    /// Clears the adapter of all content.
    /// </summary>
    public void Clear() {
      __groups.Clear();
      __children.Clear();
      // Notify the list view that we changed.
      NotifyDataSetChanged();
    }

    /// <summary>
    /// Removes the group and all of its children from the adapter.
    /// </summary>
    /// <param name="group"></param>
    public void Remove(Group group) {
      __groups.Remove(group);
      __children.Remove(group);
      // Notify the list view that we changed.
      NotifyDataSetChanged();
    }

    /// <summary>
    /// Removes the child from the adapter.
    /// </summary>
    /// <param name="group"></param>
    /// <param name="child"></param>
    public void Remove(Group group, Child child) {
      __children[group].Remove(child);
      // Notify the list view that we changed.
      NotifyDataSetChanged();
    }

    /// <summary>
    /// Sets the children for the given group to the given collection.
    /// </summary>
    /// <param name="group"></param>
    /// <param name="children"></param>
    public void Set(Group group, ICollection<Child> children) {
      List<Child> clist = __children[group];
      clist.Clear();

      foreach (Child child in children) {
        clist.Add(child);
      }

      // Notify the list view that we changed.
      NotifyDataSetChanged();
    }

    /// <summary>
    /// Sets the children of the given group to exactly the contents of the
    /// children collection. However, if any of the given children are already
    /// present in the adapter, we will attempt to keep the ordering. The result
    /// of this is an adapter whose content can be drastically changed, and
    /// common items will work themselves lower in the ordering.
    /// </summary>
    /// <remarks>
    /// Note: if the given group is NOT present within the adapter, we will add
    /// it.
    /// </remarks>
    /// <param name="group"></param>
    /// <param name="children"></param>
    public void SetRetainOrder(Group group, ICollection<Child> children) {
      // Its simpler to just have the add check whether or not the group is there.
      Add(group);

      List<Child> clist = __children[group];

      for (int i = clist.Count - 1; i >= 0; i--) {
        Child child = children.First();

        if (!children.Contains(child)) {
          clist.RemoveAt(i);
        }

        children.Remove(child);
      }

      if (children.Count > 0) {
        foreach (Child child in children) {
          clist.Add(child);
        }
      }

      // Notify the list view that we changed.
      NotifyDataSetChanged();
    }

    /// <summary>
    /// Queries the first group that the given child is apart of.
    /// </summary>
    /// <param name="child"></param>
    /// <returns>
    /// The group that the child is apart of or null if the child is not part of
    /// a group.
    /// </returns>
    public Group GetGroupFor(Child child) {
      foreach (Group group in __groups) {
        if (__children[group].Contains(child)) {
          return group;
        }
      }

      return default(Group);
    }

    /// <summary>
    /// Queries the packed position of the given group.
    /// </summary>
    /// <param name="group"></param>
    /// <returns></returns>
    public long GetPackedPositionFor(Group group) {
      return ExpandableListView.GetPackedPositionForGroup(IndexOf(group));
    }

    /// <summary>
    /// Queries the packed position of the given child.
    /// </summary>
    /// <param name="group"></param>
    /// <param name="child"></param>
    /// <returns></returns>
    public long GetPackedPositionFor(Group group, Child child) {
      return ExpandableListView.GetPackedPositionForChild(IndexOf(group), IndexOf(group, child));
    }

    /// <summary>
    /// Queries the flat position of the child at the given position.
    /// </summary>
    /// <param name="groupPosition"></param>
    /// <param name="childPosition"></param>
    /// <returns></returns>
    public int GetFlatPositionFor(int groupPosition, int childPosition) {
      return list.GetFlatListPosition(ExpandableListView.GetPackedPositionForChild(groupPosition, childPosition));
    }

    /// <summary>
    /// Gets the group position from the flat position.
    /// </summary>
    /// <param name="flatPosition"></param>
    /// <returns></returns>
    public int GetUnpackedGroupPosition(int flatPosition) {
      return ExpandableListView.GetPackedPositionGroup(list.GetExpandableListPosition(flatPosition));
    }

    /// <summary>
    /// Gets the chilsd position form the flat position.
    /// </summary>
    /// <param name="flatPosition"></param>
    /// <returns></returns>
    public int GetUnpackedChildPosition(int flatPosition) {
      return ExpandableListView.GetPackedPositionChild(list.GetExpandableListPosition(flatPosition));
    }


    /// <summary>
    /// Updates the group row view.
    /// </summary>
    /// <param name="groupPosition"></param>
    private void UpdateGroup(int groupPosition) {
      if (groupPosition >= 0 && groupPosition < GroupCount) {
        int flatPosition = list.GetFlatListPosition(ExpandableListView.GetPackedPositionForGroup(groupPosition));
        int first = list.FirstVisiblePosition;
        View convert = list.GetChildAt(flatPosition - first);
        GetGroupView(groupPosition, list.IsGroupExpanded(groupPosition), convert, list);
      }
    }

    /// <summary>
    /// Updates the child row view.
    /// </summary>
    /// <param name="groupPosition"></param>
    /// <param name="childPosition"></param>
    private void UpdateChild(int groupPosition, int childPosition) {
      if ((groupPosition >= 0 && groupPosition < GroupCount) &&
          (childPosition >= 0 && childPosition < GetChildrenCount(groupPosition))) {
        int flatPosition = list.GetFlatListPosition(ExpandableListView.GetPackedPositionForChild(groupPosition, childPosition));
        int first = list.FirstVisiblePosition;
        View convert = list.GetChildAt(flatPosition - first);
        GetChildView(groupPosition, childPosition, list.IsGroupExpanded(childPosition), convert, list);
      }
    }
  }
}