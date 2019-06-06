using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
//Author Jacob Haas, Liam McIntosh, Egan Paul, Connor Fors
//handles creation of spheres as well as movement between spheres.
//after all spheres are made gets rid of unused buttons and handles movement button them.
public class TP_Script : MonoBehaviour
{


    public GameObject Target;
    public GameObject Sphere;
    public GameObject NButton;
    public GameObject SButton;
    public GameObject EButton;
    public GameObject WButton;
    public GameObject NEButton;
    public GameObject NWButton;
    public GameObject SEButton;
    public GameObject SWButton;

    public string[] load;
    public string[] save;


    public SceneManager menuScene;
    public string selectedTour;
    public string dropText;
    public GameObject dropdown;

    public bool NUsed = false;
    public bool SUsed = false;
    public bool EUsed = false;
    public bool WUsed = false;
    public bool NEUsed = false;
    public bool NWUsed = false;
    public bool SEUsed = false;
    public bool SWUsed = false;


    public bool sphereChecked = false; //keeps track if a spheres buttons have already been checked our not

    public int holdingNum; //used as a holder for the number we will get from UI
    public bool checkSpheres = false; //check if a sphere is in the location
    public GameObject[] count;

    Vector3 SphereSpot = new Vector3(0, 0, 0);

    //if somethings in position and already made, then just move camera, dont instantiate
    //check for canvas boundary, no negatives


    void Start()
    {

        menuScene = GameObject.FindObjectOfType<SceneManager>();
        //sets canvas as parent to both sphere and target with game tag
        Sphere.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
        Target.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
        NButton.name = "NButton" + (count.Length + 1); //names each sphere's button to that button name and the spheres number
        SButton.name = "SButton" + (count.Length + 1);
        EButton.name = "EButton" + (count.Length + 1);
        WButton.name = "WButton" + (count.Length + 1);
        NEButton.name = "NEButton" + (count.Length + 1);
        NWButton.name = "NWButton" + (count.Length + 1);
        SEButton.name = "SEButton" + (count.Length + 1);
        SWButton.name = "SWButton" + (count.Length + 1);
        dropText = menuScene.UpdateTourSelect();

        if (GameObject.FindGameObjectsWithTag("sphere").Length == 1) //means we just started because we only have our first sphere created in the beginning
        {
            Renderer rend = Sphere.GetComponent<Renderer>();
            selectedTour = "Tours/" + dropText + "/";
            string temp = selectedTour + "1";
            rend.material.mainTexture = Resources.Load<Texture>(temp);
            countPics();

        }
    }



    void instantiateTP(Vector3 thePosition)
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


        count = GameObject.FindGameObjectsWithTag("sphere");

        if (holdingNum > count.Length)
        {


            SphereSpot = (Sphere.gameObject.transform.localPosition + thePosition);


            foreach (GameObject obj1 in count) //checking each sphere to see where it is
            {
                if (obj1.transform.localPosition == SphereSpot)//theres a sphere already in that location
                {
                    checkSpheres = false;
                    break;    //stop looking to see if theres a sphere there
                }
                else //there is no sphere in that location, we can make one
                {
                    checkSpheres = true;
                }
            }



            if (checkSpheres == true) //make a sphere 
            {
                creator(SphereSpot);
                saveSpheres(thePosition);
            }
            else //dont make a sphere, only move camera
            {

                Target.gameObject.transform.localPosition = SphereSpot;

            }
        }
        else //this else is what happens after the number of spheres is at the number specified during the UI
        {
            spheresMade(thePosition);
        }

    }



    void spheresMade(Vector3 thePosition)
    {
        //need these vectors to check each spheres surroundings for other spheres
        Vector3 NVector = new Vector3(0, 35, 0);
        Vector3 SVector = new Vector3(0, -35, 0);
        Vector3 EVector = new Vector3(35, 0, 0);
        Vector3 WVector = new Vector3(-35, 0, 0);
        Vector3 NEVector = new Vector3(35, 35, 0);
        Vector3 NWVector = new Vector3(-35, 35, 0);
        Vector3 SEVector = new Vector3(35, -35, 0);
        Vector3 SWVector = new Vector3(-35, -35, 0);

        SphereSpot = (Sphere.gameObject.transform.localPosition + thePosition);


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
            //creates a holder for our sphere and buttons so we can set our sphere and buttons to the next one we're going to
            GameObject sphereHolder = Sphere;
            GameObject NButtonHolder = NButton;
            GameObject SButtonHolder = SButton;
            GameObject EButtonHolder = EButton;
            GameObject WButtonHolder = WButton;
            GameObject NEButtonHolder = NEButton;
            GameObject NWButtonHolder = NWButton;
            GameObject SEButtonHolder = SEButton;
            GameObject SWButtonHolder = SWButton;

            foreach (GameObject obj in count)
            {
                for (int i = 1; i <= count.Length; i++)
                {
                    if ((SphereSpot) == GameObject.Find("sphere" + i).transform.localPosition)//finds sphere thats in spherespots position
                    {
                        Sphere = GameObject.Find("sphere" + i);
                        NButton = GameObject.Find("NButton" + i);
                        SButton = GameObject.Find("SButton" + i);
                        EButton = GameObject.Find("EButton" + i);
                        WButton = GameObject.Find("WButton" + i);
                        NEButton = GameObject.Find("NEButton" + i);
                        NWButton = GameObject.Find("NWButton" + i);
                        SEButton = GameObject.Find("SEButton" + i);
                        SWButton = GameObject.Find("SWButton" + i);
                        break;
                    }
                }
            }


            //checks the buttons for the next sphere we're going into
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
            // set sphere and buttons back to the one we're actually in
            Sphere = sphereHolder;
            NButton = NButtonHolder;
            SButton = SButtonHolder;
            EButton = EButtonHolder;
            WButton = WButtonHolder;
            NEButton = NEButtonHolder;
            NWButton = NWButtonHolder;
            SEButton = SEButtonHolder;
            SWButton = SWButtonHolder;



        }


        foreach (GameObject obj in count) //checking each sphere to see where it is
        {
            if (obj.transform.localPosition == SphereSpot)//theres a sphere already in that location
            {
                checkSpheres = false;
                break;    //stop looking to see if theres a sphere there
            }

            else //there is no sphere in that location
            {
                checkSpheres = true;
            }
        }

        if (checkSpheres)
        {
            Debug.Log("There is no image in that location");//dont move, no sphere
            Target.gameObject.transform.localPosition = Target.gameObject.transform.localPosition;
        }
        else
        {
            SphereSpot = (Sphere.gameObject.transform.localPosition + thePosition);//theres a sphere so move there
            Target.gameObject.transform.localPosition = SphereSpot;
        }

    }











    public void creator(Vector3 spot)
    {

        var clone = Instantiate(Sphere, spot, transform.rotation);




        clone.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
        count = GameObject.FindGameObjectsWithTag("sphere");

        string temp2;

        int picCount = count.Length;
        clone.name = "sphere" + count.Length;
        Renderer rend = clone.GetComponent<Renderer>();
        temp2 = selectedTour + picCount.ToString();
        rend.material.mainTexture = Resources.Load<Texture>(temp2); //gets next image in folder due to being labeled based off numbers
        Target.gameObject.transform.localPosition = spot;





    }

    public void saveSpheres(Vector3 thePosition)
    {
        Stack<string> stack = new Stack<string>();
        stack.Push(Application.dataPath);
        string saveLocation = "";
        while (stack.Count > 0)
        {
            string currentDir = stack.Pop();
            foreach(string dir in Directory.GetDirectories(currentDir))
            {
                if (Path.GetFileName(dir).Equals(dropText)){
                    saveLocation = dir;
                }
                stack.Push(dir);
            }
        }


        if (System.IO.File.Exists(saveLocation + "/" + dropText))
        {
            load = System.IO.File.ReadAllLines(saveLocation + "/" + dropText);

            save = new string[load.Length + 1];
            int i = 0;
            // Display the file contents by using a foreach loop.

            foreach (string line in load)
            {

                save[i] = line;
                i++;
            }

            save[i] = Sphere.gameObject.transform.localPosition.ToString() + " " + Sphere.name;



            if (Sphere.name == "sphere" + (holdingNum - 1))//how to write info about our final sphere
            {

                load = System.IO.File.ReadAllLines(saveLocation + "/" + dropText);

                save = new string[load.Length + 2];
                int l = 0;

                foreach (string line in load)
                {

                    save[l] = line;
                    l++;
                }
                save[l] = Sphere.gameObject.transform.localPosition.ToString() + " " + Sphere.name;
                save[l + 1] = (Sphere.gameObject.transform.localPosition + thePosition).ToString() + " " + "sphere" + holdingNum;




            }

        }
        else//if this is our first sphere we know all of it's info
        {
            save = new string[] { "(0.0, 0.0, 0.0)" + " " + "sphere1" };
        }

        print(saveLocation + "/" + dropText);
        File.WriteAllLines(saveLocation +"/"+ dropText, save);
        print("key3");
        Debug.Log(System.IO.File.Exists(saveLocation + "/" + dropText));

    }


    //Teleport scripts
    //Instantiates a sphere in that direction and moves camera to that position
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




    public void checkButtons(bool used, GameObject button, Vector3 vector)//checks to see if the button was used or not so it can make it active or inactive
    {
        //first sets ALL buttons to inactive so we can find where the spheres actually are to set them active
        bool open = true; //does the position have a sphere or not



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

        //if it wasn't clicked its still false, set inactive

        if (used == false)
        {

            button.gameObject.SetActive(false);

        }


        //if it wasn't clicked its still false, set inactive

        if (open == false)
        {
            button.gameObject.SetActive(true);

        }

    }


    public void countPics() //counts the pictures in the selected folder and add to the total amount of pictures so we know how many circles to make
    {
        string temp3;
        int tempcount = 1;
        temp3 = selectedTour + tempcount;
        while (Resources.Load<Texture>(temp3))
        {
            holdingNum++;
            tempcount++;
            temp3 = selectedTour + tempcount;



        }
    }


}