
Imports System.Web.UI
Imports DotNetNuke.Entities.Modules
Imports System.Linq

Namespace DotNetNuke.Modules.ForgeDownloadManager

    Partial Class Settings
        Inherits Entities.Modules.ModuleSettingsBase

        Private db As New DNNCorp.ForgeDownloadManager.ForgeLinksDataContext()


#Region "Base Method Implementations"

        Public Overrides Sub LoadSettings()
            Try
                If (Page.IsPostBack = False) Then

                    Dim linkSettings = (From ls In db.ForgeLinkSettings _
                                                           Where ls.ModuleId = Me.ModuleId _
                                                           Select ls).SingleOrDefault

                    If linkSettings IsNot Nothing Then
                        txtTemplate.Text = linkSettings.Template
                        txtAltTemplate.Text = linkSettings.AltTemplate
                        txtProjectname.Text = linkSettings.ProjectName
                        txtReleaseId.Text = linkSettings.ReleaseId.ToString
                    End If
                End If
            Catch exc As Exception           'Module failed to load
                ProcessModuleLoadException(Me, exc)
            End Try
        End Sub

        Public Overrides Sub UpdateSettings()
            Try
                Dim IsNew As Boolean = False

                Dim linkSettings = (From ls In db.ForgeLinkSettings _
                                       Where ls.ModuleId = Me.ModuleId _
                                       Select ls).SingleOrDefault

                If linkSettings Is Nothing Then
                    linkSettings = New DNNCorp.ForgeDownloadManager.ForgeLinkSetting
                    IsNew = True
                End If

                linkSettings.ModuleId = Me.ModuleId
                linkSettings.Template = txtTemplate.Text
                linkSettings.AltTemplate = txtAltTemplate.Text
                linkSettings.ProjectName = txtProjectname.Text
                linkSettings.ReleaseId = CInt(txtReleaseId.Text)

                If IsNew Then
                    db.ForgeLinkSettings.InsertOnSubmit(linkSettings)
                End If

                db.SubmitChanges()

                ' refresh cache
                ModuleController.SynchronizeModule(Me.ModuleId)
            Catch exc As Exception           'Module failed to load
                ProcessModuleLoadException(Me, exc)
            End Try
        End Sub

#End Region

        Protected Sub linqLinks_Selecting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.LinqDataSourceSelectEventArgs) Handles linqLinks.Selecting
            e.WhereParameters("ModuleId") = Me.ModuleId
        End Sub

        Protected Sub cmdEditLinks_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdEditLinks.Click
            Response.Redirect(EditUrl("EditLinks"))
        End Sub
    End Class

End Namespace

