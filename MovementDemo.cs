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
    private Animator WizardWalk;
    public MovementDemo()
    {
        itemList = new List<GameObject>();
        locations = new List<Vector2>();
    }

    void Start()
    {
        item = this.gameObject;
        tweener = GetComponent<Tweener>();
        WizardWalk = GetComponent<Animator>();
        itemList.Add(item);
        locations.Add(new Vector2(1, -5));
        locations.Add(new Vector2(6, -5));
        locations.Add(new Vector2(6, -1));
        locations.Add(new Vector2(1, -1));
        nextPosition = locations[0];
        nextAfterPosition = 1;
    }

    void Update()
    {
        if ((Vector2)transform.position != nextPosition)
        {
            for (int i = 0; i < itemList.Count; i++)
            {
                if (tweener.AddTween(itemList[i].transform, itemList[i].transform.position, nextPosition, 60f))
                {
                    Vector2 dir = nextPosition - (Vector2)transform.position;
                    if (WizardWalk != null)
                    {
                        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
                        {
                            if (dir.x > 0) WizardWalk.Play("WizardRight");
                            else WizardWalk.Play("WizardLeft");
                        }
                        else
                        {
                            if (dir.y > 0) WizardWalk.Play("WizardUp");
                            else WizardWalk.Play("WizardDown");
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

