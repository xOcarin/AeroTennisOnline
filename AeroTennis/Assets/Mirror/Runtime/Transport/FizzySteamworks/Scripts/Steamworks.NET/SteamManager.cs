using UnityEngine;

#if !DISABLESTEAMWORKS
using Steamworks;
#endif

[DisallowMultipleComponent]
public class SteamManager : MonoBehaviour
{
#if !DISABLESTEAMWORKS
    protected static SteamManager s_instance;

    protected bool m_bInitialized = false;
    public static bool Initialized
    {
        get { return s_instance != null && s_instance.m_bInitialized; }
    }

    protected SteamAPIWarningMessageHook_t m_SteamAPIWarningMessageHook;

    protected virtual void Awake()
    {
        if (s_instance != null && s_instance != this)
        {
            // If another instance already exists, destroy this one
            Destroy(gameObject);
            return;
        }

        // Set this instance as the singleton
        s_instance = this;

        // Make sure the SteamManager persists across scene transitions
        //DontDestroyOnLoad(gameObject);

        // Initialize the SteamAPI
        InitializeSteamAPI();
    }

    protected virtual void OnDestroy()
    {
        if (s_instance == this)
        {
            // Reset the singleton instance on destruction
            s_instance = null;
        }
    }

    protected virtual void InitializeSteamAPI()
    {
        // Ensure that Steam is running
        if (!Packsize.Test() || !DllCheck.Test() || !SteamAPI.Init())
        {
            Debug.LogError("[Steamworks.NET] SteamAPI_Init() failed. Refer to Valve's documentation or the comment above this line for more information.", this);
            m_bInitialized = false;
            return;
        }

        m_bInitialized = true;

        // Set up a callback to receive warning messages from Steam
        m_SteamAPIWarningMessageHook = new SteamAPIWarningMessageHook_t(SteamAPIDebugTextHook);
        SteamClient.SetWarningMessageHook(m_SteamAPIWarningMessageHook);
    }

    protected virtual void SteamAPIDebugTextHook(int nSeverity, System.Text.StringBuilder pchDebugText)
    {
        Debug.LogWarning("[Steamworks.NET] " + pchDebugText);
    }

    protected virtual void Update()
    {
        // Run Steam client callbacks
        if (m_bInitialized)
        {
            SteamAPI.RunCallbacks();
        }
    }
#else
    public static bool Initialized { get { return false; } }
#endif // !DISABLESTEAMWORKS
}
