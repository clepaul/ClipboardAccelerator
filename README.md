# Clipboard Accelerator
A tool to integrate many of your frequently used utilities. Executing those utilities with text from the clipboard as parameter makes it very quick and convenient to use them.

### About the tool
Clipboard Accelerator provides a user defined list of external commands which can be executed with text from the clipboard / free text as parameter.

Adding user defined external commands is easy. Either create a simple .BAT / .CMD file calling your desired command or create a simple .XML file which provides more execution options like pre-defined optional parameters. The directory "Tools" contains some example files which can be used as templates for external commands.

The tool monitors for clipboard changes and saves text on the clipboard in a clipboard history. The clipboard history only preserves plain text for the duration of the execution of the tool. Saving the clipboard text history might be added in a later version.

### Screenshot of the main window
![MainWindow](/docs/ClipboardAccelerator_MainWindow.png)

### Example usage
The "ping" utility can be used as a simple example.\
To ping a computer the following steps are usually required:
1. Open a command window
2. Type "ping"
3. Type / paste the computer name you want to ping
4. Press enter

Clipboard Accelerator eliminates some of the above steps. In the best case you already have the computer name you want to ping  somewhere (e.g. in an email / chat) and you can copy it to the clipboard. With Clipboard Accelerator you can use the computer name from the clipboard and double click the "ping" command from the list of commands. This will execute the "ping" command with the computer name as parameter.\
If you have multiple computers you want to ping, just use a list of computer names (each line must contain one single computer name) and uncheck the "execute first line only" option before double clicking the "ping" command. This will ping all computers at once.

The above example can be applied to many command line or even GUI tools which accept parameters. It hides some of the hassle with the parameters of the different tools since they can be pre-configured statically and in a list of pre-defined optional arguments.

A more detailed documentation (Clipboard Accelerator.docx) can be found in the docs directory.

### Known bugs
1. In some rare situations the tool prevents other programs to access the clipboard. This behavior was observed with MS Excel. In such a situation Excel shows a message that accessing the clipboard was not possible. A second try to copy the data to the clipboard worked in all cases. There is a setting in the tool called "Clipboard access delay in milliseconds" to minimize the likelyhood of this issue.
2. The highlighting of the external commands is not working properly (currently highlighted commands are still highlighted even though the regular expression does not match) when manually editing the text in the clipboard textbox.

### Planned improvements, enhancements (request for help)
- Un-sphaghetti the code and make it more readable.
- Make the code more OOP.
- Move the majority of logic from the main window class/function to didicated classes.
- Move the processing of non-GUI logic to dedicated threads.
- Port to Linux / other operating systems

### Notes and requirements
- .NET runtime version 4.5.2 is required.
- Using special characters like "| > < &" etc. at the command line for starting an external program may cause unpredicted behavior.
- If the "pipe option" is not in use external commands are called multiple times. Each line in the clipboard text / free text textbox will be added as a parameter to a new call of the external command.
- Since the tool copies all text from the clipboard it might also copy sensitive data (e.g. passwords).

### Installation instructions
1. Extract the ZIP file (see releases) to your prefered location
2. Edit the template files in the "tools" directory to make them point to your utilities
3. Optional: Edit the "regex.xml" file in the "Config" directory to enable command highlighting


### License / Warranty
Link to the license: https://www.gnu.org/licenses/gpl-3.0.txt

Warranty: None

