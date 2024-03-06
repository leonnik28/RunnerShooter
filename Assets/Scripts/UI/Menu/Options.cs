using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{ 
    [SerializeField] private GameObject _options;

    [SerializeField] private TMP_Dropdown _fpsDropdown;
    [SerializeField] private Toggle _musicToggle;
    [SerializeField] private Toggle _soundEffectsToggle;

    [Space]
    [SerializeField] private Animator _musicSwitchAnimator;
    [SerializeField] private Animator _soundEffectsSwitchAnimator;

    [Space]
    [SerializeField] private List<AudioSource> _musics;
    [SerializeField] private List<AudioSource> _soundEffects;

    private IStorageService _storageService;
    private OptionsValue _value;

    private async void Start()
    {
        Application.targetFrameRate = 60;
        _storageService = new StorageService();
        await UpdateOptionsOnStart();
    }

    public async void OpenOptions()
    {
        _options.gameObject.SetActive(true);
        await UpdateOptions();
    }

    public async void SetTargetFps()
    {
        int targetFPS = 60;

        switch (_fpsDropdown.value)
        {
            case 0:
                targetFPS = 30;
                break;
            case 1:
                targetFPS = 60;
                break;
            case 2:
                targetFPS = 90;
                break;
        }

        Application.targetFrameRate = targetFPS;

        _value.FpsValue = _fpsDropdown.value;
        await _storageService.SaveAsync("options", _value);
    }

    public void SetMusic(bool isChange)
    {
        bool isMusic = _musicToggle.isOn;
        UpdateAudio(_musics, isMusic);
        AnimateSwitch(_musicToggle, _musicSwitchAnimator, isChange);

        _value.Music = isMusic;
        _storageService.SaveAsync("options", _value);
    }

    public void SetSoundEffects(bool isChange)
    {
        bool isSoundEffects = _soundEffectsToggle.isOn;
        UpdateAudio(_soundEffects, isSoundEffects);
        AnimateSwitch(_soundEffectsToggle, _soundEffectsSwitchAnimator, isChange);

        _value.SoundEffects = isSoundEffects;
        _storageService.SaveAsync("options", _value);
    }

    private async Task UpdateOptionsOnStart()
    {
        var options = await _storageService.LoadAsync<OptionsValue>("options");
        _fpsDropdown.value = options.FpsValue;
        _musicToggle.isOn = options.Music;
        _soundEffectsToggle.isOn = options.SoundEffects;

        SetFps();
        SetMusic();
        SetSoundEffects();
    }

    private void SetFps()
    {
        int targetFPS = 60;

        switch (_value.FpsValue)
        {
            case 0:
                targetFPS = 30;
                break;
            case 1:
                targetFPS = 60;
                break;
            case 2:
                targetFPS = 90;
                break;
        }

        Application.targetFrameRate = targetFPS;
    }

    private void SetMusic()
    {
        UpdateAudio(_musics, _value.Music);
    }

    private void SetSoundEffects()
    {
        UpdateAudio(_soundEffects, _value.SoundEffects);
    }

    private void UpdateAudio(List<AudioSource> audioList, bool isAudio)
    {
        foreach (var audio in audioList)
        {
            audio.mute = !isAudio;
        }
    }

    private async Task UpdateOptions()
    {
        var options = await _storageService.LoadAsync<OptionsValue>("options");
        _fpsDropdown.value = options.FpsValue;
        _musicToggle.isOn = options.Music;
        _soundEffectsToggle.isOn = options.SoundEffects;

        SetTargetFps();
        SetMusic(false);
        SetSoundEffects(false);
    }

    private void AnimateSwitch(Toggle toggle, Animator switchAnimator, bool isChange)
    {
        if (isChange)
        {
            if (toggle.isOn)
            {
                switchAnimator.Play("Switch On");
            }
            else
            {
                switchAnimator.Play("Switch Off");
            }
        }
        else
        {
            if (toggle.isOn)
            {
                switchAnimator.Play("Switch On State");
            }
            else
            {
                switchAnimator.Play("Switch Off State");
            }
        }
    }

    private struct OptionsValue
    {
        public int FpsValue;
        public bool Music;
        public bool SoundEffects;
    }
}