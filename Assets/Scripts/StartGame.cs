using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    [SerializeField] private GameObject _score;
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private Aim _aim;

    private void Start()
    {
    }

    public void StartLevel()
    {
        _aim.SetActiveAim();// почему то не активируется канвас, не могу понять в чем же дело, хелп ми
        _score.SetActive(true);
        _mainMenu.SetActive(false);
    }
}
