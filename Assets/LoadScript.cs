using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Author Egan Paul, Jacob Haas, Liam McIntosh
 */
public class LoadScript : MonoBehaviour
{
    public GameObject Target;
    public GameObject NButton;
    public GameObject SButton;
    public GameObject EButton;
    public GameObject WButton;
    public GameObject NEButton;
    public GameObject NWButton;
    public GameObject SEButton;
    public GameObject SWButton;
    public GameObject Sphere;
    public GameObject[] count;
    private FileRead fileRead;
    public string[] load;


    public Vector3 vec;
    public string SphereName;
    public SceneManager menuScene;
    public string selectedTour;
    public string dropText;


    public bool sphereChecked = false;
    public bool NUsed = false;
    public bool SUsed = false;
    public bool EUsed = false;
    public bool WUsed = false;
    public bool NEUsed = false;
    public bool NWUsed = false;
    public bool SEUsed = false;
    public bool SWUsed = false;

    public bool checkSpheres = false; //check if a sphere is in the location

    Vector3 SphereSpot = new Vector3(0, 0, 0);


    // Use this for initialization
    void Start()
    {
        menuScene = GameObject.FindObjectOfType<SceneManager>();
        dropText = menuScene.UpdateTourSelect();
        selectedTour = "Tours/" + dropText + "/";
        Target.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
        Sphere.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);

    }

    public void startSphere()
    {

        fileRead = FindObjectOfType<FileRead>();

        vec = fileRead.UpdatePosition();
        SphereName = fileRead.UpdateSphereName();

        Sphere.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
        creator(vec, SphereName);


    }

    // Update is called once per frame
    void Update()
    {

    }


    public void creator(Vector3 location, string sphereName)
    {
        string temp2;
        char[] delimiterChars = { 's', 'p', 'h', 'e', 'r' };  //characters to seperate the file path
        string imageCount = sphereName.Trim(delimiterChars); //splits the file path
        if (sphereName != "sphere1")
        {
            var clone = Instantiate(Sphere, location, transform.rotation);

            clone.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);

            Renderer rend = clone.GetComponent<Renderer>();
            temp2 = selectedTour + imageCount;
            rend.material.mainTexture = Resources.Load<Texture>(temp2); //gets next image in folder due to being labeled based off numbers

            clone.name = sphereName;
        }
        else //in first sphere
        {
            Renderer rend = Sphere.GetComponent<Renderer>();
            temp2 = selectedTour + imageCount;
            rend.material.mainTexture = Resources.Load<Texture>(temp2);
        }



    }




    public void checkButtons(bool used, GameObject button, Vector3 vector)//checks to see if the button was used or not so it can make it active or inactive
    {

        bool open = true;
        foreach (GameObject obj in count)
        {

            if (obj.gameObject.transform.localPosition == (Sphere.gameObject.transform.localPosition + vector))
            {
                open = false; //the space has a sphere in it
                break;
            }
            else
            {
                open = true;//the space does not have a sphere in it
            }
        }


        if (used == false)
        {

            button.gameObject.SetActive(false);

        }
        if (open == false)
        {
            button.gameObject.SetActive(true);
        }


    }


    public void spheresMade()
    {
        count = GameObject.FindGameObjectsWithTag("sphere");
        //need these vectors to check each spheres surroundings for other spheres
        Vector3 NVector = new Vector3(0, 35, 0);
        Vector3 SVector = new Vector3(0, -35, 0);
        Vector3 EVector = new Vector3(35, 0, 0);
        Vector3 WVector = new Vector3(-35, 0, 0);
        Vector3 NEVector = new Vector3(35, 35, 0);
        Vector3 NWVector = new Vector3(-35, 35, 0);
        Vector3 SEVector = new Vector3(35, -35, 0);
        Vector3 SWVector = new Vector3(-35, -35, 0);

        foreach (GameObject obj in count)
        {


            if (sphereChecked == false) //work around for it now allowing you to check the second last spheres buttons if you click an inactice button on the final sphere
            {
                //check buttons on current sphere
                if (NButton.activeInHierarchy == true)
                {
                    checkButtons(NUsed, NButton, NVector);
                }
                if (SButton.activeInHierarchy == true)
                {
                    checkButtons(SUsed, SButton, SVector);
                }
                if (EButton.activeInHierarchy == true)
                {
                    checkButtons(EUsed, EButton, EVector);
                }
                if (WButton.activeInHierarchy == true)
                {
                    checkButtons(WUsed, WButton, WVector);
                }
                if (NEButton.activeInHierarchy == true)
                {
                    checkButtons(NEUsed, NEButton, NEVector);
                }
                if (NWButton.activeInHierarchy == true)
                {
                    checkButtons(NWUsed, NWButton, NWVector);
                }
                if (SEButton.activeInHierarchy == true)
                {
                    checkButtons(SEUsed, SEButton, SEVector);
                }
                if (SWButton.activeInHierarchy == true)
                {
                    checkButtons(SWUsed, SWButton, SWVector);
                }
                sphereChecked = true;

            }
        }
    }




    public void TpN()
    {

        Vector3 thePosition = new Vector3(0, 35, 0);
        instantiateTP(thePosition);
        NUsed = true;
    }
    public void TpS()
    {
        Vector3 thePosition = new Vector3(0, -35, 0);
        instantiateTP(thePosition);
        SUsed = true;
    }
    public void TpE()
    {
        Vector3 thePosition = new Vector3(35, 0, 0);
        instantiateTP(thePosition);
        EUsed = true;
    }
    public void TpW()
    {
        Vector3 thePosition = new Vector3(-35, 0, 0);
        instantiateTP(thePosition);
        WUsed = true;
    }
    public void TpNE()
    {
        Vector3 thePosition = new Vector3(35, 35, 0);
        instantiateTP(thePosition);
        NEUsed = true;
    }
    public void TpNW()
    {
        Vector3 thePosition = new Vector3(-35, 35, 0);
        instantiateTP(thePosition);
        NWUsed = true;
    }
    public void TpSE()
    {
        Vector3 thePosition = new Vector3(35, -35, 0);
        instantiateTP(thePosition);
        SEUsed = true;
    }
    public void TpSW()
    {
        Vector3 thePosition = new Vector3(-35, -35, 0);
        instantiateTP(thePosition);
        SWUsed = true;
    }




    public void instantiateTP(Vector3 thePosition)
    {


        foreach (GameObject obj in count) //this code handles when you decide to turn around and go back to previously built spheres, then return the opposite way it keeps track of where you actually are
        {
            for (int i = 1; i <= count.Length; i++)
            {
                if (Target.gameObject.transform.localPosition == GameObject.Find("sphere" + i).transform.localPosition)
                {
                    Sphere = GameObject.Find("sphere" + i);
                    break;
                }
            }
        }

        foreach (GameObject obj1 in count) //checking each sphere to see where it is
        {
            if (obj1.transform.localPosition == SphereSpot)//theres a sphere in that location
            {
                checkSpheres = true;
                break;    //stop looking to see if theres a sphere there
            }
            else //there is no sphere in that location, dont move there
            {
                checkSpheres = false;
            }
        }


        SphereSpot = (Sphere.gameObject.transform.localPosition + thePosition);
        if (checkSpheres == true) //make a sphere 
        {
            Target.gameObject.transform.localPosition = SphereSpot;
        }
        else
        {
            spheresMade();
            Target.gameObject.transform.localPosition = Target.gameObject.transform.localPosition;
        }

        foreach (GameObject obj in count) //this code handles when you decide to turn around and go back to previously built spheres, then return the opposite way it keeps track of where you actually are
        {
            for (int i = 1; i <= count.Length; i++)
            {
                if (Target.gameObject.transform.localPosition == GameObject.Find("sphere" + i).transform.localPosition)
                {
                    Sphere = GameObject.Find("sphere" + i);
                    break;
                }
            }
        }

    }

}