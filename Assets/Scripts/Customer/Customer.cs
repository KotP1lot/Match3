using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Customer : MonoBehaviour
{
    public Action<Customer> OnCustomerSatisfied;
    public Action<Customer> OnExitAnimFinished;
    public Action<Customer> OnCustomerReady;
    private FastManager fastManager;
    [SerializeField] Image accessoryPref;
    StatusBar statusBar;
    UIFast UIfast;
    int necessarySat;
    int currentSat;
    public int bonus;
    public void Start()
    {
        statusBar.OnComplited += MoveOut;
    }
    public void SetupVisual(List<Sprite> sprites) 
    {
        for(int i = 0; i < sprites.Count; i++) 
        {
            if (i == 0)
            {
                GetComponent<Image>().sprite = sprites[i];
                continue;
            }
            Instantiate(accessoryPref, transform).GetComponent<Image>().sprite = sprites[i];
        }
    }
    public void Setup(CustomerInfo customerInfo, StatusBar bar, UIFast fast)
    {
        statusBar = bar;
        UIfast = fast;
        necessarySat = UnityEngine.Random.Range(customerInfo.minSat, customerInfo.maxSat);
        bonus = customerInfo.bonusPercent;
        fastManager = new FastManager();
        fastManager.Setup(customerInfo.type);
    }
    public void Setup(int necessarySat, CustomerType customer = new())
    {
        this.necessarySat = necessarySat;
        statusBar.Setup(necessarySat);
        fastManager = new FastManager();
        fastManager.Setup(customer);
        UIfast.Setup(fastManager.fastidiousnesses);
    }
    public int Setup(int necessarySat, int currentSat)
    {
        this.necessarySat = necessarySat;
        statusBar.Setup(necessarySat, currentSat);
        return AddSatisfaction(currentSat);
    }
    public int SatWithFast(GemType type, int value) 
    {
        int sat = fastManager.SatWithFast(type, value);
        return AddSatisfaction(sat);
    }
    public int AddSatisfaction(int value)
    {
        statusBar.AddProgress(value);
        currentSat += value;

        int difference = currentSat - necessarySat;
        if (difference >= 0)
        {
            EventManager.instance.OnCustomerSatisfied?.Invoke();
            OnCustomerSatisfied?.Invoke(this);
            return difference;
        }
        return 0;
    }
    public void Spawn() 
    {
        transform.localPosition = new Vector3(-1000, 0, 0);
        MoveIn();
    }
    public void MoveIn() 
    {
        MoveAnim(true, () =>
        {
            statusBar.Setup(necessarySat);
            UIfast.Setup(fastManager.fastidiousnesses);
            statusBar.SetActive(true);
            transform.DOLocalMoveY(0, 0.2f); 
            OnCustomerReady?.Invoke(this);
        });
    }
    public void MoveOut() 
    {
        statusBar.SetActive(false);
        MoveAnim(false, () => { OnExitAnimFinished?.Invoke(this); });
    }
    public void MoveAnim(bool isEnter, Action func) 
    {
        Tween wobbling = transform.DOLocalMoveY(50, 0.15f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
        transform.DOLocalMoveX(isEnter?0:1000, 2f).SetEase(Ease.Linear).OnComplete(() =>
        {
            wobbling.Kill();
            func();
        });
    }
}
