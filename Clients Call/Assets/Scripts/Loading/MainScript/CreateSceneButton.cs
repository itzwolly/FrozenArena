using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateSceneButton : MonoBehaviour
{
    [SerializeField] Camera _camera;
    [SerializeField] GameObject Player1;
    private bool _createdPlayer1;
    public GameObject GetPlayer1
    {
        get { return Player1;}
    }
    [SerializeField] GameObject Player2;
    public GameObject GetPlayer2
    {
        get { return Player2; }
    }
    private bool _createdPlayer2;
    [SerializeField] GameObject LevelSave;

    [SerializeField] InputField SceneName;
    [SerializeField] InputField PlayerMass;
    [SerializeField] InputField BreakableMass;
    [SerializeField] Slider BouncePower;
    [SerializeField] Slider IcynessValue;
    [SerializeField] Toggle HaveMouse;

    [SerializeField] GameObject StartButtons;
    [SerializeField] GameObject EditButtons;
    [SerializeField] GameObject PopOutButtons;
    [SerializeField] GameObject LastBlockEditor;

    [SerializeField] GameObject StartPosition;

    [SerializeField] GameObject _normalBlockLongBrush;
    public GameObject GetNormalLongBrush
    {
        get {return _normalBlockLongBrush; }
    }
    [SerializeField] GameObject _bombTileBrush;
    public GameObject GetBombTileBrush
    {
        get { return _bombTileBrush; }
    }
    [SerializeField] GameObject _breakableTileBrush;
    public GameObject GetBreakableTileBrush
    {
        get { return _breakableTileBrush; }
    }
    [SerializeField] GameObject _multiDirectionalBoostBrush;
    public GameObject GetMultiDirBoostBrush
    {
        get { return _multiDirectionalBoostBrush; }
    }
    [SerializeField] GameObject _oneWayBoostBrush;
    public GameObject GetOneWayBoostBrush
    {
        get { return _oneWayBoostBrush; }
    }
    [SerializeField] GameObject _slowDownBlockBrush;
    public GameObject GetSlowDownBlockBrush
    {
        get { return _slowDownBlockBrush;}
    }
    [SerializeField] GameObject _normalBlockBrush;
    public GameObject GetNormalBlockBrush
    {
        get { return _normalBlockBrush; }
    }
    [SerializeField] GameObject _deleteBlockBrush;
    [SerializeField] GameObject _selectBlockBrush;
    [SerializeField] PhysicMaterial _bouncyOriginal;
    public PhysicMaterial GetBouncyMaterial
    {
        get { return _bouncyOriginal; }
    }
    [SerializeField] PhysicMaterial _iceOriginal;
    public PhysicMaterial GetIceMaterial
    {
        get { return _iceOriginal; }
    }

    private GameObject _selectedTile;
    private GameObject _movingBlock;
    private GameObject _previousTile;
    private List<GameObject> _bombs = new List<GameObject>();
    private List<GameObject> _level = new List<GameObject>();
    private GameObject _player1;
    private GameObject _player2;
    private bool _editing;
    private bool _popOut;
    private int _popOutCounter;

    private PhysicMaterial _bounceChanged;
    private PhysicMaterial _iceChanged;

    private string _sceneName;
    public string GetSceneName
    {
        get { return _sceneName; }
    }
    private float _playerMass;
    public float GetPlayerMass
    {
        get { return _playerMass; }
    }
    private float _breakableMass;
    public float GetBreakableMass
    {
        get { return _breakableMass; }
    }
    private float _bouncePower;
    public float GetBouncepower
    {
        get { return _bouncePower; }
    }
    private float _icynessValue;
    public float GetIcynessValue
    {
        get { return _icynessValue; }
    }

    private bool _haveMouse;
    private bool _changedBlock;
    private Vector3 _lastPosition;
    // Use this for initialization
    void Start () {
        _level.Add(StartPosition);
        _editing = false;
        _popOut = false;
        _createdPlayer1 = false;
        _createdPlayer2 = false;
        _selectedTile = (_normalBlockBrush);
        _movingBlock = Instantiate(_selectedTile);
        _movingBlock.transform.position = StartPosition.transform.position;
        _movingBlock.name += _level.Count.ToString();
        _normalBlockBrush.transform.rotation = new Quaternion(0, 0, 0, 0);
        _bombTileBrush.transform.rotation = new Quaternion(0, 0, 0, 0);
        _breakableTileBrush.transform.rotation = new Quaternion(0, 0, 0, 0);
        _multiDirectionalBoostBrush.transform.rotation = new Quaternion(0, 0, 0, 0);
        _oneWayBoostBrush.transform.rotation = new Quaternion(0, 0, 0, 0);
        _slowDownBlockBrush.transform.rotation = new Quaternion(0, 0, 0, 0);
        _normalBlockLongBrush.transform.rotation = new Quaternion(0, 0, 0, 0);
        //Material mat = _selectedTile.GetComponent<Renderer>().material;
        //Color col = mat.color;
        //col.a /= 2;
        //_selectedTile.GetComponent<Renderer>().material.color = col;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(_changedBlock)
        {
            _movingBlock = Instantiate(_selectedTile);
            _movingBlock.transform.position = _lastPosition;
            _changedBlock = false;
        }
        if (_editing)
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
            if(Input.GetKey(KeyCode.Space))
            {
                _popOutCounter++;
                if(_popOutCounter>60)
                {
                    StartButtons.SetActive(false);
                    EditButtons.SetActive(false);
                    PopOutButtons.SetActive(true);
                    _popOut = true;
                    _editing = false;
                }
            }
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            //Debug.Log("Popout = "+_popOut+"| Editing = "+_editing);
            if (!_popOut&&_editing)
            {
                if (_selectedTile == _deleteBlockBrush)
                {
                    RaycastHit raycasthit;
                    if (Physics.Raycast(_movingBlock.transform.position, new Vector3(0, -1, 0), out raycasthit))
                    {
                        if (raycasthit.transform.tag == "Ground" || raycasthit.transform.tag == "BreakableTile")
                        {
                            _level.Remove(raycasthit.transform.gameObject);
                            if (_level.Count > 1)
                                _previousTile = _level[_level.Count - 1];
                            else
                                _previousTile = null;
                            Destroy(raycasthit.transform.gameObject);
                        }
                    }

                }
                else if (_selectedTile==_bombTileBrush)
                {
                    _level.Add(_movingBlock);
                    _movingBlock.transform.SetParent(LevelSave.transform);
                    _previousTile = _movingBlock;
                    Vector3 pos = _movingBlock.transform.position + new Vector3(0, 1, 0);
                    _bombs.Add(_movingBlock);
                    _movingBlock = Instantiate(_selectedTile);
                    _movingBlock.name += _level.Count.ToString();
                    _movingBlock.transform.position = pos;
                }
                else if (_selectedTile == Player1)
                {
                    if (_player1 != null)
                    {
                        _player1.transform.position = _movingBlock.transform.position;
                    }
                    else
                    {
                        _level.Add(_movingBlock);
                        _movingBlock.transform.SetParent(LevelSave.transform);
                        _previousTile = _movingBlock;
                        Vector3 pos = _movingBlock.transform.position + new Vector3(0, 1, 0);
                        _player1 = _movingBlock;
                        _player1.GetComponent<Rigidbody>().mass = _playerMass;
                        _player1.GetComponent<Collider>().material = _bounceChanged;
                        _selectedTile = _normalBlockBrush;
                        _movingBlock = Instantiate(_selectedTile);
                        _movingBlock.transform.position = pos;
                        foreach(GameObject obj in _bombs)
                        {
                            obj.GetComponent<BombTile>().Players[0] = _player1;
                        }
                    }
                }
                else if (_selectedTile == Player2)
                {
                    if (_player2 != null)
                    {
                        _player2.transform.position = _movingBlock.transform.position;
                    }
                    else
                    {
                        _level.Add(_movingBlock);
                        _movingBlock.transform.SetParent(LevelSave.transform);
                        _previousTile = _movingBlock;
                        Vector3 pos = _movingBlock.transform.position + new Vector3(0, 1, 0);
                        _player2 = _movingBlock;
                        _player2.GetComponent<Rigidbody>().mass = _playerMass;
                        _player2.GetComponent<Collider>().material = _bounceChanged;
                        _selectedTile = _normalBlockBrush;
                        _movingBlock = Instantiate(_selectedTile);
                        _movingBlock.transform.position = pos;
                        foreach (GameObject obj in _bombs)
                        {
                            obj.GetComponent<BombTile>().Players[1] = _player2;
                        }
                    }
                }
                else if(_selectedTile==_selectBlockBrush)
                {
                    RaycastHit raycasthit;
                    if (Physics.Raycast(_movingBlock.transform.position, new Vector3(0, -1, 0), out raycasthit))
                    {
                        if (raycasthit.transform.tag == "Ground" || raycasthit.transform.tag == "BreakableTile")
                        {
                            _previousTile = raycasthit.transform.gameObject;
                            Destroy(_movingBlock);
                            _changedBlock = true;
                            _selectedTile = Instantiate(raycasthit.transform.gameObject);
                        }
                    }
                }
                else if(_selectedTile==_normalBlockBrush)
                {
                    _level.Add(_movingBlock);
                    _movingBlock.transform.SetParent(LevelSave.transform);
                    _previousTile = _movingBlock;
                    Vector3 pos = _movingBlock.transform.position + new Vector3(0, 1, 0);
                    _movingBlock.GetComponent<Collider>().material = _iceChanged;
                    _movingBlock = Instantiate(_selectedTile);
                    _movingBlock.name += _level.Count.ToString();
                    _movingBlock.transform.position = pos;
                }
                else
                {

                    _level.Add(_movingBlock);
                    _movingBlock.transform.SetParent(LevelSave.transform);
                    _previousTile = _movingBlock;
                    Vector3 pos = _movingBlock.transform.position + new Vector3(0, 1, 0);
                    _movingBlock = Instantiate(_selectedTile);
                    _movingBlock.name += _level.Count.ToString();
                    _movingBlock.transform.position = pos;
                }
            }
            else
            {
                EditButtons.SetActive(true);
                PopOutButtons.GetComponent<PopOutMenuScript>().Deselect();
                PopOutButtons.SetActive(false);
                _popOut = false;
                _editing = true;
                try
                {
                    //Debug.Log("Doing the action");
                    PopOutButtons.GetComponent<PopOutMenuScript>().ToDo();
                }
                catch
                {
                    //Debug.Log("Nothing to do");
                }
            }
            _popOutCounter = 0;
        }
    }

    private void MoveTile(GameObject tile, Vector3 direction)
    {
        RaycastHit raycasthit;
        Vector3 newPos = tile.transform.position + direction;
        if (Physics.Raycast(newPos, new Vector3(0, -1, 0), out raycasthit))
        {
            if (raycasthit.transform.tag == "Ground" || raycasthit.transform.tag == "BreakableTile" || raycasthit.transform.tag == "StartTile")
            {
                //Debug.Log("Was Above:"+raycasthit.transform.name);
                tile.transform.position = raycasthit.transform.position + new Vector3(0, 1, 0) - direction;
            }
        }
        else
        {
            //Debug.Log("Nothing Underneath");
            Vector3 pos = tile.transform.position;
            pos.y = StartPosition.transform.position.y;
            tile.transform.position = pos;
        }
        while (Physics.Raycast(tile.transform.position, direction, out raycasthit, 0.5f))
        {
            if (raycasthit.transform.tag == "Ground" || raycasthit.transform.tag == "BreakableTile" || raycasthit.transform.tag == "StartTile")
            {
                //Debug.Log("Have to Climb");
                tile.transform.Translate(0, 1, 0);
            }
        }
        tile.transform.Translate(direction);
    }

    public void LoadLevel()
    {
        _editing = false;
        LevelSave.GetComponent<LoadSaveLevelScript>().LoadLevel(
            out _sceneName,out _playerMass,out _breakableMass,
            out _bouncePower,out _icynessValue);
        _editing = true;
    }

    public void EditLastTile()
    {
        _editing = false;
        LastBlockEditor.SetActive(true);
        LastBlockEditor.GetComponent<EditPrevTileScript>().EditObject(_previousTile);
        //_previousTile;
    }
    public void StopEditLastTile()
    {
        _editing = true;
        LastBlockEditor.SetActive(false);

        //_previousTile;
    }

    public void SelectNormalLong()
    {
        _lastPosition = _movingBlock.transform.position;
        Destroy(_movingBlock);
        _changedBlock = true;
        _selectedTile = (_normalBlockLongBrush);
    }
    public void SelectBomb()
    {
        _lastPosition = _movingBlock.transform.position;
        Destroy(_movingBlock);
        _changedBlock = true;
        _selectedTile = (_bombTileBrush);
    }
    public void SelectBreakable()
    {
        _lastPosition = _movingBlock.transform.position;
        Destroy(_movingBlock);
        _changedBlock = true;
        _selectedTile = (_breakableTileBrush);
        _selectedTile.GetComponent<Rigidbody>().useGravity = false;
        _selectedTile.GetComponent<Rigidbody>().mass = _breakableMass;
    }
    public void SelectMultiDirectionalBoost()
    {
        _lastPosition = _movingBlock.transform.position;
        Destroy(_movingBlock);
        _changedBlock = true;
        _selectedTile = (_multiDirectionalBoostBrush);
    }
    public void SelectOneWayBoost()
    {
        _lastPosition = _movingBlock.transform.position;
        Destroy(_movingBlock);
        _changedBlock = true;
        _selectedTile = (_oneWayBoostBrush);
        _selectedTile.GetComponent<OneWayBoost>().enabled = false;
    }
    public void SelectPlayer1()
    {
        _lastPosition = _movingBlock.transform.position;
        Destroy(_movingBlock);
        if (!_createdPlayer1)
        {
            _changedBlock = true;
            _selectedTile = (Player1);
            _selectedTile.GetComponent<PlayerMovement>().enabled = false;
            _selectedTile.GetComponent<Rigidbody>().useGravity = false;
            _createdPlayer1 = true;
        }
        else
        {
            _selectedTile = (_normalBlockBrush);
        }
    }
    public void SelectPlayer2()
    {
        _lastPosition = _movingBlock.transform.position;
        Destroy(_movingBlock);
        if (!_createdPlayer2)
        {
            _changedBlock = true;
            _selectedTile = (Player2);
            _selectedTile.GetComponent<PlayerMovement>().enabled = false;
            _selectedTile.GetComponent<Rigidbody>().useGravity = false;
            _createdPlayer2 = true;
        }
        else
        {
            _selectedTile = (_normalBlockBrush);
        }
    }
    public void SelectSlowDown()
    {
        _lastPosition = _movingBlock.transform.position;
        Destroy(_movingBlock);
        _changedBlock = true;
        _selectedTile = (_slowDownBlockBrush);
    }
    public void SelectNormalBlock()
    {
        _lastPosition = _movingBlock.transform.position;
        Destroy(_movingBlock);
        _changedBlock = true;
        _selectedTile = (_normalBlockBrush);
    }
    public void SelectDeleteBlock()
    {
        _lastPosition = _movingBlock.transform.position;
        Destroy(_movingBlock);
        _changedBlock = true;
        _selectedTile = (_deleteBlockBrush);
    }
    public void SelectSelectBlock()
    {
        _lastPosition = _movingBlock.transform.position;
        Destroy(_movingBlock);
        _changedBlock = true;
        _selectedTile = (_selectBlockBrush);
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
        _bounceChanged = Instantiate(_bouncyOriginal);
        _bounceChanged.bounciness = _bouncePower;
        _iceChanged = Instantiate(_iceOriginal);
        _iceChanged.dynamicFriction = _icynessValue;
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
