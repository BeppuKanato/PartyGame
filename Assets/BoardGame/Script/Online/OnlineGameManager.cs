using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace BoardGame
{
    public class OnlineGameManager : BaseGameManager
    {
        // Start is called before the first frame update
        void Start()
        {
            base.Start();
            Debug.Log("オンラインのStart");
            //オンラインとオフラインでインスタンス化するクラスを変える
            //BaseStateProcessManager = new BaseStateProcessManager();
        }

        // Update is called once per frame
        void Update()
        {
            base.Update();
            Debug.Log("オンラインのUpdate");
        }
    }
}
