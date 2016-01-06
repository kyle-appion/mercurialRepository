namespace ION.IOS.ViewController.Help {

  using System;
  using System.Collections.Generic;

  public class HelpPage {

    public string title { get; set; }

    public IHelpItem this[int i] {
      get {
        return items[i];
      }
    }

    public int count {
      get {
        return items.Count;
      }
    }

    private List<IHelpItem> items { get; set; }

    public HelpPage(string title, IHelpItem[] items) {
      this.title = title;
      this.items = new List<IHelpItem>(items);
    }
  }

  public class HelpPageBuilder {
    private string title { get; set; }
    private List<IHelpItem> items { get; set; }

    public HelpPageBuilder(string title) {
      this.title = title;
      items = new List<IHelpItem>();
    }

    public HelpPageBuilder Link(string title, EventHandler<HelpViewController> action) {
      items.Add(new LinkHelpItem(title, action));
      return this;
    }

    public HelpPageBuilder Link(HelpPage page) {
      items.Add(new LinkHelpItem(page));
      return this;
    }

    public HelpPageBuilder Info(string title, string subtitle) {
      items.Add(new InfoHelpItem(title, subtitle));
      return this;
    }

    public HelpPage Build() {
      return new HelpPage(title, items.ToArray());
    }
  }

  public enum EHelpItemType { 
    Link,
    Info,
  }

  public interface IHelpItem {
    EHelpItemType type { get; }
    string title { get; set; }

    bool PerformClick(HelpViewController vc);
  }

  public abstract class AbstractHelpItem : IHelpItem {
    public EHelpItemType type { get; }

    public string title { get; set; }

    public AbstractHelpItem(EHelpItemType type, string title) { 
      this.type = type;
      this.title = title;
    }

    public abstract bool PerformClick(HelpViewController vc);
  }

  public class LinkHelpItem : AbstractHelpItem {
    private EventHandler<HelpViewController> action { get; set; }

    public LinkHelpItem(string title, EventHandler<HelpViewController> action) : base(EHelpItemType.Link, title) {
      this.action = action;
    }

    public LinkHelpItem(HelpPage page) : base(EHelpItemType.Link, page.title) {
      this.action = (object obj, HelpViewController vc) => {
        var nestedHelp = vc.InflateViewController<HelpViewController>(BaseIONViewController.VC_HELP);
        nestedHelp.page = page;
        vc.NavigationController.PushViewController(nestedHelp, true);
      };
    }

    public override bool PerformClick(HelpViewController vc) {
      if (action != null) {
        action(this, vc);
        return true;
      }

      return false;
    }
  }

  public class InfoHelpItem : AbstractHelpItem { 
    public string subtitle { get; set; }

    public InfoHelpItem(string title, string subtitle) : base(EHelpItemType.Info, title) {
      this.subtitle = subtitle;
    }

    public override bool PerformClick(HelpViewController vc) {
      return false;
    }
  }
}

