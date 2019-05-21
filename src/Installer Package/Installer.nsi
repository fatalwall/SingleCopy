#################################################################### 
##Copyright (C) 2019 Peter Varney - All Rights Reserved
## You may use, distribute and modify this code under the
## terms of the MIT license, 
##
## You should have received a copy of the MIT license with
## this file. If not, visit : https://github.com/fatalwall/SingleCopy
####################################################################

;Required modules
!include "MUI2.nsh"
!include "FileFunc.nsh"
!include "strExplode.nsh"
!include "DotNetVersion.nsh"
!include "LogicLib.nsh"


;Definitions
!define PRODUCT_PROJECT_NAME "{VS.ProjectName}"
!define PRODUCT_NAME "{VS.AssemblyTitle}"
!define PRODUCT_ASSEMBLY_VERSION "{VS.AssemblyVersion}"
!define PRODUCT_VERSION "{VS.ProductVersion}"
!define PRODUCT_PUBLISHER "{VS.AssemblyCompany}"
!define PRODUCT_COPYRIGHT "{VS.AssemblyCopyright}"
!define PRODUCT_DESCRIPTION "{VS.AssemblyDescription}"
!define PRODUCT_BUILD_TYPE "{VS.BuildType}"
!define PRODUCT_WEB_SITE "https://github.com/fatalwall/SingleCopy"
!define PRODUCT_UNINST_KEY "Software\Microsoft\Windows\CurrentVersion\Uninstall\${PRODUCT_NAME}"
!define PRODUCT_UNINST_ROOT_KEY "HKLM"

;MUI Settings
!define MUI_ABORTWARNING
!define MUI_ICON "${NSISDIR}\Contrib\Graphics\Icons\modern-install.ico"
!define MUI_UNICON "${NSISDIR}\Contrib\Graphics\Icons\modern-uninstall.ico"
!define MUI_DIRECTORYPAGE_VARIABLE $INSTDIR

;MUI Installer Pages
!insertmacro MUI_PAGE_WELCOME
!insertmacro MUI_PAGE_LICENSE "LICENSE"
!insertmacro MUI_PAGE_DIRECTORY
!insertmacro MUI_PAGE_COMPONENTS
!insertmacro MUI_PAGE_INSTFILES
!insertmacro MUI_PAGE_FINISH

;MUI Uninstaller Pages
!insertmacro MUI_UNPAGE_INSTFILES

;MUI Language Files
!insertmacro MUI_LANGUAGE "English"

;Settings
Name "${PRODUCT_NAME}"
OutFile "${PRODUCT_NAME} ${PRODUCT_ASSEMBLY_VERSION}.exe"
RequestExecutionLevel admin
InstallDir "$PROGRAMFILES\${PRODUCT_NAME}"
ShowInstDetails show
ShowUnInstDetails show

############################
## File Details
############################
	VIProductVersion "${PRODUCT_VERSION}"
	VIAddVersionKey /LANG=${LANG_ENGLISH} "ProductName" "${PRODUCT_NAME}"
	VIAddVersionKey /LANG=${LANG_ENGLISH} "CompanyName" "${PRODUCT_PUBLISHER}"
	VIAddVersionKey /LANG=${LANG_ENGLISH} "LegalCopyright" "${PRODUCT_COPYRIGHT}"
	VIAddVersionKey /LANG=${LANG_ENGLISH} "FileDescription" "${PRODUCT_DESCRIPTION}"
	VIAddVersionKey /LANG=${LANG_ENGLISH} "ProductVersion" "${PRODUCT_ASSEMBLY_VERSION}"
	VIAddVersionKey /LANG=${LANG_ENGLISH} "FileVersion" "${PRODUCT_ASSEMBLY_VERSION}"

############################
## Install
############################
	Section "!.Net 4.7" Net4
		SectionIn RO
		SetOutPath "$PLUGINSDIR"
		SetOverwrite on

		${DotNetVersion} $0 '4' '7' '*'
		${If} $0 == "FALSE"
			File "/oname=$PLUGINSDIR\NDP47-KB3186500-Web.exe" "..\..\Dependencies\.Net 4.7 Framework\NDP47-KB3186500-Web.exe"
			DetailPrint "Installing .Net Framework 4.7 (Installation will take several minutes)"
			nsExec::Exec '"$PLUGINSDIR\NDP47-KB3186500-Web.exe" /q /norestart'
			Pop $0
			${Switch} $0
				${Case} 0
					DetailPrint "Successfully installed .Net Framework 4.7"
					${Break}
				${Case} 1602
					DetailPrint "The user canceled installation of .Net Framework 4.7"
					Abort
					${Break}
				${Case} 1603
					DetailPrint "A fatal error occurred during installation of .Net Framework 4.7"
					Abort
					${Break}
				${Case} 1641
				${Case} 3010
					DetailPrint "A restart is required before you can install ${PRODUCT_NAME}"
					Abort
					${Break}
				${Case} 5100
					DetailPrint "The user's computer does not meet system requirements"
					Abort
					${Break}
				${Default}
					DetailPrint ".Net Framework 4.7 failed to install with exit code $0"
					${Break}
			${EndSwitch}
		${Else}
			DetailPrint ".Net Framework 4.7 or greater already installed"
		${EndIf}
	SectionEnd

	Section "Core Files (Required)" core
		SectionIn RO
		SetOutPath "$INSTDIR"

		;Write Service Files
		File /r /x "*.pdb" /x "*.xml" "..\..\..\${PRODUCT_PROJECT_NAME}\bin\${PRODUCT_BUILD_TYPE}\*"

		;Create Desktop Icon for All Users
		SetShellVarContext all
		CreateShortCut '$desktop\${PRODUCT_NAME}.lnk' '$INSTDIR\${PRODUCT_PROJECT_NAME}.exe' '' '$INSTDIR\${PRODUCT_PROJECT_NAME}.exe' 0 SW_SHOWMAXIMIZED
	SectionEnd

	Section -Post
		;Place uninstaller
		WriteUninstaller "$INSTDIR\uninst.exe"

		;Create registry keys for uninstall
		WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "DisplayName" "$(^Name)"
		WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "UninstallString" "$INSTDIR\uninst.exe"
		WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "DisplayVersion" "${PRODUCT_ASSEMBLY_VERSION}"
		WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "Publisher" "${PRODUCT_PUBLISHER}"
		WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "URLInfoAbout" "${PRODUCT_WEB_SITE}"
		WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "DisplayIcon" "$INSTDIR\${PRODUCT_PROJECT_NAME}.exe"

		;Event Log indicating installation
		nsExec::Exec 'EVENTCREATE /L APPLICATION /SO "$(^Name)" /T INFORMATION /ID 1000  /D "$(^Name) ${PRODUCT_ASSEMBLY_VERSION} Installed Successfully"'
	SectionEnd

############################
## Uninstall
############################
	Function un.onUninstSuccess
		HideWindow
		;GUI Confirm Success. Skipped if Silent parameter passed
		nsExec::Exec 'EVENTCREATE /L APPLICATION /SO "$(^Name)" /T INFORMATION /ID 1000  /D "$(^Name) ${PRODUCT_ASSEMBLY_VERSION} Uninstalled Successfully"'
		MessageBox MB_ICONINFORMATION|MB_OK "$(^Name) was successfully removed from your computer." /SD IDOK
	FunctionEnd

	Function un.onInit
		;GUI Confirm Install Request. Skipped if Silent parameter passed
		MessageBox MB_ICONQUESTION|MB_YESNO|MB_DEFBUTTON2 "Are you sure you want to completely remove $(^Name) and all of its components?" /SD IDYES IDYES yes IDNO no
		no:
		Abort
		yes:
	FunctionEnd

	Section Uninstall
		;Delete Desktop Shortcut
		SetShellVarContext all
		Delete "$desktop\${PRODUCT_NAME} Dashboard.lnk"

		############################
		## Standard Cleanup Tasks
		############################
		;Delete the the uninstaller
		Delete "$INSTDIR\uninst.exe"
		;Delete installation folder if empty
		RMDir /r /REBOOTOK "$INSTDIR"
		RMDir /REBOOTOK "$INSTDIR"
		;Delete Add/Remove Programs registry keys
		DeleteRegKey ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}"
		
		SetAutoClose true
	SectionEnd