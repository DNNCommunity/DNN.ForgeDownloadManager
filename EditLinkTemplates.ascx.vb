
Imports DotNetNuke

Namespace DotNetNuke.Modules.ForgeDownloadManager

    Partial Class EditLinkTemplates
        Inherits Entities.Modules.PortalModuleBase

#Region "Private Members"


#End Region

#Region "Event Handlers"

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            Try

                ' If this is the first visit to the page, bind the role data to the datalist
                If Page.IsPostBack = False Then
                    If Not Me.IsEditable Then
                        Response.Redirect(AccessDeniedURL(), True)
                    End If
                End If

            Catch exc As Exception    'Module failed to load
                ProcessModuleLoadException(Me, exc)
            End Try
        End Sub

#End Region

        Protected Sub linqTemplates_Inserted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.LinqDataSourceStatusEventArgs) Handles linqTemplates.Inserted
            Me.grdLinks.DataBind()
        End Sub

        Protected Sub linqTemplates_Inserting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.LinqDataSourceInsertEventArgs) Handles linqTemplates.Inserting
            Dim template = CType(e.NewObject, DNNCorp.ForgeDownloadManager.ForgeLinkTemplate)
            template.ModuleId = Me.ModuleId
        End Sub

        Protected Sub linqTemplates_Selecting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.LinqDataSourceSelectEventArgs) Handles linqTemplates.Selecting
            e.WhereParameters("ModuleId") = Me.ModuleId
        End Sub

        Protected Sub linkAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles linkAdd.Click
            DisplayFormView(True)
            grdLinks.DataBind()
        End Sub

        Protected Sub InsertButton_Click(ByVal sender As Object, ByVal e As System.EventArgs)
            DisplayFormView(False)
            grdLinks.DataBind()
        End Sub

        Protected Sub InsertCancelButton_Click(ByVal sender As Object, ByVal e As System.EventArgs)
            DisplayFormView(False)
            grdLinks.DataBind()
        End Sub

        Private Sub DisplayFormView(ByVal display As Boolean)
            frmLink.Visible = display
            grdLinks.Visible = Not display
        End Sub

        Protected Sub linkReturn_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles linkReturn.Click
            Response.Redirect(EditUrl("EditLinks"))
        End Sub
    End Class

End Namespace