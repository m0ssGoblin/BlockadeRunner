namespace UnityCore
{

    namespace Audio
    {

        public class TestAudio : MonoBehaviour
        {

            public AudioController audioController;

            #region Unity Functions

            private void Update() {

                if (Input.GetKeyUp(KeyCode.T)) {

                    audioController.PlayAudio(AudioType.ST_01);

                }

                if (Input.GetKeyUp(KeyCode.G)) {

                    audioController.RestartAudio(AudioType.ST_01);

                }

                if (Input.GetKeyUp(KeyCode.B)) {

                    audioController.StopAudio(AudioType.ST_01);

                }

                if (Input.GetKeyUp(KeyCode.Y)) {

                    audioController.PlayAudio(AudioType.ST_01);

                }

                if (Input.GetKeyUp(KeyCode.H)) {

                    audioController.RestartAudio(AudioType.ST_01);

                }

                if (Input.GetKeyUp(KeyCode.N)) {

                    audioController.StopAudio(AudioType.ST_01);

                }
            }

            #endregion

        }
    }
}