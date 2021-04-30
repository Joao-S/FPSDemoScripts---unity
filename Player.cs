using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController _controller;
    private UIManager _uiManager;
    private GameObject _audioManager;
    private AudioSource _shootingSound;
    [SerializeField]
    private GameObject _weapon;
    [SerializeField]
    private GameObject _crate;
    private GameObject _muzzleFlash;
    [SerializeField]
    private GameObject _hitMarkerPreFab;
    private GameObject _hitMarker;
    private float _speed = 4.0f;
    private float _gravity = 1.0f;
    private float _mouseSensitivity =4.0f;
    private int _maxTotalAmmo=2000;
    private int _totalAmmo=600;
    private int _maxCurrentAmmo=200;
    private int _currentAmmo=200;
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _shootingSound = GetComponent<AudioSource>();
        _audioManager = GameObject.Find("AudioManager");
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        CalculateMovement();
        CalculateRotation();
        if (_weapon.activeInHierarchy)
        {
            if (Input.GetMouseButton(0) && _currentAmmo > 0)
            {
                Fire();
            }
            else
            {
                _muzzleFlash.SetActive(false);
                _shootingSound.enabled = false;
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                Reload();
            }
        }

    }

    void CalculateMovement()
    {
        Vector3 _direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Vector3 _velocity = _direction * _speed * Time.deltaTime;
        _velocity.y += -_gravity * Time.deltaTime;
        _velocity = transform.transform.TransformDirection(_velocity);
        _controller.Move(_velocity);
    }
    void CalculateRotation()
    {
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y + Input.GetAxis("Mouse X")*_mouseSensitivity, transform.localEulerAngles.z);
    }

    void Fire()
    {
        _muzzleFlash.SetActive(true);
        _shootingSound.enabled = true;

        Ray _rayorigin = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit _hitinfo;

        if(Physics.Raycast(_rayorigin,out _hitinfo))
        {
            Debug.Log("Hit: " + _hitinfo.transform.name);
            _hitMarker = Instantiate(_hitMarkerPreFab, _hitinfo.point, Quaternion.LookRotation(_hitinfo.normal));
            Destroy(_hitMarker,0.2f);
            if (_hitinfo.transform.gameObject.CompareTag("Crate"))
            {
                Instantiate(_crate,_hitinfo.transform.position,Quaternion.identity);
                Destroy(_hitinfo.transform.gameObject);
                _audioManager.GetComponent<AudioSource>().Play();
            }
        }
        _currentAmmo--;
        _uiManager.UpdateAmmo(_currentAmmo, _totalAmmo);
    }
    void Reload()
    {
        int _ammotrade = _maxCurrentAmmo - _currentAmmo;
        if (_ammotrade > 0 && _totalAmmo > 0)
        {
            if (_ammotrade <= _totalAmmo)
            {
                _currentAmmo += _ammotrade;
                _totalAmmo -= _ammotrade;
            }
            else if (_ammotrade > _totalAmmo)
            {
                _currentAmmo += _totalAmmo;
                _totalAmmo = 0;
            }
        }
        _uiManager.UpdateAmmo(_currentAmmo, _totalAmmo);
    }

    public void EnableWeapon()
    {
        _weapon.SetActive(true);
        _muzzleFlash = GameObject.Find("Muzzle_Flash");
        _uiManager.UpdateAmmo(_currentAmmo,_totalAmmo);
    }

}
