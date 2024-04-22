public enum BatteryState
{
    Charging,
    Discharging,
    Full,
    Empty
}

public class BMS {
    static double dischargeRateMax = 25;
    static double chargeRateMax = 25;

    public double currentRate = 0;

    public double maxCapacity = 2000;
    public double currentCapacity = 1000;

    public double maxChargeRate = BMS.chargeRateMax;
    public double maxDischargeRate = BMS.dischargeRateMax;

    public BatteryState state = BatteryState.Full;

    public void UseCapacity(double c) {
        //discharge the battery, but never exceed the max discharge rate
        currentRate = Math.Min(maxDischargeRate, c);
        currentCapacity -= currentRate;
        currentCapacity = Math.Max(0, Math.Min(maxCapacity, currentCapacity));
        
        //set the state to discharge
        state = BatteryState.Discharging;
    }

    public void ChargeCapacity(double c) {
        //charge the battery, but never exceed the max charge rate
        currentRate = Math.Min(maxChargeRate, c);
        currentCapacity += currentRate;
        //clamp the capacity between 0 and 100
        currentCapacity = Math.Max(0, Math.Min(maxCapacity, currentCapacity));

        //set the state to charging
        state = BatteryState.Charging;
    }

    public double StateOfCharge() {
        //return the SoC in %
        return currentCapacity / maxCapacity * 100;
    }

    public void UpdateState() {
        //simple simulation to keep charge of the full or empty states
        if (currentCapacity == maxCapacity)state = BatteryState.Full;
        else if (currentCapacity == 0)state = BatteryState.Empty;

        if (state == BatteryState.Empty){
            maxDischargeRate = 0;
        } else {
            maxDischargeRate = BMS.dischargeRateMax;
        }
    }
}
