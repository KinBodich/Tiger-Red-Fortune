using Common.DataPersistence;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common.Managers
{
    public class BackgroundManager : BaseSingleton<BackgroundManager>, IDataPersistence
    {
        [SerializeField] private List<Sprite> _backgrounds;

        private int _currentBackground;

        private SpriteRenderer _spriteRenderer;

        protected override void Awake()
        {
            base.Awake();

            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            _spriteRenderer.sprite = _backgrounds[_currentBackground];
        }

        public void SetBackground(int index)
        {
            _spriteRenderer.sprite = _backgrounds[index];
            _currentBackground = index;
        }

        public void LoadData(GameData data)
        {
            _currentBackground = data.CurrentBackground;
        }

        public void SaveData(GameData data)
        {
            data.CurrentBackground = _currentBackground;
        }
    }
}