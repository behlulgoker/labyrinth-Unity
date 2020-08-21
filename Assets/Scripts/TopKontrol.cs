using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopKontrol : MonoBehaviour
{
    public UnityEngine.UI.Text durumYazi, can, zaman;
    public UnityEngine.UI.Button btn;
    private Rigidbody rb;
    public float hiz = 2.0f;
    public float zamanSayaci = 15;
    public float canSayaci = 3;
    bool oyunDevam = true;
    bool oyunTamam = false;
    void Start(){
        rb = GetComponent<Rigidbody>(); 
        can.text = canSayaci + "";
    }

    void Update(){
        if(oyunDevam && !oyunTamam){
            zamanSayaci -= Time.deltaTime;
            zaman.text  = (int)zamanSayaci + "";
        }else if (!oyunTamam){
            durumYazi.text = "Oyun Tamamlanamadı.";
            btn.gameObject.SetActive(true);
        }
        if(zamanSayaci < 0) oyunDevam = false;
        
    }

    void FixedUpdate() {
        if(oyunDevam && !oyunTamam){
            float yatay = Input.GetAxis("Horizontal");
            float dikey = Input.GetAxis("Vertical");
            Vector3 kuvvet = new Vector3(-dikey,0,yatay);
            rb.AddForce(kuvvet*hiz);
        }else{
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }

    private void OnCollisionEnter(Collision cls) {
        string objIsmi = cls.gameObject.name;
        if(objIsmi == "Bitis"){
            oyunTamam = true;
            durumYazi.text = "Oyun Tamamlandı.";
            btn.gameObject.SetActive(true);
        }else if(!objIsmi.Equals("LabZemin") && !objIsmi.Equals("Zemin")){
            canSayaci -= 1;
            can.text = canSayaci + "";
            if(canSayaci == 0) oyunDevam = false;
        }
    }

}
