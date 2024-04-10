using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    [SerializeField] Transform poolPos;
    [SerializeField] Customer prefab;
    private CustomerPool pool;
    private float minSat;
    private float maxSat;
    private float satPool;
    private Customer currentCus;
    void Start()
    {
        pool = new CustomerPool(poolPos, prefab, 10);
        EventManager.instance.OnGemDestroy += AddSat;
    }
    public void Setup(float minSat, float maxSat) 
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
        customer.Setup(Random.Range(minSat,maxSat));
        customer.Spawn();
        customer.OnCustomerSatisfied += OnCusSatHandler;
        customer.OnCustomerReady += OnCusReadyHandler;
    }
    private void AddSat(Gem gem) 
    {
        satPool += gem.GetScore();
        if (currentCus != null)
        {
            satPool = currentCus.AddSatisfaction(satPool);
        }
        UIDebug.Instance.Show("SatPool", $"{satPool}");
    } 
}
