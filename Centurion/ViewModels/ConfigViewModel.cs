using System;
using System.Windows.Input;
using Centurion.Core.Model;
using Centurion.Core.Utilities;

namespace Centurion.Core.ViewModels
{
  public class ConfigViewModel : ViewModelBase, IMediated
  {

    #region variables

    private TimeSpan gameLength;
    private TimeSpan intervalLength;

    #endregion

    #region properties

    public TimeSpan GameLength
    {
      get { return gameLength; }
      set
      {
        if (gameLength == value)
          return;
        gameLength = value;
        OnPropertyChanged();
      }
    }

    public TimeSpan IntervalLength
    {
      get { return intervalLength; }
      set
      {
        if (intervalLength == value)
          return;
        intervalLength = value;
        OnPropertyChanged();
      }
    }

    public ICommand SaveCommand { get; private set; }

    #endregion

    #region constructor

    public ConfigViewModel()
    {
      SaveCommand = new DelegateCommand(SaveCommandImpl);
      gameLength = Config.Instance.GameLength;
      intervalLength = Config.Instance.IntervalLength;
      ConfigMediator.Instance.Register(this,
        new[]
        {
          ConfigMediator.SoundEffectChangedMessage,
          ConfigMediator.GameLengthChangedMessage,
          ConfigMediator.IntervalLengthChangedMessage
        });
    }

    #endregion

    #region private methods

    private void SaveCommandImpl()
    {
      Config.Instance.GameLength = GameLength;
      Config.Instance.IntervalLength = IntervalLength;
      Config.Instance.Save();
    }

    #endregion

    #region IMediated impl

    public void OnMessage(string message, object args)
    {
      switch (message)
      {
        case ConfigMediator.GameLengthChangedMessage:
          GameLength = Config.Instance.GameLength;
          break;
        case ConfigMediator.IntervalLengthChangedMessage:
          IntervalLength = Config.Instance.IntervalLength;
          break;
      }
    }

    #endregion

  }
}
