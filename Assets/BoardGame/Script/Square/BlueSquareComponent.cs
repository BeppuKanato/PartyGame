using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueSquareComponent : BaseSquareComponent
{
    [field: SerializeField]
    public int nCoin { get; private set; }  //�Ⴆ��R�C���̖���
    //�}�X�Ɏ~�܂������̏���
    public override void OnProcess()
    {
        Debug.Log($"{nCoin}���R�C�����l��");
    }
}
