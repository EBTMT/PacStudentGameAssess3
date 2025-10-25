using System.Diagnostics;
using UnityEngine;

public class PacStudentController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Transform movePoint;
    private Animator wizardWalk;

    private Vector3 currentDir = Vector3.zero;
    private Vector3 nextDir = Vector3.zero;

    public AudioSource WalkSounds;
    public AudioClip Walk;
    public AudioClip Collect;
    public AudioClip Collide;
    private Collider2D lastTile = null;

    public GameObject dustTrail;

    void Start()
    {
        movePoint.parent = null;
        wizardWalk = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W)) nextDir = Vector3.up;
        else if (Input.GetKeyDown(KeyCode.S)) nextDir = Vector3.down;
        else if (Input.GetKeyDown(KeyCode.A)) nextDir = Vector3.left;
        else if (Input.GetKeyDown(KeyCode.D)) nextDir = Vector3.right;

        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, movePoint.position) <= 0.05f)
        {
            if (nextDir != Vector3.zero && FindFloor(movePoint.position + nextDir))
            {
                currentDir = nextDir;
                nextDir = Vector3.zero;
                movePoint.position += currentDir;
                AnimationDirectionLogic(movePoint.position);
                DustTrailer();
            }
            else if (FindFloor(movePoint.position + currentDir))
            {
                movePoint.position += currentDir;
                DustTrailer();
            }
            else
            {
                currentDir = Vector3.zero;
            }

            FootStepChecker(movePoint.position);
        }
    }

    bool FindFloor(Vector3 targetPos)
    {
        Collider2D hit = Physics2D.OverlapCircle(targetPos, 0.1f);
        if (hit != null && (hit.CompareTag("Wall") || hit.CompareTag("GhostWall")))
            return false;
        return true;
    }

    private void FootStepChecker(Vector3 position)
    {
        Collider2D hit = Physics2D.OverlapCircle(position, 0.1f);
        if (hit != null && hit != lastTile)
        {
            if (hit.CompareTag("Pellet"))
            {
                WalkSounds.clip = Collect;
                WalkSounds.Play();
            }
            else
            {
                WalkSounds.clip = Walk;
                WalkSounds.Play();
            }
        }
        //else
        //{
        //    WalkSounds.clip = Collide;
        //    WalkSounds.Play();
        //}

        lastTile = hit;
    }

    private void AnimationDirectionLogic(Vector3 nextPosition)
    {
        Vector3 dir = nextPosition - transform.position;
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
    }

    void DustTrailer()
    {
        if (dustTrail)
        {
            Vector3 dustPost = movePoint.position - currentDir;
            GameObject dust = Instantiate(dustTrail, dustPost, Quaternion.identity);
            //dust.transform.rotation = Quaternion.LookRotation(Vector3.forward, currentDir);
        }
    }
}