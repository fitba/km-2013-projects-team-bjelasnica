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
                                <div class="input-append">
                                    <asp:DropDownList ID="typesList" runat="server" DataSource="<%# types %>" DataTextField="Naziv" DataValueField="VrstaID"></asp:DropDownList>
                                    <asp:TextBox ID="titleInput" runat="server" CssClass="span7" Placeholder="Naslov"></asp:TextBox>
                                    <asp:Button ID="searchArticlesSubmit" runat="server" CssClass="btn btn-info" Text="Traži"
                                        Height="30px" OnClick="searchArticlesSubmit_Click" />
                                </div>
                                <asp:DataGrid ID="articlesGrid" runat="server" AutoGenerateColumns="false" Width="100%" DataSource="<%# articles %>"
                                    DataKeyField="ClanakID" AllowPaging="true" AllowCustomPaging="true" OnPageIndexChanged="articlesGrid_PageIndexChanged" GridLines="None">
                                    <PagerStyle Mode="NumericPages" />
                                    <Columns>
                                        <asp:TemplateColumn>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="titleLink" runat="server" CommandName="detailsCommand" Text='<%# Eval("Naslov") %>'></asp:LinkButton>
                                                <br />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
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
