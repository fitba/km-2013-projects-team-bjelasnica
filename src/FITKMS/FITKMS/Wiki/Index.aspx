<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="FITKMS.Wiki.Index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="dashboard-wrapper">
        <div class="main-container">
            <div class="row-fluid">
                <div class="span12">
                    <div class="widget-body">
                        <div class="widget">
                            <div class="widget-header">
                                <div class="title">
                                    Članci
                                </div>
                            </div>
                           
                            <div class="widget-body">
                                  <asp:DropDownList ID="typesList" runat="server" DataTextField="Naziv" DataValueField="VrstaID"></asp:DropDownList>
                                <asp:TextBox ID="titleInput" runat="server" CssClass="input-xxlarge" Placeholder="Naslov"></asp:TextBox>
                                <asp:DataGrid ID="articlesGrid" runat="server" AutoGenerateColumns="false" Width="100%">
                                    <Columns>
                                        <asp:TemplateColumn></asp:TemplateColumn>
                                    </Columns>
                                </asp:DataGrid>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
