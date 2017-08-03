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
        Dim hasWrittenUpdateVerString As Boolean = False

        For i = 0 To patchLines.Length - 1
            If patchLines(i).ToLower().Contains("update number") Then
                If Not hasWrittenUpdateVerString Then
                    ' We can be safe to use this only for more recent changes due to the patch history page using the template, should trigger once for most recent update found
                    verString = System.Text.RegularExpressions.Regex.Replace(patchLines(i), "\|update number\s*?\=\s*?", "", Text.RegularExpressions.RegexOptions.IgnoreCase)
                    Console.WriteLine("<!-- Last Updated: " & verString & " -->")
                    hasWrittenUpdateVerString = True
                End If

                verString = System.Text.RegularExpressions.Regex.Replace(patchLines(i), "\|update number\s*?\=\s*?", "{{ver|", Text.RegularExpressions.RegexOptions.IgnoreCase)
                If patchLines(i + 1).ToLower().Contains("type") And patchLines(i + 1).ToLower().Contains("fix") Then
                    verString = verString + "|fix}}"
                Else
                    verString = verString + "}}"
                End If

                hasWrittenVerString = False
            ElseIf patchLines(i).ToLower().Contains("==update") Then
                verString = System.Text.RegularExpressions.Regex.Replace(patchLines(i), "\=\=Update", "{{ver|", Text.RegularExpressions.RegexOptions.IgnoreCase) & "}}"
                verString = verString.Replace("=", "")
                hasWrittenVerString = False
            ElseIf patchLines(i).ToLower().Contains("=hotfix") Then
                verString = System.Text.RegularExpressions.Regex.Replace(patchLines(i), "\=\=Hotfixe?s?", "{{ver|", Text.RegularExpressions.RegexOptions.IgnoreCase) & "|fix}}"
                verString = verString.Replace("=", "")
                hasWrittenVerString = False
            End If

            If patchLines(i).ToLower().Contains(toFind) Then
                If hasWrittenVerString = False Then
                    Console.WriteLine("")
                    Console.WriteLine(verString)
                    hasWrittenVerString = True
                End If
                Console.WriteLine(patchLines(i))
            End If

        Next
    End Sub

End Module
