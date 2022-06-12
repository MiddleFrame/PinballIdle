using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using Yodo1.MAS;

namespace COPPA
{
    public class Coppa : MonoBehaviour
    {
        public static bool Init
        {
            get => bool.Parse(PlayerPrefs.GetString("yearInit", false.ToString()));
            private set => PlayerPrefs.SetString("yearInit", value.ToString());
        }

        public static int Year
        {
            
            get => int.Parse(PlayerPrefs.GetString("year",16.ToString()));
            private set => PlayerPrefs.SetString("year", value.ToString());
        }

        private void Awake()
        {
            Time.timeScale = 0;
          
        }

        [SerializeField]
        private Text _yearText;

        public void ChangeText(float year)
        {
            
            _yearText.text = year.ToString(CultureInfo.InvariantCulture);
        }

        public void ConfirmYear()
        {
            Debug.Log( int.Parse( _yearText.text));
            Year = int.Parse(_yearText.text);
            InitAds( Year);
            gameObject.SetActive(false);
        }

        public static void InitAds(int year)
        {
            Yodo1U3dMas.SetCOPPA(year>=13);
            Yodo1U3dMas.SetCCPA(true);
            Yodo1U3dMas.SetGDPR(year>=16);
            Yodo1U3dMas.InitializeSdk();
            Init = true;
            Time.timeScale = 1;
            
        }
    }
}
