# WayText - Quick access to important texts by pressing F1

WayText is an app that allows you to organize texts that you need to copy-paste frequently as a routine by simply editing .txt files on Windows
It can help store information that you can't remember or type out easily like emoji, kaomoji, regex, phone number, cmd commands,...

![image](https://github.com/Vipxpert/WayText/assets/68524186/bfc34470-e066-4617-92b6-793c1fee3577)

Interface:
+ Press the default F1 hotkey to bring up or hide the app
+ Hover over an item to see a full preview of it
+ Click on the table's items to copy. You can drag and drop them to any text box. Right-click on the table to bring up the menu
+ Click on the table's header to either expand or shrink items
+ Hold down the left mouse on any empty space to drag the app. Right-click on empty space to hide the app
+ Left-click "Directory" to open the data folder. Right-click to open the app's directory

Data:
+ Data is stored locally on your machine (C:\\Users\*your user name*\WayText\). In case you are lost just click on the "Directory" link on the app
+ Folders represent Types and Files represent Categories
+ You can add number prefixes to any of the files or folders to sort them. The number prefix will be ignored in the app, but remember to number them correctly, or else it may cause bugs. For example 1b, 2a, 3c. Incorrectly 1b, 1c, 5d,... Numbers in the middle of the file name don't have any effects
+ Table's contents are stored in .txt files. Take note that the elements inside the file are separated by 2 downlines
+ Data grouping can be customized in AppSettings.json
+ Cipher.txt stores the data for simple encryption actions

Menu:
+ Run in CMD: Run contents of that cell in CMD
+ Open data file: Open .txt file representing that column
+ Open data folder: Open the folder that stores the .txt files
+ Settings: Open AppConfig.json
+ Hide: Hide the app, reopen it by the tray bar or F1
+ Restart: Restart the app
+ Exit: Exit/Close the app

The blue links at the bottom left:
+ I can't be clearer with the hints being shown when you hover the mouse over them

Use AppConfig.json to change settings:
- Basic:
+ showHint: Turn hints on or off
+ startOnBoot: Whether to start the app on boot or not
+ hideOnBoot: Whether to show the app on boot or not
+ formHeight: App's height
+ maxNumberOfColumnVisible: Fit the view to the number of columns on the screen
+ actionInstantCopy: Whether to copy at the same time you execute an action

- Advance:
+ dataDelimiter: Character used to separate data elements in txt files
+ headerExpand: Whether to expand columns on the app start
+ groupType: List groups
+ headerGroupExpand: Whether to expand groups' columns on the app start
+ includedFolderInGroup: Folders that are included in groups (don't take effect when empty)
+ excludedFolderFromGroup: Folders that are unlisted from groups (will always take effect)
+ startTypeIndex: When opening the app, select the type with this index by default (start from 0)
+ startCategoryIndex: When opening the app, select the category with this index by default (start from 0)
+ folderDelimiter: Only folders that include this string will be listed
+ excludeFolderDelimiter: Folders that include this string will be unlisted
+ hotkeyShowHideApp: Hotkey to trigger the app hide/show. Search for Windows Virtual keycode on Google to get the code.
+ hotkeyChangeTypeCategory: Hotkey to change types and categories quickly

Made with Visual Studio, AutoHotkey, love, and a college student's brains  (✿^v^)

(￣ω￣)p

P/S: README.md on the website is always more up-to-date than the releases
P/S 2: If it gets buggy entering texts, it might be because you're writing in other language. Change it to English
P/S 3: This might be the last version. There are still features to add but it takes too much effort
- An installer
- Settings editing page
- Drag and drop, edit contents right on the form

Known bug: Typing using Vietnamese sometimes doesn't work
