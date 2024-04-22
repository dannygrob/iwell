using System;

class Program
{
    static void Main(string[] args)
    {
        BatterySystem batterySystem = new BatterySystem();

        // Simulation loop
        while (true)
        {
            batterySystem.Simulate();

            double soc = batterySystem.GetSoC();
            double setPoint = batterySystem.GetSetpoint();
            double gridLoad = batterySystem.GetGridLoad();

            double drawFromGrid = batterySystem.GetGridDraw() + (setPoint < 0 ? setPoint : 0);
            
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("System load: {0:N2} kW (Grid draw: {1:N2} kW)", gridLoad, drawFromGrid);
            Console.WriteLine("SoC: {0:N0}% ({1})", soc, batterySystem.GetBatteryState());
            
            Console.ForegroundColor = setPoint > 0 ? ConsoleColor.Green : ConsoleColor.Red;
            
            Console.WriteLine("Setpoint: {0:N2} kW", setPoint);
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("-----");

            System.Threading.Thread.Sleep(250);
        }
    }
}