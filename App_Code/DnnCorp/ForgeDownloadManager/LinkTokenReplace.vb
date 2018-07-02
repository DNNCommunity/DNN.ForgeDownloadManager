Imports Microsoft.VisualBasic

Namespace DotNetNuke.Modules.ForgeDownloadManager
    Public Class LinkTokenReplace
        Inherits Tokens.TokenReplace


        Public Sub New(ByVal release As CodePlexRelease, ByVal link As DNNCorp.ForgeDownloadManager.ForgeLink)
            MyBase.New(release.ModuleId)
            PropertySource("link") = New LinkPropertyAccess(release, link)
        End Sub
    End Class
End Namespace

