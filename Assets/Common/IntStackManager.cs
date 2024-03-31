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

        //スタックにプッシュする
        public void Push(int n)
        {
            stack.Add(n);
        }
        //スタックからポップする
        public int Pop()
        {
            if (stack.Count < 1)
            {
                throw new InvalidOperationException("スタックは空です");
            }

            int result = stack.Last();
            stack.RemoveAt(stack.Count - 1);
            return result;
        }
        //次にポップする要素を返す
        public int GetLastValue()
        {
            if (stack.Count < 1)
            {
                throw new InvalidOperationException("スタックは空です");
            }
            return stack.Last();
        }
        //スタックをリセットする
        public void ResetStack()
        {
            stack.Clear();
        }
        // スタックの内容を取得する
        public List<int> GetStackContent()
        {
            return new List<int>(stack);
        }
        //スタックの長さを返す
        public int GetStackCount()
        {
            return stack.Count;
        }
    }
}