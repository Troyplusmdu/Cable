<%@ Page Language="C#" MasterPageFile="~/SimplePage.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" Title="Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhTab" Runat="Server" Visible="false">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplhControlPanel" Runat="Server">
    	<div class="loginDiv">														<!-- Login Div -->	
			<div class="loginInner">
				<div class="loginHd">
					<h2>Login</h2>
				</div>
				<div class="loginCnt">
					<table width="100%" cellspacing="0" cellpadding="0" border="0" style="margin-top:-15px;">
						<tr height="30px" valign="center">
							<td align="right" width="24%">
								User Name
                                <asp:RequiredFieldValidator ID="reqtxtLogin" runat="server" ControlToValidate="txtLogin"
                                    Display="Dynamic" ErrorMessage="UserName Required" SetFocusOnError="true" CssClass="errorMsg">*</asp:RequiredFieldValidator>								
							</td>
							<td width="8%">
								&nbsp;
							</td>
							<td width="68%" align="left">
                                <asp:TextBox ID="txtLogin" runat="server" TabIndex="1" CssClass="loginText" MaxLength="50"></asp:TextBox>
							</td>
						</tr>
						<tr height="30px" valign="center">
							<td align="right" width="24%">
								Password <asp:RequiredFieldValidator ID="reqtxtPassword" runat="server" ControlToValidate="txtPassword"
                                    Display="Dynamic" ErrorMessage="Password Required" SetFocusOnError="true" CssClass="errorMsg">*</asp:RequiredFieldValidator>
							</td>
							<td width="8%">
								&nbsp;
							</td>
							<td width="68%" align="left">
                                <asp:TextBox ID="txtPassword" runat="server" TabIndex="2" CssClass="loginText" TextMode="Password" MaxLength="15"></asp:TextBox>								
							</td>
						</tr>
						<tr height="30px" valign="center">
							<td align="right" width="24%">
								Company Code<asp:RequiredFieldValidator ID="rvCompany" runat="server" ControlToValidate="txtCompany"
                                    Display="Dynamic" ErrorMessage="Company Code Required" SetFocusOnError="true" CssClass="errorMsg">*</asp:RequiredFieldValidator>
							</td>
							<td width="8%">
								&nbsp;
							</td>
							<td width="68%" align="left">
                                <asp:TextBox ID="txtCompany" runat="server" TabIndex="3" CssClass="loginText upper" MaxLength="50"></asp:TextBox>								
							</td>
						</tr>
						<tr>
							<td align="right" width="24%">
							</td>
							<td width="8%">
							</td>
							<td width="68%" align="left">
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
							<td colspan="3" align="center">
                                <asp:Button ID="btnLogin" runat="server" TabIndex="5" EnableTheming="false" OnClick="btnLogin_Click" CssClass="loginBtn" Text="Login"/>							
								<asp:Button ID="BtnReset" CssClass="loginBtn" EnableTheming="false" CausesValidation="false" runat="server" Text="Reset" OnClientClick="resetLogin();"/>	
							</td>
						</tr>
					</table>  
				</div>
			</div>
		</div>	              
</asp:Content>

