using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sharkman : MonoBehaviour
{
    private Text _buyTxt;
    private bool _canBuy;
    private Player _player;

    // Start is called before the first frame update
    private Image _coinImg;
    void Start()
    {
        _coinImg = GameObject.Find("Coinimg").GetComponent<Image>();
        _buyTxt = GameObject.Find("BuyGun").GetComponent<Text>();
        _player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && _canBuy && _coinImg.enabled)
        {
            _coinImg.enabled = false;
            _player.EnableWeapon();
            GetComponent<AudioSource>().Play();
            _buyTxt.text = "";
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _buyTxt.text = "[E] Buy a Gun (1 coin)";
            _canBuy = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _buyTxt.text = "";
            _canBuy = false;
        }
    }
}
