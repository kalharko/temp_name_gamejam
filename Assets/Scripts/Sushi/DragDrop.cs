using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDrop : MonoBehaviour
{

    private Vector3 _dragOffset; //to reduce the offset when just clicking on the object
    [SerializeField] private float _dragSpeed = 10;
    [SerializeField] private float _returnSpeed = 10;
    private Camera _camera;

    private Transform _anchorPoint;

    public AudioSource pickUpAudioSource;
    public AudioClip pickUpAudioClip;
    public AudioSource dropAudioSource;
    public AudioClip dropAudioClip;

    void Start()
    {
        // Find the anchor point, which is the position of the sushi object
        _anchorPoint = transform.parent;

        // Find the camera
        _camera = Camera.main;
    }

    Vector3 GetMousePosition() {
        var mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        return mousePosition;
    }
    public void OnMouseDown() {

        pickUpAudioSource.time = 0;
        pickUpAudioSource.PlayOneShot(pickUpAudioClip);
        StartCoroutine(StopAudioAfterDelay(pickUpAudioSource, 1f));

        _dragOffset = transform.position - GetMousePosition();
        
        // Store the dragged sushi instance
        GameManager.Instance.DraggedSushi = gameObject;
    }

    public void OnMouseDrag() {
         // Update the position of the object being dragged
        transform.position = Vector3.MoveTowards(transform.position, GetMousePosition() + _dragOffset, _dragSpeed * Time.deltaTime) ;
    }

    public void OnMouseUp()
    {


        if (!GameManager.Instance.IsSushiValid)
        {
            GameManager.Instance.UpdateGameState(GameState.HasFailed);
            StartCoroutine(ReturnToAnchorPoint());
            return;
        }

        StopMovingSushi();
        GameManager.Instance.UpdateGameState(GameState.HasSucceeded);
    }

    private void StopMovingSushi()
    {
        dropAudioSource.time = 0;
        dropAudioSource.PlayOneShot(dropAudioClip);
        StartCoroutine(StopAudioAfterDelay(dropAudioSource, 1f));
        StartCoroutine(ScaleDown());
        
        // _anchorPoint.position = transform.position;
        _anchorPoint.GetComponent<SushiBehavior>().StopFollowingSpline();
    }

    // Coroutine to scale the sushi object to smaller size
    private IEnumerator ScaleDown()
    {
        float scale = 1;
        while (scale > 0.5f)
        {
            scale = Mathf.MoveTowards(scale, 0, 0.1f);
            transform.localScale = new Vector3(scale, scale, scale);
            yield return new WaitForSeconds(0.0005f);
        }
        yield return null;
    }

   public void OnZoneCollided()
    {
        // If the object is in the correct zone, snap it to the zone
    
        _anchorPoint.position = transform.position;
    }

    private IEnumerator ReturnToAnchorPoint()
    {
        float distance = Vector3.Distance(transform.position, _anchorPoint.position);
        while (distance > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, _anchorPoint.position,_returnSpeed * Time.deltaTime);
            distance = Vector3.Distance(transform.position, _anchorPoint.position);
            // print(distance);
            yield return new WaitForSeconds(0.0005f);
        }
        transform.position = _anchorPoint.position;
        yield return null;
    }

    private IEnumerator StopAudioAfterDelay(AudioSource audioSource, float delay)
    {
        yield return new WaitForSeconds(delay);
        audioSource.Stop();
    }

}
