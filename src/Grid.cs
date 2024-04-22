
class Grid {
    private double baseLoad = 100;
    private double batteryLoad = 0;
    public double loadMin = -50;
    public double loadMax = 100;

    public double GetGridDraw() {
        return baseLoad + batteryLoad;
    }

    public double GetLoad() {
        //simulated load by all devices (not the battery)
        return baseLoad;
    }

    public void SetBatteryLoad(double load) {
        batteryLoad = load;
    }

    public void Simulate() {
        double change = -10 + (20 * new Random().NextDouble());
       
        baseLoad += change;
        baseLoad = Math.Max(70, Math.Min(125, baseLoad)); //to keep the simulation interesting we clamp it between min and max
    }
}