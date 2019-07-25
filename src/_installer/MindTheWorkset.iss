#define MyAppName "Freebies - Mind The Workset"
#define MyAppVersion "1.0.0"
#define MyAppPublisher "ParallaxTeam"
#define MyAppURL "https://github.com/johnpierson/MindTheWorkset"

#define RevitAppName  "MindTheWorkset"
#define RevitAddinFolder "{userappdata}\Autodesk\REVIT\Addins"

#define RevitAddin18  RevitAddinFolder+"\2018\"
#define RevitAddin19  RevitAddinFolder+"\2019\"
#define RevitAddin20  RevitAddinFolder+"\2020\"

[Setup]
; NOTE: The value of AppId uniquely identifies this application.
; Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={{A37B8A6C-6E3A-4826-81DD-C3490E5AC4E7}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
;AppVerName={#MyAppName} {#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
DefaultDirName={pf}\{#MyAppName}
DisableDirPage=yes
DefaultGroupName=Parallax Team, Inc\{#MyAppName}
DisableProgramGroupPage=yes
LicenseFile=.\LICENSEFREE
OutputDir=.
OutputBaseFilename=MindTheWorkset.v{#MyAppVersion}
Compression=lzma
SolidCompression=yes
;info: http://revolution.screenstepslive.com/s/revolution/m/10695/l/95041-signing-installers-you-create-with-inno-setup
;comment/edit the line below if you are not signing the exe with the CASE pfx
;SignTool=signtoolcase

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Components]
Name: revit18; Description: Mind the Workset for Autodesk Revit 2018;  Types: full
Name: revit19; Description: Mind the Workset for Autodesk Revit 2019;  Types: full
Name: revit20; Description: Mind the Workset for Autodesk Revit 2020;  Types: full

[Files]

;REVIT 2018 ~~~~~~~~~~~~~~~~~~~
Source: "deploy\2018\*"; DestDir: "{#RevitAddin19}"; Excludes: "*.pdb,*.xml,*.config,*.addin,*.tmp"; Flags: ignoreversion recursesubdirs createallsubdirs; Components: revit18 
Source: "deploy\{#RevitAppName}.addin"; DestDir: "{#RevitAddin18}"; Flags: ignoreversion; Components: revit18

;REVIT 2019 ~~~~~~~~~~~~~~~~~~~
Source: "deploy\2019\*"; DestDir: "{#RevitAddin19}"; Excludes: "*.pdb,*.xml,*.config,*.addin,*.tmp"; Flags: ignoreversion recursesubdirs createallsubdirs; Components: revit19 
Source: "deploy\{#RevitAppName}.addin"; DestDir: "{#RevitAddin19}"; Flags: ignoreversion; Components: revit19

;REVIT 2020 ~~~~~~~~~~~~~~~~~~~
Source: "deploy\2020\*"; DestDir: "{#RevitAddin20}"; Excludes: "*.pdb,*.xml,*.config,*.addin,*.tmp"; Flags: ignoreversion recursesubdirs createallsubdirs; Components: revit20
Source: "deploy\{#RevitAppName}.addin"; DestDir: "{#RevitAddin20}"; Flags: ignoreversion; Components: revit19

; NOTE: Don't use "Flags: ignoreversion" on any shared system files
