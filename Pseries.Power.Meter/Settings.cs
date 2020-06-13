// Author: MyName
// Copyright:   Copyright 2020 Keysight Technologies
//              You have a royalty-free right to use, modify, reproduce and distribute
//              the sample application files (and/or any modified version) in any way
//              you find useful, provided that you agree that Keysight Technologies has no
//              warranty, obligations or liability for any sample application files.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using OpenTap;

namespace Pseries.Power.Meter
{
    [Display("Settings", Group: "Pseries.Power.Meter", Description: "Insert a description here")]
    public class Settings : TestStep
    {
        #region enum

        public enum Evbw { OFF, HIGH, MEDium, LOW }
        public enum EBufferMeasType { PEAK, PTAV, AVER, MIN }
        public enum EMeasurementMode { AVERage, NORMal }
        public enum EMeasurementSpeed { NORMal, FAST, DOUBle }
        
        #endregion

        #region Settings

        [Display("Instrument", Group: "Instrument Setting", Description: "Configure Network Analyzer", Order: 1.1)]
        public PowerMeter MyInst { get; set; }

        [DisplayAttribute("SenseNo", "", "Input Parameters", 2)]
        public uint SenseNo { get; set; } = 1u;

        [DisplayAttribute("AvgState1", "used to enable and disable averaging", "Input Parameters", 2)]
        public bool AvgState1 { get; set; } = false;

        [DisplayAttribute("AvgState2", "used to enable and disable video averaging for the P-Series Sensor", "Input Parameters", 2)]
        public bool AvgState2 { get; set; } = false;

        [DisplayAttribute("VBW", "{ HIGH, MEDium, LOW, OFF } sets the sensor bandwidth", "Input Parameters", 2)]
        public Evbw VBW { get; set; } = Evbw.OFF;

        [DisplayAttribute("BufferSize", "1 to 2048,  sets the buffer size for average or peak measurement", "Input Parameters", 2)]
        public int BufferSize { get; set; } = 1;

        [DisplayAttribute("BufferMeasType", "{ \"PEAK\", \"PTAV\", \"AVER\", \"MIN\" }, This command sets the measurement type to average for Channel A", "Input Parameters", 2)]
        public EBufferMeasType BufferMeasType { get; set; } = EBufferMeasType.AVER;

        [DisplayAttribute("MeasurementMode", "{ AVERage, NORMal }", "Input Parameters", 2)]
        public EMeasurementMode MeasurementMode { get; set; } = EMeasurementMode.AVERage;

        [DisplayAttribute("MeasurementSpeed", "{ NORMal, FAST, DOUBle } sets the measurement speed on the selected channel", "Input Parameters", 2)]
        public EMeasurementSpeed MeasurementSpeed { get; set; } = EMeasurementSpeed.NORMal;

        [DisplayAttribute("CW Frequency Sweep", "Enable CW Frequency Sweep", "Input Parameters", 2)]
        public bool enableCWSweep { get; set; }

        [DisplayAttribute("Fix Frequency Sweep", "Enable Fix Frequency Sweep", "Input Parameters", 2)]
        public bool enableFixSweep { get; set; }

        [Unit("HZ", UseEngineeringPrefix: true)]
        [DisplayAttribute("Frequency", "", "Input Parameters", 2)]
        [EnabledIf("enableCWSweep", false, HideIfDisabled = true)]
        public double CWFrequency { get; set; } = 1D;

        [DisplayAttribute("CWStepFreq", "", "Input Parameters", 2)]
        [EnabledIf("enableCWSweep", true, HideIfDisabled = true)]
        public int CWStepFreq { get; set; } = 100;

        [Unit("HZ", UseEngineeringPrefix: true)]
        [DisplayAttribute("CWStartFrequ", "", "Input Parameters", 2)]
        [EnabledIf("enableCWSweep", true, HideIfDisabled = true)]
        public double CWStartFrequ { get; set; } = 1D;

        [Unit("HZ", UseEngineeringPrefix: true)]
        [DisplayAttribute("CWStopFreq", "", "Input Parameters", 2)]
        [EnabledIf("enableCWSweep", true, HideIfDisabled = true)]
        public double CWStopFreq { get; set; } = 10D;

        [Unit("HZ", UseEngineeringPrefix: true)]
        [DisplayAttribute("FixFrequency", "", "Input Parameters", 2)]
        [EnabledIf("enableFixSweep", false, HideIfDisabled = true)]
        public double FixFrequency { get; set; } = 1D;

        [Unit("HZ", UseEngineeringPrefix: true)]
        [DisplayAttribute("FixStartFrequ", "", "Input Parameters", 2)]
        [EnabledIf("enableFixSweep", true, HideIfDisabled = true)]
        public double FixStartFrequ { get; set; } = 1D;

        [Unit("HZ", UseEngineeringPrefix: true)]
        [DisplayAttribute("FixStepFreq", "", "Input Parameters", 2)]
        [EnabledIf("enableFixSweep", true, HideIfDisabled = true)]
        public int FixStepFreq { get; set; } = 100;

        [Unit("HZ", UseEngineeringPrefix: true)]
        [DisplayAttribute("FixStopFreq", "", "Input Parameters", 2)]
        [EnabledIf("enableFixSweep", true, HideIfDisabled = true)]
        public double FixStopFreq { get; set; } = 10D;

        #endregion

        public Settings()
        {
            // ToDo: Set default values for properties / settings.
        }

        public override void Run()
        {
            // ToDo: Add test case code.
            RunChildSteps(); //If the step supports child steps.

            MyInst.ScpiCommand(":SENSe{0}:AVERage:STATe {1}", SenseNo, AvgState1);
            MyInst.ScpiCommand(":SENSe{0}:AVERage2:STATe {1}", SenseNo, AvgState2);
            MyInst.ScpiCommand(":SENSe{0}:BANDwidth:VIDeo {1}", SenseNo, VBW);
            MyInst.ScpiCommand(":SENSe{0}:BUFFer:COUNt {1}", SenseNo, BufferSize);
            MyInst.ScpiCommand(":SENSe{0}:BUFFer:MTYPe {1}", SenseNo, BufferMeasType);
            MyInst.ScpiCommand(":SENSe{0}:DETector:FUNCtion {1}", SenseNo, MeasurementMode);
            MyInst.ScpiCommand(":SENSe{0}:MRATe {1}", SenseNo, MeasurementSpeed);
            
            if (enableCWSweep)
            {
                MyInst.ScpiCommand(":SENSe{0}:FREQuency:CW:STARt {1}", SenseNo, CWStartFrequ);
                MyInst.ScpiCommand(":SENSe{0}:FREQuency:CW:STEP {1}", SenseNo, CWStepFreq);
                MyInst.ScpiCommand(":SENSe{0}:FREQuency:CW:STOP {1}", SenseNo, CWStopFreq);
            }
            else
            {
                MyInst.ScpiCommand(":SENSe{0}:FREQuency:CW {1}", SenseNo, CWFrequency);
            }

            if (enableFixSweep)
            {
                MyInst.ScpiCommand(":SENSe{0}:FREQuency:FIXed:STARt {1}", SenseNo, FixStartFrequ);
                MyInst.ScpiCommand(":SENSe{0}:FREQuency:FIXed:STEP {1}", SenseNo, FixStepFreq);
                MyInst.ScpiCommand(":SENSe{0}:FREQuency:FIXed:STOP {1}", SenseNo, FixStopFreq);
            }
            else
            {
                MyInst.ScpiCommand(":SENSe{0}:FREQuency:FIXed {1}", SenseNo, FixFrequency);
            }
            

            // UpgradeVerdict(Verdict.Pass);
        }
    }
}
