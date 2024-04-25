using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

[System.Serializable] // 직렬화로 인스펙터 창에서도 보이게
public class Item   // 아이템 클래스
{
    public Item(string _Type, string _Name, string _Explain, string _Number, string _Price, bool _isUsing)
    { Type = _Type; Name = _Name; Explain = _Explain; Number = _Number; Price = _Price; isUsing = _isUsing; }

    public string Type, Name, Explain, Number, Price;
    public bool isUsing;
}

public class InventoryManager : MonoBehaviour
{
    public TextAsset ItemDatabase;  // 메모장 아이템
    public List<Item> AllItemList, MyItemList, CurItemList;
    public string curType = "Seed"; // 아이템 구분
    public GameObject[] Slot;   // 슬롯 할당

    void Start()
    {
        string[] line = ItemDatabase.text.Substring(0, ItemDatabase.text.Length - 1).Split('\n');

        for (int i = 0; i < line.Length; i++)
        {
            string[] row = line[i].Split('\t');
            AllItemList.Add(new Item(row[0], row[1], row[2], row[3], row[4], row[5] == "TRUE"));
        }

        Load();
    }

    public void TabClick(string tabName)
    {
        curType = tabName;
        CurItemList = MyItemList.FindAll(x => x.Type == tabName);

        for (int i = 0; i < Slot.Length; i++)
        {
            Slot[i].SetActive(i < CurItemList.Count);
        }
    }


        void Save()
    {
        string jdata = JsonConvert.SerializeObject(AllItemList);
        print(jdata);
        File.WriteAllText(Application.dataPath + "/Resources/MyItemText.txt", jdata);
    }
    
    void Load()
    {
        string jdata = File.ReadAllText(Application.dataPath + "/Resources/MyItemText.txt");
        MyItemList = JsonConvert.DeserializeObject<List<Item>>(jdata);

        TabClick(curType);
    }
}
