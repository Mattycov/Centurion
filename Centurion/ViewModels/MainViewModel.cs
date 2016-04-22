namespace Centurion.Core.ViewModels
{
  public class MainViewModel : ViewModelBase
  {

    #region variables

    private ConfigViewModel configViewModel;
    private PlayerViewModel playerViewModel;

    #endregion

    #region properties

    public ConfigViewModel ConfigViewModel
    {
      get { return configViewModel; }
      set
      {
        configViewModel = value;
        OnPropertyChanged();
      }
    }

    public PlayerViewModel PlayerViewModel
    {
      get { return playerViewModel; }
      set
      {
        playerViewModel = value;
        OnPropertyChanged();
      }
    }

    #endregion

    #region constructor

    public MainViewModel()
    {
      configViewModel = new ConfigViewModel();
      playerViewModel = new PlayerViewModel();
    }

    #endregion

  }
}
