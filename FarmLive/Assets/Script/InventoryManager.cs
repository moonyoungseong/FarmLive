using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

[System.Serializable] // ����ȭ�� �ν����� â������ ���̰�
public class Item   // ������ Ŭ����
{
    public Item(string _Type, string _Name, string _Explain, string _Number, string _Price, bool _isUsing)
    { Type = _Type; Name = _Name; Explain = _Explain; Number = _Number; Price = _Price; isUsing = _isUsing; }

    public string Type, Name, Explain, Number, Price;
    public bool isUsing;
}

public class InventoryManager : MonoBehaviour
{
    public TextAsset ItemDatabase;  // �޸��� ������
    public List<Item> AllItemList, MyItemList;

    void Start()
    {
        string[] line = ItemDatabase.text.Substring(0, ItemDatabase.text.Length - 1).Split('\n');

        for (int i = 0; i < line.Length; i++)
        {
            string[] row = line[i].Split('\t');
            AllItemList.Add(new Item(row[0], row[1], row[2], row[3], row[4], row[5] == "TRUE"));
        }

        Save();
    }

    void Save()
    {
        string jdata = JsonConvert.SerializeObject(AllItemList);
        print(jdata);
        File.WriteAllText(Application.dataPath + "/Resources/MyItemText.txt", jdata);
    }
}
