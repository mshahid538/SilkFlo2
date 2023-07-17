

Dim fso
Set fso = CreateObject("Scripting.FileSystemObject")


dim path
path = fso.GetParentFolderName(WScript.ScriptFullName)


'msgbox(path)

dim deleteFolder
deleteFolder = path + "\.vs"
If fso.FolderExists(deleteFolder) Then
	fso.DeleteFolder deleteFolder 
End If




deleteFolder = path + "\SilkFlo.Web\obj"
If fso.FolderExists(deleteFolder) Then
	fso.DeleteFolder deleteFolder 
End If

deleteFolder = path + "\SilkFlo.Web\bin\Release"
If fso.FolderExists(deleteFolder) Then
	fso.DeleteFolder deleteFolder 
End If

deleteFolder = path + "\SilkFlo.Web\bin\Test"
If fso.FolderExists(deleteFolder) Then
	fso.DeleteFolder deleteFolder 
End If



path = path + "\SilkFlo.Web\bin\Debug\net6.0"
If fso.FolderExists(path) Then

	Set folder = fso.GetFolder(path)

	' delete all files in root folder
	for each f in folder.Files
		On Error Resume Next
		name = f.name
		f.Delete True
		If Err Then
			msgbox( "Error deleting:" & Name & " - " & Err.Description)
		End If
	    On Error GoTo 0
	Next

	' delete all subfolders and files
	For Each f In folder.SubFolders
		On Error Resume Next
		name = f.name

		If name <> "Application Data" Then

		   f.Delete True
		   If Err Then
				WScript.Echo "Error deleting:" & Name & " - " & Err.Description
		   End If
		   On Error GoTo 0

		End If
	Next

End If

set fso = Nothing

msgbox("Deleted")