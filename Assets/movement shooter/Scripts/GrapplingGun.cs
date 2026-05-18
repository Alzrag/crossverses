using UnityEngine;
//large portions taken from dany devs grapply code at https://github.com/DaniDevy/FPS_Movement_Rigidbody/blob/master/GrapplingGun.cs
public class GrapplingGun : MonoBehaviour
{
  private LineRenderer lr;
  private Vector3 endPoint;
  private LayerMask yoinkable;
  public Transform startPoint, camera, player;
  public float maxDistance, realForce;
  private SpringJoint joint;
  private bool yoinking;
  private bool grappling;
  private Rigidbody yoinked;
  public Rigidbody playerRb;
  private Vector3 currentGrapplePosition;

  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start()
  {
      lr=GetComponent<LineRenderer>();
  }

  // Update is called once per frame
  void Update()
  {
    if (Input.GetMouseButtonDown(0)){
      StartGrapple();
    } else if (Input.GetMouseButtonUp(0)){
      StopGrapple();
    }
    if (Input.GetKey(KeyCode.E)&&yoinking){//real in logic
      yoinked.AddForce((startPoint.position - yoinked.transform.position).normalized * realForce, ForceMode.Acceleration);
    } else if (Input.GetKey(KeyCode.E)&&grappling){
      playerRb.AddForce((endPoint - player.position).normalized * realForce, ForceMode.Acceleration);
    }
    DrawRope();
  }

  void StartGrapple(){
    RaycastHit impact;
    if (Physics.Raycast(camera.position, camera.forward, out impact, maxDistance,yoinkable)){
      yoinking=true;
      yoinked = impact.rigidbody;
    } else {
      yoinking =false;
    }
    if (Physics.Raycast(camera.position, camera.forward, out impact, maxDistance)){
      endPoint=impact.point;
      joint = player.gameObject.AddComponent<SpringJoint>();
      joint.autoConfigureConnectedAnchor = false;
      joint.connectedAnchor = endPoint;

      float distanceFromPoint = Vector3.Distance(player.position, endPoint);
      joint.maxDistance = distanceFromPoint * 0.8f;
      joint.minDistance = distanceFromPoint * 0.25f;
      joint.spring = 4.5f;
      joint.damper = 7f;
      joint.massScale = 4.5f;
      lr.positionCount = 2;
      currentGrapplePosition = startPoint.position;
      grappling=true;
    }
  }

  void StopGrapple() {
    lr.positionCount=0;
    if (joint!=null) Destroy(joint);
    grappling=false;
    yoinking=false;
    yoinked=null;
  }

  void DrawRope(){
    if (!grappling && !yoinking){
      lr.positionCount = 0;
      return;
    }

    if (lr.positionCount < 2){
      lr.positionCount = 2;
    }

    if (yoinking && yoinked != null){
      endPoint = yoinked.transform.position;
    }

    if (!joint) return;

    currentGrapplePosition = Vector3.Lerp(currentGrapplePosition, endPoint, Time.deltaTime * 8f);
    
    lr.SetPosition(0, startPoint.position);
    lr.SetPosition(1, currentGrapplePosition);
  }
}
