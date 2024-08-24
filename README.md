# About
GzList Maker is a tool designed to create a "gzlist" file used in the "Ultimate Ninja" series of games for the PlayStation 2. 
A gzlist file is responsible for listing all .CCS files (which use gzip compression), folders, and their properties from the data.iso file (decrypted data.cvm).
The program can be found in the releases tab.

# Requirements
To use this tool, **you will need to have 7-Zip installed** in order to obtain a file list from the data.iso and convert it to the gzlist format used in the game. 
You can download 7-Zip here: https://7-zip.org/download.html. <br>(Note: Ensure that you **keep the default installation location (Program Files)**. 
If you change it, remember to also update the location in makegzlist.bat).

# How to Use
Keep the program's contents along with the **data.iso** file and then run the **makegzlist.bat** file. 
The gzlist.txt file will be generated in the same folder (Remember to always update gzlist.txt after adding and saving all your modified or new files to the data.iso).
