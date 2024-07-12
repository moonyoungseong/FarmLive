using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using UnityEngine.UI;

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
    public GameObject[] Slot, UsingImage;   // 슬롯 할당
    public Image[] TabImage, ItemImage;   // 슬롯에 이미지 칸
    public Sprite[] ItemSprite; // 아이템 이미지

    void Start()
    {
        string[] line = ItemDatabase.text.Substring(0, ItemDatabase.text.Length - 1).Split('\n');

        for (int i = 0; i < line.Length; i++)
        {
            string[] row = line[i].Split('\t');
            AllItemList.Add(new Item(row[0], row[1], row[2], row[3], row[4], row[5] == "TRUE"));
        }

        //Save();
        Load();
    }

    public void TabClick(string tabName)
    {
        curType = tabName;
        CurItemList = MyItemList.FindAll(x => x.Type == tabName);

        for (int i = 0; i < Slot.Length; i++)
        {
            // 슬롯과 텍스트 보이기, 아이템 리스트와 동일할게 할당
            bool isExist = i < CurItemList.Count;
            Slot[i].SetActive(isExist);
            Slot[i].GetComponentInChildren<Text>().text = isExist ? CurItemList[i].Name : "";

            if (isExist)
            {
                ItemImage[i].sprite = ItemSprite[AllItemList.FindIndex(x => x.Name == CurItemList[i].Name)];
            }
        }
    }


    void Save()
    {
        string jdata = JsonConvert.SerializeObject(AllItemList);
        File.WriteAllText(Application.dataPath + "/Resources/MyItemText.txt", jdata);

        TabClick(curType);
    }

    void Load()
    {
        string jdata = File.ReadAllText(Application.dataPath + "/Resources/MyItemText.txt");
        MyItemList = JsonConvert.DeserializeObject<List<Item>>(jdata);

        TabClick(curType);
    }
}
