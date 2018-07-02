Imports Microsoft.VisualBasic
Imports DotNetNuke.Services.Tokens
Imports DotNetNuke.Modules.ForgeDownloadManager

Public Class LinkPropertyAccess
    Implements IPropertyAccess

    Dim _link As DNNCorp.ForgeDownloadManager.ForgeLink
    Dim _release As CodePlexRelease

    Public ReadOnly Property Cacheability() As DotNetNuke.Services.Tokens.CacheLevel Implements DotNetNuke.Services.Tokens.IPropertyAccess.Cacheability
        Get
            Return CacheLevel.secureforCaching
        End Get
    End Property

    Public Function GetProperty(ByVal strPropertyName As String, ByVal strFormat As String, ByVal formatProvider As System.Globalization.CultureInfo, ByVal AccessingUser As DotNetNuke.Entities.Users.UserInfo, ByVal AccessLevel As DotNetNuke.Services.Tokens.Scope, ByRef PropertyNotFound As Boolean) As String Implements DotNetNuke.Services.Tokens.IPropertyAccess.GetProperty
        Dim OutputFormat As String = String.Empty
        If strFormat = String.Empty Then OutputFormat = "g"

        Select Case strPropertyName.ToLower
            Case "label"
                Return (PropertyAccess.FormatString(_link.Label, strFormat))
            Case "url"
                Dim PortalId As Integer = DotNetNuke.Common.Globals.GetPortalSettings().PortalId
                Dim url As String = GetRawUrl()
                Dim objUrls As New UrlController
                If objUrls.GetUrl(PortalId, url) Is Nothing Then
                    objUrls.UpdateUrl(PortalId, url, "U", True, True, _release.ModuleId, True)
                End If

                Return DotNetNuke.Common.Globals.LinkClick(GetRawUrl(), _release.TabId, _release.ModuleId)
            Case "rawurl"
                Return GetRawUrl()
        End Select
        Return String.Empty
    End Function

    Private Function GetRawUrl() As String
        Return String.Format(ForgeConstants.LINKFORMAT, _release.RawLink, _link.FileId)
    End Function

    Public Sub New(ByVal release As CodePlexRelease, ByVal link As DNNCorp.ForgeDownloadManager.ForgeLink)
        _link = link
        _release = release
    End Sub
End Class
