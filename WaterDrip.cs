using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class LineBuild2 : MonoBehaviour
{
public GameObject parent;
public List<GameObject> ptList = new List<GameObject>();
public int ptListLength;
public Shader shad1;
void Start()
{
    GetKids();
    TurnOffKids();
    }
    IEnumerator Wait()
    {
        int l;
        var t = 0.75f;
        for (l = 0; l < ptListLength; l++)
        {
        int x = 1;
        yield return new WaitForSeconds(t);
        BringKidsBack(l);
        int rInc = RIncrement(x);
        int gInc = GIncrement(x);
        int bInc = BIncrement(x);
        rInc = rInc * -1;
        gInc = gInc * -1;
        bInc = bInc * -1;
        x++;
    }
}

public void TurnOffKids()
{
    int j;
    for (j = 0; j < ptListLength; j++)
    {
        var rndr = ptList[j].GetComponent<MeshRenderer>();
        rndr.enabled = false;
    }
}

public void GetKids()
{
    GameObject pt;
    int i;
    ptListLength = 0;
    for (i = 0; i < parent.transform.childCount; i++)
    {
        int childIndex = i;
        pt = parent.transform.GetChild(childIndex).gameObject;
        ptList.Add(pt);
        ptListLength++;
    }
    ptList.Reverse();
}

public void BringKidsBack(int count)
{
    var rndr2 = ptList[count].GetComponent<MeshRenderer>();
    rndr2.enabled = true;
}

public void SetPointColor(int count, int rCol, int gCol, int bCol, int kidIndex)
{
    var ptMat = ptList[count].GetComponent<MeshRenderer>().material;
    var shad2 = ptMat.shader = Shader.Find("shad1");
    Color currColor = new Color();
    currColor = ptList[count].GetComponent<MeshRenderer>().material.color;
    var currRrGB = currColor.r;
    var currGrGB = currColor.g;
    var currBrGB = currColor.b;
    int decrValR = rCol;
    int decrValG = gCol;
    int decrValB = bCol;
    var rCol2 = currRrGB - decrValR;
    var gCol2 = currGrGB - decrValG;
    var bCol2 = currBrGB - decrValB;
    var newColor = new Color(rCol2, gCol2, bCol2, 1);
    ptMat.SetColor("_Color", newColor);
}

public int RIncrement(int k)
{
    //Starting RGB value - ending RGB value / total number of children
    int r1 = 255;
    int r2 = 0;
    int rIncrement = r1 - r2 / ptListLength;
    return rIncrement;
}

public int GIncrement(int k)
{
    int g1 = 255;
    int g2 = 175;
    int gIncrement = g1 - g2 / ptListLength;
    return gIncrement;
}

public int BIncrement(int k)
{
    int b1 = 255;
    int b2 = 222;
    int bIncrement = b1 - b2 / ptListLength;
    return bIncrement;
}

void Update()
{
    StartCoroutine(Wait());
}
}