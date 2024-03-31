using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DebugNetworkParam : ScriptableObject
{
    [Tooltip("�I�����C���Ńf�o�b�O���[�h�����s���邩")]
    public bool isOnline;
    [Tooltip("�v���C���[�̎Q���l��")]
    public int nPlayer;
    [Tooltip("���[���̖��O")]
    public string roomName;
    public int playerID;
    [Tooltip("�g�p����L�����N�^�[�v���n�u�̖��O")]
    public string charPrefabName;
}
