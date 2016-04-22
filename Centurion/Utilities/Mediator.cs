using System;
using System.Collections.Generic;

namespace Centurion.Core.Utilities
{
  public class Mediator
  {

    #region variables

    private static Dictionary<string, Mediator> mediators;

    private Dictionary<string, List<WeakReference>> mediatedStore;

    #endregion

    #region constructor

    private Mediator()
    {
      mediatedStore = new Dictionary<string, List<WeakReference>>();
    }

    public static Mediator GetMediator(string key)
    {
      if (mediators == null)
        mediators = new Dictionary<string, Mediator>();

      if (!mediators.ContainsKey(key))
        mediators.Add(key, new Mediator());

      return mediators[key];
    }

    #endregion

    #region public methods

    public void Register(IMediated mediated, IEnumerable<string> messages)
    {
      foreach (string message in messages)
      {
        if (!mediatedStore.ContainsKey(message))
          mediatedStore.Add(message, new List<WeakReference>());
        mediatedStore[message].Add(new WeakReference(mediated));
      }
    }

    public void Notify(string message, object args)
    {
      if (!mediatedStore.ContainsKey(message))
        return;

      List<WeakReference> deadList = new List<WeakReference>();

      foreach (WeakReference weakReference in mediatedStore[message])
      {
        if (weakReference.IsAlive)
          ((IMediated) weakReference.Target).OnMessage(message, args);
        else
          deadList.Add(weakReference);
      }
      deadList.ForEach(weakRef => mediatedStore[message].Remove(weakRef));

    }

    #endregion

  }
}
