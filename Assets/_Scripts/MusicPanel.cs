using Common.DataPersistence;
using Common.Managers;
using Common.UI;
using TMPro;
using UnityEngine;

namespace Common
{
    public class MusicPanel : MonoBehaviour, IDataPersistence
    {
        [SerializeField] private int _index;
        [SerializeField] private int _price;
        [SerializeField] private bool _isBought;
        [Space(5)]
        [SerializeField] private GameObject _selectButton;
        [SerializeField] private GameObject _buyPanel;
        [SerializeField] private TextMeshProUGUI _priceText;
        [Space(5)]
        [SerializeField] private GameObject _playImage;
        [SerializeField] private GameObject _stopImage;
        [Space(5)]
        [SerializeField] private AudioSource _buyMusic;

        private bool _isPlaying;

        private NoMoneyUI _noMoneyUI;
        private MoneyManager _moneyManager;
        private BackgroundMusicManager _backgroundMusicManager;
        private AudioSource _clip;

        private void Awake()
        {
            _moneyManager = MoneyManager.Instance;
            _backgroundMusicManager = BackgroundMusicManager.Instance;
            _clip = GetComponent<AudioSource>();
            _noMoneyUI = FindObjectOfType<NoMoneyUI>();
        }

        private void Start()
        {
            _clip.clip = _backgroundMusicManager.AudioClips[_index];
            CheckClip();
            _priceText.SetText($"{_price}");
            _clip.Stop();
            _stopImage.SetActive(false);
            _playImage.SetActive(true);
        }

        public void PlayClip()
        {
            _isPlaying = !_isPlaying;

            if (_isPlaying)
            {
                _backgroundMusicManager.AudioSource.Stop();
                _clip.Play();
                _stopImage.SetActive(true);
                _playImage.SetActive(false);
            }
            else
            {
                _clip.Stop();
                _backgroundMusicManager.AudioSource.Play();
                _stopImage.SetActive(false);
                _playImage.SetActive(true);
            }
        }

        public void BuyMusic()
        {
            if (_moneyManager.MoneyCount < _price)
            {
                _noMoneyUI.ShowPanel();
                return;
            }
            _moneyManager.ChangeMoney(-_price);
            _isBought = true;
            CheckClip();
            SelectMusic();
            _buyMusic.Play();
        }

        public void SelectMusic()
        {
            _backgroundMusicManager.SetAudioClip(_index);
        }

        private void CheckClip()
        {
            if (_isBought)
            {
                _selectButton.SetActive(true);
                _buyPanel.SetActive(false);
            }
            else
            {
                _selectButton.SetActive(false);
                _buyPanel.SetActive(true);
            }
        }

        public void LoadData(GameData data)
        {
            _isBought = data.BoughtMusic[_index];
        }

        public void SaveData(GameData data)
        {
            data.BoughtMusic[_index] = _isBought;
        }
    }
}