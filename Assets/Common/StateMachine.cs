using Common.Interface;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
namespace Common
{
    public class StateMachine
    {
        public Common.IntStackManager intStack { get; private set; }
        public StateMachine()
        {
            intStack = new Common.IntStackManager();
        }

        //��Ԃ�ύX���郁�\�b�h ��Ԃ��ω�������true��Ԃ�
        public bool ChangeState(StateProcess current, StateProcess next)
        {
            bool result = false;
            if (current != next)
            {
                //��Ԃ̏������Ăяo��
                current.Exit();
                next.Enter();
                result = true;
            }

            return result;
        }

        //Process�����s����
        public int ExeProcess(StateProcess stateProcess)
        {
            int result = stateProcess.Process();

            return result;
        }
    }
}