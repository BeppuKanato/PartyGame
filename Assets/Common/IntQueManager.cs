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

        //キューにプッシュする
        public void Push(int n)
        {
            que.Add(n);
        }

        //キューからポップする
        public int Pop()
        {
            if (que.Count < 1)
            {
                throw new InvalidOperationException("スタックは空です");
            }

            int result = que.First();
            que.RemoveAt(0);

            return result;
        }

        //次にポップする要素を返す
        public int GetLastValue()
        {
            if (que.Count < 1)
            {
                throw new InvalidOperationException("スタックは空です");
            }
            return que.First();
        }

        //キューをリセットする
        public void ResetQue()
        {
            que.Clear();
        }
        //キューの内容を取得する
        public List<int> GetQueContent()
        {
            return new List<int>(que);
        }

        //キューにリストを代入
        public void SetQue(List<int> list)
        {
            que = new List<int>(list);
        }
        //キューの長さを返す
        public int GetQueCount()
        {
            return que.Count;
        }
    }
}