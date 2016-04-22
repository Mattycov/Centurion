using System;
using System.Diagnostics;
using System.IO;
using System.Media;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using WindowsInput;
using WindowsInput.Native;
using Centurion.Core.Model;
using Centurion.Core.Utilities;
using Centurion.Core.Views;

namespace Centurion.Core.ViewModels
{
  public class PlayerViewModel : ViewModelBase
  {

    #region variables

    private DispatcherTimer intervalTimer;
    private DispatcherTimer clockTimer;

    private readonly InputSimulator inputSimulator;

    private DateTime gameStartTime;
    private DateTime intervalStartTime;

    private Uri soundEffectSource;

    private SoundPlayer player;
    
    #endregion

    #region properties

    public string GameTime
    {
      get { return ((gameStartTime + Config.Instance.GameLength) - DateTime.Now).ToString(@"h\:mm\:ss"); }
    }

    public string IntervalTime
    {
      get { return ((intervalStartTime + Config.Instance.IntervalLength) - DateTime.Now).ToString(@"mm\:ss"); }
    }

    public Uri SoundEffectSource
    {
      get { return soundEffectSource; }
      set
      {
        if (soundEffectSource == value)
          return;
        soundEffectSource = value;
        OnPropertyChanged();
      }
    }

    public ICommand StartCommand { get; private set; }
    public ICommand StopCommand { get; private set; }

    #endregion

    #region constructor

    public PlayerViewModel()
    {
      StartCommand = new DelegateCommand(StartCommandImpl);
      StopCommand = new DelegateCommand(StopCommandImpl);

      Assembly assembly = Assembly.GetExecutingAssembly();
      string[] resources = assembly.GetManifestResourceNames();
      player = new SoundPlayer(assembly.GetManifestResourceStream("Centurion.Core.horn.wav"));
      player.Load();
      player.LoadCompleted += (sender, args) =>
      {
        MessageBox.Show(args.Error.Message);
      };

      inputSimulator = new InputSimulator();
      intervalTimer = new DispatcherTimer(DispatcherPriority.Send);
      intervalTimer.Tick += IntervalTimerOnTick;
      clockTimer = new DispatcherTimer(DispatcherPriority.Send);
      clockTimer.Tick += ClockTimerOnTick;
      clockTimer.Interval = new TimeSpan(0, 0, 0, 0, 200);
    }

    #endregion

    #region private methods
    
    private void IntervalTimerOnTick(object sender, EventArgs eventArgs)
    {
      inputSimulator.Keyboard.KeyPress(VirtualKeyCode.MEDIA_NEXT_TRACK);
      intervalStartTime = DateTime.Now;

      player.Stop();
      player.Play();
      
    }

    private void ClockTimerOnTick(object sender, EventArgs eventArgs)
    {
      OnPropertyChanged(nameof(GameTime));
      OnPropertyChanged(nameof(IntervalTime));
    }

    private void StartCommandImpl()
    {
      clockTimer.Stop();
      intervalTimer.Interval = Config.Instance.IntervalLength;
      inputSimulator.Keyboard.KeyPress(VirtualKeyCode.MEDIA_PLAY_PAUSE);
      DateTime now = DateTime.Now;
      gameStartTime = now;
      intervalStartTime = now;
      intervalTimer.Start();
      clockTimer.Start();
    }

    private void StopCommandImpl()
    {
      clockTimer.Stop();
      intervalTimer.Stop();
      inputSimulator.Keyboard.KeyPress(VirtualKeyCode.MEDIA_STOP);
    }

    #endregion
    
  }
}
