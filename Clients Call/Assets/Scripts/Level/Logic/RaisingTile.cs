using UnityEngine;
using DLLLibrary;

public class RaisingTile : MonoBehaviour
{
    [SerializeField] private StoredInfo Info;
    [SerializeField] private float _startDelay;
    [SerializeField] private float _speedOfRaising;
    [SerializeField] private float _waitAfterUp;
    
    private float _difficultyValue;

    // Use this for initialization
    void Start ()
    {
        _difficultyValue = GameObject.FindGameObjectWithTag("Level").GetComponent<LevelConfig>().GetDifficultyValue();
        StartCoroutine(Coroutines.CallVoidAfterSeconds(Up,_startDelay));
	}

    private void Up()
    {
        if (Info.MovableCubes.Count == 0)
        {
            return;
        }
        GameObject obj = Utility.RandomSelectFromList(Info.MovableCubes);

        Info.MovableCubes.Remove(obj);
        obj.GetComponent<State>().Up = true;
        float height = 1;
        if (_waitAfterUp >= 0)
            StartCoroutine(Coroutines.MoveTransformByVector(obj.transform, WhenUp, obj, new Vector3(0, +height, 0), _speedOfRaising));

        if (Info.MovableCubes.Count > 0) StartCoroutine(Coroutines.CallVoidAfterSeconds(Up, _difficultyValue));
    }

    private void WhenUp(GameObject obj)
    {
        StartCoroutine(Coroutines.CallVoidAfterSeconds(Refall, obj, _waitAfterUp));
    }

    private void Refall(GameObject obj)
    {
        StartCoroutine(Coroutines.MoveTransformByVector(obj.transform,ReAddToList,obj, new Vector3(0,-1,0),_speedOfRaising));
    }

    private void ReAddToList(GameObject obj)
    {
        Info.MovableCubes.Add(obj);
        obj.GetComponent<State>().Up = false;
    }
}
