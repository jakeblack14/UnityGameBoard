using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackButtonManager : MonoBehaviour {

    public List<GameObject> MenuPanels;
    public GameObject BackButton;

    void Update () {
		if(MenuPanels[0].activeSelf)
        {
            BackButton.SetActive(false);
        }
        else
        {
            BackButton.SetActive(true);
        }
	}

    public void backButtonClicked()
    {
        int index = 0;

        for(int i=0; i<4; i++)
        {
            if (MenuPanels[i].activeSelf)
            {
                index = i;
            }
        }

        MenuPanels[index].SetActive(false);
        MenuPanels[0].SetActive(true);
    }
	

}
