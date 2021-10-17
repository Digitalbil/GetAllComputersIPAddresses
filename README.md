# GetAllComputersIPAddresses
This is a class in C# that was designed to check all of the IP Addresses in the range of the detected network

# Usage
1. Add this class to your c# Project by the name of "NetworkComputer.cs"
2. Call the class using "var localComputers = NetworkComputer.GetLocalNetwork();"
3. This should return an array of all of the Machines that are Windows Computers within your LAN.

To give a better idea of why or how this might be useful. I used this in an Intranet Socket Chat System that I created. One of the main things I wanted to do was to find all the machines running windows so that i could have the machines all coming up as available for chat. 
