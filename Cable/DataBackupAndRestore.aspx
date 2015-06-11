<%@ Page Language="C#" MasterPageFile="~/PageMaster.master" AutoEventWireup="true"
    CodeFile="DataBackupAndRestore.aspx.cs" Inherits="DataBackupAndRestore" Title="Data Backup and Restore" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajX" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhTab" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplhControlPanel" runat="Server">
    <h3>
        Backup & Restore</h3>
    <br />
    <div class="lblFont" style="border: solid 1 px black; background-color: aliceblue;font-weight: bold; color: Red;">
        Instruction : This screen is used to take Backup and Restore Data.this need to be
        used carefully need to be done by the approval of supervisor.
        <br />
        1. Close all the other session before backp - (Only particular user who is taking
        backup need to be in the session).
    </div>
    <br />
    <table cellpadding="2" cellspacing="2" width="100%" style="border: solid 1px black"
        border="0" class="accordionContent">
        <tr>
            <td colspan="3" class="accordionHeader">
                Backup
            </td>
        </tr>
        <tr>
            <td colspan="3" class="lblFont">
                <asp:Label ID="lblMsg" runat="server" Style="color: Red; font-weight: bold;"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="lblFont">
                Backup File Name
            </td>
            <td align="left" class="lblFont">
                <asp:TextBox ID="txtFilename" runat="server"></asp:TextBox>.MDB
            </td>
            <td class="lblFont">
                <i>Ex : Any Valid name : BackupApril2009</i>
            </td>
        </tr>
        <tr>
            <td class="lblFont">
                Backup File Path
            </td>
            <td align="left" class="lblFont">
                <asp:TextBox ID="txtFilePath" runat="server" Width="250px"></asp:TextBox>
            </td>
            <td class="lblFont">
                <i>Ex : "E:\databasebackup" - (use backward slash as separator)</i>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button ID="btnBackup" SkinID="skinButtonBig" runat="server" OnClick="btnBackup_Click"
                    Text="Backup" />
            </td>
        </tr>
    </table>
    <br />
    <table cellpadding="2" cellspacing="2" width="100%" style="border: solid 1px black"
        border="0" class="accordionContent">
        <tr>
            <td colspan="3" class="accordionHeader">
                Restore
            </td>
        </tr>
        <tr>
            <td colspan="3" class="lblFont">
                <asp:Label ID="lblMsg2" runat="server" Style="color: Red; font-weight: bold;"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="lblFont">
                Restore File Name
            </td>
            <td align="left" class="lblFont">
                <asp:TextBox ID="txtFilenameR" runat="server"></asp:TextBox>.MDB
            </td>
            <td class="lblFont">
                <i>Ex : Any Valid name : BackupApril2009</i>
            </td>
        </tr>
        <tr>
            <td class="lblFont">
                Restore File Path
            </td>
            <td align="left" class="lblFont">
                <asp:TextBox ID="txtFilePathR" runat="server" Width="250px"></asp:TextBox>
            </td>
            <td class="lblFont">
                <i>Ex : "E:\databasebackup" - (use backward slash as separator)</i>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <ajX:ConfirmButtonExtender ID="CnrfmDel" TargetControlID="btnRestore" ConfirmText="Attention: While Restoring Current Data Will be Lossed Are You Sure Want To Restore ?"
                    runat="server">
                </ajX:ConfirmButtonExtender>
                <asp:Button ID="btnRestore" SkinID="skinButtonBig" runat="server" OnClick="btnRestore_Click"
                    Text="Restore" />
            </td>
        </tr>
    </table>
</asp:Content>
