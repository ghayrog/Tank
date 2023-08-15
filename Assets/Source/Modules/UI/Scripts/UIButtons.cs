using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UIElements
{
    public class UIButtons : MonoBehaviour
    {
        [SerializeField] private GameObject _gameOverPanel;
        [SerializeField] private GameObject _startGamePanel;
        [SerializeField] private GameObject _victoryPanel;
        [SerializeField] private GameObject _pausePanel;

        private void Start()
        {
            _startGamePanel.SetActive(true);
            _gameOverPanel.SetActive(false);
            _victoryPanel.SetActive(false);
            _pausePanel.SetActive(false);
        }
        public void RestartGame()
        {
            SceneManager.LoadScene(0);
        }

        public void QuitGame()
        { 
            Application.Quit();
        }
        public void HideGameOverPanel()
        {
            _gameOverPanel.SetActive(false);
        }
        public void ShowGameOverPanel()
        {
            _gameOverPanel.SetActive(true);
        }
        public void HideStartGamePanel()
        { 
            _startGamePanel.SetActive(false);
        }

        public void HideVictoryPanel()
        {
            _victoryPanel.SetActive(false);
        }
        public void ShowVictoryPanel()
        {
            _victoryPanel.SetActive(true);
        }

        public void HidePausePanel()
        {
            _pausePanel.SetActive(false);
        }
        public void ShowPausePanel()
        {
            if (_startGamePanel.activeSelf || _gameOverPanel.activeSelf || _victoryPanel.activeSelf)
                return;
            _pausePanel.SetActive(true);
        }


    }
}
