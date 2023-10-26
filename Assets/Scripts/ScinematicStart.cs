using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class ScinematicStart : MonoBehaviour
{
    public UniWebView uniWebView;
    public GameObject soundOff;
    public AudioSource musicSource;
    public AudioSource audioSource;
    public bool isSoundOn;
    public Rotate360 rotate360;
    public GameObject StartUI;
    public Camera startCamera;
    public LevelManager levelManager;
    public GameObject airplane;
    public Transform lineEnd;
    public Transform FlyEnd;
    private void Start()
    {
        Application.targetFrameRate = 60;
        isSoundOn = true;
        audioSource.mute = !isSoundOn;
    }
    public void StartFly()
    {
        StartSound();
        rotate360.rotateZ = 2;
        StartUI.SetActive(false);
        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(airplane.transform.DOMove(lineEnd.position, 4f).SetEase(Ease.InSine).OnComplete(RotatePlane));
        startCamera.transform.DOMove(new Vector3(20, lineEnd.position.y, lineEnd.position.z), 4.5f).SetEase(Ease.InSine);
        mySequence.Append(airplane.transform.DOMove(new Vector3(lineEnd.position.x,lineEnd.position.y+2,lineEnd.position.z+5), 0.5f).SetEase(Ease.Linear).OnComplete(RotatePlane));
        mySequence.Append(airplane.transform.DOMove(FlyEnd.position, 4f).SetEase(Ease.Linear).OnComplete(StartRealGame));
    }
    private void RotatePlane()
    {
        airplane.transform.DORotate(new Vector3(airplane.transform.rotation.x-20, airplane.transform.rotation.y, airplane.transform.rotation.z), 1f);
    }
    private void StartRealGame()
    {
        
        levelManager.gameObject.SetActive(true);
        Destroy(airplane);
        startCamera.gameObject.SetActive(false);
    }
    private void StartSound()
    {
        audioSource.Play();
    }
    public void ChangeSoundStatus()
    {
        isSoundOn = !isSoundOn;
        soundOff.SetActive(!isSoundOn);
        musicSource.mute = !isSoundOn;
    }
    public void ChangeSound()
    {
        musicSource.mute = isSoundOn;
    }
    public void OpenWebview(string url)
    {
        var webviewObject = new GameObject("UniWebview");
        uniWebView = webviewObject.AddComponent<UniWebView>();
        uniWebView.Frame = new Rect(0, 0, Screen.width, Screen.height);
        uniWebView.SetShowToolbar(true, false, true, true);
        uniWebView.Load(url);
        uniWebView.Show();
    }
}
