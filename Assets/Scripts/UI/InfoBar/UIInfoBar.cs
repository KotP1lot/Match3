using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInfoBar : MonoBehaviour
{
    [SerializeField] UIResourceBar star;
    [SerializeField] UIResourceBar money;
    [SerializeField] PlayerWalletSO wallet;

    public void Start()
    {
        star.Setup(wallet.Stars);
        money.Setup(wallet.Money);
    }
}
