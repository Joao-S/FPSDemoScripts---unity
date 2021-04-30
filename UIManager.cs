using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    private GameObject _player;
    private Player _playerScript;
    private GameObject _ammo;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player");
        _playerScript = _player.GetComponent<Player>();
        _ammo = GameObject.Find("Ammotxt");
    }

    // Update is called once per frame
    void Update()
    {

        
    }

    public void UpdateAmmo(int _currentAmmo, int _totalAmmo)
    {
        _ammo.GetComponent<Text>().text = _currentAmmo + "/" + _totalAmmo;
    }


}
