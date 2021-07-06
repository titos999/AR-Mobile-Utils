using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AndroidManager : MonoBehaviour
{
   
    // Open Application Settings On Android Device
    public void OpenApplicationSettings()
    {
#if UNITY_ANDROID
        AndroidRuntimePermissions.OpenSettings();   
#endif
    }

    // Change Scene if we have handled permissions
    public void HandlePermissionsAndChangeScene()
    {
#if UNITY_ANDROID
        // Requesting MICROPHONE and CAMERA permissions simultaneously before changing to AR Scene
        AndroidRuntimePermissions.Permission[] result = AndroidRuntimePermissions.RequestPermissions("android.permission.RECORD_AUDIO", "android.permission.CAMERA");

        // If both Camera and Record Audio Permissions have been added
        if (result[0] == AndroidRuntimePermissions.Permission.Granted && result[1] == AndroidRuntimePermissions.Permission.Granted)
        {
            SceneManager.LoadScene(1);
        }
        else
        {
            // Should redirect to application settings
            if (result[0] == AndroidRuntimePermissions.Permission.Denied)
            {
                OpenApplicationSettings();
            }

            // Do the same for the other permission too
            if (result[1] == AndroidRuntimePermissions.Permission.Denied)
            {
                OpenApplicationSettings();
            }
        }
#endif   
    }

    // Open Maps Application
    public void OpenMapsApplication()
    {
#if UNITY_ANDROID
        // Request Location  Permission
        AndroidRuntimePermissions.Permission result = AndroidRuntimePermissions.RequestPermission("android.permission.ACCESS_FINE_LOCATION");

        // If Location Permission has been granted
        if (result == AndroidRuntimePermissions.Permission.Granted)
        {
            Application.OpenURL("https://maps.google.com/maps?q=London");
        }
        // If the Permission has not been granted Open the Application Settings
        else if(result == AndroidRuntimePermissions.Permission.Denied)
        {
            OpenApplicationSettings();
        }
#endif
    }

    // Quit Application
    public void QuitApplication()
    {
        Application.Quit();
    }

    // Update
    private void Update()
    {
#if UNITY_ANDROID
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                SceneManager.LoadScene(0);
            }
            else
            {
                QuitApplication();
            }
        }
#endif
    }
}
