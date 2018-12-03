Public Class TrackerSystem

    Private Accounts(9) As RunTracker
    Private AccountCount As Integer = 0
    Public Sub New()
        Accounts(0) = New RunTracker("Simon", "5000", "fish", True)
        Accounts(1) = New RunTracker("Annie", "2500", "fish", True)
        AccountCount = 2
    End Sub
    Public Function Menu() As Boolean
        Dim choice As Integer
        MenuOptions()
        choice = Console.ReadLine()
        Select Case choice
            Case 0
                Return False
            Case 1
                AddNewRunner()
            Case 2
                AddNewRun()
            Case 3
                OutputHistory()
            Case 4
                TenKmGoal()
            Case Else
                Console.WriteLine("Not an Option")
        End Select
        Return True
    End Function
    Public Sub MenuOptions()
        Console.WriteLine("")
        Console.WriteLine("---------------------------------------")
        Console.WriteLine("Choose 1 add a new runner")
        Console.WriteLine("Choose 2 add a new run")
        Console.WriteLine("Choose 3 for your run history")
        Console.WriteLine("Choose 4 for your 10 km goal analysis")
        Console.WriteLine("Choose 0 to exit")
    End Sub
    Public Sub AddNewRunner()
        Dim newName As String
        Dim newGoal As Integer
        Dim newPw As String
        Do
            Console.WriteLine("Enter Your Name")
            newName = Console.ReadLine()
            Console.WriteLine("Enter Your 10 Km goal time")
            newGoal = Console.ReadLine
            Console.WriteLine("Enter Your Password")
            newPw = Console.ReadLine
            Accounts(AccountCount) = New RunTracker(newName, newGoal, newPw, False)
        Loop Until Accounts(AccountCount).GetName <> ""
        AccountCount += 1
    End Sub
    Public Sub AddNewRun()
        Dim foundLocation As Integer
        foundLocation = Verify()
        If foundLocation > -1 Then
            Accounts(foundLocation).AddRun()
        End If
    End Sub
    Public Sub OutputHistory()
        Dim foundLocation As Integer
        foundLocation = Verify()
        If foundLocation > -1 Then
            Accounts(foundLocation).RunReview()
        End If
    End Sub
    Public Sub TenKmGoal()
        Dim foundLocation As Integer
        foundLocation = Verify()
        If foundLocation > -1 Then
            Accounts(foundLocation).RunAnalysis()
        End If
    End Sub
    Public Function Verify() As Integer
        Dim inputName As String
        Dim inputPw As String
        Console.WriteLine("Enter Your Name")
        inputName = Console.ReadLine()
        Console.WriteLine("Enter Your Password")
        inputPw = Console.ReadLine()
        For x = 0 To AccountCount - 1
            If inputName = Accounts(x).GetName Then
                If inputPw = Accounts(x).GetPw Then
                    Return x
                End If
            End If
        Next
        Console.WriteLine("Incorrect Username or password")
        Return -1
    End Function

End Class
