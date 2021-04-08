using System;
using Mirror;
using TMPro;
using UnityEngine;

public class LoginManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField useridInput = null;
    [SerializeField] private GameObject loginPopUp = null;
    [SerializeField] public TMP_Text userid = null;
    public event Action<bool> loginChanged;

    public void HandleLogin()
    {
        StaticClass.UserID = useridInput.text ;
        userid.text = StaticClass.UserID;
        loginPopUp.SetActive(false);
        loginChanged?.Invoke(false);
    }
    public void gogoleAuth(string googleIdToken, string googleAccessToken)
    {
         
        Firebase.Auth.FirebaseAuth auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        Firebase.Auth.Credential credential =
        Firebase.Auth.GoogleAuthProvider.GetCredential(googleIdToken, googleAccessToken);
        auth.SignInWithCredentialAsync(credential).ContinueWith(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("SignInWithCredentialAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SignInWithCredentialAsync encountered an error: " + task.Exception);
                return;
            }

            Firebase.Auth.FirebaseUser newUser = task.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);
        });

    }
}

