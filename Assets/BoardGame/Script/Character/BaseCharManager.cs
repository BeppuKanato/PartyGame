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
    public Dictionary<int, BaseChar> charClones { get; private set; } = new Dictionary<int, BaseChar>(); //誰の操作キャラかを識別するIDとBaseCharの辞書型配列
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
    //オンライン時のキャラのインスタンス化
    public GameObject InstantiateCharInOnline(string prefabName)
    {
        GameObject charObj = networkHandler.photonNetWorkManager.InstantiatePrefab(prefabName, initializeTransforms[0].position, initializeTransforms[0].rotation);

        return charObj;
    }
    //オフライン時のキャラのインスタンス化
    public GameObject InstantiateCharInOffline(string prefabName)
    {
        //インスタンス化するプレハブを取得
        GameObject charPrefab = GetPrefabByName(prefabName);
        GameObject charObj = Instantiate(charPrefab, initializeTransforms[0].position, initializeTransforms[0].rotation);

        return charObj;
    }
    //キャラクターオブジェクトの初期設定
    public void InitializeProcess(GameObject charObj, int actorNumber)
    {
        Debug.Log("キャラクターの初期化設定を行います");
        BaseChar baseChar = charObj.GetComponent<BaseChar>();
        baseChar.InitializeChar(field, initializeSquare);
        charClones.Add(actorNumber, charObj.GetComponent<BaseChar>());
    }

    //指定されたIDのキャラの移動メソッドを実行
    public bool ExeCharMove(int actorNumber, int branchIndex)
    {
        BaseChar moveChar = charClones[actorNumber];
        bool reachTarget = moveChar.MoveToSquare(branchIndex, setting.GetMoveSpeed());

        return reachTarget;
    }

    //使用するキャラ名のプレハブをリストから取得
    GameObject GetPrefabByName(string name)
    {
        foreach (GameObject prefab in charPrefabs)
        {
            Debug.Log($"プレハブ名 = {prefab.name} 指定名 = {name}");
        }
        GameObject returnPrefab = null;
        //指定された名前のプレハブを取り出すlinq
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
    //一つのプレハブを取得できたか確認
    bool CheckLinqResult(IEnumerable<GameObject> linq)
    {
        bool result = false;
        if (linq.Count() == 1)
        {
            result = true;
        }
        else if (linq.Count() > 1)
        {
            Debug.LogWarning("プレハブ名にミスがある可能性があります");
        }
        else
        {
            Debug.LogError("指定されたプレハブは存在しません");
        }

        return result;
    }
}
