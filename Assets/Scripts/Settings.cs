using System;
using UnityEngine;

[Serializable]
public class Settings
{
    private int _startingMinutes;
    private int _startingSeconds;
    private bool _useVoicePrompt;
    private bool _useCaloriesBurnt;
    private bool _useGpsSettings;

    public Settings(int startingMinutes, int startingSeconds, bool useVoicePrompt, bool useCaloriesBurnt, bool useGpsSettings)
    {
        _startingMinutes = startingMinutes;
        _startingSeconds = startingSeconds;
        _useVoicePrompt = useVoicePrompt;
        _useCaloriesBurnt = useCaloriesBurnt;
        _useGpsSettings = useGpsSettings;
    }

    public int StartMinutes
    {
        get => _startingMinutes;
        set => _startingMinutes = value;
    }
    
    public int StartSeconds
    {
        get => _startingSeconds;
        set => _startingSeconds = value;
    }

    public bool UseVoicePrompt
    {
        get => _useVoicePrompt;
        set => _useVoicePrompt = value;
    }

    public bool UseCaloriesBurnt
    {
        get => _useCaloriesBurnt;
        set => _useCaloriesBurnt = value;
    }

    public bool UseGpsSettings
    {
        get => _useGpsSettings;
        set => _useGpsSettings = value;
    }
}
