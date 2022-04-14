using System.Globalization;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class AppManager : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private GameObject _mainScreen;
    [SerializeField] private GameObject _settingsScreen;
    [SerializeField] private GameObject _analyticsPanel;

    [Header("Main")] 
    [SerializeField] private Text _timerText;

    [Header("Settings")]
    [SerializeField] private InputField _minutes;
    [SerializeField] private InputField _seconds;
    [SerializeField] private Toggle _useVoicePrompt;
    [SerializeField] private Toggle _useCaloriesBurnt;
    [SerializeField] private Toggle _useGpsSettings;

    private Settings _settings = new Settings(0, 0, false, false, false);
    private readonly string _settingsXMLFileName = "Settings";
    private float _timer;
    private bool _timerPaused;
    
    private void Start()
    {
        _settings = File.Exists(_settingsXMLFileName) ? BinarySerialization.ReadFromBinaryFile<Settings>(_settingsXMLFileName) : new Settings(0, 0, false, false, false);

        _minutes.text = _settings.StartMinutes.ToString(CultureInfo.InvariantCulture);
        _seconds.text = _settings.StartSeconds.ToString(CultureInfo.InvariantCulture);
        _useVoicePrompt.isOn = _settings.UseVoicePrompt;
        _useCaloriesBurnt.isOn = _settings.UseCaloriesBurnt;
        _useGpsSettings.isOn = _settings.UseGpsSettings;
        
        ResetTimer();
        PauseTimer();
        DisplayTimer();
    }

    private void Update()
    {
        UpdateTimer();
        DisplayTimer();
    }

    public void ChangeScreen()
    {
        if (_settingsScreen.activeSelf)
        {
            _settings.StartMinutes = int.Parse(_minutes.text);
            _settings.StartSeconds = int.Parse(_seconds.text);
            _settings.UseVoicePrompt = _useVoicePrompt.isOn;
            _settings.UseCaloriesBurnt = _useCaloriesBurnt.isOn;
            _settings.UseGpsSettings = _useGpsSettings.isOn;
            
            BinarySerialization.WriteToBinaryFile(_settingsXMLFileName, _settings);
            
            ResetTimer();
        }
        
        _mainScreen.SetActive(!_mainScreen.activeSelf);
        _settingsScreen.SetActive(!_settingsScreen.activeSelf);
    }

    public void AdjustSeconds()
    {
        if (!int.TryParse(_seconds.text, out var seconds)) return;

        if (seconds > 59)
        {
            seconds = 59;
        }
        else if (seconds < 0)
        {
            seconds = 0;
        }

        _seconds.text = seconds.ToString(CultureInfo.InvariantCulture);
    }
    
    public void AdjustMinutes()
    {
        if (!int.TryParse(_minutes.text, out var minutes)) return;

        if (minutes < 0)
        {
            minutes = 0;
        }

        _minutes.text = minutes.ToString(CultureInfo.InvariantCulture);
    }

    public void ResetTimer()
    {
        PauseTimer();
        _timer = 60 * _settings.StartMinutes + _settings.StartSeconds;
    }

    public void StartTimer()
    {
        _timerPaused = false;
    }

    public void PauseTimer()
    {
        _timerPaused = true;
    }

    public void CloseAnalyticsPanel()
    {
        _analyticsPanel.SetActive(false);
    }
    
    private void UpdateTimer()
    {
        if (_timerPaused) return;

        _timer -= Time.deltaTime;

        if (!(_timer < 0f)) return;
        
        _timer = 0;
        PauseTimer();

        if (_useGpsSettings.isOn)
        {
            _analyticsPanel.SetActive(true);
        }
    }
    
    private void DisplayTimer()
    {
        var minutes = (int)(_timer / 60);
        var seconds = (int)(_timer % 60);
        _timerText.text = minutes + ":" + (seconds < 10 ? "0" : "") + seconds;
    }
}
