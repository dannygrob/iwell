namespace BMSTests;

public class UnitTest1
{
    [Fact]
    public void BMSChargeTest()
    {
        BMS bms = new BMS();
        Assert.True(bms.StateOfCharge() == 50, "State should be 50");

        bms.UseCapacity(10);

        Assert.True(bms.state == BatteryState.Discharging, "Should be discharging");
    }

    [Fact]
    public void BMSDischargeTest()
    {
        BMS bms = new BMS();
        Assert.True(bms.StateOfCharge() == 50, "State should be 50");

        //start charging
        bms.ChargeCapacity(10);

        Assert.True(bms.state == BatteryState.Charging, "Should be charging");
    }
}