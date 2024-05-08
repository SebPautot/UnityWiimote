using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Utilities;
using UnityEditor;
using UnityEngine.InputSystem.LowLevel;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Processors;
using System;
using UnityEngine.InputSystem.Haptics;

#if UNITY_EDITOR
[InitializeOnLoad]
#endif
public class RequireNunchuckValueProcessor : InputProcessor<float>
{
    public override float Process(float value, InputControl control)
    {
        if (control.device is Wiimote)
        {
            if (!(control.device as Wiimote).hasExtension)
            {
                return 0;
            }
        }

        return value;
    }

    public override string ToString()
    {
        return $"RequireNunchuckValueProcessor()";
    }

#if UNITY_EDITOR
    static RequireNunchuckValueProcessor()
    {
        Initialize();
    }
#endif

    [RuntimeInitializeOnLoadMethod]
    static void Initialize()
    {
        InputSystem.RegisterProcessor<RequireNunchuckValueProcessor>(
            "RequireNunchuckValueProcessor"
        );
    }
}

#if UNITY_EDITOR
[InitializeOnLoad]
#endif
public class ReverseValueProcessor : InputProcessor<byte>
{
    public override byte Process(byte value, InputControl control)
    {
        if (control.device is Wiimote)
        {
            if (!(control.device as Wiimote).hasExtension)
            {
                return 0x00;
            }
        }

        switch (value)
        {
            case 0x00:
                value = 0x01;
                break;
            case 0x01:
                value = 0x00;
                break;
        }

        return value;
    }

    public override string ToString()
    {
        return $"ReverseValueProcessor()";
    }

#if UNITY_EDITOR
    static ReverseValueProcessor()
    {
        Initialize();
    }
#endif

    [RuntimeInitializeOnLoadMethod]
    static void Initialize()
    {
        InputSystem.RegisterProcessor<ReverseValueProcessor>("ReverseValueProcessor");
    }
}

public struct WiimoteState : IInputStateTypeInfo
{
    public FourCC format => new FourCC('H', 'I', 'D');

    [InputControl(
        name = "reportMode",
        format = "BYTE",
        layout = "Integer",
        sizeInBits = 8,
        offset = 0,
        noisy = true
    )]
    public byte reportId;

    [InputControl(
        name = "statusReportExtensionBit",
        displayName = " ",
        layout = "Integer",
        format = "BIT",
        sizeInBits = 1,
        offset = 3,
        bit = 1,
        noisy = true
    )]
    public byte statusReportExtensionBit;

    [InputControl(
        name = "OutputReportByte",
        displayName = " ",
        layout = "Integer",
        format = "BIT",
        sizeInBits = 1,
        offset = 3,
        bit = 1,
        noisy = true
    )]
    public byte outputReportByte;

    [InputControl(
        name = "dpad",
        format = "BYTE",
        layout = "Dpad",
        sizeInBits = 4,
        offset = 0,
        noisy = false
    )]
    public byte dpad;

    [InputControl(
        name = "dpad/left",
        format = "BIT",
        layout = "Button",
        bit = 0,
        sizeInBits = 1,
        offset = 1
    )]
    [InputControl(
        name = "dpad/right",
        format = "BIT",
        layout = "Button",
        bit = 1,
        sizeInBits = 1,
        offset = 1,
        noisy = false
    )]
    [InputControl(
        name = "dpad/down",
        format = "BIT",
        layout = "Button",
        bit = 2,
        sizeInBits = 1,
        offset = 1,
        noisy = false
    )]
    [InputControl(
        name = "dpad/up",
        format = "BIT",
        layout = "Button",
        bit = 3,
        sizeInBits = 1,
        offset = 1,
        noisy = false
    )]
    [InputControl(
        name = "plusButton",
        layout = "Button",
        format = "BIT",
        sizeInBits = 1,
        bit = 4,
        offset = 1,
        noisy = false
    )]
    public byte buttons1;

    [InputControl(
        name = "twoButton",
        layout = "Button",
        format = "BIT",
        sizeInBits = 1,
        bit = 0,
        offset = 2,
        noisy = false
    )]
    [InputControl(
        name = "oneButton",
        layout = "Button",
        format = "BIT",
        sizeInBits = 1,
        bit = 1,
        offset = 2,
        noisy = false
    )]
    [InputControl(
        name = "bButton",
        layout = "Button",
        format = "BIT",
        sizeInBits = 1,
        bit = 2,
        offset = 2,
        noisy = false
    )]
    [InputControl(
        name = "aButton",
        layout = "Button",
        format = "BIT",
        sizeInBits = 1,
        bit = 3,
        offset = 2,
        usage = "PrimaryAction",
        noisy = false
    )]
    [InputControl(
        name = "minusButton",
        layout = "Button",
        format = "BIT",
        sizeInBits = 1,
        bit = 4,
        offset = 2,
        noisy = false
    )]
    [InputControl(
        name = "homeButton",
        layout = "Button",
        format = "BIT",
        sizeInBits = 1,
        bit = 7,
        offset = 2,
        noisy = false
    )]
    public byte buttons2;

    [InputControl(
        name = "accelerometer",
        format = "BYTE",
        layout = "Vector3",
        sizeInBits = 24,
        offset = 3
    )]
    public byte accelerometer;

    [InputControl(
        name = "accelerometer/x",
        format = "BYTE",
        layout = "Axis",
        sizeInBits = 8,
        offset = 0,
        noisy = true,
        parameters = "normalize,normalizeMin=0,normalizeMax=1,normalizeZero=0.5,clamp=2,clampMin=0,clampMax=1"
    )]
    public byte accelerometerX;

    [InputControl(
        name = "accelerometer/y",
        format = "BYTE",
        layout = "Axis",
        sizeInBits = 8,
        offset = 1,
        noisy = true,
        parameters = "normalize,normalizeMin=0,normalizeMax=1,normalizeZero=0.5,clamp=2,clampMin=0,clampMax=1"
    )]
    public byte accelerometerY;

    [InputControl(
        name = "accelerometer/z",
        format = "BYTE",
        layout = "Axis",
        sizeInBits = 8,
        offset = 2,
        noisy = true,
        parameters = "normalize,normalizeMin=0,normalizeMax=1,normalizeZero=0.5,clamp=2,clampMin=0,clampMax=1"
    )]
    public byte accelerometerZ;

    [InputControl(
        name = "nunchuckStick",
        format = "BYTE",
        layout = "Stick",
        sizeInBits = 16,
        offset = 6
    )]
    public byte nunchuckStick;

    [InputControl(
        name = "nunchuckStick/x",
        format = "BYTE",
        layout = "Axis",
        sizeInBits = 8,
        offset = 0,
        processors = "RequireNunchuckValueProcessor()",
        parameters = "normalize,normalizeMin=0,normalizeMax=1,normalizeZero=0.5",
        noisy = false
    )]
    public byte nunchuckStickX;

    [InputControl(
        name = "nunchuckStick/y",
        format = "BYTE",
        layout = "Axis",
        sizeInBits = 8,
        offset = 1,
        processors = "RequireNunchuckValueProcessor()",
        parameters = "normalize,normalizeMin=0,normalizeMax=1,normalizeZero=0.5",
        noisy = false
    )]
    public byte nunchuckStickY;

    [InputControl(
        name = "nunchuckStick/up",
        processors = "RequireNunchuckValueProcessor()",
        parameters = "normalize,normalizeMin=0,normalizeMax=1,normalizeZero=0.5,clamp=2,clampMin=0,clampMax=1"
    )]
    [InputControl(
        name = "nunchuckStick/down",
        processors = "RequireNunchuckValueProcessor()",
        parameters = "normalize,normalizeMin=0,normalizeMax=1,normalizeZero=0.5,clamp=2,clampMin=-1,clampMax=0,invert"
    )]
    [InputControl(
        name = "nunchuckStick/left",
        processors = "RequireNunchuckValueProcessor()",
        parameters = "normalize,normalizeMin=0,normalizeMax=1,normalizeZero=0.5,clamp=2,clampMin=-1,clampMax=0,invert"
    )]
    [InputControl(
        name = "nunchuckStick/right",
        processors = "RequireNunchuckValueProcessor()",
        parameters = "normalize,normalizeMin=0,normalizeMax=1,normalizeZero=0.5,clamp=2,clampMin=0,clampMax=1"
    )]
    public byte nunchuckStickDirections;

    [InputControl(
        name = "nunchuckAccelerometer",
        format = "BYTE",
        layout = "Vector3",
        sizeInBits = 24,
        offset = 8
    )]
    public byte nunchuckAccelerometer;

    [InputControl(
        name = "nunchuckAccelerometer/x",
        format = "BYTE",
        layout = "Axis",
        sizeInBits = 8,
        offset = 0,
        noisy = true,
        parameters = "normalize,normalizeMin=0,normalizeMax=1,normalizeZero=0.5,clamp=2,clampMin=0,clampMax=1",
        processors = "RequireNunchuckValueProcessor()"
    )]
    public byte nunchuckAccelerometerX;

    [InputControl(
        name = "nunchuckAccelerometer/y",
        format = "BYTE",
        layout = "Axis",
        sizeInBits = 8,
        offset = 1,
        noisy = true,
        parameters = "normalize,normalizeMin=0,normalizeMax=1,normalizeZero=0.5,clamp=2,clampMin=0,clampMax=1",
        processors = "RequireNunchuckValueProcessor()"
    )]
    public byte nunchuckAccelerometerY;

    [InputControl(
        name = "nunchuckAccelerometer/z",
        format = "BYTE",
        layout = "Axis",
        sizeInBits = 8,
        offset = 2,
        noisy = true,
        parameters = "normalize,normalizeMin=0,normalizeMax=1,normalizeZero=0.5,clamp=2,clampMin=0,clampMax=1",
        processors = "RequireNunchuckValueProcessor()"
    )]
    public byte nunchuckAccelerometerZ;

    [InputControl(
        name = "cButton",
        layout = "DiscreteButton",
        format = "BIT",
        sizeInBits = 1,
        bit = 1,
        offset = 11,
        parameters = "maxValue=0,minValue=-0.5, nullValue=1, wrapAtValue=1, writeMode=1",
        defaultState = 1,
        noisy = false
    )]
    public int cButton;

    [InputControl(
        name = "zButton",
        layout = "DiscreteButton",
        format = "BIT",
        sizeInBits = 1,
        bit = 0,
        offset = 11,
        parameters = "maxValue=0,minValue=-0.5, nullValue=1, wrapAtValue=1, writeMode=1",
        defaultState = 1,
        noisy = false
    )]
    public int zButton;
}

[StructLayout(LayoutKind.Explicit, Size = 3)]
internal struct WiimoteHIDUSBOutputReportPayload
{
    [FieldOffset(0)]
    public byte reportType;

    [FieldOffset(1)]
    public byte continuousReport;

    [FieldOffset(2)]
    public byte reportMode;
}

[StructLayout(LayoutKind.Explicit, Size = kSize)]
internal struct WiimoteHIDUSBOutputReport : IInputDeviceCommandInfo
{
    public static FourCC Type => new FourCC('H', 'I', 'D', 'O');
    public FourCC typeStatic => Type;

    internal const int kSize = InputDeviceCommand.BaseCommandSize + 22;

    [FieldOffset(0)]
    public InputDeviceCommand baseCommand;

    [FieldOffset(InputDeviceCommand.BaseCommandSize + 0)]
    public byte reportType;

    [FieldOffset(InputDeviceCommand.BaseCommandSize + 1)]
    public byte continuousReport;

    [FieldOffset(InputDeviceCommand.BaseCommandSize + 2)]
    public byte reportMode;

    public static WiimoteHIDUSBOutputReport Create(WiimoteHIDUSBOutputReportPayload payload)
    {
        return new WiimoteHIDUSBOutputReport
        {
            baseCommand = new InputDeviceCommand(Type, kSize),
            reportType = payload.reportType,
            continuousReport = payload.continuousReport,
            reportMode = payload.reportMode
        };
    }
}

[StructLayout(LayoutKind.Explicit, Size = 3)]
internal struct WiimoteFeedbackFeaturesHIDUSBOutputReportPayload
{
    [FieldOffset(0)]
    public byte reportType;

    [FieldOffset(1)]
    public byte feedbackStateByte;
}

[StructLayout(LayoutKind.Explicit, Size = kSize)]
internal struct WiimoteFeedbackFeaturesHIDUSBOutputReport : IInputDeviceCommandInfo
{
    public static FourCC Type => new FourCC('H', 'I', 'D', 'O');
    public FourCC typeStatic => Type;

    internal const int kSize = InputDeviceCommand.BaseCommandSize + 22;

    [FieldOffset(0)]
    public InputDeviceCommand baseCommand;

    [FieldOffset(InputDeviceCommand.BaseCommandSize + 0)]
    public byte reportType;

    [FieldOffset(InputDeviceCommand.BaseCommandSize + 1)]
    public byte feedbackStateByte;

    public static WiimoteFeedbackFeaturesHIDUSBOutputReport Create(
        WiimoteFeedbackFeaturesHIDUSBOutputReportPayload payload
    )
    {
        return new WiimoteFeedbackFeaturesHIDUSBOutputReport
        {
            baseCommand = new InputDeviceCommand(Type, kSize),
            reportType = payload.reportType,
            feedbackStateByte = payload.feedbackStateByte
        };
    }
}

[StructLayout(LayoutKind.Explicit, Size = 7)]
internal struct WiimoteExtensionHIDUSBOutputReportPayload
{
    [FieldOffset(0)]
    public byte reportType;

    [FieldOffset(1)]
    public byte rumbleOn;

    [FieldOffset(2)]
    public byte addressByteOne;

    [FieldOffset(3)]
    public byte addressByteTwo;

    [FieldOffset(4)]
    public byte addressByteThree;

    [FieldOffset(5)]
    public byte dataSize;

    [FieldOffset(6)]
    public byte data;
}

[StructLayout(LayoutKind.Explicit, Size = kSize)]
internal struct WiimoteExtensionHIDUSBOutputReport : IInputDeviceCommandInfo
{
    public static FourCC Type => new FourCC('H', 'I', 'D', 'O');
    public FourCC typeStatic => Type;

    internal const int kSize = InputDeviceCommand.BaseCommandSize + 22;

    [FieldOffset(0)]
    public InputDeviceCommand baseCommand;

    [FieldOffset(InputDeviceCommand.BaseCommandSize + 0)]
    public byte reportType;

    [FieldOffset(InputDeviceCommand.BaseCommandSize + 1)]
    public byte rumbleOn;

    [FieldOffset(InputDeviceCommand.BaseCommandSize + 2)]
    public byte addressByteOne;

    [FieldOffset(InputDeviceCommand.BaseCommandSize + 3)]
    public byte addressByteTwo;

    [FieldOffset(InputDeviceCommand.BaseCommandSize + 4)]
    public byte addressByteThree;

    [FieldOffset(InputDeviceCommand.BaseCommandSize + 5)]
    public byte dataSize;

    [FieldOffset(InputDeviceCommand.BaseCommandSize + 6)]
    public byte data;

    public static WiimoteExtensionHIDUSBOutputReport Create(
        WiimoteExtensionHIDUSBOutputReportPayload payload
    )
    {
        return new WiimoteExtensionHIDUSBOutputReport
        {
            baseCommand = new InputDeviceCommand(Type, kSize),
            reportType = payload.reportType,
            rumbleOn = payload.rumbleOn,
            addressByteOne = payload.addressByteOne,
            addressByteTwo = payload.addressByteTwo,
            addressByteThree = payload.addressByteThree,
            dataSize = payload.dataSize,
            data = payload.data
        };
    }
}

[InputControlLayout(stateType = typeof(WiimoteState))]
#if UNITY_EDITOR
[InitializeOnLoad] // Make sure static constructor is called during startup.
#endif
public class Wiimote : InputDevice, IInputUpdateCallbackReceiver, IDualMotorRumble
{
    public IntegerControl reportMode { get; private set; }
    IntegerControl statusReportExtensionBit { get; set; }
    IntegerControl outputReportByte { get; set; }
    public DpadControl dpad { get; private set; }

    public ButtonControl aButton { get; private set; }
    public ButtonControl bButton { get; private set; }
    public ButtonControl minusButton { get; private set; }
    public ButtonControl homeButton { get; private set; }
    public ButtonControl plusButton { get; private set; }
    public ButtonControl oneButton { get; private set; }
    public ButtonControl twoButton { get; private set; }

    public Vector3Control accelerometer { get; private set; }

    public bool hasExtension { get; private set; }

    public StickControl nunchuckStick { get; private set; }
    public Vector3Control nunchuckAccelerometer { get; private set; }
    public ButtonControl cButton { get; private set; }
    public ButtonControl zButton { get; private set; }

    protected override void FinishSetup()
    {
        reportMode = GetChildControl<IntegerControl>("reportMode");
        statusReportExtensionBit = GetChildControl<IntegerControl>("statusReportExtensionBit");
        outputReportByte = GetChildControl<IntegerControl>("OutputReportByte");

        dpad = GetChildControl<DpadControl>("dpad");

        aButton = GetChildControl<ButtonControl>("aButton");
        bButton = GetChildControl<ButtonControl>("bButton");
        minusButton = GetChildControl<ButtonControl>("minusButton");
        homeButton = GetChildControl<ButtonControl>("homeButton");
        plusButton = GetChildControl<ButtonControl>("plusButton");
        oneButton = GetChildControl<ButtonControl>("oneButton");
        twoButton = GetChildControl<ButtonControl>("twoButton");

        accelerometer = GetChildControl<Vector3Control>("accelerometer");

        nunchuckStick = GetChildControl<StickControl>("nunchuckStick");
        nunchuckAccelerometer = GetChildControl<Vector3Control>("nunchuckAccelerometer");

        cButton = GetChildControl<ButtonControl>("cButton");
        // cButton.
        zButton = GetChildControl<ButtonControl>("zButton");

        base.FinishSetup();

        requires0x20ReportMode = true;
        SetLEDs(true, false, false, false);
        SetWiimoteReportMode(0x20);
    }

    static Wiimote()
    {
        InputSystem.RegisterLayout<Wiimote>(
            matches: new InputDeviceMatcher()
                .WithInterface("HID")
                .WithCapability("productId", 774)
                .WithCapability("vendorId", 1406)
        );
    }

    bool hasSentRequestToEnableExtension = false;
    bool requires0x20ReportMode = false;
    bool requireFeedbackStateSet = false;

    public void OnUpdate()
    {
        switch (reportMode.ReadValue())
        {
            case 0x35:
                break;
            case 0x20:
                if (requires0x20ReportMode)
                {
                    requires0x20ReportMode = false;
                }

                if (!hasSentRequestToEnableExtension)
                {
                    if (statusReportExtensionBit.ReadValue() > 0)
                    {
                        bool res = EnableExtension();

                        if (res)
                        {
                            hasSentRequestToEnableExtension = true;
                        }
                        else
                        {
                            SetWiimoteReportMode();
                        }
                    }
                    else
                    {
                        hasExtension = false;
                        SetWiimoteReportMode();
                    }
                }
                else
                {
                    SetWiimoteReportMode();
                }
                break;
            case 0x22:
                switch (outputReportByte.ReadValue())
                {
                    case 0x11:
                        Debug.Log("0x11");
                        break;
                    case 0x12:
                        Debug.Log("0x11");
                        break;
                    case 0x16:
                        hasSentRequestToEnableExtension = false;
                        hasExtension = true;
                        SetWiimoteReportMode();
                        break;
                    default:
                        hasSentRequestToEnableExtension = false;
                        hasExtension = true;
                        SetWiimoteReportMode();
                        break;
                }
                break;
            default:
                if (requires0x20ReportMode)
                    SetWiimoteReportMode(0x20);
                else
                    SetWiimoteReportMode();
                break;
        }

        if (requireFeedbackStateSet)
        {
            bool res = SetFeedbackFeatures();

            if (res)
                requireFeedbackStateSet = false;
        }
    }

    bool SetWiimoteReportMode(byte reportMode = 0x35)
    {
        //send HID report 0xa2 0x12 0x00 0x32
        requireFeedbackStateSet = true;

        WiimoteHIDUSBOutputReport cmd = WiimoteHIDUSBOutputReport.Create(
            new WiimoteHIDUSBOutputReportPayload
            {
                reportType = 0x12,
                continuousReport = 0x00,
                reportMode = reportMode
            }
        );

        long res = ExecuteCommand(ref cmd);

        return res >= 0;
    }

    bool EnableExtension()
    {
        requireFeedbackStateSet = true;

        WiimoteExtensionHIDUSBOutputReport cmd = WiimoteExtensionHIDUSBOutputReport.Create(
            new WiimoteExtensionHIDUSBOutputReportPayload
            {
                reportType = 0x16,
                rumbleOn = 0x04,
                addressByteOne = 0xA4,
                addressByteTwo = 0x00,
                addressByteThree = 0xF0,
                dataSize = 0x01,
                data = 0x55
            }
        );

        long res = ExecuteCommand(ref cmd);

        if (res < 0)
        {
            return false;
        }

        WiimoteExtensionHIDUSBOutputReport cmd2 = WiimoteExtensionHIDUSBOutputReport.Create(
            new WiimoteExtensionHIDUSBOutputReportPayload
            {
                reportType = 0x16,
                rumbleOn = 0x04,
                addressByteOne = 0xA4,
                addressByteTwo = 0x00,
                addressByteThree = 0xFB,
                dataSize = 0x01,
                data = 0x00
            }
        );

        long res2 = ExecuteCommand(ref cmd2);
        if (res2 < 0)
        {
            return false;
        }

        WiimoteExtensionHIDUSBOutputReport cmd3 = WiimoteExtensionHIDUSBOutputReport.Create(
            new WiimoteExtensionHIDUSBOutputReportPayload
            {
                reportType = 0x16,
                rumbleOn = 0x04,
                addressByteOne = 0xA4,
                addressByteTwo = 0x00,
                addressByteThree = 0xFA,
                dataSize = 0x01,
                data = 0x06
            }
        );

        long res3 = ExecuteCommand(ref cmd3);
        if (res3 < 0)
        {
            return false;
        }
        Debug.Log("EnableExtension: " + res + " " + res2);
        return true;
    }

    public bool LED1 { get; private set; }
    public bool LED2 { get; private set; }
    public bool LED3 { get; private set; }
    public bool LED4 { get; private set; }
    public bool rumble { get; private set; }

    byte GetFeedbackStateByte()
    {
        byte feedbackStateByte = 0;

        if (LED1)
        {
            feedbackStateByte |= 0x10;
        }

        if (LED2)
        {
            feedbackStateByte |= 0x20;
        }

        if (LED3)
        {
            feedbackStateByte |= 0x40;
        }

        if (LED4)
        {
            feedbackStateByte |= 0x80;
        }

        if (rumble)
        {
            feedbackStateByte |= 0x01;
        }

        return feedbackStateByte;
    }

    bool SetFeedbackFeatures()
    {
        byte feedbackStateByte = GetFeedbackStateByte();

        WiimoteFeedbackFeaturesHIDUSBOutputReport cmd =
            WiimoteFeedbackFeaturesHIDUSBOutputReport.Create(
                new WiimoteFeedbackFeaturesHIDUSBOutputReportPayload
                {
                    reportType = 0x11,
                    feedbackStateByte = feedbackStateByte
                }
            );

        long res = ExecuteCommand(ref cmd);

        return res >= 0;
    }

    public void SetLEDs(bool led1 = false, bool led2 = false, bool led3 = false, bool led4 = false)
    {
        LED1 = led1;
        LED2 = led2;
        LED3 = led3;
        LED4 = led4;

        requireFeedbackStateSet = true;
    }

    public void SetMotorSpeeds(float lowFrequency, float highFrequency)
    {
        if (lowFrequency > 0 || highFrequency > 0)
        {
            rumble = true;
        }
        else
        {
            rumble = false;
        }

        requireFeedbackStateSet = true;
    }

    bool pausedRubleValue = false;

    public void PauseHaptics()
    {
        pausedRubleValue = rumble;
        rumble = false;
        requireFeedbackStateSet = true;
    }

    public void ResumeHaptics()
    {
        rumble = pausedRubleValue;
        requireFeedbackStateSet = true;
    }

    public void ResetHaptics()
    {
        rumble = false;
        requireFeedbackStateSet = true;
    }

    public static Wiimote current { get; private set; }

    public override void MakeCurrent()
    {
        base.MakeCurrent();
        current = this;
    }

    protected override void OnAdded() { }

    protected override void OnRemoved()
    {
        if (current == this)
            current = null;
    }

    [RuntimeInitializeOnLoadMethod]
    static void Init() { }
}
