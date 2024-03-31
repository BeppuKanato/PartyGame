using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedSquareComponent : BaseSquareComponent
{
    [field: SerializeField]
    public int nCoin { get; private set; }
    public override void OnProcess()
    {
        Debug.Log($"{nCoin}–‡ƒRƒCƒ“‚ðŽ¸‚¢‚Ü‚·");
    }
}
