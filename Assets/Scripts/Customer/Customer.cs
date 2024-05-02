using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    public Action<Customer> OnCustomerSatisfied;
    public Action<Customer> OnExitAnimFinished;
    public Action<Customer> OnCustomerReady;
    private FastManager fastManager;
    [SerializeField] StatusBar statusBar;
    int necessarySat;
    int currentSat;
    public void Start()
    {
        statusBar.OnComplited += MoveOut;
    }
    public void Setup(int necessarySat, CustomerType customer = new())
    {
        this.necessarySat = necessarySat;
        statusBar.Setup(necessarySat);
        fastManager.Setup(customer);
    }
    public int Setup(int necessarySat, int currentSat)
    {
        this.necessarySat = necessarySat;
        statusBar.Setup(necessarySat, currentSat);
        return AddSatisfaction(currentSat);
    }
    public int AddSatisfaction(Gem gem)
    {
        int sat = fastManager.SatWithFast(gem);
        statusBar.AddProgress(sat);
        currentSat += sat;

        int difference = currentSat - necessarySat;
        if (difference > 0)
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
            statusBar.Activate(); 
            transform.DOLocalMoveY(0, 0.2f); 
            OnCustomerReady?.Invoke(this);
        });
    }
    public async void MoveOut() 
    {
        await statusBar.Deactivate();

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
