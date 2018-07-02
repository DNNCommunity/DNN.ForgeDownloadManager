Imports System.Linq

Namespace DotNetNuke.Modules.ForgeDownloadManager

    Partial Class ViewForgeDownloads
        Inherits Entities.Modules.PortalModuleBase

#Region "Private Members"
        Private db As New DNNCorp.ForgeDownloadManager.ForgeLinksDataContext()
#End Region

#Region "Event Handlers"

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            Try
                If Not Page.IsPostBack Then
                    Dim linkSettings = (From ls In db.ForgeLinkSettings _
                                        Where ls.ModuleId = Me.ModuleId _
                                        Select ls).SingleOrDefault

                    If linkSettings IsNot Nothing Then

                        Try
                            Dim release As CodePlexRelease = CodePlexFeed.GetReleaseData(linkSettings.ProjectName, linkSettings.ReleaseId, ModuleId, TabId)

                            release.LinkList = From ls In db.ForgeLinks _
                                               Where ls.ModuleId = Me.ModuleId _
                                               And ls.Active = True _
                                               Select ls

                            If release.IsValid Then
                                Dim tr As New ReleaseTokenReplace(Me.ModuleId, release)
                                Me.Controls.Add(New LiteralControl(tr.ReplaceEnvironmentTokens(linkSettings.Template)))
                            Else
                                Dim t As String = Localization.GetString("Template", Me.LocalResourceFile)
                                Me.Controls.Add(New LiteralControl(t))
                            End If
                        Catch webex As System.Net.WebException
                            Me.Controls.Add(New LiteralControl(linkSettings.AltTemplate))
                            LogException(webex)
                        End Try
                    End If
                End If
            Catch exc As Exception        'Module failed to load
                ProcessModuleLoadException(Me, exc)
            End Try
        End Sub

#End Region

    End Class

End Namespace

