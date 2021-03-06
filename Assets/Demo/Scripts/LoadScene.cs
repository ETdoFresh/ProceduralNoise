﻿using UnityEngine;
using UnityEngine.SceneManagement;

namespace Demo.Scripts
{
    public class LoadScene : MonoBehaviour
    {
        public void Load(int sceneBuildIndex)
        {
            SceneManager.LoadScene(sceneBuildIndex);
        }

        public void Load(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }

        public void Reload()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
