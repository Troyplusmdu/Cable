<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MobileLogin.aspx.cs" Inherits="MobileLogin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Mobile Login</title>
    <meta http-equiv = "X-UA-Compatible" content = "IE=EmulateIE7" />
    <link href="App_Themes/DefaultTheme/DefaultTheme.css" rel="stylesheet" type="text/css" />
    <link href="App_Themes/NewTheme/base.css" rel="stylesheet" type="text/css" />
    <link href="App_Themes/DefaultTheme/ComboBox.css" rel="Stylesheet" type="text/css" />        
</head>
<body>
    <form id="form1" runat="server">
    <br />
    <br />
    <br />
<div style="font-size:small; text-align:left">
					<table cellpadding="4" cellspacing="0" border="0" width="90%" align="center" class="brdClrLogin">
						<tr valign="middle" class="LoginHeader">
							<td width="100%" colspan="3" align="center">
							    Login
						    </td>
						</tr>
						<tr height="30px" valign="center">
							<td align="right" width="25%">
								User Name
                                <asp:RequiredFieldValidator ID="reqtxtLogin" runat="server" ControlToValidate="txtLogin"
                                    Display="Dynamic" ErrorMessage="UserName Required" SetFocusOnError="true" CssClass="errorMsg">*</asp:RequiredFieldValidator>								
							</td>
							<td width="3%">
								&nbsp;
							</td>
							<td width="72%" align="left">
                                <asp:TextBox ID="txtLogin" runat="server" TabIndex="1" CssClass="cssTextBox" Font-Size="Medium" Height="22px" MaxLength="50" Width="99%"></asp:TextBox>
							</td>
						</tr>
						<tr height="30px" valign="center">
							<td align="right" width="25%">
								Password <asp:RequiredFieldValidator ID="reqtxtPassword" runat="server" ControlToValidate="txtPassword"
                                    Display="Dynamic" ErrorMessage="Password Required" SetFocusOnError="true" CssClass="errorMsg">*</asp:RequiredFieldValidator>
							</td>
							<td width="3%">
								&nbsp;
							</td>
							<td width="72%" align="left">
                                <asp:TextBox ID="txtPassword" runat="server" TabIndex="2" CssClass="cssTextBox" Font-Size="Medium" Height="22px" Width="99%" TextMode="Password" MaxLength="15"></asp:TextBox>								
							</td>
						</tr>
						<tr height="30px" valign="center">
							<td align="right" width="25%">
								Company 
                                <asp:RequiredFieldValidator ID="rvCompany" runat="server" ControlToValidate="txtCompany"
                                    Display="Dynamic" ErrorMessage="Company Code Required" SetFocusOnError="true" CssClass="errorMsg">*</asp:RequiredFieldValidator>
							</td>
							<td width="3%">
								&nbsp;
							</td>
							<td width="72%" align="left">
                                <asp:TextBox ID="txtCompany" runat="server" TabIndex="3" Width="96%" CssClass="txtBox upper" MaxLength="50"></asp:TextBox>								
							</td>
						</tr>
						<tr>
							<td align="right" width="25%">
							</td>
							<td width="3%">
							</td>
							<td width="72%" align="left">
								<table cellspacing="2px" cellpadding="0px" border="0">
									<tr valign="middle">
										<td>
                                            <asp:CheckBox ID="chkRemember" runat="server" TabIndex="4" style="margin-left:-3px;"/>
										</td>
										<td>
											&nbsp;&nbsp;
										</td>
										<td>
											Remember Me
										</td>
									</tr>
								</table>
							</td>
						</tr>
						<tr>
							<td align="right" width="25%">
							</td>
							<td width="3%">
							</td>
							<td width="72%" align="left">								
								<asp:ImageButton ID="btnLogin" runat="server" TabIndex="5" OnClick="btnLogin_Click" SkinID="BtnLogin" Text="Login"/>							
								<asp:ImageButton ID="ImageButton1" runat="server" TabIndex="7" CausesValidation="false"  ImageUrl="~/App_Themes/DefaultTheme/Images/imgClear.png" OnClientClick="resetLogin();"/>
								<br />
							</td>
						</tr>
						<tr>
							<td colspan="3" style="height:5px">
							    <asp:Label ID="lblErrorMsg" runat="server" SkinID="skinErrorMsg" Visible="true" meta:resourcekey="lblErrorMsg"></asp:Label>
							</td>
                        </tr>						
					</table>  
				</div>
    </form>
</body>
</html>
