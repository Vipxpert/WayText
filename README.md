# CopyTool
Instantly find and copy important stuff

![Screenshot 2023-10-17 231817](https://github.com/Vipxpert/CopyTool/assets/68524186/63479662-9703-414a-8bf3-21b91fb04189)

CopyTool is an app that allows you to organize data that you need to copy-paste frequently as a routine by simply editing txt files on Windows
It can be helpful for storing information that you can't remember or type out easily. For etc emoji, kaomoji, regex, phone number,...

Run CopyTool.exe to override 2 shortcuts, F1 and Ctrl + Space for the app's feature. CopyToolGui.exe is the app UI

Right-click to close the app. Left-click to copy, you can drag and drop the table's contents.

F1 is the hotkey to open the app. And no. There isn't any way to customize that. It's not a feature... yet. You might use AutoHotkey

Notice: By default, the txt file uses "ㅤ" (Hangul filler) as a delimiter. Not " " (Space)

To conveniently edit data, click Ctrl + Space (The hotkey is assigned when you run CopyTool.exe in the background). This will switch the spaceㅤbarㅤbehaviourㅤtoㅤtypeㅤthisㅤcharacterㅤ(HangulㅤFiller),ㅤwhichㅤisㅤusedㅤasㅤtheㅤdefaultㅤdelimiter (or just to prank friends).

Folders work as types and files work as categories. You store categories in types

For example, Emoji is a type, that includes sad, happy, and angry categories. You make a folder named Emoji that stores Sad.txt, Happy.txt,...

Add a number before a file's names to sort them manually 1Method.txt, 2Class.txt,... The same goes for folders

Cipher.txt affects Simple Cipher and Simple Decipher actions

Use AppConfig.json to change settings:

- folderDelimiter: folderDelimiter will include only folders that include the delimiter
- excludeFolderDelimiter: excludeDelimiter will exclude any folders that include the delimiter
- excludedFolderFromAll: Specifically names the files that are excluded from the program when selecting all
- dataDelimiter: For separate the elements in a txt file
- maxNumberOfColumnVisible: As it said
- startTypeIndex: Start from the nth type when opening the app up
- actionInstantCopy: Copy at the same time you execute an action
- path: path (current prefix means current directory). Default is current/Data/

Made with Visual Studio, AutoHotkey, love, and a college student's brains  (✿^v^)

(￣ω￣)p


