#Persistent
$F1::
    Run, CopyToolGUI.exe,
return

#NoEnv
SendMode Input
SetWorkingDir %A_ScriptDir%
toggle := false
^Space:: 
    toggle := !toggle
return
#if toggle
Space::Send {U+3164}
#if
