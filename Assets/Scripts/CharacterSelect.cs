using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterSelect : MonoBehaviour
{
    private PlayerInfo info;
    // Start is called before the first frame update
    void Start()
    {
        info = GameObject.Find("Preloaded").GetComponent<PlayerInfo>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ActivateGODeactiveSelf(GameObject obj)
    { 
        gameObject.SetActive(false);
        obj.SetActive(true);
    }
}
