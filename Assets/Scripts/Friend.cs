using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Friend : MonoBehaviour
{
    private BeetMovement beet;
    private UI ui;

    [SerializeField]
    private float score;
    [SerializeField]
    private float speed; // this is subtracted from player so higher speed moves slower

    private enum Status { offboard, onboard, following }
    [SerializeField]
    private Status currentStatus = Status.offboard;
    //info for setup
    private Status startStatus;
    private Vector3 startLocation;

    // Start is called before the first frame update
    void Start()
    {
        beet = FindObjectOfType<BeetMovement>();
        ui = FindObjectOfType<UI>();

        startStatus = currentStatus;
        startLocation = transform.position;
        speed = Mathf.Min(speed, beet.GetSpeed() - 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        switch(currentStatus)
        {
            case Status.offboard:
                break;
            case Status.onboard:
                if (Vector2.Distance(transform.position, beet.transform.position) < 0.2f)
                    CollectFriend();
                break;
            case Status.following:
                transform.position = Vector2.MoveTowards(transform.position, beet.GetFollowLoc(), (beet.GetSpeed() - speed) * Time.deltaTime);
                break;
        }        
    }

    public void CollectFriend()
    {
        //add score to ui tracker
        ui.AddScore(score);
        currentStatus = Status.following;
    }

    //to be called when level resets (if we use friends on random maps)
    public void ResetFriend()
    {
        transform.position = startLocation;
        currentStatus = startStatus;
    }
}
