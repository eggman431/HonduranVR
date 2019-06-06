using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;

/*
 * Author: Liam McIntosh 
 */

public class SceneManager : MonoBehaviour {
    
    public GameObject dropdown1, dropdown2;
    int dropvalue;
    static public string dropdownText;
    List<string> list = new List<string>();
    List<string> fileName = new List<string>();
    List<string> tempList = new List<string>();

    void Start()
    {
        GetResourcesDirectories();


        try { 

            dropdown1 = GameObject.FindGameObjectWithTag("Dropdown1"); //finds the dropdown in the create menu scene   
            dropdown2 = GameObject.FindGameObjectWithTag("Dropdown2"); //finds the dropdown in the load menu scene

            if (dropdown1) //returns true if in the create menu
            {
                dropdown1.GetComponent<Dropdown>().options.Clear(); //makes sure the dropdown is empty
                dropdown1.GetComponent<Dropdown>().AddOptions(list); //populates the dropdown with all the tours in the tour folder
            }

            if (dropdown2)  //returns true if in the load menu
            {

                dropdown2.GetComponent<Dropdown>().options.Clear(); //makes sure the dropdown is empty

                int count = 0;

                while (count < list.Count) //goes through the names of the tours found in the tour folder
                {
                    if (System.IO.File.Exists(fileName[count] + "/" + list[count])) //looks to see if there is a saved file in the tours folder
                    {
                        tempList.Add(list[count]);  //adds the name of the tour to a list if the file is found
                    }
                    count++;

                }
            dropdown2.GetComponent<Dropdown>().AddOptions(tempList);  //the list of tours that had a saved file is added to the dropdown

            }

        }
        catch
        {
        }
      
       
       
    }


    public void GetResourcesDirectories()
    {
        Stack<string> stack = new Stack<string>();
        // Add the root directory to the stack
        stack.Push(Application.dataPath); // all the directories in the project
        // While we have directories to process...
        while (stack.Count > 0)
        {
            // Grab a directory off the stack
            string currentDir = stack.Pop();
           
            try
            {
                foreach (string dir in Directory.GetDirectories(currentDir)) // all the directories in the project
                {
                    char[] delimiterChars = { ':', '/', '\\' };  //characters to seperate the file path
                    string[] words = dir.Split(delimiterChars); //splits the file path
                    int x = words.Length - 2; 
                    if (words[x].Equals("Tours")) /*  the tour folder with all the images is the last name in the words [], in order to identify that it is a tour and not
                        another directory within the project we see if its parent folder is Tours.  Tours is a unique folder name found within the resouce folder allowing us to 
                        find the tours and filter out all the other directories.
                        */
                    {
                        list.Add(Path.GetFileName(dir));  //adds the filename to the list
                        fileName.Add(dir);   //the file name of the tour folder to be used for load menu
                        //print(list[list.Count]);
                    }
                   
                    // Add directories at the current level into the stack
                    stack.Push(dir);
                }
            }
            catch
            {
                Debug.LogError("Directory " + currentDir + " couldn't be read from.");
            }
        }
    }

    public string UpdateTourSelect()
    {
        return dropdownText;
    }


    public void CreateTourTransition()     //loads the create tour scene
    {
         dropvalue = dropdown1.GetComponent<Dropdown>().value; // finds the selected value of the drop down
         dropdownText = dropdown1.GetComponent<Dropdown>().options[dropvalue].text;  //gets the text from the selected value (the text is the name of the tour)
                
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
  
    }
 

    public void MainMenuTransition()    //Sends the user back to the main menu
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);  
         
    }

    public void LoadTourTransition()  //send the user to the load tour scene
    {
        dropvalue = dropdown2.GetComponent<Dropdown>().value; // finds the selected value of the drop down
        dropdownText = dropdown2.GetComponent<Dropdown>().options[dropvalue].text;  //gets the text from the selected value (the text is the name of the tour)

        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }



    public void LoadMenuTransition()    //Sends the user to the load tour menu
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(3);

    }

    public void CreateMenuTransition()    //Sends the user to the create tour menu
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(4);

    }


}
