using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/*
 *Author: Liam McIntosh, Jacob Haas, Egan Paul 
 */
public class FileRead : MonoBehaviour
{
    
    private SceneManager menuScene;
    private LoadScript createSphere;
    public Vector3 vec;
    string sphereName;
    public string[] load;

    // Use this for initialization
    void Start()
    {

        menuScene = GameObject.FindObjectOfType<SceneManager>();

        string file = menuScene.UpdateTourSelect();

        loadSpheres(file);


    }

    // Update is called once per frame
    void Update()
    {

    }


    //read the file to make the spheres
    public void loadSpheres(string fileName)
    {

        Stack<string> stack = new Stack<string>();
        stack.Push(Application.dataPath);
        string saveLocation = "";
        while (stack.Count > 0)
        {
            string currentDir = stack.Pop();
            foreach (string dir in Directory.GetDirectories(currentDir))
            {
                if (Path.GetFileName(dir).Equals(fileName))
                {
                    saveLocation = dir;
                }
                stack.Push(dir);
            }
        }


        string vector;
        createSphere = FindObjectOfType<LoadScript>();

        Debug.Log(System.IO.File.Exists(saveLocation + "/" + fileName));

        load = System.IO.File.ReadAllLines(saveLocation + "/" + fileName);
        Debug.Log(System.IO.File.Exists(saveLocation + "/" + fileName));

        foreach (string line in load)
        {
         
            vector = line.Substring(0, line.IndexOf(")") + 1);

            sphereName = line.Substring(line.IndexOf(")") + 2);

            
            vec = StringToVector3(vector);
         

            createSphere.startSphere();
        }

        createSphere.spheresMade();

    }

    public Vector3 StringToVector3(string sVector)
    {
        // Remove the parentheses


        if (sVector.StartsWith("(") && sVector.EndsWith(")"))
        {
            sVector = sVector.Substring(1, sVector.Length - 2);
        }

        // split the items
        string[] sArray = sVector.Split(',');
        // store as a Vector3
        Vector3 result = new Vector3(
            float.Parse(sArray[0]),
            float.Parse(sArray[1]),
            float.Parse(sArray[2]));



        return result;
    }


    public Vector3 UpdatePosition()
    {
        return vec;
    }

    public string UpdateSphereName()
    {
        return sphereName;
    }
}