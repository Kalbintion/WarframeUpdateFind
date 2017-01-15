Module Module1

    Sub Main()
        Dim runLoc As String = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).Replace("file:\", "")

        Dim patchLines As String() = System.IO.File.ReadAllLines(runLoc & "\patch_notes.txt")

        Dim cmdLineArgs() As String = Environment.GetCommandLineArgs()

        Dim toFind As String = ""

        For i = 1 To cmdLineArgs.Length - 1
            toFind = toFind & " " & cmdLineArgs(i)
        Next

        Console.WriteLine("Looking for: " + toFind)

        For i = 0 To patchLines.Length - 1
            If patchLines(i).ToLower().Contains("update number") Then Console.WriteLine(patchLines(i).Replace("|update number  =", "{{ver|") & "}}")
            If patchLines(i).ToLower().Contains(toFind) Then Console.WriteLine(patchLines(i))
        Next
    End Sub

End Module
