using System;
using Centurion.Core.Properties;

namespace Centurion.Core.Model
{
  public class Config
  {

    #region variables

    private static Config instance;

    #endregion

    #region properties

    public static Config Instance
    {
      get
      {
        if (instance == null)
          instance = new Config();
        return instance;
      }
    }

    public TimeSpan GameLength
    {
      get { return Settings.Default.GameLength; }
      set
      {
        Settings.Default.GameLength = value;
        ConfigMediator.Instance.Notify(ConfigMediator.GameLengthChangedMessage, null);
      }
    }

    public TimeSpan IntervalLength
    {
      get { return Settings.Default.IntervalLength; }
      set
      {
        Settings.Default.IntervalLength = value;
        ConfigMediator.Instance.Notify(ConfigMediator.IntervalLengthChangedMessage, null);
      }
    }

    #endregion

    #region constructor

    private Config()
    {
      Settings.Default.Reload();
    }

    #endregion

    #region public methods

    public void Load()
    {
      Settings.Default.Reload();
    }

    public void Save()
    {
      Settings.Default.Save();
    }

    #endregion


  }
}
