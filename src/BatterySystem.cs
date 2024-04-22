public class BatterySystem {
    private Grid activeGrid = new Grid();
    private BMS activeBMS = new BMS();
    private PCS activePCS;

    public BatterySystem() {
         activePCS = new PCS(activeBMS);
    }

    public void Simulate() {
        activeGrid.Simulate();

        double loadDiff = activeGrid.loadMax - activeGrid.GetLoad();
        activePCS.setSetpoint(loadDiff);

        activePCS.Simulate();
        activeBMS.UpdateState();

        if (activeBMS.state == BatteryState.Charging) {
            activeGrid.SetBatteryLoad(activeBMS.currentRate);
        } else if (activeBMS.state == BatteryState.Discharging) {
            activeGrid.SetBatteryLoad(0);
        } else {
            activeGrid.SetBatteryLoad(0);
        }
    }

    public double GetGridLoad() {
        return activeGrid.GetLoad();
    }

    public double GetGridDraw() {
        return activeGrid.GetGridDraw();
    }

    public double GetSoC() {
        return activeBMS.StateOfCharge();
    }

    public double GetSetpoint() {
        return activePCS.GetSetpoint();
    }

    public string GetBatteryState() {
        return activeBMS.state.ToString();
    }
}
