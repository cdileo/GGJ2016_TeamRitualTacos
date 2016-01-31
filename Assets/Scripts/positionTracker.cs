using UnityEngine;
using System.Collections;

public class positionTracker : MonoBehaviour {

    // public vars
    public GameObject partner;
    public positionTracker partnerTracker;
    public GameObject boss;
    public float moveSpeed = 5f;
    public float moveCoolDown = 2f;
    public bool moveEnabled = false;
    public bool debugMe;
    public float partnerDistanceThreshold = 1.5f;
    public float nearBossThreshold = 4f;
    public bool isNearBoss = false;
    public bool readyToAttack = false;
    public float lastMove;
    public Vector2 lastPosition;

    // private
    private Transform partnerTransform;
    private bool isMoving = false;

    private const float piBy8 = Mathf.PI / 8;
    

    void Start () {
        if (partner != null)
        {
            partnerTransform = partner.transform;
            partnerTracker = partner.GetComponent<positionTracker>();
            if (partnerTracker == null)
                print("You forgot to attach a tracker to one of the chars");
            lastPosition = transform.position;
        }
	}
	
    // State machine for what options are available - 1 per cardinal and half-cardinal dirs

	void Update () {
        isMoving = false;

        if (debugMe && Input.GetButtonDown("Jump"))
        {
            print("Partner direction = " + partnerDirection());
            print("Partner is within our threshold distance: " + checkPartnerDistance());
        }

        //if (Input.GetAxis("Vertical") != 0 && moveEnabled) {
        //    this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + moveSpeed * Time.deltaTime * Input.GetAxis("Vertical"), this.transform.position.z);
        //    lastMove = Time.time;
        //    isMoving = true;
        //}
        //if (Input.GetAxis("Horizontal") != 0 && moveEnabled) {
        //    this.transform.position = new Vector3(this.transform.position.x + moveSpeed * Time.deltaTime * Input.GetAxis("Horizontal"), this.transform.position.y, this.transform.position.z);
        //    lastMove = Time.time;
        //    isMoving = true;
        //}
        //print("Last pos = " + lastPosition + "  Current position = " + (Vector2)transform.position);
        isMoving = !(lastPosition == (Vector2) transform.position);
        lastPosition = transform.position;
    }

    // dump all that decision logic here
    public bool canAttack()
    {
        readyToAttack = (Time.time - lastMove) > moveCoolDown;
        return readyToAttack;
    }

    // positional cases as follows:
    /*
    this character is +
    3 2 1 
    4 + 0
    5 6 7
    */
    private int partnerDirection()
    {
        float relativeAngle = 0;
        float myY = transform.position.y;
        float myX = transform.position.x;
        float theirY = partnerTransform.position.y; 
        float theirX = partnerTransform.position.x;
        int ret;

        relativeAngle = Mathf.Atan2(theirY - myY, theirX - myX);
        if (debugMe) { print("Relative angle from " + this.name + " to " + partner.name + " is: " + relativeAngle); }

        float noSign = Mathf.Abs(relativeAngle);

        // tried a bunch of ternary operators, VS complained heavily....
        if (noSign < piBy8)
        {
            return 0;
        } else if (noSign < 3 * piBy8)
        {
            if (relativeAngle > 0)
                ret = 1;
            else
                ret = 7;
        } else if (noSign < 5 * piBy8)
        {
            if (relativeAngle > 0)
            {
                ret = 2;
            } else
            {
                ret = 6;
            }
        } else if (noSign < 7 * piBy8)
        {
            if(relativeAngle > 0)
            {
                ret = 3;
            } else
            {
                ret = 5;
            }
        }
        else
        {
            ret = 4;
        }
        
        return ret;
    }

    private bool checkPartnerDistance()
    {
        return (Vector2.Distance(transform.position, partnerTransform.position) < partnerDistanceThreshold);
    }

    public int getPartnerDirection()
    {
        if (checkPartnerDistance())
        {
            return partnerDirection();
        }
        else
        {
            return -1;
        }
    }

    public bool checkNearBoss()
    {
        return Vector2.Distance(transform.position, boss.transform.position) < nearBossThreshold;
    }

    public bool getIsMoving()
    {
        return isMoving;
    }
}
