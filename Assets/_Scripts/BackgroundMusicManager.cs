using Common.DataPersistence;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common.Managers
{
    public class BackgroundMusicManager : BaseSingleton<BackgroundMusicManager>, IDataPersistence
    {
        private const string MUSIC_VOLUME = "MusicVolume";

        [field: SerializeField] public List<AudioClip> AudioClips { get; private set; }

        private int _currentClip;
        private float _musicVolume;

        public AudioSource AudioSource { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            
            AudioSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            AudioSource.clip = AudioClips[_currentClip];
            AudioSource.Play();

            _musicVolume = PlayerPrefs.GetFloat(MUSIC_VOLUME, 0);
            AudioSource.volume = _musicVolume;
        }

        public void SetAudioClip(int index)
        {
            AudioSource.clip = AudioClips[index];
            _currentClip = index;
            AudioSource?.Play();
        }

        public void LoadData(GameData data)
        {
            _currentClip = data.CurrentMusic;
        }

        public void SaveData(GameData data)
        {
            data.CurrentMusic = _currentClip;
        }
    }
}