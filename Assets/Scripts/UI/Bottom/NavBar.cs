using System;
using UnityEngine;
using UnityEngine.UI;

public class NavBar : MonoBehaviour
{
    [Serializable]
    public struct NavBarInfo 
    {
        public NavBtn btn;
        public Image panel;

        public void SetActive(bool isActive) 
        {
            panel.gameObject.SetActive(isActive);
            btn.SetActive(isActive);
        }
    }
    public NavBarInfo[] navBars;

    private void Start()
    {
        foreach (NavBarInfo navBar in navBars) 
        {
            navBar.btn.OnNavBtnClick += OnBtnClick;
        }
        OnBtnClick(navBars[1].btn);
    }

    private void OnBtnClick(NavBtn btn) 
    {
        foreach (NavBarInfo navBar in navBars) 
        {
            if (navBar.btn == btn)
            {
                navBar.SetActive(true);
                continue;
            }
            navBar.SetActive(false);
        }
    }
}
