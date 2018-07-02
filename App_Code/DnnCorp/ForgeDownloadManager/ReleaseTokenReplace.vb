Imports Microsoft.VisualBasic

Namespace DotNetNuke.Modules.ForgeDownloadManager
    Public Class ReleaseTokenReplace
        Inherits Tokens.TokenReplace


        Public Sub New(ByVal ModuleId As Integer, ByVal release As CodePlexRelease)
            MyBase.New(ModuleId)
            PropertySource("project") = release
        End Sub
    End Class
End Namespace

