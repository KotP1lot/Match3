using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    [SerializeField] Transform poolPos;
    [SerializeField] Customer prefab;
    private CustomerPool pool;
    private int minSat;
    private int maxSat;
    private int satPool;
    private Customer currentCus;
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
    public void Setup(int minSat, int maxSat) 
    {
        this.minSat = minSat;
        this.maxSat = maxSat;
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
        customer.OnCustomerSatisfied -= OnCusSatHandler;
        customer.OnCustomerReady -= OnCusReadyHandler;
        currentCus = null;
        SpawnNewCustomer();
    }
    public void SpawnNewCustomer() 
    {
        Customer customer = pool.Get();
        customer.transform.SetParent(transform);
        customer.Setup(Random.Range(minSat,maxSat), new CustomerType { isMeh = true, isBad = true});
        customer.Spawn();
        customer.OnCustomerSatisfied += OnCusSatHandler;
        customer.OnCustomerReady += OnCusReadyHandler;
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
