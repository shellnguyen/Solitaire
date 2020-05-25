using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCard : MonoBehaviour
{
    public string parentTag;

    // Start is called before the first frame update
    private void Start()
    {
        parentTag = this.transform.parent.tag;
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    public void Move(GameObject obj)
    {
        iTween.MoveTo(this.gameObject, obj.transform.position, 1.0f);
        this.transform.SetParent(obj.transform);
        parentTag = this.transform.parent.tag;
    }
}
