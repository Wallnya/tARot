using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

namespace tARot
{
    [RequireComponent(typeof(ARTrackedImageManager))]
    public class TrackedImageInfoMultipleManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject welcomePanel;

        [SerializeField]
        private Button dismissButton;

        [SerializeField]

        private Text imageTrackedText;
        [SerializeField]
        private Text imageCardsText;

        [SerializeField]
        private GameObject[] arObjectsToPlace;

        [SerializeField]
        private Vector3 scaleFactor = new Vector3(0.1f, 0.1f, 0.1f);

        private ARTrackedImageManager m_TrackedImageManager;

        private Dictionary<string, GameObject> arObjects = new Dictionary<string, GameObject>();
        public HashSet<Card> cards = new HashSet<Card>();

        void Awake()
        {
            dismissButton.onClick.AddListener(Dismiss);
            m_TrackedImageManager = GetComponent<ARTrackedImageManager>();

            // setup all game objects in dictionary
            foreach (GameObject arObject in arObjectsToPlace)
            {
                GameObject newARObject = Instantiate(arObject, Vector3.zero, Quaternion.identity);
                newARObject.name = arObject.name;
                arObjects.Add(arObject.name, newARObject);
            }
        }

        void OnEnable()
        {
            m_TrackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
        }

        void OnDisable()
        {
            m_TrackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
        }

        private void Dismiss() => welcomePanel.SetActive(false);

        void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
        {
            foreach (ARTrackedImage trackedImage in eventArgs.added)
            {
                UpdateARImageAdded(trackedImage);
            }

            foreach (ARTrackedImage trackedImage in eventArgs.updated)
            {
                if (trackedImage.trackingState == UnityEngine.XR.ARSubsystems.TrackingState.Tracking)
                {
                    UpdateARImageUpdated(trackedImage);
                }
            }

            foreach (ARTrackedImage trackedImage in eventArgs.removed)
            {
                arObjects[trackedImage.referenceImage.name].SetActive(false);
            }
        }

        private void UpdateARImageAdded(ARTrackedImage trackedImage)
        {
            // Display the name of the tracked image in the canvas
            imageTrackedText.text = trackedImage.referenceImage.name;

            string[] subs = trackedImage.referenceImage.name.Split('-');

            Card card = new Card(subs[0], subs[1]);

            cards.Add(card);
            

            GameObject goARObject = arObjects[trackedImage.referenceImage.name];
            goARObject.transform.position = trackedImage.transform.position;
            goARObject.SetActive(true);

            foreach (GameObject go in arObjects.Values)
            {
                Debug.Log($"Go in arObjects.Values: {go.name}");
                if (go.name != trackedImage.referenceImage.name)
                {
                    Debug.Log($"Go in arObjects.Values DESACTIVATED: {go.name}");
                    go.SetActive(false);
                }
            }

            Debug.Log($"trackedImage.referenceImage.name: {trackedImage.referenceImage.name}");
            /* Debug.Log($"subs[0]: {subs[0]} subs[1]: {subs[1]}");
             Debug.Log($"card: {card.ToString()}");
             foreach (Card i in cards)
             {
                 Debug.Log($"cards: {i.ToString()}");
             }*/
            imageCardsText.text = "Cards " + cards.Count + "/10";
        }
        private void UpdateARImageUpdated(ARTrackedImage trackedImage)
        {
            // Display the name of the tracked image in the canvas
            imageTrackedText.text = trackedImage.referenceImage.name;

            GameObject goARObject = arObjects[trackedImage.referenceImage.name];
            goARObject.transform.position = trackedImage.transform.position;
            goARObject.SetActive(true);

            foreach (GameObject go in arObjects.Values)
            {
                Debug.Log($"Go in arObjects.Values: {go.name}");
                if (go.name != trackedImage.referenceImage.name)
                {
                    Debug.Log($"Go in arObjects.Values DESACTIVATED: {go.name}");
                    go.SetActive(false);
                }
            }

            Debug.Log($"trackedImage.referenceImage.name: {trackedImage.referenceImage.name}");
        }

    }

}
