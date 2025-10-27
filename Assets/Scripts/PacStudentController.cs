using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
    public AudioClip Death;
    private Collider2D lastTile = null;

    public GameObject dustTrail1;
    public GameObject dustTrail2;
    private bool dustStart = true;

    public GameObject wallCollide;
    private bool wallCollided = false;

    //GUI
    public int score = 0;
    public TextMeshProUGUI scoreText;
    public int lives = 3;
    public Image[] liveImages;

    private bool alive = true;

    private void ScoreTextUpdate()
    {
        scoreText.text = "Score: " + score;
    }

    void Start()
    {
        movePoint.parent = null;
        wizardWalk = GetComponent<Animator>();
        
    }

    void Update()
    {
        if (!alive) return;
        ScoreTextUpdate();
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
                wallCollided = false;

            }
            else if (FindFloor(movePoint.position + currentDir))
            {
                movePoint.position += currentDir;
                DustTrailer();
                wallCollided = false;
            }
            else
            {
                if (!wallCollided)
                {
                    wallCollided = true;

                    WalkSounds.clip = Collide;
                    WalkSounds.Play();

                    Vector3 barrierPos = transform.position + currentDir * 0.7f;
                    float angle = Mathf.Atan2(currentDir.y, currentDir.x) * Mathf.Rad2Deg;
                    if (wallCollide != null)
                        GameObject.Instantiate(wallCollide, barrierPos, Quaternion.Euler(0, 0, angle));
                }
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

    private void OnTriggerEnter2D(Collider2D pellet)
    {
        if (pellet.CompareTag("Pellet"))
        {
            WalkSounds.clip = Collect;
            WalkSounds.Play();
            score += 10;
            Destroy(pellet.gameObject);
        }
        else if (pellet.CompareTag("PowerPellet"))
        {
            WalkSounds.clip = Collect;
            WalkSounds.Play();
            score += 50;
            PowerUp();
            Destroy(pellet.gameObject);
        }
        else if (pellet.CompareTag("Cherry"))
        {
            WalkSounds.clip = Collect;
            WalkSounds.Play();
            score += 100;
            Destroy(pellet.gameObject);
        }
        else if (pellet.CompareTag("Ghost"))
        {
            alive = false;
            PacDeath();
        }
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
        if (currentDir != Vector3.zero)
        {
            Vector3 dustPos = transform.position - currentDir * 0.3f;

            GameObject whichDust = dustStart ? dustTrail1 : dustTrail2;
            dustStart = !dustStart;

            if (whichDust != null)
            {
                GameObject dust = Instantiate(whichDust, dustPos, Quaternion.identity);
                float angle = Mathf.Atan2(currentDir.y, currentDir.x) * Mathf.Rad2Deg;
                dust.transform.rotation = Quaternion.Euler(0, 0, angle - 90);
            }
        }
    }
    public void TeleportArriveSetDirection(Vector3 inboundDir)
    {
        if (inboundDir.sqrMagnitude > 0.0001f)
            currentDir = inboundDir.normalized;
        else
            currentDir = Vector3.zero;

        nextDir = Vector3.zero;

        if (movePoint != null)
        {
            if ((movePoint.position - transform.position).sqrMagnitude < 0.01f)
                movePoint.position = transform.position + currentDir;
        }
        AnimationDirectionLogic(movePoint != null ? movePoint.position : transform.position);
    }

    private void PowerUp()
    {

    }

    private void PacDeath()
    {
        lives--;
        LifeUpdate();

        wizardWalk.Play("WizardDeath");
        WalkSounds.clip = Death;
        WalkSounds.Play();
    }

    private void LifeUpdate()
    {
        for (int i = 0; i < liveImages.Length; i++)
        {
            if (i < lives)
                liveImages[i].enabled = true;
            else
                liveImages[i].enabled = false;
        }
    }

    private void Respawn()
    {
        alive = true;

    }
}