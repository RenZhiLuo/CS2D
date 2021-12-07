using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class TabControlPanel : MonoBehaviour
{

    [Serializable]
    public class TabCell
    {
        public Button btn;
        public GameObject panel;
        public void Open(bool value)
        {
            panel.SetActive(value);
            btn.interactable = !value;
        }
    }

    [SerializeField] private TabCell[] tabCells;

    private int lastIndex = 0;

    private void Start()
    {
        for (int i = 0; i < tabCells.Length; i++)
        {
            int index = i;
            tabCells[i].btn.onClick.AddListener(() => { OnClickTab(index); });
            tabCells[i].Open(i == 0);
        }
    }

    public void OnClickTab(int index)
    {
        tabCells[lastIndex].Open(false);
        tabCells[index].Open(true);
        lastIndex = index;
    }
}
