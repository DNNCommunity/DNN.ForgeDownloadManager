<%@ Control Language="vb" AutoEventWireup="false" CodeFile="Settings.ascx.vb" Inherits="DotNetNuke.Modules.ForgeDownloadManager.Settings" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>
<%@ Register TagPrefix="dnn" TagName="SectionHead" Src="~/controls/SectionHeadControl.ascx" %>

<dnn:sectionhead id="dshProjectSettings" cssclass="Head" runat="server" text="Forge Project Settings" section="tblSettings" resourcekey="dshSettings" includerule="True" isexpanded="True" />
<table id="tblSettings" runat="server" cellspacing="0" cellpadding="2" border="0" summary="Forge Download Manager Settings Design Table">
    <tr>
        <td class="SubHead" width="200"><dnn:label id="plTemplate" runat="server" controlname="txtTemplate" suffix=":"></dnn:label></td>
        <td valign="bottom" >
            <asp:textbox id="txtTemplate" cssclass="NormalTextBox" width="350" columns="30" textmode="MultiLine" rows="10" maxlength="2000" runat="server" />
        </td>
    </tr>
    <tr>
        <td class="SubHead" width="200"><dnn:label id="plAltTemplate" runat="server" controlname="txtAltTemplate" suffix=":"></dnn:label></td>
        <td valign="bottom" >
            <asp:textbox id="txtAltTemplate" cssclass="NormalTextBox" width="350" columns="30" textmode="MultiLine" rows="10" maxlength="2000" runat="server" />
        </td>
    </tr>
    <tr>
        <td class="SubHead" width="200"><dnn:label id="plProjectName" runat="server" controlname="txtProjectname" suffix=":"></dnn:label></td>
        <td valign="bottom" >
            <asp:textbox id="txtProjectname" cssclass="NormalTextBox" width="350" columns="30" maxlength="50" runat="server" />
        </td>
    </tr>
    <tr>
        <td class="SubHead" width="200"><dnn:label id="plReleaseId" runat="server" controlname="txtReleaseId" suffix=":"></dnn:label></td>
        <td valign="bottom" >
            <asp:textbox id="txtReleaseId" cssclass="NormalTextBox" width="350" columns="30" maxlength="7" runat="server" />
        </td>
    </tr>
</table>

<asp:LinqDataSource ID="linqLinks" runat="server" 
    ContextTypeName="DNNCorp.ForgeDownloadManager.ForgeLinksDataContext" 
    Select="new (Label, FileId, Active, ForgeLinkTemplate, ForgeLinkId, ModuleId)" 
    TableName="ForgeLinks" Where="ModuleId == @ModuleId">
    <WhereParameters>
        <asp:Parameter Name="ModuleId" Type="Int32" />
    </WhereParameters>
</asp:LinqDataSource>

<dnn:sectionhead id="dshLinks" cssclass="Head SectionHead" runat="server" text="Forge Links" section="LinkPanel" resourcekey="dshLinks" includerule="True" isexpanded="True" />
<div id="LinkPanel" runat="server">
    <asp:GridView ID="grdLinks" runat="server" AllowPaging="True" 
        AutoGenerateColumns="False" DataKeyNames="ForgeLinkId" 
        DataSourceID="linqLinks" GridLines="None" cssclass="DataGrid_Container" 
        HeaderStyle-CssClass="NormalBold" RowStyle-CssClass="Normal" AlternatingRowStyle-CssClass="Normal">
        <Columns>
            <asp:BoundField DataField="Label" HeaderText="Label" ReadOnly="True" SortExpression="Label" />
            <asp:BoundField DataField="FileId" HeaderText="FileId" ReadOnly="True" SortExpression="FileId" />
            <asp:CheckBoxField DataField="Active" HeaderText="Active" ReadOnly="True" SortExpression="Active" />
            <asp:BoundField DataField="ForgeLinkId" HeaderText="ForgeLinkId" ReadOnly="True" SortExpression="ForgeLinkId" Visible="False" />
        </Columns>
        <EmptyDataTemplate>
            <span><%=DotNetNuke.Services.Localization.Localization.GetString("NoLinks", Me.LocalResourceFile)%></span>
        </EmptyDataTemplate>
        <HeaderStyle CssClass="NormalBold GridHead" />
        <RowStyle CssClass="Normal GridRow" />
        <AlternatingRowStyle CssClass="Normal GridRow" />
    </asp:GridView>
    <div class="ButtonPanel">
        <asp:LinkButton ID="cmdEditLinks" runat="server" class="CommandButton" resourcekey="cmdEditLinks" borderstyle="none" >Edit Links</asp:LinkButton>
    </div>
</div>