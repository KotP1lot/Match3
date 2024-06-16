using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    [SerializeField] Transform poolPos;
    [SerializeField] Customer prefab;
    [SerializeField] UIFast fast;
    [SerializeField] StatusBar bar;

    private CustomerPool pool;
    private int satPool;
    private Customer currentCus;
    int moneyFromCustomer;
    [SerializeField]  CustomerInfo[] customerInfo;
    [SerializeField]  db_CustomerSO customersSO;
    public int totalMoney = 0;
    void Start()
    {
        pool = new CustomerPool(poolPos, prefab, 2);
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
        }
    }
    private void OnCusSatHandler(Customer customer) 
    {
        int money = moneyFromCustomer + (moneyFromCustomer * customer.bonus / 100);
        totalMoney += money;
        EventManager.instance.OnMoneyEarned?.Invoke(money);
        customer.OnCustomerSatisfied -= OnCusSatHandler;
        customer.OnCustomerReady -= OnCusReadyHandler;
        currentCus = null;
        SpawnNewCustomer();
    }
    public void SpawnNewCustomer() 
    {
        Customer customer = pool.Get();
        customer.transform.SetParent(transform);
        customer.SetupVisual(customersSO.GetRandomCustomer());
        customer.Setup(GetCustomerInfo(), bar, fast);
        customer.OnCustomerSatisfied += OnCusSatHandler;
        customer.OnCustomerReady += OnCusReadyHandler;
        customer.Spawn();
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
    } 

}
