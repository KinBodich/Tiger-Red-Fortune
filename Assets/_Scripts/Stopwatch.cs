using UnityEngine;
using TMPro;

namespace Common.UI
{
    public class Stopwatch : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _timerText;

        private float _startTime;
        private bool _isRunning;

        void Start()
        {
            _startTime = Time.time;
            _isRunning = true;
        }

        void Update()
        {
            if (_isRunning)
            {
                float elapsedTime = Time.time - _startTime;

                int minutes = (int)(elapsedTime / 60);
                int seconds = (int)(elapsedTime % 60);

                _timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            }
        }

        public void ToggleStopwatch()
        {
            _isRunning = !_isRunning;

            if (_isRunning)
            {
                _startTime = Time.time - (Time.time - _startTime);
            }
            else
            {
                _startTime = Time.time;
            }
        }

        public void ResetStopwatch()
        {
            _startTime = Time.time;
            _timerText.text = "00:00";
        }
    }
}