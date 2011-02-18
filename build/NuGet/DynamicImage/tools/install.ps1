param($rootPath, $toolsPath, $package, $project)

if ($project) { $projectName = $project.Name }
Get-ProjectItem "InstallationDummyFile.txt" -Project $projectName | %{ $_.Delete() }