using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterierManager : MonoBehaviour
{
    [SerializeField] PointManager stul1;
    [SerializeField] PointManager stul2;
    [SerializeField] PointManager stil;
    [SerializeField] PointManager light;

    [SerializeField] UICaffeStat stat;
    public void Start()
    {
        stul1.Setup(() => { Show(InteriorType.stul); }, () => { }, () => { });
        stul2.Setup(() => { Show(InteriorType.stul); }, () => { }, () => { });
        stil.Setup(() => { Show(InteriorType.stil); }, () => { }, () => { });
       // light.Setup(() => { Show(InteriorType.stul); }, () => { }, () => { });
    }
    private void Show(InteriorType type) 
    {
        stat.Setup(type);
    }
}
