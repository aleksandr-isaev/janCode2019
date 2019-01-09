Public Class RunTracker

    Private runnerName As String
    Private goalTenK As Integer '3000s = 50mins
    Private runs(4) As Run
    Private pw As String
    Private runCount As Integer = 0
    Public Sub New(ByVal newName As String, ByVal newGoal As Integer, ByVal newPw As String, ByVal test As Boolean)
        runnerName = newName
        goalTenK = newGoal
        pw = newPw
        If test = True Then
            TestDataPopulation()
        End If
    End Sub
    Public Function SetName(ByVal inputName As String) As Boolean
        runnerName = inputName
        If runnerName <> "" Then
            Return True
        End If
        Return False
    End Function
    Public Function SetGoalTenK(ByVal inputGoal As Integer) As Boolean
        goalTenK = inputGoal
        Return True
    End Function
    Public Function SetPw(ByVal inputPw As String) As Boolean
        pw = inputPw
        Return True
    End Function
    Public Sub RunReview() ' output small review of each run in account
        If runCount > 0 Then
            For x = 0 To runCount - 1
                runs(x).OutputRun()
            Next
        End If
    End Sub
    Public Function GetName() As String
        Return runnerName
    End Function
    Public Function GetPw() As String
        Return pw
    End Function
    Public Function GetKmGoal() As String
        Return goalTenK
    End Function
    Public Sub AddRun()
        Dim value As Integer
        runs(runCount) = New Run()
        Dim satisfy As Boolean = 0
        Dim distance As Integer
        Dim timing As Integer

        Try
            runs(runCount) = New Run() ' tries to add new run, if given error allocates more space for runs in array
        Catch ex As Exception
            Console.WriteLine("")
            Dim temp(runCount - 1) As Run ' temp runs() array
            For x = 0 To runCount - 1
                temp(x) = runs(x) ' moves old runs into temp array

            Next
            ReDim runs(runCount) ' redims main array to add extra slot
            For x = 0 To runCount - 1
                runs(x) = temp(x) ' puts old runs back into new array

            Next
            runs(runCount) = New Run() ' adds new run
        End Try

        Do
            Do ' distance check for > 0 and data type
                Try
                    Console.WriteLine("Enter the DISTANCE you ran, in meters")
                    value = Console.ReadLine()
                Catch ex As Exception
                    MsgBox("Not entered a suitable answer. Please try again. ")
                End Try
            Loop Until value > 0
            distance = value
            runs(runCount).SetDistance(value)

            Do ' time check for range and data type
                Try
                    Console.WriteLine("Enter the TIME you ran, in seconds")
                    value = Console.ReadLine()
                Catch ex As Exception
                    MsgBox("Not entered a suitable answer. Please try again. ")
                End Try
            Loop Until value > 0.0 And value < 60 * 60 * 80
            timing = value
            runs(runCount).SetSeconds(value)

            If distance / timing >= 36.0 Then ' speed check
                satisfy = 1
                Console.WriteLine("The data you have entered suggests that you have magically been able to run more than 36 km / h which is wonderful but we don't believe you. Please input your data again! :)")
            End If
        Loop Until satisfy = 0
        'runs(runCount).GetPace() 'not needed and so deleted (on version 2)
        'runs(runCount).GetSpeed()
        runCount += 1
    End Sub
    Public Sub RunAnalysis()
        Dim max As Double = 0
        Dim goalSpeed As Double = Convert.mps2kmph(10000 / goalTenK)
        If runCount = 0 Then ' checks if there are any runs in the system
            Console.WriteLine("Unable to check as no runs have been detected")
        Else
            Dim latestSpeed As Double = runs(runCount - 1).GetSpeed
            If goalSpeed > latestSpeed Then
                Console.WriteLine("You have not met your goal speed")
            Else
                Console.WriteLine("You have met your goal speed")
            End If
            If latestSpeed > 40 Then
                Console.WriteLine("Classification of the run: Olympic")
            ElseIf latestSpeed > 30 And latestSpeed <= 40 Then
                Console.WriteLine("Classification of the run: Advanced")
            ElseIf latestSpeed > 12 And latestSpeed <= 30 Then
                Console.WriteLine("Classification of the run: Average")
            Else
                Console.WriteLine("Classification of the run: Basic")
            End If
            For count = 0 To runCount - 1
                If runs(count).GetSpeed > max Then
                    max = runs(count).GetSpeed
                End If
            Next
            Console.WriteLine("Personal best was " & max & " km/h")
            If goalSpeed > latestSpeed Then
                Console.WriteLine("You are " & goalSpeed - max & "km/h away from reaching your goal which is " & (((goalSpeed * 60) / 1000) * 10 & " meters in 10 minutes"))
            End If
        End If


    End Sub
    Private Sub TestDataPopulation()
        runs(0) = New Run
        runs(0).SetDistance(1000)
        runs(0).SetSeconds(240)
        runs(1) = New Run
        runs(1).SetDistance(2000)
        runs(1).SetSeconds(720)
        runs(2) = New Run
        runs(2).SetDistance(3000)
        runs(2).SetSeconds(1200)
        runCount = 3
    End Sub
    Public Sub SaveFile() ' save file function
        Dim filename As String = "c:\data.txt"
        Dim file As System.IO.StreamWriter
        file = My.Computer.FileSystem.OpenTextFileWriter(filename, False)
        ' user details
        file.WriteLine("Name: " & GetName())
        file.WriteLine("Password: " & GetPw())
        file.WriteLine("10KM Goal: " & GetKmGoal())
        ' run details
        For x = 0 To runCount - 1
            file.WriteLine("Distance: " & runs(x).GetDistance / 1000)
            file.WriteLine("Time: " & runs(x).GetSeconds / 60)
            file.WriteLine("Speed: " & runs(x).GetSpeed)
            file.WriteLine("Pace: " & runs(x).GetPace)
            file.WriteLine()
        Next
        file.WriteLine()
        file.Close()
    End Sub


End Class
