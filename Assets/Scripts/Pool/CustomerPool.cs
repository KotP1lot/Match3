using UnityEngine;

public class CustomerPool : PoolBase<Customer>
{
    public CustomerPool(Transform pos, Customer prefab, int preloadCount) :
        base(() => Preload(pos, prefab), (customer) => GetAction(customer, pos), (customer) => ReturnAction(customer, pos), preloadCount)
    { }
    public static Customer Preload(Transform pos, Customer prefab)
    {
        Customer customer = Object.Instantiate(prefab, pos);
        return customer;
    }
    public static void GetAction(Customer customer, Transform pos)
    {
        customer.gameObject.SetActive(true);
        customer.OnExitAnimFinished += (customer) => ReturnAction(customer, pos);
    }
    public static void ReturnAction(Customer customer, Transform pos)
    {
        customer.gameObject.SetActive(false);
        customer.transform.SetParent(pos);
    }
}
