using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    [SerializeField] Transform poolPos;
    [SerializeField] Customer prefab;
    private CustomerPool pool;
    private int satPool;
    private Customer currentCus;
    int moneyFromCustomer;
    CustomerInfo[] customerInfo;
    public int totalMoney = 0;
    void Start()
    {
        pool = new CustomerPool(poolPos, prefab, 10);
        EventManager.instance.OnChiefBonus += AddSat;
        EventManager.instance.OnGameStarted += OnGameStartHandler;
    }
    private void OnGameStartHandler()
    {
        SpawnNewCustomer();
    }
    public void Setup(CustomerInfo[] customer, int moneyFromCustomer)
    {
        this.customerInfo = customer;
        this.moneyFromCustomer = moneyFromCustomer;
    }
    private void OnCusReadyHandler(Customer customer) 
    {
        currentCus = customer;
        if (satPool > 0) 
        {
            satPool = currentCus.AddSatisfaction(satPool);
            UIDebug.Instance.Show("SatPool", $"{satPool}");
        }
    }
    private void OnCusSatHandler(Customer customer) 
    {
        totalMoney += moneyFromCustomer + (moneyFromCustomer * customer.bonus / 100);
        customer.OnCustomerSatisfied -= OnCusSatHandler;
        customer.OnCustomerReady -= OnCusReadyHandler;
        currentCus = null;
        SpawnNewCustomer();
    }
    public void SpawnNewCustomer() 
    {
        Customer customer = pool.Get();
        customer.transform.SetParent(transform);
        customer.Setup(GetCustomerInfo());
        customer.Spawn();
        customer.OnCustomerSatisfied += OnCusSatHandler;
        customer.OnCustomerReady += OnCusReadyHandler;
    }
    private CustomerInfo GetCustomerInfo()
    {
        int totalChance = 0;
        foreach (var info in customerInfo)
        {
            totalChance += info.chancePercent;
        }
        int randomNum = Random.Range(0, totalChance);
        int cumulativeChance = 0;
        foreach (var info in customerInfo)
        {
            cumulativeChance += info.chancePercent;
            if (randomNum < cumulativeChance)
            {
                return info;
            }
        }
        return customerInfo[0];
    }
    private void AddSat(GemType type, int value) 
    {
        satPool += value;
        if (currentCus != null)
        {
            satPool = currentCus.SatWithFast(type, value);
        }
        UIDebug.Instance.Show("SatPool", $"{satPool}");
    } 

}
