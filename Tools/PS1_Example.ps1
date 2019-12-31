# Example file for PowerShell PS1 files

param (
    # The below variable will contain the text that was on the clipboard
    [string]$Clipboardstring,
    # The below variable will contain the text representing the optional argument (currently only supported when called using an XML file)
    [string]$OptArg
 )

echo "In the PowerShell script"
"Clipboardstring: $Clipboardstring"
"OptArg: $OptArg"