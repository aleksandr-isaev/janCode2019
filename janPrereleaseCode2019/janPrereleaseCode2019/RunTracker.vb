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
    Public Sub RunReview()
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
    Public Sub AddRun()
        Dim value As Integer
        runs(runCount) = New Run()
        Dim satisfy As Boolean = 0
        Dim distance As Integer
        Dim timing As Integer
        Dim temp(1) As Run
        Try
            runs(runCount) = New Run()
        Catch ex As Exception
            Console.WriteLine("")
            ReDim temp(runCount - 1)
            For x = 0 To runCount - 1
                temp(x) = runs(x)

            Next
            ReDim runs(runCount)
            For x = 0 To runCount - 1
                runs(x) = temp(x)

            Next
            runs(runCount) = New Run()
        End Try
        Do
            Do
                Try
                    Console.WriteLine("Enter the DISTANCE you ran, in meters")
                    value = Console.ReadLine()
                Catch ex As Exception
                    MsgBox("Not entered a suitable answer. Please try again. ")
                End Try
            Loop Until value > 0
            distance = value
            runs(runCount).SetDistance(value)

            Do
                Try
                    Console.WriteLine("Enter the TIME you ran, in seconds")
                    value = Console.ReadLine()
                Catch ex As Exception
                    MsgBox("Not entered a suitable answer. Please try again. ")
                End Try
            Loop Until value > 0.0 And value < 60 * 60 * 80
            timing = value
            runs(runCount).SetSeconds(value)
            If distance / timing >= 36.0 Then
                satisfy = 1
                Console.WriteLine("The data you have entered suggests that you have magically been able to run more than 36 km / h which is wonderful but we don't believe you. Please input your data again! :)")
            End If
        Loop Until satisfy = 0
        'runs(runCount).GetPace() 'not needed and so deleted (on version 2)
        'runs(runCount).GetSpeed()
        runCount += 1
    End Sub
    Public Sub RunAnalysis()
        Dim goalSpeed As Double = Convert.mps2kmph(10000 / goalTenK)
        If runCount = 0 Then
            Console.WriteLine("WRONG ")
        Else
            Dim latestSpeed As Double = runs(runCount - 1).GetSpeed
            If goalSpeed > latestSpeed Then
                Console.WriteLine("You have not met your goal speed")
            Else
                Console.WriteLine("You have met your goal speed")
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

End Class
