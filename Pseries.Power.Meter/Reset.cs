using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTap;   // Use OpenTAP infrastructure/core components (log,TestStep definition, etc)

namespace Pseries.Power.Meter
{
    [Display("Step", Group: "Pseries.Power.Meter", Description: "Insert description here")]
    public class Reset : TestStep
    {
        #region Settings

        [Display("Instrument", Group: "Instrument Setting", Description: "Configure Network Analyzer", Order: 1.1)]
        public PowerMeter MyInst { get; set; }

        #endregion

        public Reset()
        {
            // ToDo: Set default values for properties / settings.
        }

        public override void Run()
        {
            // ToDo: Add test case code.
            RunChildSteps(); //If the step supports child steps.

            MyInst.ScpiCommand("*RST");
            MyInst.ScpiCommand(":SYSTem:PRESet DEF");
            // UpgradeVerdict(Verdict.Pass);
        }
    }
}
