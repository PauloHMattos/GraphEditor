using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Klak.Wiring;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.GraphSystem.Wiring.Action
{
    [NodeType("Action", "LoadLevel")]
    [AddComponentMenu("Klak/Wiring/Action/LoadScene")]
    public class LoadScene : NodeBase
    {
        [SerializeField]
        private bool _reloadActiveScene;

        [SerializeField]
        private string _sceneName;
        [SerializeField]
        private int _sceneIndex;

        [Inlet]
        public string sceneName
        {
            set
            {
                if (!enabled)
                    return;
                _sceneName = value;
            }
        }

        //[Inlet]
        //public int sceneIndex
        //{
        //    set
        //    {
        //        if (!enabled)
        //            return;
        //        _sceneIndex = value;
        //    }
        //}

        [Inlet]
        public bool reloadActiveScene
        {
            set
            {
                if (!enabled)
                    return;
                _reloadActiveScene = value;
            }
        }

        protected override void InvokeEvents()
        {
            base.InvokeEvents();

            var newSceneName = _sceneName;
            if (_reloadActiveScene)
            {
                newSceneName = SceneManager.GetActiveScene().name;
            }
            SceneManager.LoadScene(newSceneName);
        }
    }
}
