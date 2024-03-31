using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Common
{
    public class IntStackManager
    {
        const int ERROR_NUMBER = -1;

        List<int> stack = new List<int>();

        //�X�^�b�N�Ƀv�b�V������
        public void Push(int n)
        {
            stack.Add(n);
        }
        //�X�^�b�N����|�b�v����
        public int Pop()
        {
            if (stack.Count < 1)
            {
                throw new InvalidOperationException("�X�^�b�N�͋�ł�");
            }

            int result = stack.Last();
            stack.RemoveAt(stack.Count - 1);
            return result;
        }
        //���Ƀ|�b�v����v�f��Ԃ�
        public int GetLastValue()
        {
            if (stack.Count < 1)
            {
                throw new InvalidOperationException("�X�^�b�N�͋�ł�");
            }
            return stack.Last();
        }
        //�X�^�b�N�����Z�b�g����
        public void ResetStack()
        {
            stack.Clear();
        }
        // �X�^�b�N�̓��e���擾����
        public List<int> GetStackContent()
        {
            return new List<int>(stack);
        }
        //�X�^�b�N�̒�����Ԃ�
        public int GetStackCount()
        {
            return stack.Count;
        }
    }
}