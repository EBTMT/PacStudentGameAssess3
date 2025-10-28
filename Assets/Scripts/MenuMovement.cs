using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMovement : MonoBehaviour
{
    [SerializeField]
    private GameObject item;
    private Tweener tweener;
    private List<GameObject> itemList;
    private List<Vector2> locations;
    private Vector2 nextPosition;
    private int nextAfterPosition;
    private Animator wizardWalk;
    public MenuMovement()
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
        locations.Add(new Vector2(50, -110));
        locations.Add(new Vector2(-80, -110));
        nextPosition = locations[0];
        nextAfterPosition = 1;

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
                    }
                    break;
                }

            }
        }
        else
        {
            if (nextAfterPosition == 2)
            {
                nextAfterPosition = 0;
            }
            nextPosition = locations[nextAfterPosition];
            nextAfterPosition++;
        }


    }
}

