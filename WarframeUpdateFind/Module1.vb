Module Module1

    Sub Main()
        Dim runLoc As String = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).Replace("file:\", "")

        Dim patchLines As String() = System.IO.File.ReadAllLines(runLoc & "\patch_notes.txt")

        Dim cmdLineArgs() As String = Environment.GetCommandLineArgs()

        Dim toFind As String = ""

        For i = 1 To cmdLineArgs.Length - 1
            toFind = toFind & " " & cmdLineArgs(i)
        Next
        If toFind.Length > 1 Then toFind = toFind.Substring(1, toFind.Length - 1)

        Console.WriteLine("Looking for: " + toFind)

        Dim verString As String = ""
        Dim hasWrittenVerString As Boolean = False

        For i = 0 To patchLines.Length - 1
            If patchLines(i).ToLower().Contains("update number") Then
                verString = System.Text.RegularExpressions.Regex.Replace(patchLines(i), "\|update number\s*?\=\s*?", "{{ver|", Text.RegularExpressions.RegexOptions.IgnoreCase) & "}}"
                hasWrittenVerString = False
            End If
            If patchLines(i).ToLower().Contains(toFind) Then
                If hasWrittenVerString = False Then
                    Console.WriteLine(verString)
                    hasWrittenVerString = True
                End If
                Console.WriteLine(patchLines(i))
            End If

        Next
    End Sub

End Module
