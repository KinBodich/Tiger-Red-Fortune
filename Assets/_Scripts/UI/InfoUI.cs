using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common.UI
{
    public class InfoUI : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _infoPanels;

        private int _currentPanel;

        public void ShowInfo()
        {
            _currentPanel = 0;
            _infoPanels[_currentPanel].SetActive(true);
        }

        public void NextPanel()
        {
            _infoPanels[_currentPanel].SetActive(false);
            _currentPanel++;
            if (_currentPanel >= _infoPanels.Count)
            {
                return;
            }
            _infoPanels[_currentPanel].SetActive(true);
        }
    }
}