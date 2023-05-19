using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuControl : MonoBehaviour
{
    public static MenuControl instance;

    [SerializeField] List<Menu> _menus;

    public void OpenMenu(string menuName)
    {
        foreach (Menu menu in _menus)
        {
            if(menu.menuName == menuName)
            {
                menu.Open();
            }
            else
            {
                menu.Close();
            }
        }
    }
    private void Awake()
    {
        instance = this;
    }
}
