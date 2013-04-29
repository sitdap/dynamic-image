param($installPath, $toolsPath, $package, $project)

. (Join-Path $toolsPath "GetGsdll.ps1")
$MoveGsDllString = Get-MoveGsDllString($toolsPath)

# Get the current Post Build Event cmd
$currentPostBuildCmd = $project.Properties.Item("PostBuildEvent").Value


# Append our post build command if it's not already there
if (!$currentPostBuildCmd.Contains($MoveGsDllString)) {
    $project.Properties.Item("PostBuildEvent").Value += $MoveGsDllString
}