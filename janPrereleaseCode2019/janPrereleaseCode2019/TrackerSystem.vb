' imports for the save Function
Imports System
Imports System.IO

Public Class TrackerSystem
    Private Accounts(1) As RunTracker ' 2 accounts set for preset data
    Private AccountCount As Integer = 0 ' number of accounts in use
    Public Sub New()
        Accounts(0) = New RunTracker("Simon", "5000", "fish", True)
        Accounts(1) = New RunTracker("Annie", "2500", "fish", True)
        AccountCount = 2 ' 2 set for preset data
    End Sub
    Public Function Menu() As Boolean ' function to get input from user and run menu methods/functions
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
            Case 5
                Update()
            Case 6
                RemoveRunner()
            Case 7
                Save()
            Case 8
                RemoveRun()
            Case Else
                Console.WriteLine("Not an Option")
        End Select
        Return True
    End Function
    Public Sub MenuOptions() ' written menu
        Console.WriteLine("")
        Console.WriteLine("---------------------------------------")
        Console.WriteLine("Choose 1 add a new runner")
        Console.WriteLine("Choose 2 add a new run")
        Console.WriteLine("Choose 3 for your run history")
        Console.WriteLine("Choose 4 for your 10 km goal analysis")
        Console.WriteLine("Choose 5 to update data")
        Console.WriteLine("Choose 6 to remove a runner")
        Console.WriteLine("Choose 7 to save")
        Console.WriteLine("Choose 8 to remove run")
        Console.WriteLine("Choose 0 to exit")
    End Sub
    Public Sub RemoveRun()
        Dim foundLocation As Integer
        Dim choice As Char
        Dim temp(AccountCount - 1) As RunTracker
        foundLocation = Verify()
        If foundLocation > -1 Then
            Console.WriteLine("Are you sure you want to delete your account? (Y/N)")
            choice = UCase(Console.ReadLine())
            If choice = "Y" Then
                'Shuffle everything below the dismissed runner one slot up (execute the disapoinment)
                If (foundLocation + 1) - (runs(runCount) < 1 Then 'only shuffle if its not the last account
                    For x = foundLocation + 1 To AccountCount - 1
                        runs(x - 1) = Accounts(x)
                    Next
                End If
                'transfer all info into temp array
                For x = 0 To AccountCount - 1
                    temp(x) = Accounts(x)
                Next
                'make ur accounts array 1 slot smaller
                ReDim Accounts(AccountCount - 2)
                'bye bye disappointment
                AccountCount -= 1
                'Put everything back to the accounts array and all is right with the world again
                For x = 0 To AccountCount - 1
                    Accounts(x) = temp(x)
                Next
            End If
        End If
    End Sub
    'Public Sub RemoveRunner()
    '    Dim foundLocation As Integer
    '    Dim choice As String
    '    Dim accNum As Integer
    '    foundLocation = Verify()
    '    Console.WriteLine("Are you sure you want to delete your account? (Y/N)")
    '    choice = Console.ReadLine()
    '    If choice = "Y" Then
    '        accNum = AccountCount - foundLocation - 1
    '        If accNum > 0 Then
    '            For x = foundLocation + 1 To accNum
    '                Accounts(x - 1) = Accounts(x)
    '            Next
    '        Else

    '        End If

    '    End If
    'End Sub
    Public Sub RemoveRunner()
        Dim foundLocation As Integer
        Dim choice As Char
        Dim temp(AccountCount - 1) As RunTracker
        foundLocation = Verify()
        If foundLocation > -1 Then
            Console.WriteLine("Are you sure you want to delete your account? (Y/N)")
            choice = UCase(Console.ReadLine())
            If choice = "Y" Then
                'Shuffle everything below the dismissed runner one slot up (execute the disapoinment)
                If (foundLocation + 1) - (AccountCount - 1) < 1 Then 'only shuffle if its not the last account
                    For x = foundLocation + 1 To AccountCount - 1
                        Accounts(x - 1) = Accounts(x)
                    Next
                End If
                'transfer all info into temp array
                For x = 0 To AccountCount - 1
                    temp(x) = Accounts(x)
                Next
                'make ur accounts array 1 slot smaller
                ReDim Accounts(AccountCount - 2)
                'bye bye disappointment
                AccountCount -= 1
                'Put everything back to the accounts array and all is right with the world again
                For x = 0 To AccountCount - 1
                    Accounts(x) = temp(x)
                Next
            End If
        End If
    End Sub

    Public Sub AddNewRunner()
        Dim newName As String
        Dim newGoal As Integer
        Dim newPw As String
        Dim check As Boolean = False ' used for validations checks
        Dim currentChar As Integer ' used to check for integer
        Dim count As Integer = 0
        Dim total As Integer = 2
        Dim passwordCorrect As String

        Dim oldaccounts(AccountCount - 1) As RunTracker ' temp Accounts() array
        For x = 0 To AccountCount - 1
            oldaccounts(x) = Accounts(x) ' moves accounts to temp array
        Next
        ReDim Accounts(AccountCount) ' adds extra account slot to array
        For x = 0 To AccountCount - 1
            Accounts(x) = oldaccounts(x) ' moves old data from temp array to new array
        Next

        Console.WriteLine("Enter Your Name")
        Do

            While check = False ' name check
                newName = Console.ReadLine()
                For x = 0 To AccountCount - 1
                    If newName = Accounts(x).GetName() Then
                        Console.WriteLine("That username has already been taken. Please enter another username.")
                        newName = Console.ReadLine()
                        check = False
                    Else
                        check = True
                    End If
                Next
                For x = 1 To Len(newName) ' integer check
                    currentChar = (Asc(Mid(newName, x, 1)))
                    If currentChar > 47 And currentChar < 58 And count = 0 Then
                        Console.WriteLine("You have entered an integer as part of your name. Please try again. ")
                        count = count + 1
                        check = False
                    End If
                Next
            End While

            Do ' input check for 10km goal
                Try
                    Console.WriteLine("Enter Your 10 Km goal time (in seconds)")

                    newGoal = Console.ReadLine
                Catch ex As Exception
                    MsgBox("Not entered an integer")
                End Try
            Loop Until newGoal > 0

            Console.WriteLine("Enter Your Password") ' password input
            newPw = Console.ReadLine
            check = PasswordChecker(newPw)

            Do While check = False ' strength check form Function PasswordChecker()
                Console.WriteLine("Enter a new password. It is not a strong enough password.")
                newPw = Console.ReadLine
                check = PasswordChecker(newPw)
            Loop

            check = False
            Do ' password reentry for extra validation
                Console.WriteLine("Please re enter your password (validating your password for you)")
                passwordCorrect = Console.ReadLine()
                If passwordCorrect <> newPw Then
                    Console.WriteLine("Your passwords do not match. Please reinput again. ")
                    passwordCorrect = Console.ReadLine()
                Else
                    check = True
                End If
            Loop Until check = True
            Accounts(AccountCount) = New RunTracker(newName, newGoal, newPw, False) ' sends all correct data to Accounts() array
        Loop Until Accounts(AccountCount).GetName <> ""
        AccountCount += 1
        Console.WriteLine("")
        Console.WriteLine("Runner Added!")
    End Sub
    Public Function PasswordChecker(ByVal newPw As String) As Boolean
        Dim upC As Integer
        Dim lowC As Integer
        Dim num As Integer
        Dim sym As Integer
        Dim points As Integer
        ' Length check
        If Len(newPw) >= 8 Then
            points = points + 1
        End If
        For x = 1 To Len(newPw)

            ' Uppercase check
            If Asc(Mid(newPw, x, 1)) > 64 And Asc(Mid(newPw, x, 1)) < 91 Then
                upC = upC + 1
                If upC = 1 Then
                    points = points + 1
                End If
            End If

            ' Lowercase check
            If Asc(Mid(newPw, x, 1)) > 96 And Asc(Mid(newPw, x, 1)) < 123 Then
                lowC = lowC + 1
                If lowC = 1 Then
                    points = points + 1
                End If
            End If

            ' Symbols check
            If Asc(Mid(newPw, x, 1)) >= 0 And Asc(Mid(newPw, x, 1)) < 48 Then
                sym = sym + 1
                If sym = 1 Then
                    points = points + 1
                End If
            End If

            ' Numbers check
            If Asc(Mid(newPw, x, 1)) > 47 And Asc(Mid(newPw, x, 1)) < 58 Then
                num = num + 1
                If num = 1 Then
                    points = points + 1
                End If
            End If
        Next
        If points > 3 Then ' must get at least 4 to pass strength check
            Return True
        End If
        Return False
    End Function
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
    Public Function Verify() As Integer ' verify username
        Dim inputName As String
        Dim inputPw As String

        Console.WriteLine("Enter Your Name")
        inputName = Console.ReadLine()
        Console.WriteLine("Enter Your Password")
        inputPw = Console.ReadLine()

        For x = 0 To AccountCount - 1
            If inputName = Accounts(x).GetName Then
                If inputPw = Accounts(x).GetPw Then
                    Return x ' return location of name in array
                End If
            End If
        Next

        Console.WriteLine("Incorrect Username or password")
        Return -1
    End Function
    Public Sub Update()
        Dim newName As String
        Dim newGoal As Integer
        Dim choice As String
        Dim nmCheck As Integer ' account number of user
        Dim check As Boolean ' verifies if data has been entered
        Console.WriteLine("UPDATE DATA")
        nmCheck = Verify()

        If nmCheck > -1 Then
            Console.WriteLine("Choose 1 to update name
Choose 2 to update goal")
            choice = Console.ReadLine()

            Select Case choice

                Case 1
                    Console.WriteLine("Enter new name")
                    check = Accounts(nmCheck).SetName(Console.ReadLine())
                    If check = True Then
                        Console.WriteLine("Name changed")
                    Else
                        Console.WriteLine("Enter name again")
                    End If

                Case 2
                    Console.WriteLine("Enter new goal")
                    check = Accounts(nmCheck).SetGoalTenK(Console.ReadLine())
                    If check = True Then
                        Console.WriteLine("Goal changed")
                    Else
                        Console.WriteLine("Enter goal again")
                    End If
                Case Else

            End Select
        End If
    End Sub
    Public Sub Save() ' save to file method
        For x = 0 To AccountCount - 1
            Accounts(x).SaveFile()
        Next
    End Sub

End Class
