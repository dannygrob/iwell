
# iwell battery system simulator

This program is a simple simulation of some basic components:
- Grid simulator
- Power Conversion System (PCS)
- Battery Management System (BMS)
- The Battery System

### Grid simulator
The grid meter is simulating a measurement at the grid connection. The grid has a max load that can be configured, this is the max load the user is able to draw from the grid. The simulate function generates a random increase or decrease to the current load.

### Power Conversion System
The PCS handles the communication to the BMS. It determines if the battery system needs to charge or discharge according to the setpoint.

### Battery Management System
The BMS in this simulation can charge or discharge, and keeps it's current state of charge. The full capacity of the battery is configured here. There are also current limits for charging and discharging. If the commands to charge or discharge exceed these limits, the battery will use the max configured values.

This BMS has the following features:
- Track the current state of charge in %
- Set a max charge- and discharge rate via `UseCapacity` and `ChargeCapacity`
- Track the current battery state: `charging` / `discharging` / `full` / `empty`

### Battery System
This is the main class that connects all parts. It controls the simulation and updates the main setpoint. At the moment this is very simple: calculate the difference between the max load allowed on the grid and the current load. If there is room to charge (load is below the max) we charge the battery. If the load exceeds the limit we start peak shaving and discharge the battery with the difference capacity. If the difference exceeds the limits of the battery, the load can still be higher than the grid limit. The battery will never charge higher than the charge limit.

The battery system has references to all parts. Only the PCM needs to have a connection to the BMS to avoid having to talk via the system. This is a choice to keep it simple.

### Output explanation
-----
System load: 72.16 kW (Grid draw: 97.16 kW)
SoC: 49% (Charging)
Setpoint: 25.00 kW

Here the current simulated load on the system is 72.16 kW. The maximum charge rate of the battery is set to 25, so the setpoint will be set to this. Because the battery is charging, the draw from the grid is 97.15 kW.

-----
System load: 77.98 kW (Grid draw: 100.00 kW)
SoC: 50% (Charging)
Setpoint: 22.02 kW

Here the current simulated load on the system is 77.98 kW. The setpoint will be set to 22.02 to fully make use of the room between the max grid load and the system load. Because the battery is charging, the draw from the grid is 100 kW (the max).

-----
System load: 110.48 kW (Grid draw: 100.00 kW)
SoC: 38% (Discharging)
Setpoint: -10.48 kW

Here the current simulated load on the system is 110.48 kW, above the max allowed system load. The setpoint will be set to -10.48 to discharge the battery to make sure the grid load will stay at the max. At this point we are peakshaving.

### Trade-offs
- To simulate a load on the grid I've used a random number generator. This is not a realistic scenario, as in real life devices with a certain load will be turned on or off, increasing or decreasing the load in larger steps instead of gradually. 
- A battery should also preferably not discharge to 0%, but it's possible for the sake of simplicity.
- The battery immediatly starts charging when the gridload is under the max. A buffer could be implemented here to not constantly use the max load.

### Usage
In the src dir, run: `dotnet run`. For tests run `dotnet test` in the tests folder.
