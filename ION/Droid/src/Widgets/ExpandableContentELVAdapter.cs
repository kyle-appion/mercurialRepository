using System;
using System.Collections.Generic;

using Android.Animation;
using Android.Content;
using Android.Views;
using Android.Widget;

using ION.Droid.Animation;

namespace ION.Droid.Widgets {
  /// <summary>
  /// An adapter for an ExpandingListView that allows its children to expand.
  /// </summary>
  public abstract class ExpandableContentELVAdapter<Group, Child> : IONExpandableListViewAdapter<Group, Child> {
    /// <summary>
    /// A delegate that is notified when the adapter expands or collapses a child.
    /// </summary>
    /// <param name="groupPosition"></param>
    /// <param name="childPosition"></param>
    /// <param name="expanded"></param>
    public delegate void OnExpandEvent(int groupPosition, int childPosition, bool expanded);
    /// <summary>
    /// An event handler that allows for subscriptions to content expansions.
    /// </summary>
    public event OnExpandEvent onExpandEvent;

    /// <summary>
    /// Indicates that not action id has been provided for the content expansion.
    /// </summary>
    private const int NO_ACTION_ID = 0;
    /// <summary>
    /// Indicates that no layout has been provided for use in auto-inflation.
    /// </summary>
    private const int NO_LAYOUT = 0;
    /// <summary>
    /// Indicates that there is not limit in the number of items that may
    /// be expanded at one time.
    /// </summary>
    private const int NO_LIMIT = 0;

    /// <summary>
    /// The number of items that can be expanded at a single time. Defaults to
    /// NO_LIMIT.
    /// </summary>
    public int expansionLimit {
      get {
        return __expansionLimit;
      }
      set {
        if (value < NO_LIMIT) {
          __expansionLimit = NO_LIMIT;
        } else {
          __expansionLimit = value;
        }

        if (__expansionLimit != NO_LIMIT) {
          while (__expansionLimit < __expandedChildren.Count) {
            __expandedChildren.RemoveFirst();
          }
        }

        // Notify the list view that we changed.
        NotifyDataSetChanged();
      } 
    } int __expansionLimit;

    /// <summary>
    /// The number of children that are expanded.
    /// </summary>
    public int expansionCount {
      get {
        return __expandedChildren.Count;
      }
    }

    /// <summary>
    /// The collection that will hold the children that are currently expanded.
    /// Note: the count of this collection will not exceed the expansion limit.
    /// </summary>
    private LinkedList<Child> __expandedChildren = new LinkedList<Child>();

    /// <summary>
    /// The resource id that is used to get the action (content toggle) if for
    /// the title view.
    /// </summary>
    private int __actionViewId;
    /// <summary>
    /// The resource layout that is used to auto-inflate the title layout.
    /// </summary>
    private int __titleLayout;
    /// <summary>
    /// The resource layout that is used to auto-inflate the content layout.
    /// </summary>
    private int __contentLayout;

    public ExpandableContentELVAdapter(ExpandableListView list) :
      this(list, NO_ACTION_ID, NO_LAYOUT, NO_LAYOUT) {
      // Nope
    }

    public ExpandableContentELVAdapter(ExpandableListView list, int titleLayout, int contentLayout) :
      this(list, NO_ACTION_ID, titleLayout, contentLayout) {
      // Nope
    }

    public ExpandableContentELVAdapter(ExpandableListView list, int actionViewId, int titleLayout, int contentLayout) : base(list) {
      __actionViewId = actionViewId;
      __titleLayout = titleLayout;
      __contentLayout = contentLayout;

      expansionLimit = NO_LIMIT;
    }

    // Overridden from IONExpandableListViewAdapter
    public override View GetChildView(int groupPosition, int childPosition, bool expanded, View convert, ViewGroup mommy) {
      RootView ret;

      if (convert is RootView) {
        ret = (RootView)convert;
      } else {
        ret = new RootView(context);

        if (NO_LAYOUT != __titleLayout) {
          ret.titleView = LayoutInflater.From(context).Inflate(__titleLayout, ret, false);
        }

        if (NO_ACTION_ID == __actionViewId) {
          ret.titleLayout.SetOnClickListener(new TitleClickHandler<Group, Child>(this, ret));
        } else {
          ret.titleLayout.FindViewById(__actionViewId).SetOnClickListener(new TitleClickHandler<Group, Child>(this, ret));
        }

        if (NO_LAYOUT != __contentLayout) {
          ret.contentView = LayoutInflater.From(context).Inflate(__contentLayout, ret, false);
        }
      }

      ret.position = GetFlatPositionFor(groupPosition, childPosition);
      ret.titleView = GetTitleView(groupPosition, childPosition, ret.titleView, ret.titleLayout);
      ret.contentView = GetContentView(groupPosition, childPosition, ret.contentView, ret.contentLayout);

      if (IsExpanded(groupPosition, childPosition)) {
        ret.contentLayout.Visibility = ViewStates.Visible;
      } else {
        ret.contentLayout.Visibility = ViewStates.Gone;
      }

      return ret;
    }

    // Overridden from IONExpandableListViewAdapter
    public override void OnClear() {
      base.Clear();
      __expandedChildren.Clear();
    }

    /// <summary>
    /// Called by the ExpandableContentELVAdapter when it needs to create and/or
    /// update a title view.
    /// </summary>
    /// <param name="groupPosition"></param>
    /// <param name="childPosition"></param>
    /// <param name="convert"></param>
    /// <param name="mommy"></param>
    /// <returns></returns>
    public abstract View GetTitleView(int groupPosition, int childPosition, View convert, ViewGroup mommy);

    /// <summary>
    /// Called by the ExpandableContentELVAdapter when is needs to create and/or
    /// update a content view.
    /// </summary>
    /// <param name="groupPosition"></param>
    /// <param name="childPosition"></param>
    /// <param name="convert"></param>
    /// <param name="mommy"></param>
    /// <returns></returns>
    public abstract View GetContentView(int groupPosition, int childPosition, View convert, ViewGroup mommy);

    /// <summary>
    /// Queries whether or not the given child the ExpandableContentELVAdapter
    /// is expanded.
    /// </summary>
    /// <param name="groupPosition"></param>
    /// <param name="childPosition"></param>
    /// <returns></returns>
    public bool IsExpanded(int groupPosition, int childPosition) {
      return __expandedChildren.Contains(this[groupPosition, childPosition]);
    }

    /// <summary>
    /// Expands the given child.
    /// </summary>
    /// <param name="groupPosition"></param>
    /// <param name="childPosition"></param>
    public void Expand(int groupPosition, int childPosition) {
      if (!IsExpanded(groupPosition, childPosition)) {
        Toggle(groupPosition, childPosition);
      }
    }

    public void Collapse(int groupPosition, int childPosition) {
      if (IsExpanded(groupPosition, childPosition)) {
        Toggle(groupPosition, childPosition);
      }
    }

    /// <summary>
    /// An intermediary that will extract a root view from the given positions
    /// and then toggle the content of the view.
    /// </summary>
    /// <param name="groupPosition"></param>
    /// <param name="childPosition"></param>
    private void Toggle(int groupPosition, int childPosition) {
      if (!list.IsGroupExpanded(groupPosition)) {
        return; // We can't toggle; there is nothing to toggle.
      }

      bool isExpanded = IsExpanded(groupPosition, childPosition);

      int flatPosition = list.GetFlatListPosition(ExpandableListView.GetPackedPositionForChild(groupPosition, childPosition));
      int first = list.FirstVisiblePosition;

      RootView view = (RootView)list.GetChildAt(flatPosition - first);

      if (view != null) {
        Toggle(view);
      }

      if (view == null && isExpanded) {
        __expandedChildren.Remove(this[groupPosition, childPosition]);
      } else {
        __expandedChildren.AddLast(this[groupPosition, childPosition]);
      }
    }

    /// <summary>
    /// Toggle the given root view.
    /// </summary>
    /// <param name="view"></param>
    internal void Toggle(RootView root) {
      if (root == null) {
        return; // We can't toggle; there is nothing to toggle.
      }

      bool isVisible = root.contentLayout.Visibility == ViewStates.Visible;
      bool shouldCollapseOther = !isVisible && NO_LIMIT != expansionLimit && __expandedChildren.Count >= expansionLimit;

      int groupPosition;
      int childPosition;

      if (shouldCollapseOther) {
        // TODO ahodder@appioninc.com: Look into this cast, it doesn't seem right.
        Child child = __expandedChildren.First.Value;
        Group group = GetGroupFor(child);

        if (ContainsGroup(group)) {
          childPosition = IndexOf(group, child);
          groupPosition = IndexOf(group);

          RootView toCollapse = (RootView)list.GetChildAt(GetFlatPositionFor(groupPosition, childPosition));

          if (toCollapse != null) {
            AnimateCollapsing(toCollapse);
          }

          NotifyOnExpandEvent(groupPosition, childPosition, false);
        }

        __expandedChildren.RemoveFirst();
      }

      groupPosition = GetUnpackedGroupPosition(root.position);
      childPosition = GetUnpackedChildPosition(root.position);


      if (isVisible) {
        AnimateCollapsing(root);
        __expandedChildren.Remove(this[groupPosition, childPosition]);
        NotifyOnExpandEvent(groupPosition, childPosition, false);
      } else {
        AnimateExpanding(root);
        __expandedChildren.AddLast(this[groupPosition, childPosition]);
        NotifyOnExpandEvent(groupPosition, childPosition, true);
      }
    }

    private void NotifyOnExpandEvent(int groupPosition, int childPosition, bool expanded) {
      PostUpdate(groupPosition, childPosition);
      if (onExpandEvent != null) {
        onExpandEvent(groupPosition, childPosition, expanded);
      }
    }

    /// <summary>
    /// Animates the root view's content view closed.
    /// </summary>
    /// <param name="root"></param>
    private void AnimateCollapsing(RootView root) {
      ValueAnimator animator = AnimationHelper.CreateHeightAnimator(root.contentLayout, root.contentLayout.Height, 0);
      animator.AddListener(new SetViewToGoneOnAnimatorEndAction(root.contentLayout));
      animator.Start();
    }

    /// <summary>
    /// Animates the root view's content view open.
    /// </summary>
    /// <param name="root"></param>
    private void AnimateExpanding(RootView root) {
      root.contentLayout.Visibility = ViewStates.Visible;

      int widthSpec = View.MeasureSpec.MakeMeasureSpec(root.MeasuredWidth - root.PaddingLeft - root.PaddingRight, MeasureSpecMode.AtMost);
      int heightSpec = View.MeasureSpec.MakeMeasureSpec(0, MeasureSpecMode.Unspecified);

      root.contentLayout.Measure(widthSpec, heightSpec);

      ValueAnimator animator = AnimationHelper.CreateHeightAnimator(root.contentLayout, 0, root.contentLayout.MeasuredHeight);
      animator.AddUpdateListener(new ExpandingAnimatorListener(root, list));
      animator.Start();
    }

    /// <summary>
    /// An action class that will set the view's visibility to gone.
    /// </summary>
    private class SetViewToGoneOnAnimatorEndAction : AnimatorListenerAdapter {
      private View view { get; set; }

      public SetViewToGoneOnAnimatorEndAction(View view) {
        this.view = view;
      }

      // Overridden from AnimatorListenerAdapter
      public override void OnAnimationEnd(Animator animator) {
        view.Visibility = ViewStates.Gone;
      }
    }

    /// <summary>
    /// A simple callback implementation that will properly scroll the list when
    /// an item is expanded of screen.
    /// </summary>
    private class ExpandingAnimatorListener : Java.Lang.Object, ValueAnimator.IAnimatorUpdateListener {
      private RootView root { get; set; }
      private ListView list { get; set; }

      public ExpandingAnimatorListener(RootView root, ListView list) {
        this.root = root;
        this.list = list;
      }

      // Overridden from IAnimatorUpdateListener
      public void OnAnimationUpdate(ValueAnimator va) {
        if (root.Bottom > list.Height) {
          if (root.Top > 0) {
            int yDelta = root.Bottom - list.Height + list.PaddingBottom;
            list.SmoothScrollBy(Math.Min(yDelta, root.Top), 0);
          }
        }
      }
    }
  }

  /// <summary>
  /// The title click listener that will resolve content toggles.
  /// </summary>
  internal class TitleClickHandler<Group, Child> : Java.Lang.Object, View.IOnClickListener {
    public ExpandableContentELVAdapter<Group, Child> adapter { get; set; }
    public RootView root { get; set; }

    public TitleClickHandler(ExpandableContentELVAdapter<Group, Child> adapter, RootView root) {
      this.adapter = adapter;
      this.root = root;
    }

    // Overridden from View.IOnClickListener
    public void OnClick(View view) {
      adapter.Toggle(root);
    }
  }
}