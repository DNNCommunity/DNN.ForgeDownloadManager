Imports System.Xml
Imports System.Xml.XPath

Namespace DotNetNuke.Modules.ForgeDownloadManager
    Public Class CodePlexFeed
        Public Shared Function GetReleaseFeed(ByVal ProjectName As String) As XmlDocument
            Dim url As String = String.Format("http://{0}.codeplex.com/Project/ProjectRss.aspx?ProjectRSSFeed=codeplex%3a%2f%2frelease%2f{0}", ProjectName)
            Dim rss As New DotNetNuke.Services.Syndication.RssDataSource()
            rss.Url = url
            Return rss.Channel.SaveAsXml()
        End Function

        Public Shared Function GetReleaseData(ByVal ProjectName As String, ByVal ReleaseId As Integer, ByVal moduleid As Integer, ByVal tabid As Integer) As CodePlexRelease
            Dim data As New CodePlexRelease With {.ProjectName = ProjectName, .ReleaseId = ReleaseId, .ModuleId = moduleid, .TabId = tabid}

            Dim doc As XmlDocument = GetReleaseFeed(ProjectName)
            Dim nav As XPathNavigator = doc.CreateNavigator

            Dim query As String = String.Format("//item[contains(link, '{0}')]", ReleaseId)
            Dim item As XPathNavigator = nav.SelectSingleNode(query)

            If item IsNot Nothing Then
                Dim desc As XPathNavigator = item.SelectSingleNode("./description")
                If desc IsNot Nothing Then
                    data.Description = HttpUtility.HtmlDecode(desc.Value)
                End If

                Dim title As XPathNavigator = item.SelectSingleNode("./title")
                If title IsNot Nothing Then
                    data.Title = HttpUtility.HtmlDecode(title.Value)
                End If

                Dim link As XPathNavigator = item.SelectSingleNode("./link")
                If link IsNot Nothing Then
                    data.RawLink = HttpUtility.HtmlDecode(link.Value)
                End If

                Dim releaseDate As XPathNavigator = item.SelectSingleNode("./pubDate")
                If releaseDate IsNot Nothing Then
                    DateTime.TryParse(releaseDate.Value, data.ReleaseDate)
                End If
            End If

            Return data
        End Function
    End Class
End Namespace

