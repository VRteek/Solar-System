using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
/// <summary>
/// Rotation around planet.
/// </summary>
public class Walkthrough : MonoBehaviour {
    public float oldDistance, newDistance;
    public AnimationCurve AnimMove, AnimMove_Y;
    public Transform target, oldtarget;
    public float distance = 5f;
    public float speed = 40f;
    private float startTime, playTime;
    private float journeyLength;
    float distCovered;
    public float rate = 0;
    Quaternion rotation;
    Vector3 position;
    public AudioSource audioSource;
    public List<GameObject> planets;
    public float autime;
    public static Walkthrough instance;
    public bool start = false;
    public string url, lang, obj;
    float fracJourney;
    WaitUntil x;
    bool end = false;

    void Awake () {

        if (instance == null)
            instance = this;

        else
            Destroy (gameObject);
    }
    void Start () {
        lang = PlayerPrefs.GetString ("Selected_Language");
        url = Application.persistentDataPath + "/Languages/" + lang + "/";
        startTime = Time.time;
        oldDistance = distance;
        newDistance = 5;
        playTime = -1;
        x = new WaitUntil (() => end);
        StartCoroutine (exp ());
        // Make the rigid body not change rotation
        if (GetComponent<Rigidbody> ())
            GetComponent<Rigidbody> ().freezeRotation = true;

    }

    private void SetPosition () {
        if (target != null) {
            distCovered = (Time.time - startTime) * speed;
            fracJourney = distCovered / journeyLength;

            if (fracJourney >= 0.6 && fracJourney < 0.97) {
                foreach (var item in GetComponentsInChildren<ParticleSystem> ()) {
                    ParticleSystem.EmissionModule emissionModule;
                    emissionModule = item.emission;
                    rate -= 2f;
                    emissionModule.rateOverTime = Mathf.Clamp (rate, 0, 200);

                }
            }
            if (fracJourney < 0.6 && fracJourney > 0.1) {

                foreach (var item in GetComponentsInChildren<ParticleSystem> ()) {
                    ParticleSystem.EmissionModule emissionModule;
                    emissionModule = item.emission;
                    rate = item.emission.rateOverTimeMultiplier;
                    rate += 2f;
                    emissionModule.rateOverTime = Mathf.Clamp (rate, 0, 200);

                }

            }

            if (fracJourney >= 1) {
                position = Vector3.Lerp (transform.position, rotation * new Vector3 (0, 0, -distance) + target.position, 1);
                foreach (var item in GetComponentsInChildren<ParticleSystem> ()) {
                    item.Stop ();
                }

            } else {

                if (target.name != "Moon") {
                    position = rotation * new Vector3 (0f, 0f, -distance) + Vector3.Lerp (oldtarget.position, target.position, AnimMove.Evaluate (fracJourney)) + Vector3.Lerp (Vector3.zero, Vector3.one * 10, AnimMove_Y.Evaluate (fracJourney));
                    rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation ((target.position - oldtarget.position)), fracJourney * 0.1f);
                    foreach (var item in GetComponentsInChildren<ParticleSystem> ()) {
                        if (item.isStopped)
                            item.Play ();
                    }
                } else {

                    rotation = Quaternion.Slerp (transform.rotation, Quaternion.Euler (0, 0, 0), fracJourney * 0.1f);
                    position = rotation * new Vector3 (0f, 0f, -distance) + Vector3.Lerp (oldtarget.position, target.position, AnimMove.Evaluate (fracJourney));
                }
                distance = Mathf.Lerp (oldDistance, newDistance, AnimMove.Evaluate (fracJourney));

            }

        } else {

            position = transform.position;
        }
        transform.position = position;
        transform.rotation = rotation;

    }
    void Update () {
        if (playTime >= autime - 0.5f) {
            end = true;

        }
        if (audioSource.isPlaying)
            playTime = audioSource.time;


    }
    void LateUpdate () {
        SetPosition ();

    }

    public void travel (Transform old, Transform target, float oldDistance, float newDistance) {

        this.oldDistance = oldDistance;
        this.newDistance = newDistance;
        oldtarget = old;
        this.target = target;
        startTime = Time.time;
        journeyLength = Vector3.Distance (old.position, target.position);
        journeyLength = Mathf.Clamp (journeyLength, 200, 400);
        fracJourney = 0;
        distCovered = 0;

    }
    public IEnumerator play (float x) {
        yield return new WaitForSeconds (x);
    }
    public IEnumerator exp () {
        yield return new WaitUntil (() => start);
        yield return StartCoroutine (play (1));
        yield return StartCoroutine (playClip ("1"));
         print (end + ", " + autime + ", " + playTime);
         yield return x;
         end = false;
        yield return StartCoroutine (play (1));
        travel (planets[0].transform, planets[1].transform, distance, 4);
        yield return StartCoroutine (play (8));
        yield return StartCoroutine (playClip ("2"));
        yield return x;
        end = false;
        yield return StartCoroutine (play (1));
        travel (planets[1].transform, planets[2].transform, distance, 4);
        yield return StartCoroutine (play (8));
        yield return StartCoroutine (playClip ("3"));
        yield return x;
        end = false;
        yield return StartCoroutine (play (1));
        travel (planets[2].transform, planets[3].transform, distance, 4);
        yield return StartCoroutine (play (7));
        yield return StartCoroutine (playClip ("4"));
        yield return x;
        end = false;
        yield return StartCoroutine (play (1));
        travel (planets[3].transform, planets[4].transform, distance, 1.25f);
        yield return StartCoroutine (play (8));
        yield return StartCoroutine (playClip ("5"));
        yield return x;
        end = false;
        yield return StartCoroutine (play (1));
        travel (planets[4].transform, planets[5].transform, distance, 5.5f);
        yield return StartCoroutine (play (8));
        yield return StartCoroutine (playClip ("6"));
        yield return x;
        end = false;
        yield return StartCoroutine (play (1));
        travel (planets[5].transform, planets[6].transform, distance, 24);
        yield return StartCoroutine (play (8));
        yield return StartCoroutine (playClip ("7"));
        yield return x;
        end = false;
        yield return StartCoroutine (play (1));
        travel (planets[6].transform, planets[7].transform, distance, 20);
        yield return StartCoroutine (play (14));
        yield return StartCoroutine (playClip ("8"));
        yield return x;
        end = false;
        yield return StartCoroutine (play (1));
        travel (planets[7].transform, planets[8].transform, distance, 20);
        yield return StartCoroutine (play (13));
        yield return StartCoroutine (playClip ("9"));
        yield return x;
        end = false;
        yield return StartCoroutine (play (1));
        travel (planets[8].transform, planets[9].transform, distance, 20);
        yield return StartCoroutine (play (8));
        yield return StartCoroutine (playClip ("10"));
        yield return x;
        end = false;
        
    }
    public IEnumerator setClip () {

        if (File.Exists (url + obj + ".mp3")) {
            WWW www = new WWW ("file://" + url + obj + ".mp3");
            yield return www;
            audioSource.clip = www.GetAudioClip ();

        }
    }
    public IEnumerator playClip (string fileName) {
        obj = fileName;
        yield return StartCoroutine (setClip ());
        autime = audioSource.clip.length;
        playTime = -1;
        end = false;
        audioSource.Play ();

    }
}