using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public bool _autoGenerate;
    public PlayerObj _prefabObj;
    public List<GameObject> _savedUnitList = new List<GameObject>();
    public Vector3 _startPos;
    public int _columnNum;

    public Transform _playerPool;
    public List<PlayerObj> _playerList = new List<PlayerObj>();
    public PlayerObj _nowObj;
    public Transform _playerObjCircle;
    public Transform _goalObjCircle;
    // Start is called before the first frame update
    void Start()
    {
        if(_autoGenerate) GetPlayerList();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if(hit.collider != null)
            {
                if(hit.collider.CompareTag("Player"))
                {
                    _nowObj = hit.collider.GetComponent<PlayerObj>();
                }
                else
                {
                    //Set move Player object to this point
                    if(_nowObj!=null)
                    {
                        Vector2 goalPos = hit.point;
                        _goalObjCircle.transform.position = hit.point;
                        _nowObj.SetMovePos(goalPos);
                    }
                }
            }
        }

        if(_nowObj!=null)
        {
            _playerObjCircle.transform.position = _nowObj.transform.position;
        }
    }

    public void GetPlayerList()
    {
        _playerList.Clear();
        _savedUnitList.Clear();

        Object[] tObjList = Resources.LoadAll("SPUM/SPUM_Units/",typeof(GameObject));
        foreach(var i in tObjList)
        {
            _savedUnitList.Add(i as GameObject);
        }

        float numXStart = 0;
        float numYStart = 0;

        float numX = 1f;
        float numY = 1f;

        int sColumnNum = _columnNum;

        for(var i = 0 ; i < _savedUnitList.Count;i++)
        {
            if(i > sColumnNum-1)
            {
                numYStart -= 1f;
                numXStart -= numX * _columnNum;
                sColumnNum += _columnNum;
            }
            
            GameObject ttObj = Instantiate(_prefabObj.gameObject) as GameObject;
            ttObj.transform.SetParent(_playerPool);
            ttObj.transform.localScale = new Vector3(1,1,1);

            GameObject tObj = Instantiate(_savedUnitList[i]) as GameObject;
            tObj.transform.SetParent(ttObj.transform);
            tObj.transform.localScale = new Vector3(1,1,1);
            tObj.transform.localPosition = Vector3.zero;

            PlayerObj tObjST = ttObj.GetComponent<PlayerObj>();
            SPUM_Prefabs tObjSTT = tObj.GetComponent<SPUM_Prefabs>();

            tObjST._prefabs = tObjSTT;

            ttObj.transform.localPosition = new Vector3(numXStart + numX * i,numYStart + numY,0);

            _playerList.Add(tObjST);
            
        }
    }
}
