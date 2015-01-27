fbipLoL
=======
ForceBindIP bridge for League of Legends

What is this for?
-----------------
The purpose of this program is to run the League of Legends in-game client on a specific network interface using ForceBindIP.

Why is it necessary?
--------------------
The PvP.net (Adobe AIR) Client is a separate program from the in-game client. If the PvP.net client is launched using ForceBindIP it will use the desired network interface. However, when you load load into the game, it will use the default Windows network interface.
To solve this problem, this program will intercept the arguments being passed from the PvP.net client to the in-game client and launch the in-game client on the desired network interface using ForceBindIP.

How do I set it up?
-------------------
<ol>
<li> Rename your "League of Legends.exe" file in your League of Legends folder (RADS\solutions\lol_game_client_sln\releases\<version>\deploy) to something else. I chose "League of Legends Client.exe" </li>
<li> Compile this program and name it "League of Legends.exe"</li>
<li> Copy the compiled version of this program to the "RADS\solutions\lol_game_client_sln\releases\<version>\deploy" directory.</li>
<li> Determine the GUID of the network interface you want League of Legends to run on. Here's how you do that: http://superuser.com/questions/686163/where-can-i-find-my-nic-s-guid-for-use-with-forcebindip </li>
<li> Create a file called "fbipLoL-settings.txt" and save it in the "RADS\solutions\lol_game_client_sln\releases\<version>\deploy" directory.</li>
<li> In the settings file you created in step 4, the following should be defined (in this order) and each on a separate line. Save the file again after you have your desired settings. An example settings file is provided in the root directory of this repository (fbipLoL-settings-example.txt).</li>
<ul>
<li> Use ForceBindIP: true/false - You probably want this to be set as true, but the option is there to disable it.</li>
<li> The full path to the ForceBindIP executable. In 62 bit systems this is typically "C:\Windows\SysWOW64\ForceBindIP.exe" in 32 bit systems it should be "C:\Windows\system32\ForceBindIP.exe"</li>
<li> The GUID of the network interface you want League of Legends to run on. Make sure the letters in the GUID are in capitals (ForceBindIP didn't like it when I used the non-capitals version).</li>
<li> The League of Legends client that you renamed in step 1.</li>
</ul>
</ol>
What is ForceBindIP?
--------------------
ForceBindIP is a freeware Windows application that will inject itself into another application and alter how certain Windows Sockets calls are made, allowing you to force the other application to use a specific network interface / IP address. This is useful if you are in an environment with multiple interfaces and your application has no such option for binding to a specific interface.

ForceBindIP works in two stages - the loader, ForceBindIP.exe will load the target application in a suspended state. It will then inject a DLL (BindIP.dll) which loads WS2_32.DLL into memory and intercepts the bind(), connect(), sendto(), WSAConnect() and WSASendTo() functions, redirecting them to code in the DLL which verifies which interface they will be bound to and if not the one specified, (re)binds the socket. Once the function intercepts are complete, the target application is resumed. Note that some applications with anti-debugger / injection techniques may not work correctly when an injected DLL is present; for the vast majority of applications though this technique should work fine.

You can download ForceBindIP here:
http://old.r1ch.net/stuff/forcebindip/