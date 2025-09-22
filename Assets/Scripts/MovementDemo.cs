using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementDemo : MonoBehaviour
{
    [SerializeField]
    private GameObject item;
    private Tweener tweener;
    private List<GameObject> itemList;
    private List<Vector2> locations;
    private Vector2 nextPosition;
    private int nextAfterPosition;
    private Animator wizardWalk;
    public AudioClip footSteps;
    public MovementDemo()
    {
        itemList = new List<GameObject>();
        locations = new List<Vector2>();
    }

    void Start()
    {
        item = this.gameObject;
        tweener = GetComponent<Tweener>();
        wizardWalk = GetComponent<Animator>();
        itemList.Add(item);
        locations.Add(new Vector2(1, -5));
        locations.Add(new Vector2(1, -1));
        locations.Add(new Vector2(6, -1));
        locations.Add(new Vector2(6, -5));
        nextPosition = locations[0];
        nextAfterPosition = 1;
        AudioSource walkNoise = gameObject.AddComponent<AudioSource>();
        walkNoise.clip = footSteps;
        walkNoise.loop = true;
        walkNoise.Play();
    }

    void Update()
    {
        if ((Vector2)transform.position != nextPosition)
        {
            for (int i = 0; i < itemList.Count; i++)
            {
                if (tweener.AddTween(itemList[i].transform, itemList[i].transform.position, nextPosition, 30f))
                {
                    Vector2 dir = nextPosition - (Vector2)transform.position;
                    if (wizardWalk != null)
                    {
                        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
                        {
                            if (dir.x > 0) wizardWalk.Play("WizardRight");
                            else wizardWalk.Play("WizardLeft");
                        }
                        else
                        {
                            if (dir.y > 0) wizardWalk.Play("WizardUp");
                            else wizardWalk.Play("WizardDown");
                        }
                    }
                    break;
                }

            }
        }
        else
        {
            if (nextAfterPosition == 4)
            {
                nextAfterPosition = 0;
            }
            nextPosition = locations[nextAfterPosition];
            nextAfterPosition++;
        }


    }
}

