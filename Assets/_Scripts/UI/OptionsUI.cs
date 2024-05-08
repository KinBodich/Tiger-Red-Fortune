using Common.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Common.UI
{
    public class OptionsUI : MonoBehaviour
    {
        private const string MUSIC_VOLUME = "MusicVolume";
        private const string SOUND_VOLUME = "SoundVolume";

        [SerializeField] private GameObject _optionsPanel;
        [SerializeField] private Slider _musicSlider;
        [SerializeField] private Slider _soundSlider;

        private BackgroundMusicManager _backgroundMusicManager;
        private SoundManager _soundManager;

        private void Awake()
        {
            _backgroundMusicManager = BackgroundMusicManager.Instance;
            _soundManager = SoundManager.Instance;
        }

        private void Start()
        {
            _musicSlider.value = PlayerPrefs.GetFloat(MUSIC_VOLUME, 0);
            _soundSlider.value = PlayerPrefs.GetFloat(SOUND_VOLUME, 0);
        }

        public void SetMusicVolume()
        {
            _backgroundMusicManager.AudioSource.volume = _musicSlider.value;
            PlayerPrefs.SetFloat(MUSIC_VOLUME, _backgroundMusicManager.AudioSource.volume);
        }

        public void SetSoundVolume()
        {
            _soundManager.SetVolume(_soundSlider.value);
        }

        public void ShowPanel()
        {
            _optionsPanel.SetActive(true);
        }

        public void HidePanel()
        {
            _optionsPanel.SetActive(false);
        }
    }
}