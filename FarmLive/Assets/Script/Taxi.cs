using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Taxi : MonoBehaviour
{
    public GameObject taxibtn; // ��ư 3�� ��� ��
    public GameObject Player; 

    public Button Home_button; // �� �̵� ��ư
    public Button Shop_button;  // Ʈ�� �� �̵� ��ư
    public Button Office_button;  // �繫�� �̵� ��ư

    void Start()
    {
        Home_button.onClick.AddListener(Button1Clicked);
        Shop_button.onClick.AddListener(Button2Clicked);
        Office_button.onClick.AddListener(Button3Clicked);
    }

    private void OnCollisionEnter(Collision collision)  // �ýÿ� ����
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            taxibtn.gameObject.SetActive(true); 
        }
    }

    private void OnCollisionExit(Collision collision) // �ýÿ��� ������
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            taxibtn.gameObject.SetActive(false); 
        }
    }

    public void Button1Clicked()    // �� ��ǥ
    {
        Player.transform.position = new Vector3(39.6f, 0f, -176f);
    }

    public void Button2Clicked()    // Ʈ�� �ŷ� ��ǥ
    {
        Player.transform.position = new Vector3(-171f, 0f, -6f);
    }

    public void Button3Clicked()    // �ֹμ���
    {
        Player.transform.position = new Vector3(116f, 0f, 29f);
    }
}
