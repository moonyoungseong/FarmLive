using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] // ����ȭ�� �ν����� â������ ���̰�
public class Item   // ������ Ŭ����
{
    public string Type, Name, Explain, Number, Price;
    public bool isUsing;
}

public class InventoryManager : MonoBehaviour
{
    public TextAsset ItemDatabase;  // �޸��� ������
    public Item MyItem; // ������ Ŭ���� ���

    void Start()
    {
        
    }
}
