using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using Random = UnityEngine.Random;

public class Ball : MonoBehaviour
{
    [SerializeField] private float _vSpeed = 8f;
    [SerializeField] private float _hSpeed = 0f;
    private float _vDirection = 8f;
    private bool _sideHit = false;

    private bool _faceUp = true;

    void Start()
    {
        _hSpeed = Random.Range(-1f, 1f);
        _sideHit = SideHit();
    }
    //[SerializeField] 
    //private UIManager _uiManager;
    // Update is called once per frame

    void Update()
    {
        Wall();
        if (_faceUp)
        {
            transform.Translate(Vector3.up * Time.deltaTime * _vSpeed
                                + Vector3.right * Time.deltaTime * (_hSpeed * 6));
            _sideHit = SideHit();
        }
        else
        {
            transform.Translate(Vector3.down * Time.deltaTime * _vSpeed
                                + Vector3.right * Time.deltaTime * (_hSpeed * 6));
            _sideHit = SideHit();
        }

        if (transform.position.y > 4.8f)
        {
            _faceUp = false;
            _sideHit = SideHit();
        }
        else if (transform.position.y < -5.2f)
        {
            Destroy(this.gameObject);
            GameObject.FindWithTag("Player").GetComponent<Player>().LoseBall();
            //FindObjectOfType<Player>().LoseBall();
            //_uiManager.SubBall();
        }
        
    }


    void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.name);
        if (other.CompareTag("Player"))
        {
            //hit at the side
            _sideHit = SideHit();
            _faceUp = true;
            _hSpeed += other.GetComponent<Player>()._movSpeed;
            if (_hSpeed > 6f)
            {
                _hSpeed = 6f;
            }
            else if (_hSpeed < -6f)
            {
                _hSpeed = -6f;
            }
        }
        else if (other.tag.Contains("Collider"))
        {
            // Debug.Log("Relevant"+_sideHit);
            if (_sideHit)
            {
                _hSpeed = -_hSpeed;
                Debug.Log("Yes");
            }
            else
            {
                _faceUp = !_faceUp;
            }
            
        }
    }

    void Wall()
    {
        if (transform.position.x > 8.7f)
        {
            _hSpeed = Math.Abs(_hSpeed) * -1;
        }
        else if (transform.position.x < -8.7f)
        {
            _hSpeed = Math.Abs(_hSpeed);
        }
    }

    private bool SideHit()
    {
        _vDirection = _vSpeed;
        if (!_faceUp)
        {
            _vDirection = -_vDirection;
        }

        //nach jedem Richtungswechsel?
        RaycastHit hit;
        //_hSpeed immer positiv!?
        Ray ray = new Ray(transform.position, new Vector3(_hSpeed, _vDirection, 0));
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider != null)
            {
                // Debug.Log(hit.collider.name == "HCollider");
                return(hit.collider.name == "HCollider"); //.GetComponent(tag) 
                //collider kann von ball sein!!!!!!!!!!!!! -> _vDirction
                // Debug.Log(hit.point);
            }
        }

        return false;
    }
}