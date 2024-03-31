using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ゲームの基本設定
[CreateAssetMenu]
public class BoardGameSetting : ScriptableObject
{
    [field: SerializeField]
    public int _gameSpeed { get; private set; } = 1;        //ゲームスピード
    [field: SerializeField]
    public float _moveSpeed { get; private set;} = 0.01f;    //移動スピード

    //移動速度を返す
    public float GetMoveSpeed()
    {
        return _moveSpeed * _gameSpeed;
    }
}
