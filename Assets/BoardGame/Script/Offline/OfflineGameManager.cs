using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BoardGame
{
    public class OfflineGameManager : BaseGameManager
    {
        // Start is called before the first frame update
        void Start()
        {
            base.Start();
            Debug.Log("�I�t���C������Start");

            //BaseStateProcessManager stateProcessManager = new BaseStateProcessManager();
        }

        // Update is called once per frame
        void Update()
        {
            base.Update();

            Debug.Log("�I�t���C������Update");
        }
    }
}