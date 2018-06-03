using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lines : MonoBehaviour {

    public GameObject linePrefab;

	// Use this for initialization
	void Start () {
        //DrawNewLine(Vector2.zero, Vector2.right * 10);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    float rad = 0f;
    int num = 0;
    public void DrawNewLine(Vector2 start, Vector2 end)
    {

        GameObject go = Instantiate(linePrefab, this.transform);
        go.name = "line" + num;

        LineRenderer lr = linePrefab.GetComponent<LineRenderer>();

        lr.SetPosition(0, start);
        lr.SetPosition(1, end);

        //lr.SetPositions(new Vector3[2] { start, end });

        Color color = Color.gray;
        color.r = rad;

        lr.startColor = color;
        lr.endColor = color;
        //Debug.Log(lr.startColor);
        rad += 0.1f;
        num++;
    }

    public void Flush()
    {
        rad = 0;
        num = 0;
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject.Destroy(transform.GetChild(i).gameObject);
        }
    }
}
