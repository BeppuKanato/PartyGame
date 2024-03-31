using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�Q�[���̊�{�ݒ�
[CreateAssetMenu]
public class BoardGameSetting : ScriptableObject
{
    [field: SerializeField]
    public int _gameSpeed { get; private set; } = 1;        //�Q�[���X�s�[�h
    [field: SerializeField]
    public float _moveSpeed { get; private set;} = 0.01f;    //�ړ��X�s�[�h

    //�ړ����x��Ԃ�
    public float GetMoveSpeed()
    {
        return _moveSpeed * _gameSpeed;
    }
}
