using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Taxi : MonoBehaviour
{
    public GameObject taxibtn; // 버튼 3개 담는 것
    public GameObject Player; 

    public Button Home_button; // 집 이동 버튼
    public Button Shop_button;  // 트럭 쪽 이동 버튼
    public Button Office_button;  // 사무소 이동 버튼

    void Start()
    {
        Home_button.onClick.AddListener(Button1Clicked);
        Shop_button.onClick.AddListener(Button2Clicked);
        Office_button.onClick.AddListener(Button3Clicked);
    }

    private void OnCollisionEnter(Collision collision)  // 택시에 닿음
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            taxibtn.gameObject.SetActive(true); 
        }
    }

    private void OnCollisionExit(Collision collision) // 택시에서 떨어짐
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            taxibtn.gameObject.SetActive(false); 
        }
    }

    public void Button1Clicked()    // 집 좌표
    {
        Player.transform.position = new Vector3(39.6f, 0f, -176f);
    }

    public void Button2Clicked()    // 트럭 거래 좌표
    {
        Player.transform.position = new Vector3(-171f, 0f, -6f);
    }

    public void Button3Clicked()    // 주민센터
    {
        Player.transform.position = new Vector3(116f, 0f, 29f);
    }
}
