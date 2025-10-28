<<<<<<< Updated upstream
﻿using System.Diagnostics;
=======
﻿using JetBrains.Annotations;
using System.Diagnostics;
>>>>>>> Stashed changes
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

<<<<<<< Updated upstream
    //GUI
    public int score = 0;
    public TextMeshProUGUI scoreText;
    public int lives = 3;
    public Image[] liveImages;

    private bool alive = true;

    private void ScoreTextUpdate()
    {
        scoreText.text = "Score: " + score;
=======
    public int score = 0;
    public static int pelletCounter = 222;
    public TextMeshProUGUI scoreText;
    public static int lives = 3;
    public Image[] liveImages;

    private bool respawning = false;
    private float deathTimer = 0f;
    private float respawnTimer = 2f;
    private Vector3 spawnLocation;

    public TextMeshProUGUI scaredText;
    private float powerUpLength = 0f;
    public static bool powerUpActive = false;
    private bool recovery = false;



    private void ScoreTextUpdate()
    {
        scoreText.text = "Score: " + score.ToString("D6");
    }

    private void ScaredTextUpdate()
    {
        scaredText.text = "Ghost Timer: " + powerUpLength.ToString("F0");
>>>>>>> Stashed changes
    }

    void Start()
    {
<<<<<<< Updated upstream
=======
        spawnLocation = transform.position;
>>>>>>> Stashed changes
        movePoint.parent = null;
        wizardWalk = GetComponent<Animator>();
        
    }

    void Update()
    {
<<<<<<< Updated upstream
        if (!alive) return;
        ScoreTextUpdate();
=======
        ScoreTextUpdate();
        ScaredTextUpdate();
        if (powerUpActive)
        {
            powerUpLength -= Time.deltaTime;

            if (powerUpLength <= 3f && !recovery)
            {
                recovery = true;
            }

            if (powerUpLength <= 0f)
            {
                powerUpActive = false;
                recovery = false;
                BGM.Instance?.PlayNormalBGM();
                var ghosts = FindObjectsOfType<GhostController>();
                foreach (var g in ghosts)
                {
                    if (g != null && g.skeletonAnimator != null)
                        g.skeletonAnimator.Play("WalkRight");
                }
                if (wizardWalk != null)
                {
                    Vector3 animTarget;
                    if (currentDir.sqrMagnitude > 0.0001f)
                        animTarget = transform.position + currentDir;
                    else if (movePoint != null)
                        animTarget = movePoint.position;
                    else
                        animTarget = transform.position + Vector3.right;
                    AnimationDirectionLogic(animTarget);
                }
            }
        }

        if (respawning)
        {
            deathTimer += Time.deltaTime;
            if (deathTimer >= respawnTimer)
            {
                Respawn();
            }
            return;
        }

>>>>>>> Stashed changes
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

<<<<<<< Updated upstream
=======

>>>>>>> Stashed changes
    private void OnTriggerEnter2D(Collider2D pellet)
    {
        if (pellet.CompareTag("Pellet"))
        {
            WalkSounds.clip = Collect;
            WalkSounds.Play();
<<<<<<< Updated upstream
            score += 10;
            Destroy(pellet.gameObject);
=======
            pelletCounter--;
            score += 10;
            Destroy(pellet.gameObject);

            var lo = Object.FindFirstObjectByType<LevelOpening>();
            if (lo != null) lo.WinState();
>>>>>>> Stashed changes
        }
        else if (pellet.CompareTag("PowerPellet"))
        {
            WalkSounds.clip = Collect;
            WalkSounds.Play();
<<<<<<< Updated upstream
            score += 50;
            PowerUp();
            Destroy(pellet.gameObject);
=======
            BGM.Instance?.PlayScaredGhosts();
            pelletCounter--;
            score += 50;
            PowerUp();
            Destroy(pellet.gameObject);

            var lo = FindObjectOfType<LevelOpening>();
            if (lo != null) lo.WinState();
>>>>>>> Stashed changes
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
<<<<<<< Updated upstream
            alive = false;
            PacDeath();
        }
=======
            if (powerUpActive)
            {
                PacKill(pellet.gameObject);
            }
            else
            {
                PacDeath();
            }
        }

>>>>>>> Stashed changes
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
<<<<<<< Updated upstream
        //else
        //{
        //    WalkSounds.clip = Collide;
        //    WalkSounds.Play();
        //}
=======
>>>>>>> Stashed changes

        lastTile = hit;
    }

    private void AnimationDirectionLogic(Vector3 nextPosition)
    {
        Vector3 dir = nextPosition - transform.position;
        if (wizardWalk != null)
        {
<<<<<<< Updated upstream
            if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
            {
                if (dir.x > 0) wizardWalk.Play("WizardRight");
                else wizardWalk.Play("WizardLeft");
            }
            else
            {
                if (dir.y > 0) wizardWalk.Play("WizardUp");
                else wizardWalk.Play("WizardDown");
=======
            if (powerUpActive)
            {
                if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
                {
                    if (dir.x > 0) wizardWalk.Play("SuperWizardRight");
                    else wizardWalk.Play("SuperWizardLeft");
                }
                else
                {
                    if (dir.y > 0) wizardWalk.Play("SuperWizardUp");
                    else wizardWalk.Play("SuperWizardDown");
                }
            }
            else
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
>>>>>>> Stashed changes
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
<<<<<<< Updated upstream

=======
        powerUpActive = true;
        recovery = false;
        powerUpLength = 10f;

        if (wizardWalk != null)
        {
            Vector3 animTarget;
            if (currentDir.sqrMagnitude > 0.0001f)
                animTarget = transform.position + currentDir;
            else if (movePoint != null)
                animTarget = movePoint.position;
            else
                animTarget = transform.position + Vector3.right; 
            AnimationDirectionLogic(animTarget);
        }

        var ghosts = FindObjectsOfType<GhostController>();
        foreach (var g in ghosts)
        {
            if (g != null && g.skeletonAnimator != null)
                g.skeletonAnimator.Play("ScaredRight");
        }
    }

    private void PacKill(GameObject ghost)
    {
        BGM.Instance?.PlayOneGhostDown();
        score += 300;
        var gc = ghost.GetComponent<GhostController>();
        if (gc != null && gc.skeletonAnimator != null)
            gc.skeletonAnimator.Play("DeadRight");
>>>>>>> Stashed changes
    }

    private void PacDeath()
    {
        lives--;
        LifeUpdate();

<<<<<<< Updated upstream
=======
       
        respawning = true;
        deathTimer = 0f;

        currentDir = Vector3.zero;
        nextDir = Vector3.zero;

>>>>>>> Stashed changes
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
<<<<<<< Updated upstream
        alive = true;

    }
=======
        if (lives == 0)
        {
            var lo = FindObjectOfType<LevelOpening>();
            if (lo != null) lo.LoseState();
        }

        transform.position = spawnLocation;
        movePoint.position = spawnLocation;

       
        respawning = false;
        wizardWalk.Play("WizardRight");
    }


>>>>>>> Stashed changes
}