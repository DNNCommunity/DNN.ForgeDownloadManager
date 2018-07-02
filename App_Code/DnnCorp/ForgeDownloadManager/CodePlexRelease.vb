Imports Microsoft.VisualBasic
Imports DotNetNuke.Services.Tokens
Imports System.Collections.Generic

Namespace DotNetNuke.Modules.ForgeDownloadManager

    Public Class CodePlexRelease
        Implements IPropertyAccess

#Region "Private Members"
        Private _description As String
        Private _linkList As IEnumerable(Of DNNCorp.ForgeDownloadManager.ForgeLink)
        Private _moduleId As Integer
        Private _projectName As String
        Private _rawLink As String
        Private _releaseDate As Date
        Private _releaseId As Integer
        Private _tabId As Integer
        Private _title As String
        Private _version As String
#End Region

#Region "Properties"
        Public Property Description() As String
            Get
                Return _description
            End Get
            Set(ByVal Value As String)
                _description = Value
            End Set
        End Property

        Public Property RawLink() As String
            Get
                Return _rawLink
            End Get
            Set(ByVal Value As String)
                _rawLink = Value
            End Set
        End Property

        Public ReadOnly Property Link() As String
            Get
        'Dim objUrls As New UrlController
        'Dim PortalId As Integer = DotNetNuke.Common.Globals.GetPortalSettings().PortalId
        'If objUrls.GetUrl(PortalId, _rawLink) Is Nothing Then
        '    objUrls.UpdateUrl(PortalId, _rawLink, "U", True, True, ModuleId, True)
        'End If

        'Return Common.Globals.LinkClick(_rawLink, TabId, ModuleId)
        Return RawLink()
            End Get
        End Property

        Public Property ModuleId() As Integer
            Get
                Return _moduleId
            End Get
            Set(ByVal Value As Integer)
                _moduleId = Value
            End Set
        End Property

        Public Property TabId() As Integer
            Get
                Return _tabId
            End Get
            Set(ByVal Value As Integer)
                _tabId = Value
            End Set
        End Property

        Public Property ProjectName() As String
            Get
                Return _projectName
            End Get
            Set(ByVal Value As String)
                _projectName = Value
            End Set
        End Property

        Public ReadOnly Property PublishDate() As Date
            Get
                Dim _releasedate As Date = Date.MinValue
                If _title IsNot Nothing Then
                    Dim reg As New Regex("(?<=\().*(?=\))")
                    Dim match As Match = reg.Match(_title)
                    If match.Success Then
                        Date.TryParse(match.Value, _releasedate)
                    End If
                End If
                Return _releasedate
            End Get
        End Property

        Public Property  ReleaseDate() As Date
            Get
                Return _releaseDate
            End Get
            Set(ByVal Value As Date)
                _releaseDate = Value
            End Set
        End Property


        Public Property ReleaseId() As Integer
            Get
                Return _releaseId
            End Get
            Set(ByVal Value As Integer)
                _releaseId = Value
            End Set
        End Property

        Public Property Title() As String
            Get
                Return _title
            End Get
            Set(ByVal Value As String)
                _title = Value
            End Set
        End Property

        Public ReadOnly Property Version() As String
            Get
                Dim _version As String = String.Empty
                If _title IsNot Nothing Then
                    Dim reg As New Regex("(?<=\: ).*(?= \()")
                    Dim match As Match = reg.Match(_title)
                    If match.Success Then
                        _version = match.Value
                    End If
                End If
                Return _version
            End Get
        End Property

        Public ReadOnly Property IsValid() As Boolean
            Get
                Return Not String.IsNullOrEmpty(Link)
            End Get
        End Property

        Public Property LinkList() As IEnumerable(Of DNNCorp.ForgeDownloadManager.ForgeLink)
            Get
                Return _linkList
            End Get
            Set(ByVal Value As IEnumerable(Of DNNCorp.ForgeDownloadManager.ForgeLink))
                _linkList = Value
            End Set
        End Property

#End Region

        Public ReadOnly Property Cacheability() As Services.Tokens.CacheLevel Implements Services.Tokens.IPropertyAccess.Cacheability
            Get
                Return CacheLevel.secureforCaching
            End Get
        End Property

        Public Function GetProperty(ByVal strPropertyName As String, ByVal strFormat As String, ByVal formatProvider As System.Globalization.CultureInfo, ByVal AccessingUser As Entities.Users.UserInfo, ByVal AccessLevel As Services.Tokens.Scope, ByRef PropertyNotFound As Boolean) As String Implements Services.Tokens.IPropertyAccess.GetProperty
            Dim OutputFormat As String = String.Empty
            If strFormat = String.Empty Then OutputFormat = "g"

            Select Case strPropertyName.ToLower
                Case "description"
                    Return (PropertyAccess.FormatString(Me.Description, strFormat))
                Case "link"
                    Return (PropertyAccess.FormatString(Me.Link, strFormat))
                Case "projectname"
                    Return (PropertyAccess.FormatString(Me.ProjectName, strFormat))
                Case "releasedate"
                    Return (Me.ReleaseDate.ToString(OutputFormat, formatProvider))
                Case "releaseid"
                    Return (Me.ReleaseId.ToString(OutputFormat, formatProvider))
                Case "title"
                    Return (PropertyAccess.FormatString(Me.Title, strFormat))
                Case "version"
                    Return (PropertyAccess.FormatString(Me.Version, strFormat))
                Case "linklist"
                    Dim links As String = String.Empty
                    If LinkList IsNot Nothing Then
                        For Each releaseLink As DNNCorp.ForgeDownloadManager.ForgeLink In LinkList
                            Dim lr As New LinkTokenReplace(Me, releaseLink)
                            links += lr.ReplaceEnvironmentTokens(releaseLink.ForgeLinkTemplate.Template)
                        Next
                    End If
                    Return links
            End Select
            Return String.Empty
        End Function
    End Class
End Namespace

