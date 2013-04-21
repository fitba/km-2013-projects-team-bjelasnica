<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="FITKMS.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="dashboard-wrapper">
        <div class="main-container">
            <div class="row-fluid">
                <div class="span12" id="error_label" runat="server" visible="false">
                    <div class="widget-body">

                        <div class="alert alert-block alert-error fade in">
                            <button data-dismiss="alert" class="close" type="button">
                                ×
                            </button>
                            <h4 class="alert-heading">Greška!
                            </h4>
                            <p>
                                <asp:Label ID="errorLabel" runat="server" Text="Label"></asp:Label>

                            </p>
                        </div>

                    </div>
                </div>
            </div>
            <div class="row-fluid">
                <div class="span12">
                    <div class="widget">
                        <div class="widget-header">
                            <div class="title">
                                <span class="fs1" aria-hidden="true" data-icon="&#xe088;"></span>Login
                            </div>
                        </div>
                        <div class="widget-body">
                            <div class="row-fluid">
                                <div class="span4 offset4">
                                    <div class="signup">
                                        <div class="signup-wrapper">
                                            <div class="header">
                                                <h2>Login</h2>
                                            </div>
                                            <div class="content">
                                                <asp:TextBox ID="usernameInput" class="input input-block-level" placeholder="Username" required="required" runat="server"></asp:TextBox>
                                                <asp:TextBox ID="passwordInput" class="input input-block-level" placeholder="Password" required="required" type="password" runat="server"></asp:TextBox>
                                               
                                            </div>
                                            <div class="actions">
                                                <asp:Button ID="loginSubmit" class="btn btn-info btn-large pull-right" runat="server" Text="Login" OnClick="loginSubmit_Click" />
                                                <div class="clearfix"></div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
