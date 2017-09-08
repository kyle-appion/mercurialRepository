namespace ION.Droid.Tasks {

  using System;
  using System.Collections.Generic;

  using Android.OS;

  /// <summary>
  /// A simple wrapper that will allow async task to be used similiar to how it is in android native.
  /// </summary>
  public abstract class IONTask<Param, Progress, Result> : AsyncTask {

    /// <summary>
    /// Executes the task.
    /// </summary>
    /// <param name="parameters">Parameters.</param>
    public void Execute(IEnumerable<Param> parameters) {
      var jobjs = new List<Java.Lang.Object>();

      foreach (var p in parameters) {
        jobjs.Add(new JavaWrapper<Param>(p));
      }

      base.Execute(jobjs.ToArray());
    }

    /// <Docs>To be added.</Docs>
    /// <returns>To be added.</returns>
    /// <summary>
    /// Dos the in background.
    /// </summary>
    /// <param name="params">Parameters.</param>
    protected override sealed Java.Lang.Object DoInBackground(params Java.Lang.Object[] parameters) {
      var p = new List<Param>();

      foreach (var obj in parameters) {
        p.Add(((JavaWrapper<Param>)obj).t);
      }

      return new JavaWrapper<Result>(DoInBackground(p));
    }

    /// <summary>
    /// Published the given items to the task.
    /// </summary>
    public void PublishProgress(params Progress[] objs) {
      var jobjs = new Java.Lang.Object[objs.Length];

      for (int i = 0; i < objs.Length; i++) {
        jobjs[i] = new JavaWrapper<Progress>(objs[i]);
      }

      base.PublishProgress(jobjs);
    }

    /// <Docs>To be added.</Docs>
    /// <remarks>To be added.</remarks>
    /// <summary>
    /// Raises the post execute event.
    /// </summary>
    /// <param name="result">Result.</param>
    protected override sealed void OnPostExecute(Java.Lang.Object result) {
      OnPostExecute(((JavaWrapper<Result>)result).t);
    }

    /// <summary>
    /// The function that is called in a background thread that will do the work for the task.
    /// </summary>
    /// <param name="p">P.</param>
    protected abstract Result DoInBackground(List<Param> parameters);

    /// <summary>
    /// Raises the post execute event.
    /// </summary>
    protected virtual void OnPostExecute(Result result) {
    }

    /// <summary>
    /// A wrapper class that will bring a C# object into and out of java land.
    /// </summary>
    private class JavaWrapper<T> : Java.Lang.Object {
      public T t;

      public JavaWrapper(T t) {
        this.t = t;
      }
    }
  }
}

