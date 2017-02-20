Imports System.Web.Mvc

Namespace Controllers
    Public Class AdminsController
        Inherits Controller

        ' GET: AdminS
        Function Index() As ActionResult
            'TO DO - Before below, must check if logged=in as admin.  If not, must make them log-in as admin.
            'That part will be later
            If Session("LoggedinAdminID") IsNot Nothing AndAlso IsNumeric(Session("LoggedinAdminID")) = True AndAlso Integer.Parse(Session("LoggedinAdminID")) > 0 Then
                Return View()
            Else
                'Session("NeedsAdminAccess") = 1
                ViewBag.LoginPageHeader = "The admin area of the site needs you to have admin priveleges.  Please log in as admin"
                Session("For") = "ADMINISTRATORS"
                TempData("LoginPageFor") = Session("For")
                Return RedirectToAction("Index", "Login")
            End If
        End Function

#Region "CLASSES - Adding, Removing and Viewing"
        Function AddClass() As ActionResult
            If Session("LoggedinAdminID") IsNot Nothing AndAlso IsNumeric(Session("LoggedinAdminID")) = True AndAlso Integer.Parse(Session("LoggedinAdminID")) > 0 Then
                Dim objClassData As New ClassData
                'objClassData.LoadClassesList()
                Return View(objClassData)
            Else
                'Session("NeedsAdminAccess") = 1
                ViewBag.LoginPageHeader = "The admin area of the site needs you to have admin priveleges.  Please log in as admin"
                Session("For") = "ADMINISTRATORS"
                TempData("LoginPageFor") = Session("For")
                Return RedirectToAction("Index", "Login")
            End If
        End Function

        <HttpPost()>
        Function AddClass(ByVal objClassData As ClassData) As ActionResult
            If Session("LoggedinAdminID") IsNot Nothing AndAlso IsNumeric(Session("LoggedinAdminID")) = True AndAlso Integer.Parse(Session("LoggedinAdminID")) > 0 Then
                Dim objClassData1 As New ClassData
                Dim strError As String = ""
                ' objClassData1.LoadClassesList() 'Should fill each time like this
                If ModelState.IsValid Then
                    If CheckInputs(objClassData, strError) Then
                        'Dim strSelClass As String = GetClassID(objClassData.Classname)
                        If objClassData.AddClassTOSystem(strError) Then
                            ViewBag.StatusMessage = String.Format("Successfully CREATED '{0}' class in SYSTEM", objClassData.Classname)
                        Else
                            ViewBag.StatusMessage = strError
                        End If
                    Else
                        ViewBag.StatusMessage = strError
                    End If
                End If
                Return View(objClassData1)
            Else
                Return RedirectToAction("Index", "Home")
            End If
        End Function

        Private Function CheckInputs(ByVal objClassData As ClassData, ByRef strError As String) As Boolean
            Dim bOK As Boolean = False
            If String.IsNullOrEmpty(objClassData.ClassDuration) = False AndAlso IsNumeric(objClassData.ClassDuration) = False Then
                strError = "CLASS duration must be numeric"
                bOK = False
            ElseIf String.IsNullOrEmpty(objClassData.NumCreditHrs) = False AndAlso IsNumeric(objClassData.ClassDuration) = False Then
                'Decimal.TryParse(objClassData.NumCreditHrs) 
                strError = "NUMBER OF CREDIT HOURS must be numeric"
                bOK = False
            ElseIf String.IsNullOrEmpty(objClassData.StartDate) = False AndAlso Not IsDateOK(objClassData.StartDate) Then
                'Decimal.TryParse(objClassData.NumCreditHrs) 
                bOK = False
                strError = "CLASS START date must be a valid date in mm/dd/yyyy format"
            ElseIf String.IsNullOrEmpty(objClassData.EndDate) = False AndAlso Not IsDateOK(objClassData.EndDate) Then
                'Decimal.TryParse(objClassData.NumCreditHrs) 
                strError = "CLASS END date must be a valid date in mm/dd/yyyy format"
                bOK = False
            ElseIf String.IsNullOrEmpty(objClassData.StartDate) = False AndAlso String.IsNullOrEmpty(objClassData.EndDate) AndAlso IsDateOK(objClassData.StartDate) AndAlso IsDateOK(objClassData.EndDate) AndAlso DateTime.Parse(objClassData.EndDate) < DateTime.Parse(objClassData.StartDate) Then
                'Decimal.TryParse(objClassData.NumCreditHrs) 
                strError = "CLASS END date must occur AFTER CLASS START date"
                bOK = False
            Else
                bOK = True
            End If
            Return bOK
        End Function



        Function RemoveClass() As ActionResult
            If Session("LoggedinAdminID") IsNot Nothing AndAlso IsNumeric(Session("LoggedinAdminID")) = True AndAlso Integer.Parse(Session("LoggedinAdminID")) > 0 Then
                Dim objClassData As New ClassData
                objClassData.LoadClassesList()
                Return View(objClassData)
            Else
                Return RedirectToAction("Index", "Home")
            End If
        End Function

        <HttpPost()>
        Function RemoveClass(ByVal objClassData As ClassData) As ActionResult
            If Session("LoggedinAdminID") IsNot Nothing AndAlso IsNumeric(Session("LoggedinAdminID")) = True AndAlso Integer.Parse(Session("LoggedinAdminID")) > 0 Then
                Dim objClassData1 As New ClassData
                objClassData1.LoadClassesList() 'Should fill each time like this
                Dim strClassname As String = GetClassname(objClassData.SelectedClass)
                'If ModelState.IsValid Then
                If Integer.Parse(objClassData.SelectedClass) > 0 Then
                    Dim strerror As String = ""
                    If objClassData.RemoveClassFromSystem(strerror) Then
                        ViewBag.StatusMessage = String.Format("Successfully REMOVED '{0}' CLASS from SYSTEM", strClassname)
                    Else
                        ViewBag.StatusMessage = strerror
                    End If
                Else

                End If
                Return View(objClassData1)
            Else
                Return RedirectToAction("Index", "Home")
            End If
        End Function

        Function EditClass() As ActionResult
            If Session("LoggedinAdminID") IsNot Nothing AndAlso IsNumeric(Session("LoggedinAdminID")) = True AndAlso Integer.Parse(Session("LoggedinAdminID")) > 0 Then
                Dim objClassData As New ClassData
                objClassData.LoadClassesList()
                Return View(objClassData)
            Else
                'Session("NeedsAdminAccess") = 1
                ViewBag.LoginPageHeader = "The admin area of the site needs you to have admin priveleges.  Please log in as admin"
                Session("For") = "ADMINISTRATORS"
                TempData("LoginPageFor") = Session("For")
                Return RedirectToAction("Index", "Login")
            End If
        End Function

        <HttpPost()>
        Function EditClass(ByVal objClassData As ClassData) As ActionResult
            If Session("LoggedinAdminID") IsNot Nothing AndAlso IsNumeric(Session("LoggedinAdminID")) = True AndAlso Integer.Parse(Session("LoggedinAdminID")) > 0 Then
                ' objClassData.Classname = "New Class1"
                Dim objClassData1 As New ClassData
                objClassData.LoadClassesList()
                Dim strError As String = ""
                objClassData1.LoadClassesList() 'Should fill each time like this
                objClassData1.SelectedClass = objClassData.SelectedClass
                objClassData.Classname = GetClassname(objClassData.SelectedClass)
                'TO DO - Model is returning false even if filled everything...find out why
                'If ModelState.IsValid Then
                If CheckInputs(objClassData, strError) Then
                    'Dim strSelClass As String = GetClassID(objClassData.Classname)
                    If objClassData.UpdateClassINSystem(strError) Then
                        ViewBag.StatusMessage = String.Format("Successfully UPDATED '{0}' class in SYSTEM", objClassData.Classname)
                    Else
                        ViewBag.StatusMessage = strError
                    End If
                Else
                    ViewBag.StatusMessage = strError
                End If
                ' End If
                Return View(objClassData)
            Else
                Return RedirectToAction("Index", "Home")
            End If
        End Function

        Function AssignToTeachers() As ActionResult
            If Session("LoggedinAdminID") IsNot Nothing AndAlso IsNumeric(Session("LoggedinAdminID")) = True AndAlso Integer.Parse(Session("LoggedinAdminID")) > 0 Then
                Dim objClassData As New ClassData
                objClassData.LoadClassesList()
                objClassData.LoadTeacherstoAssigntoList()
                Return View(objClassData)
            Else
                'Session("NeedsAdminAccess") = 1
                ViewBag.LoginPageHeader = "The admin area of the site needs you to have admin priveleges.  Please log in as admin"
                Session("For") = "ADMINISTRATORS"
                TempData("LoginPageFor") = Session("For")
                Return RedirectToAction("Index", "Login")
            End If
        End Function

        <HttpPost()>
        Function AssignToTeachers(ByVal objClassData As ClassData) As ActionResult
            If Session("LoggedinAdminID") IsNot Nothing AndAlso IsNumeric(Session("LoggedinAdminID")) = True AndAlso Integer.Parse(Session("LoggedinAdminID")) > 0 Then
                ' objClassData.Classname = "New Class1"
                'Dim objClassData1 As New ClassData
                objClassData.LoadClassesList()
                objClassData.LoadTeacherstoAssigntoList()
                Dim strError As String = ""
                'TO DO - Model is returning false even if filled everything...find out why
                'If ModelState.IsValid Then
                If CheckInputs(objClassData, strError) Then
                    'Dim strSelClass As String = GetClassID(objClassData.Classname)
                    If objClassData.AssignClasstoTeacher(strError) Then
                        ViewBag.StatusMessage = String.Format("Successfully assigned CLASS '{0}' TO teacher {1}",
                                                              GetClassname(objClassData.SelectedClass), GetTeachername(objClassData.SelectedTeacher))
                    Else
                        ViewBag.StatusMessage = strError
                    End If
                Else
                    ViewBag.StatusMessage = strError
                End If
                ' End If
                Return View(objClassData)
            Else
                Return RedirectToAction("Index", "Home")
            End If
        End Function

        Function ViewClasses() As ActionResult
            If Session("LoggedinAdminID") IsNot Nothing AndAlso IsNumeric(Session("LoggedinAdminID")) = True AndAlso Integer.Parse(Session("LoggedinAdminID")) > 0 Then
                Dim objDT As DataTable
                Dim objClassData As New ClassData
                objDT = objClassData.GetClassesDT
                If objDT.Rows.Count = 0 Then
                    ViewBag.Error = String.Format("{0}'s list of classes is currently empty", GetTeacherName(Session("LoggedinTeacherID")))
                Else
                    ViewBag.Data = objDT
                End If
                Return View(objClassData)
            Else
                Return RedirectToAction("Index", "Home")
            End If

        End Function

#End Region

#Region "Subjects"
        Function AddSubject() As ActionResult
            If Session("LoggedinAdminID") IsNot Nothing AndAlso IsNumeric(Session("LoggedinAdminID")) = True AndAlso Integer.Parse(Session("LoggedinAdminID")) > 0 Then
                Dim objSubjectsData As New SubjectsData
                objSubjectsData.LoadClassesTobeTaughtInList()
                Return View(objSubjectsData)
            Else
                ViewBag.LoginPageHeader = "The admin area of the site needs you to have admin priveleges.  Please log in as admin"
                Session("For") = "ADMINISTRATORS"
                TempData("LoginPageFor") = Session("For")
                Return RedirectToAction("Index", "Login")
            End If
        End Function

        <HttpPost()>
        Function AddSubject(ByVal objSubjectsData As SubjectsData) As ActionResult
            If Session("LoggedinAdminID") IsNot Nothing AndAlso IsNumeric(Session("LoggedinAdminID")) = True AndAlso Integer.Parse(Session("LoggedinAdminID")) > 0 Then
                Dim strError As String = ""
                objSubjectsData.LoadClassesTobeTaughtInList()
                'TO Do - does not work if there are MANY records of this subject being connected to DIFFERENT classes.
                'What happens is it picks only the top one as SubjectID.
                'So, need to write GetSubjectIDs and make it return a list of subject records having been connected to
                'different classes.  If it is connected to only one class, there will be only one record.
                'To be done later
                Dim iSubjectID As Integer = GetSubjectID(objSubjectsData.Subjectname)
                If iSubjectID > 0 Then
                    'Subject that is already there.
                    'Check if it is connected to this class
                    If Not SubjectAlreadyConnectedtoClass(objSubjectsData.SelectedClass, iSubjectID) Then
                        'subject there BUT NOT connected to THIS class

                        If ModelState.IsValid Then
                            If objSubjectsData.AddSubjectToSystem(strError) Then
                                ViewBag.StatusMessage = String.Format("Successfully CREATED '{0}' subject in SYSTEM UNDER {1}",
                                objSubjectsData.Subjectname, GetClassname(objSubjectsData.SelectedClass))
                            Else
                                ViewBag.StatusMessage = strError
                            End If
                        Else
                            ViewBag.StatusMessage = strError
                        End If
                    Else
                        'Subject ALREADY CONNECTED to THIS class
                        ViewBag.StatusMessage = String.Format("'{0}' <u>is already being</u> taught in '{1}' class", objSubjectsData.Subjectname, GetClassname(objSubjectsData.SelectedClass))
                    End If
                Else
                    'NEW SUBJECT - Not already there 
                    If ModelState.IsValid Then
                        If objSubjectsData.AddSubjectToSystem(strError) Then
                            ViewBag.StatusMessage = String.Format("Successfully CREATED '{0}' subject in SYSTEM UNDER {1}",
                            objSubjectsData.Subjectname, GetClassname(objSubjectsData.SelectedClass))
                        Else
                            ViewBag.StatusMessage = strError
                        End If
                    Else
                        ViewBag.StatusMessage = strError
                    End If
                End If


                Return View(objSubjectsData)
            Else
                Return RedirectToAction("Index", "Home")
            End If
        End Function

        Function RemoveSubject() As ActionResult
            If Session("LoggedinAdminID") IsNot Nothing AndAlso IsNumeric(Session("LoggedinAdminID")) = True AndAlso Integer.Parse(Session("LoggedinAdminID")) > 0 Then
                Dim objSubjectsData As New SubjectsData
                objSubjectsData.LoadSubjectsList()
                Return View(objSubjectsData)
            Else
                Return RedirectToAction("Index", "Home")
            End If
        End Function

        <HttpPost()>
        Function RemoveSubject(ByVal objSubjectsData As SubjectsData) As ActionResult
            If Session("LoggedinAdminID") IsNot Nothing AndAlso IsNumeric(Session("LoggedinAdminID")) = True AndAlso Integer.Parse(Session("LoggedinAdminID")) > 0 Then
                objSubjectsData.LoadSubjectsList() 'Should fill each time like this
                Dim strSubjectname As String = GetSubjectName(objSubjectsData.SelectedSubject)
                'If ModelState.IsValid Then
                'TO DO - COME BACK HERE
                If Integer.Parse(objSubjectsData.SelectedSubject) > 0 Then
                    Dim strerror As String = ""
                    If objSubjectsData.RemoveSubjectFromSystem(strerror) Then
                        ViewBag.StatusMessage = String.Format("Selected SUBJECT is REMOVED '{0}'  from SYSTEM", strSubjectname)
                    Else
                        ViewBag.StatusMessage = strerror
                    End If
                Else

                End If
                Return View(objSubjectsData)
            Else
                Return RedirectToAction("Index", "Home")
            End If
        End Function

        Function EditSubject() As ActionResult
            If Session("LoggedinAdminID") IsNot Nothing AndAlso IsNumeric(Session("LoggedinAdminID")) = True AndAlso Integer.Parse(Session("LoggedinAdminID")) > 0 Then
                Dim objSubjectsData As New SubjectsData
                objSubjectsData.LoadSubjectsList()
                objSubjectsData.LoadClassesTobeTaughtInList()
                Return View(objSubjectsData)
            Else
                ViewBag.LoginPageHeader = "The admin area of the site needs you to have admin priveleges.  Please log in as admin"
                Session("For") = "ADMINISTRATORS"
                TempData("LoginPageFor") = Session("For")
                Return RedirectToAction("Index", "Login")
            End If
        End Function

        <HttpPost()>
        Function EditSubject(ByVal objSubjectsData As SubjectsData) As ActionResult
            If Session("LoggedinAdminID") IsNot Nothing AndAlso IsNumeric(Session("LoggedinAdminID")) = True AndAlso Integer.Parse(Session("LoggedinAdminID")) > 0 Then
                objSubjectsData.LoadSubjectsList()
                objSubjectsData.LoadClassesTobeTaughtInList()
                Dim strError As String = ""
                'objSubjectsData.LoadSubjectsList() 'Should fill each time like this
                objSubjectsData.SelectedClass = objSubjectsData.SelectedClass
                objSubjectsData.Subjectname = GetSubjectName(objSubjectsData.SelectedSubject)
                'TO DO - Model is returning false even if filled everything...find out why
                'If ModelState.IsValid Then
                ' If CheckInputs(objClassData, strError) Then
                'Dim strSelClass As String = GetClassID(objClassData.Classname)
                If objSubjectsData.UpdateSubjectINSystem(strError) Then
                    ViewBag.StatusMessage = String.Format("Successfully UPDATED '{0}' class in SYSTEM", objSubjectsData.Subjectname)
                Else
                    ViewBag.StatusMessage = strError
                End If
                ' Else
                'ViewBag.StatusMessage = strError
                'End If
                ' End If
                Return View(objSubjectsData)
            Else
                Return RedirectToAction("Index", "Home")
            End If
        End Function

        Function ViewSubjects() As ActionResult
            If Session("LoggedinAdminID") IsNot Nothing AndAlso IsNumeric(Session("LoggedinAdminID")) = True AndAlso Integer.Parse(Session("LoggedinAdminID")) > 0 Then
                Dim objDT As DataTable
                Dim objSubjectsData As New SubjectsData
                objDT = objSubjectsData.GetSubjectsDT
                If objDT.Rows.Count = 0 Then
                    ViewBag.Error = String.Format("{0}'s list of classes is currently empty", GetTeacherName(Session("LoggedinTeacherID")))
                Else
                    ViewBag.Data = objDT
                End If
                Return View(objSubjectsData)
            Else
                Return RedirectToAction("Index", "Home")
            End If

        End Function
#End Region

#Region "Students"
        Function AddStudent() As ActionResult
            If Session("LoggedinAdminID") IsNot Nothing AndAlso IsNumeric(Session("LoggedinAdminID")) = True AndAlso Integer.Parse(Session("LoggedinAdminID")) > 0 Then
                Session("AcctType") = "Student"
                Return RedirectToAction("CreateAccounts", "Home")
            Else
                ViewBag.LoginPageHeader = "The admin area of the site needs you to have admin priveleges.  Please log in as admin"
                Session("For") = "ADMINISTRATORS"
                TempData("LoginPageFor") = Session("For")
                Return RedirectToAction("Index", "Login")
            End If
        End Function

        Function RemoveStudent() As ActionResult
            If Session("LoggedinAdminID") IsNot Nothing AndAlso IsNumeric(Session("LoggedinAdminID")) = True AndAlso Integer.Parse(Session("LoggedinAdminID")) > 0 Then
                Dim objStudentData As New StudentData
                objStudentData.LoadStudentsList()
                Return View(objStudentData)
            Else
                Return RedirectToAction("Index", "Home")
            End If
        End Function

        <HttpPost()>
        Function RemoveStudent(ByVal objStudentData As StudentData) As ActionResult
            If Session("LoggedinAdminID") IsNot Nothing AndAlso IsNumeric(Session("LoggedinAdminID")) = True AndAlso Integer.Parse(Session("LoggedinAdminID")) > 0 Then
                objStudentData.LoadStudentsList() 'Should fill each time like this
                Dim strStudentName As String = GetStudentName(objStudentData.SelectedStudent)
                'If ModelState.IsValid Then
                'TO DO - COME BACK HERE
                If Integer.Parse(objStudentData.SelectedStudent) > 0 Then
                    Dim strerror As String = ""
                    If objStudentData.RemoveStudentFromSystem(strerror) Then
                        ViewBag.StatusMessage = String.Format("Selected SUBJECT is REMOVED '{0}'  from SYSTEM", strStudentName)
                    Else
                        ViewBag.StatusMessage = strerror
                    End If
                Else

                End If
                Return View(objStudentData)
            Else
                Return RedirectToAction("Index", "Home")
            End If
        End Function

        Function ViewStudents() As ActionResult
            If Session("LoggedinAdminID") IsNot Nothing AndAlso IsNumeric(Session("LoggedinAdminID")) = True AndAlso Integer.Parse(Session("LoggedinAdminID")) > 0 Then
                Dim objDT As DataTable
                Dim objSubjectsData As New StudentData
                objDT = objSubjectsData.GetStudentsDT
                If objDT.Rows.Count = 0 Then
                    ViewBag.Error = "The list of students is empty"
                Else
                    ViewBag.Data = objDT
                End If
                Return View(objSubjectsData)
            Else
                Return RedirectToAction("Index", "Home")
            End If
        End Function
#End Region

#Region "Teacher"
        Function AddTeacher() As ActionResult
            If Session("LoggedinAdminID") IsNot Nothing AndAlso IsNumeric(Session("LoggedinAdminID")) = True AndAlso Integer.Parse(Session("LoggedinAdminID")) > 0 Then
                Session("AcctType") = "teacher"
                Return RedirectToAction("CreateAccounts", "Home")
            Else
                ViewBag.LoginPageHeader = "The admin area of the site needs you to have admin priveleges.  Please log in as admin"
                Session("For") = "ADMINISTRATORS"
                TempData("LoginPageFor") = Session("For")
                Return RedirectToAction("Index", "Login")
            End If
        End Function

        Function RemoveTeacher() As ActionResult
            If Session("LoggedinAdminID") IsNot Nothing AndAlso IsNumeric(Session("LoggedinAdminID")) = True AndAlso Integer.Parse(Session("LoggedinAdminID")) > 0 Then
                Dim objTeacherData As New TeacherData
                objTeacherData.LoadTeachersList()
                Return View(objTeacherData)
            Else
                Return RedirectToAction("Index", "Home")
            End If
        End Function

        <HttpPost()>
        Function RemoveTeacher(ByVal objTeacherData As TeacherData) As ActionResult
            If Session("LoggedinAdminID") IsNot Nothing AndAlso IsNumeric(Session("LoggedinAdminID")) = True AndAlso Integer.Parse(Session("LoggedinAdminID")) > 0 Then
                objTeacherData.LoadTeachersList() 'Should fill each time like this
                Dim strTeachername As String = GetTeacherName(objTeacherData.SelectedTeacher)
                'If ModelState.IsValid Then
                'TO DO - COME BACK HERE
                If Integer.Parse(objTeacherData.SelectedTeacher) > 0 Then
                    Dim strerror As String = ""
                    If objTeacherData.RemoveTeacherFromSystem(strerror) Then
                        ViewBag.StatusMessage = String.Format("Selected TEACHER is REMOVED '{0}'  from SYSTEM", strTeachername)
                    Else
                        ViewBag.StatusMessage = strerror
                    End If
                Else

                End If
                Return View(objTeacherData)
            Else
                Return RedirectToAction("Index", "Home")
            End If
        End Function

        Function ViewTeachers() As ActionResult
            If Session("LoggedinAdminID") IsNot Nothing AndAlso IsNumeric(Session("LoggedinAdminID")) = True AndAlso Integer.Parse(Session("LoggedinAdminID")) > 0 Then
                Dim objDT As DataTable
                Dim objSubjectsData As New TeacherData
                objDT = objSubjectsData.GetTeacherDT
                If objDT.Rows.Count = 0 Then
                    ViewBag.Error = "The list of teachers is empty"
                Else
                    ViewBag.Data = objDT
                End If
                Return View(objSubjectsData)
            Else
                Return RedirectToAction("Index", "Home")
            End If
        End Function
#End Region

        Private Function GetClassname(ByVal iClassID As Integer) As String
            Dim objClassData As New ClassData With {.ClassID = iClassID}
            objClassData.LoadCurrentClassInfo()
            Return objClassData.Classname
        End Function

        Public Function GetAssocClass(ByVal iSubjectID As Integer) As ActionResult
            Dim objStudentData As New SubjectsData
            objStudentData.SubjectID = iSubjectID
            objStudentData.LoadCurrentSubjectInfo()
            'Return Json(String.Format("Returning from FirstAJAX action {0}", param), JsonRequestBehavior.AllowGet)
            Return Json(New With {.classID = objStudentData.ClassID}, JsonRequestBehavior.AllowGet)
        End Function
    End Class
End Namespace