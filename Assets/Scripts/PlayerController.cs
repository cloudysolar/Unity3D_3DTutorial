using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum MoveType {
    Transform, Rigidbody_Move
}

[System.Serializable]
public class KeyboardUI {
    public Image keyW;
    public Image keyA;
    public Image keyS;
    public Image keyD;
}

public class PlayerController : MonoBehaviour {
    public KeyboardUI keyboard;

    public MoveType moveType = MoveType.Transform;
    public bool allowKeyRoation = false;
    public bool allowMouseRotation = true;

    public Space space = Space.World;

    public Dropdown dd_moveType;
    public Dropdown dd_space;

    public Toggle tg_key;
    public Toggle tg_mouse;

    public float transformSpeed = 3f;
    public float rigidbodySpeed = 3f;

    public float rotationSpeed = 5f;

    Rigidbody rig;

    private void Start() {
        rig = GetComponent<Rigidbody>();
    }

    private void Update() {
        showPressedKey();

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        float mouseX = Input.GetAxisRaw("Mouse X");

        switch (moveType) {
            case MoveType.Transform:
                transform.Translate(new Vector3(h * transformSpeed * Time.deltaTime, 0f, v * transformSpeed * Time.deltaTime), space);

                if (allowKeyRoation) {
                    transform.Rotate(new Vector3(0f, h * rotationSpeed, 0f));
                }
                if (allowMouseRotation) {
                    transform.Rotate(new Vector3(0, mouseX, 0) * rotationSpeed);
                }
                break;
            case MoveType.Rigidbody_Move:
                Vector3 moveDir = new Vector3(h * transformSpeed * Time.deltaTime, 0f, v * transformSpeed * Time.deltaTime);

                if (space == Space.World) {
                    rig.MovePosition(rig.position + moveDir);
                }
                else {
                    rig.MovePosition(rig.position + transform.TransformDirection(moveDir));
                }

                if (allowKeyRoation) {
                    rig.MoveRotation(rig.rotation * Quaternion.Euler(0f, h * rotationSpeed, 0f));
                }
                if (allowMouseRotation) {
                    rig.MoveRotation(rig.rotation * Quaternion.Euler(0f, mouseX * rotationSpeed, 0f));
                }

                break;
        }
    }

    private void showPressedKey() {
        Color pressedColor = new Color(0.1176471f, 0.1176471f, 0.1176471f, 0.3921569f);
        Color unpressedColor = new Color(1f, 1f, 1f, 0.3921569f);

        if (Input.GetKeyDown(KeyCode.W)) {
            keyboard.keyW.color = pressedColor;
        }

        if (Input.GetKeyDown(KeyCode.A)) {
            keyboard.keyA.color = pressedColor;
        }

        if (Input.GetKeyDown(KeyCode.S)) {
            keyboard.keyS.color = pressedColor;
        }

        if (Input.GetKeyDown(KeyCode.D)) {
            keyboard.keyD.color = pressedColor;
        }

        if (Input.GetKeyUp(KeyCode.W)) {
            keyboard.keyW.color = unpressedColor;
        }

        if (Input.GetKeyUp(KeyCode.A)) {
            keyboard.keyA.color = unpressedColor;
        }

        if (Input.GetKeyUp(KeyCode.S)) {
            keyboard.keyS.color = unpressedColor;
        }

        if (Input.GetKeyUp(KeyCode.D)) {
            keyboard.keyD.color = unpressedColor;
        }
    }

    public void onMoveTypeValueChanged() {
        moveType = (MoveType) dd_moveType.value;
    }

    public void onSpaceValueChanged() {
        space = (Space) dd_space.value;
    }

    public void onKeyRotationValueChanged() {
        allowKeyRoation = tg_key.isOn;
    }

    public void onMouseRotationValueChanged() {
        allowMouseRotation = tg_mouse.isOn;
    }
}
