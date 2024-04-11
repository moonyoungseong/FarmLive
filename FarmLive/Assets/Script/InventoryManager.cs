using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] // 직렬화로 인스펙터 창에서도 보이게
public class Item   // 아이템 클래스
{
    public string Type, Name, Explain, Number, Price;
    public bool isUsing;
}

public class InventoryManager : MonoBehaviour
{
    public TextAsset ItemDatabase;  // 메모장 아이템
    public Item MyItem; // 아이템 클래스 사용

    void Start()
    {
        
    }
}
