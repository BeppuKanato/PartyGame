using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Photon.Pun.Demo.SlotRacer.Utils;

public class BaseCharManager : MonoBehaviour
{
    [SerializeField]
    List<Transform> initializeTransforms = new List<Transform>();
    [SerializeField]
    BaseSquareComponent initializeSquare;
    public Transform field;
    // Start is called before the first frame update
    [SerializeField]
    BoardGameSetting setting;
    [SerializeField]
    List<GameObject> charPrefabs = new List<GameObject>();
    [SerializeField]
    NetworkHandler networkHandler;
    public Dictionary<int, BaseChar> charClones { get; private set; } = new Dictionary<int, BaseChar>(); //�N�̑���L�����������ʂ���ID��BaseChar�̎����^�z��
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
    //�I�����C�����̃L�����̃C���X�^���X��
    public GameObject InstantiateCharInOnline(string prefabName)
    {
        GameObject charObj = networkHandler.photonNetWorkManager.InstantiatePrefab(prefabName, initializeTransforms[0].position, initializeTransforms[0].rotation);

        return charObj;
    }
    //�I�t���C�����̃L�����̃C���X�^���X��
    public GameObject InstantiateCharInOffline(string prefabName)
    {
        //�C���X�^���X������v���n�u���擾
        GameObject charPrefab = GetPrefabByName(prefabName);
        GameObject charObj = Instantiate(charPrefab, initializeTransforms[0].position, initializeTransforms[0].rotation);

        return charObj;
    }
    //�L�����N�^�[�I�u�W�F�N�g�̏����ݒ�
    public void InitializeProcess(GameObject charObj, int actorNumber)
    {
        Debug.Log("�L�����N�^�[�̏������ݒ���s���܂�");
        BaseChar baseChar = charObj.GetComponent<BaseChar>();
        baseChar.InitializeChar(field, initializeSquare);
        charClones.Add(actorNumber, charObj.GetComponent<BaseChar>());
    }

    //�w�肳�ꂽID�̃L�����̈ړ����\�b�h�����s
    public bool ExeCharMove(int actorNumber, int branchIndex)
    {
        BaseChar moveChar = charClones[actorNumber];
        bool reachTarget = moveChar.MoveToSquare(branchIndex, setting.GetMoveSpeed());

        return reachTarget;
    }

    //�g�p����L�������̃v���n�u�����X�g����擾
    GameObject GetPrefabByName(string name)
    {
        foreach (GameObject prefab in charPrefabs)
        {
            Debug.Log($"�v���n�u�� = {prefab.name} �w�薼 = {name}");
        }
        GameObject returnPrefab = null;
        //�w�肳�ꂽ���O�̃v���n�u�����o��linq
        IEnumerable<GameObject> linq = from prefab in charPrefabs
                                       where prefab.name == name
                                       select prefab;

        bool checkResult = CheckLinqResult(linq);

        if (checkResult)
        {
            returnPrefab = linq.First();
        }

        return returnPrefab;
    }
    //��̃v���n�u���擾�ł������m�F
    bool CheckLinqResult(IEnumerable<GameObject> linq)
    {
        bool result = false;
        if (linq.Count() == 1)
        {
            result = true;
        }
        else if (linq.Count() > 1)
        {
            Debug.LogWarning("�v���n�u���Ƀ~�X������\��������܂�");
        }
        else
        {
            Debug.LogError("�w�肳�ꂽ�v���n�u�͑��݂��܂���");
        }

        return result;
    }
}
