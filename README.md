# Clipboard Accelerator
A tool to integrate many of your frequently used utilities. Executing those with text from the clipboard as parameter makes it very quick and convenient to use them.

### About the tool ###
Clipboard Accelerator provides a user defined list of external commands which can be executed with text from the clipboard / free text as parameter.

Adding user defined external commands is easy. Either create a simple .BAT / .CMD file calling your desired command or create a simple .XML file which provides more execution options like pre-defined optional parameters. The directory "Tools" contains some example files which can be used as templates.

The tool monitors for clipboard changes and saves text on the clipboard in a clipboard history. The clipboard history only preserves plain text for the duration of the execution of the tool. (Saving the clipboard text history might be added in a later version.)

### Screenshot of the main window ###
![MainWindow](/docs/ClipboardAccelerator_MainWindow.png)

### Example usage ###
The "ping" utility can be used as a simple example.\
To ping a computer the following steps are usually required:
1. Open a command window
2. Type "ping"
3. Type / paste the computer name you want to ping
4. Press enter

Clipboard Accelerator eliminates some of the above steps. In the best case you already have the computer name you want to ping  somewhere (e.g. in an email / chat) and you can copy it to the clipboard. With Clipboard Accelerator you can use the computer name from the clipboard and double click the "ping" command from the list of commands. This will execute the "ping" command with the computer name as parameter.\
If you have multiple computers you want to ping, just use a list of computer names (each line must contain one single computer name) and uncheck the "execute first line only" option before double clicking the "ping" command. This will ping all computers at once.

The above example can be applied to many command line or even GUI tools which accept parameters. It hides some of the hassle with the parameters of the different tools since they can be pre-configured statically and in a list of pre-defined optional arguments.

### Known bugs ###
1. In some rare situations the tool prevents other programs to access the clipboard. This behavior was observed with MS Excel. In such a situation Excel shows a message that accessing the clipboard was not possible. A second try to copy the data to the clipboard worked in all cases. There is a setting in the tool called "Clipboard access delay in milliseconds" to minimize the likelyhood of this issue.
