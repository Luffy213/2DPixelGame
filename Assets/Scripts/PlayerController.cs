using UnityEngine;
using Photon.Pun;

public class PlayerController : MonoBehaviour
{ 
    [SerializeField] private float _speed;
    private PhotonView _photonView;

    private Rigidbody2D _rb;

    [SerializeField] private FixedJoystick _joystick;

    private Vector2 _moveVeclocity;
    [SerializeField] private bool _subscribe;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _photonView = GetComponent<PhotonView>();
       
    }
    void Update()
    {
        if(_joystick.Horizontal>0)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        if(_joystick.Horizontal<0)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        if(_joystick.Horizontal!=0)
        {
            _subscribe = true;
        }
        else
        {
            _subscribe = false;
        }
        Vector2 moveInput = new(_joystick.Horizontal, _joystick.Vertical);
        _moveVeclocity = moveInput.normalized * _speed;
       /* if (!_photonView.IsMine)
            return;*/
    }
    private void FixedUpdate()
    {
        /*if(_photonView.IsMine)
        {
            _rb.MovePosition(_rb.position + _moveVeclocity * Time.deltaTime);
        }*/
        _rb.MovePosition(_rb.position + _moveVeclocity * Time.deltaTime);
    }
}
