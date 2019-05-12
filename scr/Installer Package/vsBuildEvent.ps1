param (
	[string]$ProjectName,
    [string]$SolutionDir,
    [string]$ConfigurationName
)
$sourceDir = $SolutionDir + 'Installer Package\'
$sourceFileName = 'Installer.nsi'
$sourceFile = $sourceDir + $sourceFileName								  
$workingDir = $sourceDir + 'bin\' + $ConfigurationName + '\'
$workingFile = $workingDir + $ProjectName + '.nsi'
$ErrorCode = 0

set-location "$sourceDir"

<#
#	Create required directories
#>
New-Item -ItemType Directory -Force -Path "$workingDir"

<#
#	Copy NSIS Script into directory matching VS Build option
#>
Copy-Item -Path "$sourceFile" -Destination "$workingFile" -Force
#Licence File
copy "..\..\LICENSE"  -Destination (New-Item "$workingDir" -Type container -force) -Container -force


<#
#	Insert Assembly Varables
#>
# Version
$argsBase = $SolutionDir + 'bin\'+ $ConfigurationName + '\' + $ProjectName + '.exe';
$Version = cmd /c "GetAssemblyValue.exe $argsBase Version" 2`>`&1
(gc "$workingFile") -replace '{VS.ProductVersion}', "$Version" | Out-File "$workingFile"
if ($ConfigurationName -eq 'Release') {
    if($Version -match '\d*.\d*.\d*') { $Version = $matches[0] }
    (gc "$workingFile") -replace '{VS.AssemblyVersion}', "$Version" | Out-File "$workingFile"
}else{
    (gc "$workingFile") -replace '{VS.AssemblyVersion}', "$Version" | Out-File "$workingFile"
}


# CompanyName
$Company = cmd /c "GetAssemblyValue.exe $argsBase Company" 2`>`&1
(gc "$workingFile") -replace '{VS.AssemblyCompany}', "$Company" | Out-File "$workingFile"

# ProductName
$ProductName = cmd /c "GetAssemblyValue.exe $argsBase Product" 2`>`&1
(gc "$workingFile") -replace '{VS.AssemblyTitle}', "$ProductName" | Out-File "$workingFile"

# Copyright
$Copyright = cmd /c "GetAssemblyValue.exe $argsBase Copyright" 2`>`&1
(gc "$workingFile") -replace '{VS.AssemblyCopyright}', "$Copyright" | Out-File "$workingFile"

# Year
$Year = Get-Date -UFormat "%Y"
(gc "$workingFile") -replace '&{Year}', "$Year" | Out-File "$workingFile"

# Description
$Description = cmd /c "GetAssemblyValue.exe $argsBase Description" 2`>`&1
(gc "$workingFile") -replace '{VS.AssemblyDescription}', "$Description" | Out-File "$workingFile"

# BuildType
(gc "$workingFile") -replace '{VS.BuildType}', "$ConfigurationName" | Out-File "$workingFile"


<#
#	Compile NSIS Script
#>
$output = cmd /c "`"C:\Program Files (x86)\NSIS\makensis.exe`" `"$workingFile`"" 2`>`&1
if ($output -like '*Total size:*bytes*')
{ $output = 'NSIS script successfully compiled' }
else { $ErrorCode += -1 }
Write-Output $output

exit $ErrorCode
