using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coin : MonoBehaviour
{
    private Text _pickUptxt;
    private bool _canPickUp;
    private Image _coinImg;
    private AudioSource _pickUpSound;
    // Start is called before the first frame update
    void Start()
    {
        _pickUptxt = GameObject.Find("PickUp").GetComponent<Text>();
        _pickUpSound = GetComponent<AudioSource>();
        _coinImg = GameObject.Find("Coinimg").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)&&_canPickUp)
        {
            _pickUptxt.text = "";
            _canPickUp = false;
            _pickUpSound.Play();
            GetComponent<MeshRenderer>().enabled = false;
            _coinImg.enabled = true;
            Destroy(gameObject,1.0f);
        }   
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            _pickUptxt.text = "[E] Pick Up";
            _canPickUp = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            _pickUptxt.text = "";
            _canPickUp = false;
        }
    }
}
