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
    [SerializeField] GameObject StartPosition;


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
    private GameObject _movingBlock;
    private List<GameObject> _level = new List<GameObject>();
    private bool _editing;

    private string _sceneName;
    private float _playerMass;
    private float _breakableMass;
    private float _bouncePower;
    private float _icynessValue;
    private bool _haveMouse;
    // Use this for initialization
    void Start () {
        _level.Add(StartPosition);
        _editing = false;
        _selectedTile = (NormalBlockBrush);
        _movingBlock = Instantiate(_selectedTile);
        _movingBlock.transform.position = StartPosition.transform.position;
        _movingBlock.name += _level.Count.ToString();
        NormalBlockBrush.transform.rotation = new Quaternion(0, 0, 0, 0);
        BombTileBrush.transform.rotation = new Quaternion(0, 0, 0, 0);
        BreakableTileBrush.transform.rotation = new Quaternion(0, 0, 0, 0);
        MultiDirectionalBoostBrush.transform.rotation = new Quaternion(0, 0, 0, 0);
        OneWayBoostBrush.transform.rotation = new Quaternion(0, 0, 0, 0);
        SlowDownBlockBrush.transform.rotation = new Quaternion(0, 0, 0, 0);
        NormalBlockLongBrush.transform.rotation = new Quaternion(0, 0, 0, 0);
        //Material mat = _selectedTile.GetComponent<Renderer>().material;
        //Color col = mat.color;
        //col.a /= 2;
        //_selectedTile.GetComponent<Renderer>().material.color = col;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveTile(_movingBlock, new Vector3(-1, 0, 0));
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveTile(_movingBlock, new Vector3(1, 0, 0));
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            MoveTile(_movingBlock, new Vector3(0, 0, 1));
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            MoveTile(_movingBlock, new Vector3(0, 0, -1));
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _level.Add(_movingBlock);
            Vector3 pos = _movingBlock.transform.position+new Vector3(0,1,0);
            _movingBlock = Instantiate(_selectedTile);
            _movingBlock.name += _level.Count.ToString();
            _movingBlock.transform.position = pos;
        }
        /**
        if (Input.GetMouseButtonUp(0))
        {
            CreateBlockUnderMouse();
        }
        if (Input.GetMouseButtonUp(1))
        {
            DeleteBlockUnderMouse();
        }
        /**/
    }

    private void MoveTile(GameObject tile, Vector3 direction)
    {
        RaycastHit raycasthit;
        Vector3 newPos = tile.transform.position + direction;
        Ray ray = _camera.ScreenPointToRay(newPos);
        if (Physics.Raycast(newPos, new Vector3(0, -1, 0), out raycasthit))
        {
            if (raycasthit.transform.tag == "Ground" || raycasthit.transform.tag == "StartTile")
            {
                Debug.Log("Was Above:"+raycasthit.transform.name);
                tile.transform.position = raycasthit.transform.position + new Vector3(0, 1, 0) - direction;
            }
        }
        else
        {
            Debug.Log("Nothing Underneath");
            Vector3 pos = tile.transform.position;
            pos.y = StartPosition.transform.position.y;
            tile.transform.position = pos;
        }
        while (Physics.Raycast(tile.transform.position, direction, out raycasthit, 0.5f))
        {
            if (raycasthit.transform.tag == "Ground" || raycasthit.transform.tag == "StartTile")
            {
                Debug.Log("Have to Climb");
                tile.transform.Translate(0, 1, 0);
            }
        }
        tile.transform.Translate(direction);
    }

    public void SelectNormalLong()
    {
        Destroy(_selectedTile);
        _selectedTile = Instantiate(NormalBlockLongBrush);
    }
    public void SelectBomb()
    {
        Destroy(_selectedTile);
        _selectedTile = Instantiate(BombTileBrush);
    }
    public void SelectBreakable()
    {
        Destroy(_selectedTile);
        _selectedTile = Instantiate(BreakableTileBrush);
    }
    public void SelectMultiDirectionalBoost()
    {
        Destroy(_selectedTile);
        _selectedTile = Instantiate(MultiDirectionalBoostBrush);
    }
    public void SelectOneWayBoost()
    {
        Destroy(_selectedTile);
        _selectedTile = Instantiate(OneWayBoostBrush);
    }
    public void SelectSlowDown()
    {
        Destroy(_selectedTile);
        _selectedTile = Instantiate(SlowDownBlockBrush);
    }
    public void SelectNormalBlock()
    {
        Destroy(_selectedTile);
        _selectedTile = Instantiate(NormalBlockBrush);
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
