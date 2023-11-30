namespace BotetteUI.Models.Stucts
{
    public class Notifications
    {
        public bool IsAnonymous { get; set; }
        public bool OnBlazeRadar { get; set; }
        public bool OnDisconnect { get; set; }
        public bool OnCatchAttempt { get; set; }
        public bool OnWisperMessage { get; set; }
        public bool OnFriendRequest { get; set; }
        public bool OnAdminPin { get; set; }
        public bool OnBallCount { get; set; }

        public Notifications(bool isAnonymous, bool onBlazeRadar, bool onDisconnect, bool onCatchAttempt, bool onWisperMessage, bool onFriendRequest, bool onAdminPin, bool onBallCount)
        {
            IsAnonymous = isAnonymous;
            OnBlazeRadar = onBlazeRadar;
            OnDisconnect = onDisconnect;
            OnCatchAttempt = onCatchAttempt;
            OnWisperMessage = onWisperMessage;
            OnFriendRequest = onFriendRequest;
            OnAdminPin = onAdminPin;
            OnBallCount = onBallCount;
        }

        public Notifications()
        {
            IsAnonymous = false;
            OnBlazeRadar = true;
            OnDisconnect = true;
            OnCatchAttempt = true;
            OnWisperMessage = true;
            OnFriendRequest = true;
            OnAdminPin = true;
            OnBallCount = true;
        }
    }
}
