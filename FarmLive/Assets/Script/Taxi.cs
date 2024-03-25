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

    private void OnCollisionEnter(Collision collision) 
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            taxibtn.gameObject.SetActive(true); 
        }
    }

    private void OnCollisionExit(Collision collision) 
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            taxibtn.gameObject.SetActive(false); 
        }
    }

    public void Button1Clicked()    // 
    {
        Player.transform.position = new Vector3(8.1f, -0.4f, -45f);
    }

    public void Button2Clicked()    
    {
        Player.transform.position = new Vector3(-93f, -0.4f, -140f);
    }

    public void Button3Clicked()    
    {
        Player.transform.position = new Vector3(-184f, -0.4f, -184f);
    }
}
