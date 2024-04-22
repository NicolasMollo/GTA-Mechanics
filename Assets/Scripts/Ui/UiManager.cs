using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class UiManager : MonoBehaviour {
    #region TextArea attributes
    [SerializeField]
    [Tooltip("Area where notices will be written")]
    private Image textArea = null;

    private Text areaText = null;

    [SerializeField]
    private Font areaTextFont = null;

    [SerializeField]
    private int areaTextFontSize = 20;

    [SerializeField]
    private FontStyle areaTextFontStyle = FontStyle.Normal;

    [SerializeField]
    private TextAnchor areaTextFontAnchor = TextAnchor.UpperLeft;

    [SerializeField]
    private Color areaTextColor = Color.black;

    [SerializeField]
    private Canvas canvas = null;

    [SerializeField]
    private RawImage pressButton = null;

    [SerializeField]
    [Tooltip("Phrases that will appear upon collision with non-traversable areas")]
    private string[] phrases = new string[(int)CollisionObjectType.Last];
    public string[] Phrases {
        get { return phrases; }
    }
    #endregion
    #region UiCoins attributes
    [SerializeField]
    public RawImage[] uiCoins;

    public Animation[] uiCoinsAnimations;

    [SerializeField]
    public Texture lockedUiCoin = null;

    [SerializeField]
    public Texture unlockedUiCoin = null;
    #endregion
    #region Buttons attributes
    [SerializeField]
    private Button entersTheCar = null;
    private Text entersTheCarText = null;

    [SerializeField]
    [Tooltip("Text that will be placed in the button 'entersTheCar'")]
    private string enterText;

    [SerializeField]
    private Button stayOutOfTheCar = null;
    private Text stayOutOfTheCarText = null;

    [SerializeField]
    [Tooltip("Text that will be placed in the button 'stayOutOfTheCar'")]
    private string stayText;
    #endregion
    #region Panel attributes
    [SerializeField]
    private GameObject victoryPanel = null;

    private Text victoryPanelText = null;

    [SerializeField]
    [Tooltip("Text that will be placed in the victory panel")]
    private string panelText = null;
    #endregion
    #region Counter attributes
    [SerializeField]
    [Tooltip("Counter reload time")]
    private float reloadCounter = 5f;

    private float counter = 0f; 
    #endregion
    #region Singleton
    public static UiManager Instance {
        get;
        private set;
    } = null; 
    #endregion



    private void Awake() {
        TaketheReferences();      
    }
    #region Awake methods
    private void TaketheReferences() {
        areaText = textArea.GetComponentInChildren<Text>();
        entersTheCarText = entersTheCar.GetComponentInChildren<Text>();
        stayOutOfTheCarText = stayOutOfTheCar.GetComponentInChildren<Text>();
        victoryPanelText = victoryPanel.GetComponentInChildren<Text>();
        Instance = this;
    } 
    #endregion



    private void Start() {
        ObjectSwitch(canvas.gameObject, true);
        ObjectSwitch(textArea.gameObject, false);
        SetAreaTextParameters();
        ObjectSwitch(pressButton.gameObject, false);
        SetPanel(false);    
        SetCounter(reloadCounter);
        SetButtonParameters();
        DisableCoinsAnimations();
        AddListener();
    }
    #region Start methods
    private void SetAreaTextParameters() {
        areaText.alignment = areaTextFontAnchor;
        areaText.font = areaTextFont;
        areaText.fontSize = areaTextFontSize;
        areaText.fontStyle = areaTextFontStyle;
        areaText.color = areaTextColor;
    }
    private void SetButtonParameters() {
        entersTheCarText.text = enterText;
        stayOutOfTheCarText.text = stayText;
        entersTheCar.gameObject.SetActive(false);
        stayOutOfTheCar.gameObject.SetActive(false);
    }
    private void SetPanel(bool _value) {
        SetText(victoryPanelText, panelText);
        victoryPanel.SetActive(_value);
    }
    private void DisableCoinsAnimations() {
        for (int i = 0; i < uiCoins.Length; i++) {
            uiCoinsAnimations[i].enabled = false;
        }
    }
    private void AddListener() {
        MessageManager.OnTakeTheCoin += EnableCoin;
        MessageManager.OnCollisionWithTheCar += ActivateButtons;
        MessageManager.OnPlayerSelectionArea += ActivatePressTabImage;
        MessageManager.OnGameArea += TurnOffPressTabImage;
        entersTheCar.onClick.AddListener(GetIntoTheCar);
        stayOutOfTheCar.onClick.AddListener(StayOutOfTheCar);
    }
    #endregion



    #region Events methods
    private void EnableCoin() {
        for (int i = 0; i < uiCoinsAnimations.Length; i++) {
            if (!uiCoinsAnimations[i].enabled) {
                uiCoins[i].texture = unlockedUiCoin;
                uiCoinsAnimations[i].enabled = true;
                return;
            }
        }
    }
    private void ActivateButtons() {
        entersTheCar.gameObject.SetActive(true);
        stayOutOfTheCar.gameObject.SetActive(true);
    }
    private void ActivatePressTabImage() {
        pressButton.gameObject.SetActive(true);
    }
    private void TurnOffPressTabImage() {
        pressButton.gameObject.SetActive(false);
    }
    private void GetIntoTheCar() {
        entersTheCar.gameObject.SetActive(false);
        stayOutOfTheCar.gameObject.SetActive(false);
        Controller.CharacterType = HumanoidMovement.Car;
        SetTimeScale(1);
    }
    private void StayOutOfTheCar() {
        entersTheCar.gameObject.SetActive(false);
        stayOutOfTheCar.gameObject.SetActive(false);
        SetTimeScale(1);
    }
    #endregion



    private void Update() {
        DisableTextArea();
        CollectedTheCoins();
    }
    #region Update methods
    private void DisableTextArea() {
        if (textArea.gameObject.activeSelf) {
            counter -= Time.deltaTime;
            if (counter <= 0) {
                ObjectSwitch(textArea.gameObject, false);
                SetCounter(reloadCounter);
            }
        }
    }
    private void CollectedTheCoins() {
        #region Variables assignment
        int i = 0;
        #endregion

        for (i = 0; i < uiCoins.Length; i++) {
            if (uiCoinsAnimations[i].enabled) {
                i += 1;
            }
            if (i == uiCoins.Length) {
                SetPanel(true);
                SetTimeScale(0);
            }
        }
    } 
    #endregion



    private void OnDestroy() {
        RemoveListener();
    }
    #region OnDestroy methods
    private void RemoveListener() {
        MessageManager.OnTakeTheCoin -= EnableCoin;
        MessageManager.OnCollisionWithTheCar -= ActivateButtons;
        MessageManager.OnPlayerSelectionArea -= ActivatePressTabImage;
        MessageManager.OnGameArea -= TurnOffPressTabImage;
        entersTheCar.onClick.RemoveListener(GetIntoTheCar);
        stayOutOfTheCar.onClick.RemoveListener(StayOutOfTheCar);
    } 
    #endregion



    #region Public methods
    public void TextAreaSwitch(bool value) {
        textArea.gameObject.SetActive(value);
    } 
    #endregion



    #region Reusable methods
    public void SetAreaText(string _text) {
        areaText.text = _text;
    }
    private void SetText(Text _textObject, string _text) {
        _textObject.text = _text;
    }
    private void ObjectSwitch(GameObject _obj, bool _value) {
        _obj.SetActive(_value);
    }
    private void SetCounter(float _time) {
        counter = _time;
    }
    private void SetTimeScale(float _timeScale) {
        Time.timeScale = _timeScale;
    }
    #endregion
}
