using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDrop : MonoBehaviour
{

    private Vector3 _dragOffset; //to reduce the offset when just clicking on the object
    [SerializeField] private float _dragSpeed = 10;
    [SerializeField] private float _returnSpeed = 10;
    private Camera _camera;

    private Vector3 _position;
    private Transform _anchorPoint;

    //audio variables
    [SerializeField] public AudioSource pickUpAudioSource;
    [SerializeField] public AudioSource dropAudioSource;

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
        _dragOffset = transform.position - GetMousePosition();
        GameManager.Instance.DraggedSushi = gameObject;

        pickUpAudioSource.Play(); //play the pick up sound
    }

    public void OnMouseDrag() {
         // Update the position of the object being dragged
        transform.position = Vector3.MoveTowards(transform.position, GetMousePosition() + _dragOffset, _dragSpeed * Time.deltaTime) ;
    }

    public void OnMouseUp()
    {
        dropAudioSource.time = 0; //reset the sound to the beginning
        dropAudioSource.Play(); //play the drop sound
        StartCoroutine(StopAudioAfterDelay(dropAudioSource, 1f)); 
        
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
        // _anchorPoint.position = transform.position;
        _anchorPoint.GetComponent<SushiBehavior>().StopFollowingSpline();
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
