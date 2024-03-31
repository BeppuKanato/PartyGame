using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace Common
{
    public class IntQueManager
    {
        List<int> que = new List<int>();

        //�L���[�Ƀv�b�V������
        public void Push(int n)
        {
            que.Add(n);
        }

        //�L���[����|�b�v����
        public int Pop()
        {
            if (que.Count < 1)
            {
                throw new InvalidOperationException("�X�^�b�N�͋�ł�");
            }

            int result = que.First();
            que.RemoveAt(0);

            return result;
        }

        //���Ƀ|�b�v����v�f��Ԃ�
        public int GetLastValue()
        {
            if (que.Count < 1)
            {
                throw new InvalidOperationException("�X�^�b�N�͋�ł�");
            }
            return que.First();
        }

        //�L���[�����Z�b�g����
        public void ResetQue()
        {
            que.Clear();
        }
        //�L���[�̓��e���擾����
        public List<int> GetQueContent()
        {
            return new List<int>(que);
        }

        //�L���[�Ƀ��X�g����
        public void SetQue(List<int> list)
        {
            que = new List<int>(list);
        }
        //�L���[�̒�����Ԃ�
        public int GetQueCount()
        {
            return que.Count;
        }
    }
}