using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SPUM_PreviewItem : MonoBehaviour
{
    #if UNITY_EDITOR
    public List<GameObject> _objList = new List<GameObject>();
    public Image _basicImage;
    public List<Image> _armorList = new List<Image>();
    public List<Image> _clothList = new List<Image>();
    public List<Image> _pantList = new List<Image>();
    public List<Image> _fullSetList = new List<Image>();


    public void ShowObj(int num)
    {
        for(var i = 0 ; i < _objList.Count;i++)
        {
            if( i==num) _objList[i].SetActive(true);
            else _objList[i].SetActive(false);
        }
    }

    public SPUM_Manager _managerST;
    public int _itemType;
    public Sprite _sprite;
    public string _name;
    public int _index;

    public void SetSprite()
    {
        _managerST.SetSprite(_itemType,_sprite,_name,_index);
    }

    public GameObject DeleteButton;
    public void DeleteObj()
    {
        _managerST.DeleteUnit(_index);
    }
    #endif
}
