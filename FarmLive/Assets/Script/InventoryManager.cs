using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using UnityEngine.UI;

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
    public List<Item> AllItemList, MyItemList, CurItemList;
    public string curType = "Seed"; // ������ ����
    public GameObject[] Slot, UsingImage;   // ���� �Ҵ�
    public Image[] TabImage, ItemImage;   // ���Կ� �̹��� ĭ
    public Sprite[] ItemSprite; // ������ �̹���

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
            // ���԰� �ؽ�Ʈ ���̱�, ������ ����Ʈ�� �����Ұ� �Ҵ�
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
