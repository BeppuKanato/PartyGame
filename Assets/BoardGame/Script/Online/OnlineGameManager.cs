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
            Debug.Log("�I�����C����Start");
            //�I�����C���ƃI�t���C���ŃC���X�^���X������N���X��ς���
            //BaseStateProcessManager = new BaseStateProcessManager();
        }

        // Update is called once per frame
        void Update()
        {
            base.Update();
            Debug.Log("�I�����C����Update");
        }
    }
}
