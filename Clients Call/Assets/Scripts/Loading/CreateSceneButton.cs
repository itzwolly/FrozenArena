using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateSceneButton : MonoBehaviour
{
    [SerializeField] Camera _camera;

    [SerializeField] InputField SceneName;
    [SerializeField] InputField PlayerMass;
    [SerializeField] InputField BreakableMass;
    [SerializeField] Slider BouncePower;
    [SerializeField] Slider IcynessValue;
    [SerializeField] Toggle HaveMouse;
    [SerializeField] GameObject StartButtons;
    [SerializeField] GameObject EditButtons;

    [SerializeField] GameObject NormalBlockLongBrush;
    [SerializeField] GameObject BombTileBrush;
    [SerializeField] GameObject BreakableTileBrush;
    [SerializeField] GameObject MultiDirectionalBoostBrush;
    [SerializeField] GameObject OneWayBoostBrush;
    [SerializeField] GameObject SlowDownBlockBrush;
    [SerializeField] GameObject NormalBlockBrush;
    [SerializeField] PhysicMaterial BouncyOriginal;
    [SerializeField] PhysicMaterial IceOriginal;

    private GameObject _selectedTile;
    private bool _editing;

    private string _sceneName;
    private float _playerMass;
    private float _breakableMass;
    private float _bouncePower;
    private float _icynessValue;
    private bool _haveMouse;
    // Use this for initialization
    void Start () {
        _editing = false;
        _selectedTile = NormalBlockBrush;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonUp(0))
        {
            CreateBlockUnderMouse();
        }
        if (Input.GetMouseButtonUp(1))
        {
            DeleteBlockUnderMouse();
        }

    }

    public void SelectNormalLong()
    {
        _selectedTile = (NormalBlockLongBrush);
    }
    public void SelectBomb()
    {
        _selectedTile = (BombTileBrush);
    }
    public void SelectBreakable()
    {
        _selectedTile = (BreakableTileBrush);
    }
    public void SelectMultiDirectionalBoost()
    {
        _selectedTile = (MultiDirectionalBoostBrush);
    }
    public void SelectOneWayBoost()
    {
        _selectedTile = (OneWayBoostBrush);
    }
    public void SelectSlowDown()
    {
        _selectedTile = (SlowDownBlockBrush);
    }
    public void SelectNormalBlock()
    {
        _selectedTile = (NormalBlockBrush);
    }

    public void StartSceneCreation()
    {
        Debug.Log("Starting Creation");

        if (SceneName.text.Length <= 0)
        {
            _sceneName = "New Level";
        }
        else
        {
            _sceneName = SceneName.text;
        }
        try
        {
            _playerMass = Convert.ToSingle(PlayerMass.text);
        }
        catch
        {
            _playerMass = 50;
        }
        try
        {
            _breakableMass = Convert.ToSingle(BreakableMass.text);
        }
        catch
        {
            _breakableMass = 25;
        }
        try
        {
            _bouncePower = BouncePower.value;
        }
        catch
        {
            _bouncePower = 0.8f;
        }
        try
        {
            _icynessValue = IcynessValue.value;
        }
        catch
        {
            _icynessValue = 0.1f;
        }
        _haveMouse = HaveMouse.isOn;

        StartButtons.SetActive(false);
        _editing = true;
        EditButtons.SetActive(true);
    }
    private void CreateBlockUnderMouse()
    {
        RaycastHit raycasthit;
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out raycasthit))
        {
            GameObject target = raycasthit.collider.gameObject;

            if (target.tag=="Ground" || target.tag == "BreakableTile" || target.tag == "StartTile")
            {
                /**/
                Vector3 spawnPoint = target.transform.position;
                Vector3 spawnNormal = raycasthit.normal;
                spawnNormal.Normalize();
                spawnPoint += spawnNormal;

                /**
                Vector3 spawnPoint = target.transform.position;
                //spawnPoint.x += 1f;
                if (raycasthit.point.y<spawnPoint.y)
                    spawnPoint.y -= target.transform.lossyScale.y;
                else if (raycasthit.point.y > spawnPoint.y)
                    spawnPoint.y += target.transform.lossyScale.y;
                if (raycasthit.point.x < spawnPoint.x)
                    spawnPoint.x -= target.transform.lossyScale.x;
                else if (raycasthit.point.x > spawnPoint.x)
                    spawnPoint.x += target.transform.lossyScale.x;
                if (raycasthit.point.z > spawnPoint.z)
                    spawnPoint.z += target.transform.lossyScale.z;
                else if (raycasthit.point.z < spawnPoint.z)
                    spawnPoint.z -= target.transform.lossyScale.z;
                //spawnPoint.z -= 0.5f;
                /**/
                CreateCube(spawnPoint,_selectedTile);
            }
            //else if(target.CompareTag((Tags.TagName.WorldCube.ToString())))
            //{
            //    Vector3 spawnPoint = target.transform.position + raycasthit.normal;
            //    CreateCube(spawnPoint);p
            //}
        }
    }


    private void DeleteBlockUnderMouse()
    {
        RaycastHit raycasthit;
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out raycasthit))
        {
            GameObject target = raycasthit.collider.gameObject;
            if (target.tag=="Ground" || target.tag == "BreakableTile")
            {
                Destroy(target);
            }
        }
    }

    public void CreateCube(Vector3 pPosition,GameObject brush)
    {
        GameObject newCube = GameObject.Instantiate(brush);
        newCube.transform.position = pPosition;
        //newCube.transform.Translate(0,0.5f,0); 
    }


}
