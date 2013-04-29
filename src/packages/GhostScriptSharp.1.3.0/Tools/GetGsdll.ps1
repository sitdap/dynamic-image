function Get-MoveGsDllString($toolsPath) {
"
if not exist `"`$(TargetDir)\gsdll32.dll`" xcopy /Y `"`$(ProjectDir)\Ghostscript\gsdll32.dll`" `"`$(TargetDir)`""
}