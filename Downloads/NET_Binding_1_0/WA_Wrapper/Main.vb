Public Module WolframAlphaWrapperExample

    Dim Engine As New WolframAlphaEngine

    Public Sub Output(ByVal Data As String, ByVal Indenting As Integer, ByVal Color As System.ConsoleColor)
        Data = New String(" ", Indenting * 4) & Data

        Console.ForegroundColor = Color
        Console.WriteLine(Data)
        Console.ForegroundColor = ConsoleColor.White

        Dim Writer As New IO.StreamWriter(System.Environment.GetFolderPath(Environment.SpecialFolder.Desktop) & "\Wolfram Alpha wrapper log.log", True)
        Writer.WriteLine(Data)
        Writer.Close()
        Writer.Dispose()

    End Sub

    Public Sub Main()

        'Try to delete the log file if it already exists.
        Try
            IO.File.Delete(System.Environment.GetFolderPath(Environment.SpecialFolder.Desktop) & "\Wolfram Alpha wrapper log.log")
        Catch
        End Try

        'Define what our application ID is.
        Dim WolframAlphaApplicationID As String = "beta824g1"

        'Define what we want to search for.
        Dim WolframAlphaSearchTerms As String = "england"

        'Print out what we're about to do in the console.
        Output("Getting response for the search terms """ & WolframAlphaSearchTerms & """ and the application ID string """ & WolframAlphaApplicationID & """ ...", 0, ConsoleColor.White)

        'Use the engine to get a response, from the application ID specified, and the search terms.
        Engine.LoadResponse(WolframAlphaSearchTerms, WolframAlphaApplicationID)

        'Print out a message saying that the last task was successful.
        Output("Response injected.", 0, ConsoleColor.White)

        'Make 2 empty spaces in the console.
        Output("", 0, ConsoleColor.White)

        Output("Response details", 1, ConsoleColor.Blue)

        'Print out how many different pods that were found.
        Output("Pods found: " & Engine.QueryResult.NumberOfPods, 1, ConsoleColor.White)
        Output("Query pasing time: " & Engine.QueryResult.ParseTiming & " seconds", 1, ConsoleColor.White)
        Output("Query execution time: " & Engine.QueryResult.Timing & " seconds", 1, ConsoleColor.White)

        Dim PodNumber As Integer = 1

        For Each Item As WolframAlphaPod In Engine.QueryResult.Pods

            'Make an empty space in the console.
            Output("", 0, ConsoleColor.White)

            Output("Pod " & PodNumber, 2, ConsoleColor.Red)

            Output("Sub pods found: " & Item.NumberOfSubPods, 2, ConsoleColor.White)
            Output("Title: """ & Item.Title & """", 2, ConsoleColor.White)
            Output("Position: " & Item.Position, 2, ConsoleColor.White)

            Dim SubPodNumber As Integer = 1

            For Each SubItem As WolframAlphaSubPod In Item.SubPods

                Output("", 0, ConsoleColor.White)

                Output("Sub pod " & SubPodNumber, 3, ConsoleColor.Magenta)
                Output("Title: """ & SubItem.Title & """", 3, ConsoleColor.White)
                Output("Pod text: """ & SubItem.PodText & """", 3, ConsoleColor.White)
                Output("Pod image title: """ & SubItem.PodImage.Title & """", 3, ConsoleColor.White)
                Output("Pod image width: " & SubItem.PodImage.Width, 3, ConsoleColor.White)
                Output("Pod image height: " & SubItem.PodImage.Height, 3, ConsoleColor.White)
                Output("Pod image location: """ & SubItem.PodImage.Location.ToString & """", 3, ConsoleColor.White)
                Output("Pod image description text: """ & SubItem.PodImage.HoverText & """", 3, ConsoleColor.White)

                SubPodNumber += 1

            Next

            PodNumber += 1


        Next

        'Make an empty space in the console.
        Output("", 0, ConsoleColor.White)

        'Make the application stay open until there is user interaction.
        Output("All content has been saved to " & System.Environment.GetFolderPath(Environment.SpecialFolder.Desktop) & "\Wolfram Alpha wrapper log.log. Press a key to close the example.", 0, ConsoleColor.Green)
        Console.ReadLine()

    End Sub
End Module