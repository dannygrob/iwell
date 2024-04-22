class PCS {
    private double setpoint;
    private BMS currentBMS;

    public PCS(BMS bms) {
        currentBMS = bms;
    }

    public void setSetpoint(double s) {
        setpoint = Math.Min(Math.Max(s, -currentBMS.maxDischargeRate), currentBMS.maxChargeRate);
    }  

    public double GetSetpoint() {
        return setpoint;
    }   

    public void Simulate() {
        //1 tick = 1 second
        if (setpoint < 0)currentBMS.UseCapacity(-setpoint);
        else currentBMS.ChargeCapacity(setpoint);
    }
}
