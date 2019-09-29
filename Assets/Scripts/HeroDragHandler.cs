using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HeroDragHandler : MonoBehaviour, IDragHandler, IEndDragHandler {
    // TODO: this field is just for debugging, it will be removed later when more logic is being done
    [SerializeField]
    private GameObject _hero;
    private Vector3 _position;
    // TODO: Maybe I can find a more elegant solution
    private Transform _placePoint;

    public void OnDrag(PointerEventData eventData)
    {
        var screenPoint = Input.mousePosition;
        screenPoint.z = 1.0f;
        transform.position = Camera.main.ScreenToWorldPoint(screenPoint);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        foreach (Transform placePoint in LevelManager.Instance.PlacePoints)
        {
            if ((transform.position - placePoint.transform.position).magnitude < 10)
            {
                Instantiate(_hero, placePoint.transform.position, Quaternion.identity);
                _placePoint = placePoint;
                Destroy(gameObject);
            }
            else
            {
                transform.position = _position;
            }
        }
        LevelManager.Instance.RemovePlacePoint(_placePoint);
        _placePoint = null;
    }



    // Use this for initialization
    void Start ()
    {
        // Initialize the _placePoint, it is needed to remove the PlacePoint from the LevelManager when a hero is placed
        _placePoint = null;
        // Get the correct position within the UI.
        StartCoroutine(CoWaitForPosition());
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    // Find position of objects in grid, because at first frame the position is wrong.
    IEnumerator CoWaitForPosition()
    {
        yield return new WaitForEndOfFrame();
        _position = transform.position;
        Debug.Log(_position);
    }
}
