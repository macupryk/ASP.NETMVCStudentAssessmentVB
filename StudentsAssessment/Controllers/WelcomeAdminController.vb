Imports System.Web.Mvc

Namespace Controllers
    Public Class WelcomeAdminController
        Inherits Controller

        ' GET: WelcomeAdmin
        Function Index() As ActionResult
            If Session("LoggedInAdminID") IsNot Nothing Then
                Session("For") = "ADMINISTRATORS"
                'Dim objLoginData As New LoginData
                'objLoginData.IDNo = Integer.Parse(Session("LoggedInTeacherID"))
                'objLoginData.LoadTeacherInfo()
                If Session("LoginData") IsNot Nothing AndAlso TypeOf Session("LoginData") Is LoginData Then
                    Dim objLoginData As LoginData = CType(Session("LoginData"), LoginData)
                    Return View(objLoginData)
                Else
                    Return RedirectToAction("Index", "Home")
                End If
            Else
                Return RedirectToAction("Index", "Home")
            End If
            Return View()
        End Function
    End Class
End Namespace