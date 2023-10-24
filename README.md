# CopyTool
Instantly find and copy important stuff

![Screenshot 2023-10-17 231817](https://github.com/Vipxpert/CopyTool/assets/68524186/63479662-9703-414a-8bf3-21b91fb04189)

CopyTool is an app that allows you to organize data that you need to copy-paste frequently as a routine by simply editing txt files on Windows
It can be helpful for storing information that you can't remember or type out easily. For etc emoji, kaomoji, regex, phone number,...

Interface:
+ Press the default F1 hotkey to bring up or hide the app
+ Hover over an item to see a full preview of it
+ Click on the table's items to copy. You can drag and drop them to any text box. Right-click on them to copy and hide the app at the same time
+ Click on the table's header to either expand or shrink items
+ Hold down the left mouse on any empty space to drag the app. Right-click on empty space to hide the app
+ Left-click "Directory" to open the data folder. Right-click to open the app's directory

Data:
+ Data is stored in the same folder with the exe. In case being lost just click on the "Data folder" link on the app
+ Folders represent Types and Files represent Categories
+ You can add number prefixes to any of the files or folders to sort them. The number prefix will be ignored in the app, but remember to number them correctly with where it's being at 1b, 2a, 3c. Incorrectly 1b, 1c, 5d,.... Numbers at the middle of the file name won't be removed
+ Table's contents are stored in .txt files. Take note that the elements inside the file are separated by "ㅤ" or say the \u3164 Hangul filler Unicode characters. It might look like " " (space) but it isn't. This'll avoid conflicting between data. You can always customise it.
+ Data grouping can be customized in AppSettings.json
+ Cipher.txt stores the data for simple encryption actions
+ Change DataPath.txt in the app directory to change data location. The default is "current/Data/". You can change it to anywhere else "C://ProgramFile/..."

To conveniently edit data, run CtrlSpace.ahk with AutoHotkey then click Ctrl + Space. This will switch the space bar behaviourㅤtoㅤtypeㅤHangulㅤFiller,ㅤwhichㅤisㅤusedㅤasㅤtheㅤdefaultㅤdelimiter (or just to prank friends).

Use AppConfig.json to change settings:
+ startOnBoot: Whether to start the app on boot or not
+ hideOnBoot: Whether to show the app on boot or not
+ dataDelimiter: Character used to separate data elements in txt files
+ headerExpand: Whether to expand columns on startup
+ groupType: List groups
+ headerGroupExpand: Whether to expand groups' columns on start-up
+ excludedFolderFromGroup: Folders that are unlisted from groups (will always take effect)
+ includedFolderInGroup: Folders that are included in groups (don't take effect when empty)
+ startTypeIndex: Which type to start up with
+ maxNumberOfColumnVisible: Fit the view to these number of columns on the screen
+ formHeight: Form height
+ actionInstantCopy: Whether to copy at the same time you execute an action
+ hotkey: Hotkey to trigger the app open. Search for Windows Virtual keycode to get the number.
+ folderDelimiter: Only folders that include this string will be listed
+ excludeFolderDelimiter: Unlist any folders that include this string

Made with Visual Studio, AutoHotkey, love, and a college student's brains  (✿^v^)

(￣ω￣)p

P/S: README.md on the website is always more up-to-date than the releases
