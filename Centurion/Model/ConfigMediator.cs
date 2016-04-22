using Centurion.Core.Utilities;

namespace Centurion.Core.Model
{
  public class ConfigMediator
  {

    #region vairables

    public const string ConfigMediatorKey = "ConfigMediator";

    public const string SoundEffectChangedMessage = "SoundEffectChanged";
    public const string GameLengthChangedMessage = "GameLengthChanged";
    public const string IntervalLengthChangedMessage = "IntervalLengthChanged";

    private static Mediator instance;

    #endregion

    #region properties

    public static Mediator Instance
    {
      get { return Mediator.GetMediator(ConfigMediatorKey); }
    }

    #endregion

    #region constructor

    private ConfigMediator() {}

    #endregion


  }
}
