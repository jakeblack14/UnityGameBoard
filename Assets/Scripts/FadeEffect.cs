using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class FadeEffect : MonoBehaviour {

    public Image[] characters;
    public Image mainImage;
    public Text text;

    Animator animator;

    private void OnEnable()
    {
        for(int i=0; i<6; i++)
        {
            characters[i].enabled = false;
        }

        text.enabled = false;
        mainImage.enabled = false;
        
    }

    public IEnumerator FadeIn()
    {
        mainImage.enabled = true;
        text.enabled = true;

        for(int i=0; i<6; i++)
        {
            AnimateCharacters(characters[i]);
            yield return new WaitForSecondsRealtime(0.750f);
        }
    }

    private void AnimateCharacters(Image character)
    {
        animator = character.GetComponent<Animator>();
        animator.SetBool("characterEnabled", false);

        character.enabled = true;

        animator.SetBool("characterEnabled", true);
    }

    public IEnumerator EffectsAndLoadScene(string sceneToLoad)
    {
        yield return FadeIn();
        SceneManager.LoadScene(sceneToLoad);
    }
}
