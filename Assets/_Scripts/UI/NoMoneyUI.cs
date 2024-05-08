using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Common.UI
{
    public class NoMoneyUI : MonoBehaviour
    {
        [SerializeField] private GameObject _noMoneyPanel;

        public void ShowPanel()
        {
            _noMoneyPanel.SetActive(true);
        }

        public void HidePanel()
        {
            _noMoneyPanel.SetActive(false);
        }

        public void Play()
        {
            SceneManager.LoadScene(1);
        }
    }
}