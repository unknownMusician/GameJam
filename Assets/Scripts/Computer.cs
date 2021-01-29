﻿using UnityEngine;

public class Computer : MonoBehaviour {

    [SerializeField] private Material computerMaterial;

    #region Instance

    public static Computer instance = null;
    private void Awake() => instance = this;
    private void OnDestroy() => instance = null;

    #endregion

    private bool hasTexture = false;
    private bool isClose = false;

    private void SetHasTexture(bool value) => SetIsOn((hasTexture = value) && isClose);
    private void SetIsClose(bool value) => SetIsOn(hasTexture && (isClose = value));

    private void SetIsOn(bool isOn) => computerMaterial.SetInt("IsOn", isOn ? 1 : 0);

    private void OnEnable() => computerMaterial.SetInt("IsOn", 0);
    private void OnDisable() {
        computerMaterial.SetInt("IsOn", 1);
        computerMaterial.SetTexture("PaintingTexture", null);

        computerMaterial.SetColor($"Color{1}", Color.cyan);
        computerMaterial.SetColor($"Color{2}", Color.magenta);
        computerMaterial.SetColor($"Color{3}", Color.yellow);
    }

    public void SetPainting(Painting painting) {
        computerMaterial.SetTexture("PaintingTexture", painting.rgbTexture);

        for (int i = 0; i < painting.colors.Length; i++) {
            computerMaterial.SetColor($"Color{i + 1}", painting.colors[i]);
        }
        SetHasTexture(painting.rgbTexture != null);
    }

    private void OnTriggerEnter(Collider other) { if (other.GetComponent<Player>() != null) { SetIsClose(true); } }
    private void OnTriggerExit(Collider other) { if (other.GetComponent<Player>() != null) { SetIsClose(false); } }
}